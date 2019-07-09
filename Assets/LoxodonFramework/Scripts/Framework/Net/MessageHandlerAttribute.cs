using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class MessageHandlerAttribute : Attribute
{
    public int Opcode { get; }


    public MessageHandlerAttribute(int Opcode)
    {
        this.Opcode = Opcode;
    }
}
