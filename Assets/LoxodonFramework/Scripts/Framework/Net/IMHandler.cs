using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IMHandler
{
    void Handle(Session session, object message);
    Type GetMessageType();
}