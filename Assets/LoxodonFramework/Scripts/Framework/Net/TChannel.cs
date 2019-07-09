using Loxodon.Framework.Messaging;
using Loxodon.Log;
using Microsoft.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;


class TChannel : AbstractChannel
{
    private static readonly ILog log = LogManager.GetLogger(typeof(TChannel));

    public RecyclableMemoryStreamManager MemoryStreamManager = new RecyclableMemoryStreamManager();
    private readonly MemoryStream memoryStream;

    private Socket socket;
    private SocketAsyncEventArgs innArgs = new SocketAsyncEventArgs();
    private SocketAsyncEventArgs outArgs = new SocketAsyncEventArgs();

    private readonly CircularBuffer recvBuffer = new CircularBuffer();
    private readonly CircularBuffer sendBuffer = new CircularBuffer();

    private readonly PacketParser parser;

    private bool isSending;

    private bool isRecving;

    private bool isConnected;

    private readonly byte[] opcodeCache;
    private readonly byte[] IdCache;
    private readonly byte[] packetSizeCache;

    public TChannel(string host, int port)
    {

        this.opcodeCache = new byte[Packet.OpcodeLength];
        this.IdCache = new byte[Packet.IdLength];
        this.packetSizeCache = new byte[Packet.PacketSizeLength];

        this.memoryStream = MemoryStreamManager.GetStream("message", ushort.MaxValue);

        this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        this.socket.NoDelay = true;

        this.parser = new PacketParser(this.recvBuffer, this.memoryStream);

        this.innArgs.Completed += this.OnComplete;
        this.outArgs.Completed += this.OnComplete;

        //int index = address.LastIndexOf(':');
        //string host = address.Substring(0, index);
        //string p = address.Substring(index + 1);
        //int port = int.Parse(p);

        this.Address = new IPEndPoint(IPAddress.Parse(host), port);

        this.isConnected = false;
        this.isSending = false;
    }
    public override void Dispose()
    {
  
        base.Dispose();

        this.socket.Close();
        this.innArgs.Dispose();
        this.outArgs.Dispose();
        this.innArgs = null;
        this.outArgs = null;
        this.socket = null;
        this.memoryStream.Dispose();
    }

    public void OnComplete(object sender, SocketAsyncEventArgs e)
    {
        switch (e.LastOperation)
        {
            case SocketAsyncOperation.Connect:
                OneThreadSynchronizationContext.Instance.Post(this.OnConnectComplete, e);
                break;
            case SocketAsyncOperation.Receive:
                OneThreadSynchronizationContext.Instance.Post(this.OnRecvComplete, e);
                break;
            case SocketAsyncOperation.Send:
                OneThreadSynchronizationContext.Instance.Post(this.OnSendComplete, e);
                break;
            case SocketAsyncOperation.Disconnect:
                OneThreadSynchronizationContext.Instance.Post(this.OnDisconnectComplete, e);
                break;
            default:
                throw new Exception($"socket error: {e.LastOperation}");
        }
    }

    public override MemoryStream Stream
    {
        get
        {
            return this.memoryStream;
        }
    }

    public override bool IsConnect { get { return this.isConnected; } }

    public override void Send(int opcode, long pid, object message)
    {
        MemoryStream stream = this.Stream;
        stream.Seek(0, SeekOrigin.Begin);
        stream.SetLength(0);
        ProtobufHelper.ToStream(message, stream);

        this.opcodeCache.WriteTo(0, opcode);
        this.IdCache.WriteTo(0, pid);
        this.packetSizeCache.WriteTo(0, (int)stream.Length);

        if (BitConverter.IsLittleEndian)
        {
            Array.Reverse(this.opcodeCache, 0, opcodeCache.Length);
            Array.Reverse(this.IdCache, 0, IdCache.Length);
            Array.Reverse(this.packetSizeCache, 0, packetSizeCache.Length);

        }


        this.sendBuffer.Write(this.opcodeCache, 0, this.opcodeCache.Length);
        this.sendBuffer.Write(this.IdCache, 0, this.IdCache.Length);
        this.sendBuffer.Write(this.packetSizeCache, 0, this.packetSizeCache.Length);
        this.sendBuffer.Write(stream);


    }

    public override void Start()
    {
        Messenger messenger = Messenger.Default;
        messenger.Publish("ConnectBegin", "开始连接");
        if (!this.isConnected)
        {
            this.ConnectAsync(this.Address);
            return;
        }

        if (!this.isRecving)
        {
            this.isRecving = true;
            this.StartRecv();
        }
    }

