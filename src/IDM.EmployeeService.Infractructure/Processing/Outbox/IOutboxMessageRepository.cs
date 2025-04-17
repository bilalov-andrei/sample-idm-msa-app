using IDM.EmployeeService.Domain.AggregatesModel.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDM.EmployeeService.Infractructure.Processing.Outbox
{
    public interface IOutboxMessageRepository
    {
        Task<int> CreateAsync(OutboxMessage message, CancellationToken cancellationToken);
        Task<OutboxMessage[]> GetForProcessingAsync(OutboxConfigurationOptions _outboxOptions, CancellationToken cancellationToken);
        Task UpdateAsync(OutboxMessage[] outboxMessages, CancellationToken cancellationToken);
    }
}
