using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace projetobibliiaapi.Context
{
    public interface IConnection
    {
        Task Open();
        Task Close();
        IDbConnection GetConnection();
    }
}