
namespace Enchiridion.Data.EntityMappings;

public class TodoMapping : IEntityTypeConfiguration<Todo>
{
    public void Configure(EntityTypeBuilder<Todo> builder)
    {
        builder.HasOne<User>()
            .WithMany(u => u.Todos)
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(t => t.UserId);

        builder.OwnsOne(t => t.TodoOptions);
    }
}