using System;
using System.IO;
using System.Net;

public abstract class AbstractChannel : IDisposable
{

    public abstract void Start();

    public abstract void Send(int opcode, long pid, object message);

    public abstract void StartSend();

    public IPEndPoint Address { get; protected set; }

    public abstract MemoryStream Stream { get; }

    public int Error { get; set; }

    private Action<AbstractChannel, int> errorCallback;

    public event Action<AbstractChannel, int> ErrorCallback
    {
        add
        {
            this.errorCallback += value;
        }
        remove
        {
            this.errorCallback -= value;
        }
    }

    private Action<MemoryStream> readCallback;

    public event Action<MemoryStream> ReadCallback
    {
        add
        {
            this.readCallback += value;
        }
        remove
        {
            this.readCallback -= value;
        }
    }
    

    protected void OnRead(MemoryStream memoryStream)
    {
        this.readCallback.Invoke(memoryStream);
    }

    protected void OnError(int e)
    {
        this.Error = e;
        this.errorCallback?.Invoke(this, e);
    }

    

    public virtual void Dispose()
    {
        
    }
}
