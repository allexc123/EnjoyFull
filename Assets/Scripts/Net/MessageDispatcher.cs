using Loxodon.Framework.Messaging;
using Loxodon.Log;
using LX;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class MessageDispatcher : IMessageDispatcher
{
    private static readonly ILog log = LogManager.GetLogger(typeof(MessageDispatcher));

    private readonly Dictionary<int, object> typeMessages = new Dictionary<int, object>();

    private readonly Dictionary<int, SubjectBase> notifiers = new Dictionary<int, SubjectBase>();

    public MessageDispatcher()
    {

        this.Load();
    }


    public void Load()
    {
        typeMessages.Clear();
        //网络监听的消息都在这里注册
        AddMessage(20001, typeof(LoginResult));

    }
    private void AddMessage(int opcode, Type messageType)
    {
        this.typeMessages.Add(opcode, Activator.CreateInstance(messageType));
    }

    public IDisposable Subscribe<T>(int opcode, Action<T> action)
    {
        SubjectBase notifier;
        lock (notifiers)
        {
            if (!notifiers.TryGetValue(opcode, out notifier))
            {
                notifier = new Subject<T>();
                this.notifiers.Add(opcode, notifier);
            }
        }
        return (notifier as Subject<T>).Subscribe(action); ;
    }

    public void Publish<T>(int opcode, T message)
    {
        if (message == null)
        {
            return;
        }
        List<KeyValuePair<int, SubjectBase>> list;
        lock (notifiers)
        {
            if (notifiers.Count <= 0)
            {
                return;
            }
            list = new List<KeyValuePair<int, SubjectBase>>(this.notifiers);
        }
        foreach (KeyValuePair<int, SubjectBase> kv in list)
        {
            try
            {
                if (kv.Key == opcode)
                {
                    kv.Value.Publish(message);
                }
            }
            catch (Exception e)
            {
                if (log.IsWarnEnabled)
                    log.Warn(e);
            }
        }
    }

    public void Publish(MemoryStream memoryStream)
    {
        memoryStream.Seek(0, SeekOrigin.Begin);
        int opcode = BitConverter.ToInt32(memoryStream.GetBuffer(), Packet.OpcodeIndex);
        //long pid = 
        BitConverter.ToInt64(memoryStream.GetBuffer(), Packet.IdIndex);

        memoryStream.Seek(Packet.MessageIndex, SeekOrigin.Begin);

        object instance = this.typeMessages[opcode];

        object message = ProtobufHelper.FromStream(instance, memoryStream);
        this.Publish(opcode, message);
    }




    public void Dispose()
    {
        throw new NotImplementedException();
    }
}
