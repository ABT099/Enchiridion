using System.Reflection;

namespace Enchiridion.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Author> Authors { get; set; }
    public DbSet<AuthorRequest> AuthorRequests { get; set; }
    public DbSet<Habit> Habits { get; set; }
    public DbSet<HabitCategory> HabitCategories { get; set; }
    public DbSet<Quote> Quotes { get; set; }
    public DbSet<Routine> Routines { get; set; }
    public DbSet<RoutineStep> RoutineSteps { get; set; }
    public DbSet<Todo> Todos { get; set; }
    public DbSet<User> AppUsers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var assembly = GetCurrentAssembly();

        modelBuilder.ApplyConfigurationsFromAssembly(assembly);
        base.OnModelCreating(modelBuilder);
    }

    private static Assembly GetCurrentAssembly()
    {
        var assembly = Assembly.GetAssembly(typeof(AppDbContext));

        if (assembly is null)
        {
            throw new NullReferenceException($"The assembly specified is null, assembly: {assembly?.FullName}");
        }

        return assembly;
    }
}