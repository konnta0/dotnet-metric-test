using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Entity.Employee;

[Table("titles")]
public partial class TitlesModel
{
    [Key]
    [Column("emp_no", TypeName = "int")]
    [Required]
    public int EmpNo { get; set; } = 0;

    [Column("salary", TypeName = "int")]
    [Required]
    public int Salary { get; set; } =0;
    
    [Column("from_date", TypeName = "date")]
    [Required]
    public DateTime FromDate { get; set; }
    
    [Column("to_date", TypeName = "date")]
    [Required]
    public DateTime ToDate { get; set; }

    public static partial void OnModelCreating(EntityTypeBuilder<TitlesModel> entityTypeBuilder)
    {
        entityTypeBuilder.HasKey(salariesModel => new { salariesModel.EmpNo, salariesModel.FromDate });
        entityTypeBuilder.HasOne<EmployeesModel>();
    }
}