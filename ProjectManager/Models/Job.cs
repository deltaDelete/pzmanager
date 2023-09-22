using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MySqlConnector;

namespace ProjectManager.Models;

[Table("jobs")]
public class Job
{
    [Key]
    [Column("job_id")]
    [DbType(MySqlDbType.Int32)]
    public int JobId { get; set; }

    [Column("name")]
    [DbType(MySqlDbType.VarChar)]
    public string Name { get; set; } = string.Empty;
}