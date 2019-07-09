using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public interface ISession : IDisposable
{
    void Connect(string address, int port);

    void Send(int opcode, object message);

    void OnRead(MemoryStream memoryStream);

    void Update();
}
