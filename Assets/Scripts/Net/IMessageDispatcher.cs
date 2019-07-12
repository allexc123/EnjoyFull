using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IMessageDispatcher : IDisposable
{
    IDisposable Subscribe<T>(int opcode, Action<T> action);

    void Publish(MemoryStream memoryStream);
}
