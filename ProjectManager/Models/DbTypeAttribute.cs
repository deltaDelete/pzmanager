using System;
using MySqlConnector;

namespace ProjectManager.Models;

public class DbTypeAttribute : Attribute
{
    public DbTypeAttribute(MySqlDbType type)
    {
        DbType = type;
    }

    public MySqlDbType DbType { get; }
}