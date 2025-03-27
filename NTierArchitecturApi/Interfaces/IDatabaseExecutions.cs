using DataAccessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IDatabaseExecutions
    {
        string ExecuteQuery(string storedProcedureName, ParameterList parameters);
        int ExecuteDeleteQuery(string storedProcedureName, ParameterList parameters);
    }
}
