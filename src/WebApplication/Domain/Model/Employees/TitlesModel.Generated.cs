using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Model.Employees;

public partial class TitlesModel : IModel
{
    public static partial void OnModelCreating(EntityTypeBuilder<TitlesModel> entityTypeBuilder);
}