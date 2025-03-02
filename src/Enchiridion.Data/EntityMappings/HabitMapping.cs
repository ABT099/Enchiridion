namespace Enchiridion.Data.EntityMappings;

public class HabitMapping : IEntityTypeConfiguration<Habit>
{
    public void Configure(EntityTypeBuilder<Habit> builder)
    {
        builder.HasOne<User>()
            .WithMany(u => u.Habits)
            .HasForeignKey(u => u.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(d => d.UserId);
    }
}