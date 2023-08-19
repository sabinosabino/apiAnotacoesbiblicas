using System.Data;
using MySql.Data.MySqlClient;
using projetobibliiaapi.Context;

namespace projetobibliiaapi.Context
{
    public class Db : IConnection, IDisposable
    {
        const string connectionString = "Server=192.168.1.6;DataBase=biblia;Uid=sabino;Pwd=123456";
        public MySqlConnection connection;
        public Db()
        {
            try
            {
                connection = new MySqlConnection(connectionString);
                Open().GetAwaiter().GetResult();
            }
            catch
            {
                throw;
            }
        }
        public async Task Close()
        {
            try
            {
                if (connection.State == ConnectionState.Open)
                    await connection.CloseAsync();
            }
            catch
            {
                throw;
            }
        }
        public async Task Open()
        {
            try
            {
                if (connection.State == ConnectionState.Open)
                {
                    await connection.OpenAsync();
                }
            }
            catch
            {
                throw;
            }
        }
        public IDbConnection GetConnection()
        {
            return connection;
        }

        public void Dispose()
        {
            _ = Close();
        }
    }
}