    public void ConnectAsync(IPEndPoint iPEndPoint)
    {
        log.Debug("----"+ isConnected);
        this.outArgs.RemoteEndPoint = iPEndPoint;
        if (this.socket.ConnectAsync(this.outArgs))
        {
            return;
        }
        OnConnectComplete(this.outArgs);
    }

    private void OnConnectComplete(object o)
    {
        if (this.socket == null)
        {
            return;
        }
        SocketAsyncEventArgs e = (SocketAsyncEventArgs)o;

        if (e.SocketError != SocketError.Success)
        {

            this.OnError((int) e.SocketError);
            return;
        }
        e.RemoteEndPoint = null;
        this.isConnected = true;

        log.Debug("连接成功");
        Messenger messenger = Messenger.Default;
        messenger.Publish("ConnectComplet", "连接成功");
        this.Start();
    }

    private void OnDisconnectComplete(object o)
    {
        SocketAsyncEventArgs e = (SocketAsyncEventArgs)o;
        this.OnError((int)e.SocketError);
    }

    private void StartRecv()
    {
        int size = this.recvBuffer.ChunkSize - this.recvBuffer.LastIndex;
        this.RecvAsync(this.recvBuffer.Last, this.recvBuffer.LastIndex, size);
    }

    public void RecvAsync(byte[] buffer, int offset, int count)
    {
        try
        {
            this.innArgs.SetBuffer(buffer, offset, count);
        }catch(Exception e)
        {
            throw new Exception($"socket set buffer error: {buffer.Length}, {offset}, {count}", e);
        }
        if (this.socket.ReceiveAsync(this.innArgs))
        {
            return;
        }

        OnRecvComplete(this.innArgs);
    }

    private void OnRecvComplete(object o)
    {
        if (this.socket == null)
        {
            return;
        }
        SocketAsyncEventArgs e = (SocketAsyncEventArgs)o;

        if (e.SocketError != SocketError.Success)
        {
            this.OnError((int)e.SocketError);
            return;
        }

        if (e.BytesTransferred == 0)
        {
            //this.OnError(ErrorCode.ERR_PeerDisconnect);
            return;
        }
        this.recvBuffer.LastIndex += e.BytesTransferred;
        if (this.recvBuffer.LastIndex == this.recvBuffer.ChunkSize)
        {
            this.recvBuffer.AddLast();
            this.recvBuffer.LastIndex = 0;
        }

        // 收到消息回调
        while (true)
        {
            try
            {
                if (!this.parser.Parse())
                {
                    break;
                }
            }
            catch (Exception ee)
            {
                log.Error($"ip: {this.Address} {ee}");
                //this.OnError(ErrorCode.ERR_SocketError);
                return;
            }

            try
            {
                this.OnRead(this.parser.GetPacket());
            }
            catch (Exception ee)
            {
                log.Error(ee);
            }
        }

        if (this.socket == null)
        {
            return;
        }

        this.StartRecv();
    }

    public override void StartSend()
    {
        if (!this.isConnected)
        {
            return;
        }

        // 没有数据需要发送
        if (this.sendBuffer.Length == 0)
        {
            this.isSending = false;
            return;
        }

        this.isSending = true;

        int sendSize = this.sendBuffer.ChunkSize - this.sendBuffer.FirstIndex;
        if (sendSize > this.sendBuffer.Length)
        {
            sendSize = (int)this.sendBuffer.Length;
        }

        this.SendAsync(this.sendBuffer.First, this.sendBuffer.FirstIndex, sendSize);
    }

    public void SendAsync(byte[] buffer, int offset, int count)
    {
        try
        {
            this.outArgs.SetBuffer(buffer, offset, count);
        }
        catch (Exception e)
        {
            throw new Exception($"socket set buffer error: {buffer.Length}, {offset}, {count}", e);
        }
        if (this.socket.SendAsync(this.outArgs))
        {
            return;
        }
        OnSendComplete(this.outArgs);
    }

    private void OnSendComplete(object o)
    {
        if (this.socket == null)
        {
            return;
        }
        SocketAsyncEventArgs e = (SocketAsyncEventArgs)o;

        if (e.SocketError != SocketError.Success)
        {
            this.OnError((int)e.SocketError);
            return;
        }

        if (e.BytesTransferred == 0)
        {
            //this.OnError(ErrorCode.ERR_PeerDisconnect);
            return;
        }

        this.sendBuffer.FirstIndex += e.BytesTransferred;
        if (this.sendBuffer.FirstIndex == this.sendBuffer.ChunkSize)
        {
            this.sendBuffer.FirstIndex = 0;
            this.sendBuffer.RemoveFirst();
        }

        this.StartSend();
    }

}