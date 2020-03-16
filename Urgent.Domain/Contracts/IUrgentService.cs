using System.Collections.Generic;
using Urgent.Domain.Enums;

namespace Urgent.Domain.Contracts
{
    public interface IUrgentService
    {
        string CreateWidget(bool broken);
        OperationType GetOperationType(string answer);
        List<string> ErrorsLog();
    }
}
