namespace Enchiridion.Data.EntityMappings;

public class UserMapping : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasIndex(u => u.AuthId).IsUnique();
        builder.HasIndex(u => u.Email).IsUnique();
        
        builder.HasOne(u => u.Author)
            .WithOne(a => a.User)
            .HasForeignKey<Author>(a => a.UserId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(u => u.SubscribedAuthors)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "user_author_subscription",
                j => j.HasOne<Author>().WithMany().HasForeignKey("author_id"),
                j => j.HasOne<User>().WithMany().HasForeignKey("user_id"),
                j => j.HasKey("user_id", "author_id")
            );;
    }
}