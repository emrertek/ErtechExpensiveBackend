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
        int ExecuteQueryWithOutput(string storedProcedureName, ParameterList parameters, string outputParameterName);
        string ExecuteQueryWithOutputString(string storedProcedureName, ParameterList parameters, string outputParameterName);

        // Yeni method - sadece tipli veri çekmek için
        List<T> ExecuteReader<T>(string storedProcedureName, ParameterList parameters);
    }

}
