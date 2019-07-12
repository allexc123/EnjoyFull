using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public static class OpCode
{
    public const int LoginClient = 10001;

    public const int S_HEARTBEAT = 10002;
    public const int S_DRAW = 10003;

    public const int S_REWARD = 10004;



    public const int loginServer = 20001;

    public const int C_HEARTBEAT = 20002;
    public const int C_DRAW = 20003;
}
