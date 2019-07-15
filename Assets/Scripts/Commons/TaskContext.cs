using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Loxodon.Framework.Execution;

class TaskContext : ITask
{
    private ThreadScheduledExecutor scheduled;
    public TaskContext()
    {
        scheduled = new ThreadScheduledExecutor();
        scheduled.Start();
    }
    public IScheduledExecutor Scheduled
    {
        get => this.scheduled;
    }

    public void Dispose()
    {
        scheduled.Stop();
        scheduled.Dispose();
    }
}
