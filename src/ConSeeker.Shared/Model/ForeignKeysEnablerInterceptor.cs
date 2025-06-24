using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Data.Common;

namespace ConSeeker.Shared.Model
{
    internal class ForeignKeysEnablerInterceptor : DbConnectionInterceptor
    {
        public override void ConnectionOpened(DbConnection connection, ConnectionEndEventData eventData)
        {
            if (connection is SqliteConnection sqliteConnection)
            {
                using var command = sqliteConnection.CreateCommand();
                command.CommandText = "PRAGMA foreign_keys=ON;";
                command.ExecuteNonQuery();
            }
            base.ConnectionOpened(connection, eventData);
        }

        public override async Task ConnectionOpenedAsync(
            DbConnection connection,
            ConnectionEndEventData eventData,
            CancellationToken cancellationToken = default)
        {
            if (connection is SqliteConnection sqliteConnection)
            {
                using var command = sqliteConnection.CreateCommand();
                command.CommandText = "PRAGMA foreign_keys=ON;";
                await command.ExecuteNonQueryAsync(cancellationToken);
            }
            await base.ConnectionOpenedAsync(connection, eventData, cancellationToken);
        }
    }
}