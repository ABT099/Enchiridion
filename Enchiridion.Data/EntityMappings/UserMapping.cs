namespace Enchiridion.Data.EntityMappings;

public class UserMapping : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasIndex(u => u.AuthId).IsUnique();
        builder.HasIndex(u => u.Email).IsUnique();
    }
}