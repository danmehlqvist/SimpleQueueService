using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public  interface IMessageQueue
    {
        Task<int> SendMessage<T>(T message);
        Task PollForMessages<T>(CancellationTokenSource cancellationTokenSource);
    }
}
