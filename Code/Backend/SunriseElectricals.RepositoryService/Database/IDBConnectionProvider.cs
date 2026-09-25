using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SunriseElectricals.RepositoryService.Database
{
    public interface IDbConnectionProvider : IDisposable
    {
        IDbConnection Connection { get; }
    }
}
