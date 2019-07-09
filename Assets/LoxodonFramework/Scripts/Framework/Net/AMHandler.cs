using Loxodon.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public abstract class AMHandler<Message> : IMHandler where Message : class
{
    private static readonly ILog log = LogManager.GetLogger(typeof(AMHandler<Message>));

    protected abstract void Run(Session session, Message message);

    public void Handle(Session session, object msg)
    {
        Message message = msg as Message;
        if (message == null)
        {
            log.Error($"消息类型转换错误: {msg.GetType().Name} to {typeof(Message).Name}");
        }
        this.Run(session, message);
    }

    public Type GetMessageType()
    {
        return typeof(Message);
    }
}
