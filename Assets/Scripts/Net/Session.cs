using Loxodon.Framework.Contexts;
using Loxodon.Framework.Execution;
using Loxodon.Log;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Session : ISession
{
    private static readonly ILog log = LogManager.GetLogger(typeof(TChannel));

    public const string NET_ALERT = "net";
    private long pid;
    public long Pid
    {
        set { this.pid = value; }
        get { return this.pid; }
    }
    private AbstractChannel channel;

    private ThreadScheduledExecutor scheduled;

    public Session()
    {
        this.scheduled = new ThreadScheduledExecutor();
    }

    public void Connect(string host, int port)
    {
        this.channel = new TChannel(host, port);
        this.channel.Start();

        channel.ReadCallback += OnRead;
        this.channel.ErrorCallback += OnError;

        //this.scheduled.Start();
        //this.scheduled.ScheduleAtFixedRate(CheckConnect, 1000, 1000);

    }
    private void OnError(int e)
    {
        //if (!this.channel.IsConnect)
        //{
        //}
        this.channel.Start();
    }

    public void OnRead(MemoryStream memoryStream)
    {
        ApplicationContext context = ApplicationContext.GetApplicationContext();
        IMessageDispatcher dispatcher = context.GetService<IMessageDispatcher>();
        dispatcher.Publish(memoryStream);
    }

    public void Send(int opcode, object message)
    {
        if (channel == null)
        {
            return;
        }
        this.channel.Send(opcode, this.pid, message);
    }

    public void Dispose()
    {
        if (channel != null)
        {
            this.channel.Dispose();
        }
    }

    public void Update()
    {
        OneThreadSynchronizationContext.Instance.Update();
        if (channel == null)
        {
            return;
        }
        channel.StartSend();

    }
}