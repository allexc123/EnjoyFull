using Loxodon.Framework.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface ITask : IDisposable
{
    IScheduledExecutor Scheduled { get; }
}