
namespace Enchiridion.Data.EntityMappings;

public class AuthorRequestMapping : IEntityTypeConfiguration<AuthorRequest>
{
    public void Configure(EntityTypeBuilder<AuthorRequest> builder)
    {
        builder.HasOne<User>()
            .WithMany(u => u.AuthorRequests)
            .HasForeignKey(ar => ar.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(ar => ar.UserId);
    }
}