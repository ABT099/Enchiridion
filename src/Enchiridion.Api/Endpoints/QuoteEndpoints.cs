using Enchiridion.Api.ViewModels;

namespace Enchiridion.Api.Endpoints;

public static class QuoteEndpoints
{
    public static void AddQuoteEndpoints(this RouteGroupBuilder api)
    {
        api.MapGet("quotes", GetAll);
        api.MapGet("quotes/{id:int}", GetById);
        api.MapGet("quotes/random", GetRandom);
        api.MapPost("quotes", Create);
        api.MapPut("quotes/{id:int}", Update);
        api.MapDelete("quotes/{id:int}", Delete);
    }
    
    private static async Task<IResult> GetAll(AppDbContext db)
    {
        var quotes = await db.Quotes
            .Select(QuoteViewModels.Projection)
            .ToListAsync();

        return Results.Ok(quotes);
    }

    private static async Task<IResult> GetById(int id, AppDbContext ctx)
    {
        var quote = await ctx.Quotes
            .Where(x => x.Id == id)
            .Select(QuoteViewModels.Projection)
            .FirstOrDefaultAsync();

        return quote is null
            ? Results.NotFound()
            : Results.Ok(quote);
    }

    private static async Task<IResult> GetRandom(AppDbContext db)
    {
        var count = await db.Quotes.CountAsync();

        if (count < 1)
        {
            return Results.NotFound();
        }

        var random = new Random();
        var randomId = random.Next(1, count);

        var quote = await db.Quotes.FindAsync(randomId);

        return quote is null
            ? Results.NotFound()
            : Results.Ok(quote);
    }

    private static async Task<IResult> Create(CreateQuoteRequest request, AppDbContext db)
    {
        var author = await db.Authors.FindAsync(request.AuthorId);

        if (author is null)
        {
            return Results.NotFound();
        }

        var quote = new Quote
        {
            QuoteText = request.QuoteText,
            Author = author
        };
        
        await db.Quotes.AddAsync(quote);
        await db.SaveChangesAsync();
        
        return Results.CreatedAtRoute(nameof(GetById), quote.Id, quote);
    }

    private static async Task<IResult> Update(int id, UpdateQuoteRequest request, AppDbContext db)
    {
        var quote = await db.Quotes.FirstOrDefaultAsync(x => x.Id == id);

        if (quote is null)
        {
            return Results.NotFound();
        }

        if (quote.QuoteText == request.QuoteText)
        {
            return Results.BadRequest("No updates have been detected");
        }
        
        quote.QuoteText = request.QuoteText;
        
        await db.SaveChangesAsync();

        return Results.Ok();
    }

    private static async Task<IResult> Delete(int id, AppDbContext db)
    {
        var quote = await db.Quotes.FindAsync(id);

        if (quote is null)
        {
            return Results.NotFound();
        }
        
        db.Quotes.Remove(quote);
        await db.SaveChangesAsync();
        
        return Results.Ok();
    }
}