namespace Enchiridion.Data.EntityMappings;

public class RoutineStepMapping : IEntityTypeConfiguration<RoutineStep>
{
    public void Configure(EntityTypeBuilder<RoutineStep> builder)
    {
        builder.HasIndex(rs => rs.RoutineId);

        builder.HasMany(rs => rs.Habits)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "step_habit",
                sh => sh.HasOne<Habit>()
                    .WithMany()
                    .HasForeignKey("habit_id")
                    .HasConstraintName("fk_step_habit_to_habit"),
                sh => sh.HasOne<RoutineStep>()
                    .WithMany()
                    .HasForeignKey("step_id")
                    .HasConstraintName("fk_step_habit_to_step"),
                sh =>
                {
                    sh.HasIndex("step_id");
                    sh.HasIndex("habit_id");
                }
            );
    }
}