using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MySqlConnector;

namespace ProjectManager.Models;

[Table("members")]
public class Member
{
    [Key]
    [Column("member_id")]
    [DbType(MySqlDbType.Int32)]
    public int MemberId { get; set; }

    [Column("full_name")]
    [DbType(MySqlDbType.VarChar)]
    public string FullName { get; set; } = string.Empty;
    [Column("job_id")]
    [DbType(MySqlDbType.Int32)]
    public int JobId { get; set; }
    public Job? Job { get; set; }
}