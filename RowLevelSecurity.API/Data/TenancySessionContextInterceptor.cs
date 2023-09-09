using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Data.Common;

namespace RowLevelSecurity.API.Data
{
    public class TenancySessionContextInterceptor : DbCommandInterceptor
    {
        public Guid TenantId { get; set; }
        public TenancySessionContextInterceptor(Guid tenantId)
        {
            TenantId = tenantId;
        }
        // runs before a query is executed
        public override InterceptionResult<DbDataReader> ReaderExecuting(DbCommand command, CommandEventData eventData, InterceptionResult<DbDataReader> result)
        {
            command = SetSessionStateSqlCommand(command);
            return result;
        }

        public override ValueTask<InterceptionResult<DbDataReader>> ReaderExecutingAsync(
            DbCommand command,
            CommandEventData eventData,
            InterceptionResult<DbDataReader> result,
            CancellationToken cancellationToken = default)
        {
            command = SetSessionStateSqlCommand(command);
            return base.ReaderExecutingAsync(command, eventData, result, cancellationToken);
        }

        public override InterceptionResult<object> ScalarExecuting(DbCommand command, CommandEventData eventData, InterceptionResult<object> result)
        {
            command = SetSessionStateSqlCommand(command);
            return result;
        }

        public override ValueTask<InterceptionResult<object>> ScalarExecutingAsync(DbCommand command, CommandEventData eventData, InterceptionResult<object> result,
            CancellationToken cancellationToken = new CancellationToken())
        {
            command = SetSessionStateSqlCommand(command);
            return base.ScalarExecutingAsync(command, eventData, result, cancellationToken);
        }

        public override InterceptionResult<int> NonQueryExecuting(DbCommand command, CommandEventData eventData, InterceptionResult<int> result)
        {
            command = SetSessionStateSqlCommand(command);
            return result;
        }

        public override ValueTask<InterceptionResult<int>> NonQueryExecutingAsync(DbCommand command, CommandEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            command = SetSessionStateSqlCommand(command);
            return base.NonQueryExecutingAsync(command, eventData, result, cancellationToken);
        }

        private DbCommand SetSessionStateSqlCommand(DbCommand command)
        {
            var setSessionStateSql = "EXEC sp_set_session_context N'tenantid', @tenantId;";
            command.CommandText = setSessionStateSql + command.CommandText;
            DbParameter parameters = command.CreateParameter();
            parameters.ParameterName = "@tenantId";
            parameters.Value = TenantId;
            command.Parameters.Add(parameters);

            return command;
        }


    }
}
