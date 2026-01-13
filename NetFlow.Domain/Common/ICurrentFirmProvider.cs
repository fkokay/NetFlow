using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Domain.Common
{
    public interface ICurrentFirmProvider
    {
        int GetFirmId();
        string GetFirmCode();
    }
}
