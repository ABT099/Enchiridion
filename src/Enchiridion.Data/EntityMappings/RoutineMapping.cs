namespace Enchiridion.Data.EntityMappings;

public class RoutineMapping : IEntityTypeConfiguration<Routine>
{
    public void Configure(EntityTypeBuilder<Routine> builder)
    {
        builder.HasOne<User>()
            .WithMany(u => u.Routines)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(d => d.UserId);

        builder.HasMany(r => r.Steps)
            .WithOne()
            .HasForeignKey(r => r.RoutineId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}