using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Session : ISession
{
    private AbstractChannel channel;
    public void Start()
    {
        this.channel = new TChannel("127.0.0.1:10001");
        this.channel.Start();
    }

    public void OnRead(MemoryStream memoryStream)
    {
        
    }

    public void Send(int opcode, long pid, object message)
    {
        if (channel == null)
        {
            return;
        }
        this.channel.Send(opcode, pid, message);
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
        if (channel == null)
        {
            return;
        }
        channel.StartSend();
    }
}