namespace Enchiridion.Api.BackgroundServices;

public class DailyRefreshIntervalBackgroundService(IServiceProvider services) : BackgroundService
{
    private static readonly Func<AppDbContext, DateTime, CancellationToken, Task> BatchHabitUpdate =
        EF.CompileAsyncQuery((AppDbContext db, DateTime nextRun, CancellationToken stoppingToken) =>
            db.Habits
                .Where(x => x.HabitOptions.RepeatInterval == RepeatInterval.Daily &&
                            x.HabitOptions.TargetDate == DateTime.Today.AddDays(1) &&
                            x.Status == HabitStatus.Completed)
                .ExecuteUpdateAsync(setters => setters
                        .SetProperty(h => h.Status, HabitStatus.Active)
                        .SetProperty(h => h.HabitOptions.TargetDate, nextRun),
                    stoppingToken));
    
    private static readonly Func<AppDbContext, DateTime, CancellationToken, Task> BatchTodoUpdate =
        EF.CompileAsyncQuery((AppDbContext db, DateTime nextRun, CancellationToken stoppingToken) =>
            db.Todos.Where(
                x => x.TodoOptions != null && 
                     x.TodoOptions.RepeatInterval == RepeatInterval.Daily &&
                     x.TodoOptions.TargetDate ==  DateTime.Today.AddDays(1) &&
                     x.isRepeated == true)
            .ExecuteUpdateAsync(setters => setters
                .SetProperty(t => t.IsComplete, false)
                .SetProperty(t => t.TodoOptions!.TargetDate, nextRun),
            stoppingToken));
    
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var nextRun = DateTime.UtcNow.AddDays(1);
            var delay = nextRun - DateTime.UtcNow;
            
            if (delay <= TimeSpan.Zero)
            {
                delay = TimeSpan.FromDays(1);
            }
            
            await Task.Delay(delay, stoppingToken);

            try
            {
                using var scope = services.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                var habitsUpdate = BatchHabitUpdate(dbContext, nextRun, stoppingToken);
                var todosUpdate = BatchTodoUpdate(dbContext, nextRun, stoppingToken);
                await Task.WhenAll(habitsUpdate, todosUpdate);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}