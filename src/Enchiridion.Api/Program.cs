using Enchiridion.Api;
using Enchiridion.Api.Endpoints;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

KeyGen.Invoke();

builder.RegisterServices();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var ctx = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

    var username = builder.Configuration["AdminUsername"];
    var password = builder.Configuration["AdminPassword"];

    if (string.IsNullOrEmpty(username))
    {
        throw new Exception("Admin username is missing");
    }

    if (string.IsNullOrEmpty(password))
    {
        throw new Exception("Admin password is missing");
    }

    if (await ctx.Users.AsNoTracking().AnyAsync(x => x.UserName == username) == false)
    {
        var identityUser = new IdentityUser{UserName = username, Email = username, EmailConfirmed = true};
        await userMgr.CreateAsync(identityUser, password);

        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        string[] roles = [EnchiridionConstants.Roles.Admin, EnchiridionConstants.Roles.User];

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        var admin = new User
        {
            AuthId = identityUser.Id,
            FirstName = "Admin",
            LastName = "Admin",
            Email = "admin@admin.com",
            UserName = username
        };

        await ctx.Users.AddAsync(admin);
        await ctx.SaveChangesAsync();

        await userMgr.AddToRoleAsync(identityUser, EnchiridionConstants.Roles.Admin);
    }
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.AddAuthenticationEndpoints();

var api = app.MapGroup("api/");

api.AddQuoteEndpoints();
api.AddAuthorEndpoints();
api.AddUserEndpoints();
api.AddHabitCategoryEndpoints();
api.AddHabitEndpoints();
api.AddTodoEndpoints();

app.Run();
