using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Model.Employees;

public partial class TitlesModel
{
    public static partial void OnModelCreating(EntityTypeBuilder<TitlesModel> entityTypeBuilder);
}