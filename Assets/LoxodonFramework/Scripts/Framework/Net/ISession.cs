using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public interface ISession : IDisposable
{
    void Start();

    void Send(int opcode, long pid, object message);

    void OnRead(MemoryStream memoryStream);

    void Update();
}
