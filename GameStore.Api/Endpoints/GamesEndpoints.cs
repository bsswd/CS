using GameStore.Api.Dtos;

namespace GameStore.Api.Endpoints;

public static class GamesEndpoints
{
    const string GetGameEndpointName = "GetGame";

    private static readonly List <GameDto> games = [
    new (1,
        "The Legend of Zelda: Breath of the Wild",
        "Action-adventure",
         29.99M,
        new DateOnly(2017, 3, 3)),

    new (2,
        "Super Mario Odyssey",
        "Platformer",
        69.99M,
        new DateOnly(2017, 10, 27)),


    new (3,
        "Red Dead Redemption 2",
        "Action-adventure", 89.99M,
        new DateOnly(2018, 10, 26))
    ];

    public static void MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/games");

        //GET /games
        group.MapGet("/", () => games);

        // GET /games/id
        group.MapGet("/{id}", (int id) =>
        {
        var game = games.Find(game => game.Id == id);
    
        return game is null ? Results.NotFound() : Results.Ok(game);
        })
        .WithName(GetGameEndpointName);

        //POST /games
        group.MapPost("/", (CreateGameDto newGame) =>
        {
            GameDto game = new(
            games.Count + 1,
            newGame.Name,
            newGame.Genre,
            newGame.Price,
            newGame.ReleaseDate
        );

        games.Add(game);

        return Results.CreatedAtRoute(GetGameEndpointName, new {id = game.Id}, game);   
        });

        //PUT /games/id
        group.MapPut("/{id}", (int id, UpdateGameDto updatedGame) =>
        {
            var index = games.FindIndex(game => game.Id == id);

            if (index == -1)
            {
                return Results.NotFound();
            }

            games[index] = new GameDto(
                id,
                updatedGame.Name,
                updatedGame.Genre,
                updatedGame.Price,
                updatedGame.ReleaseDate
            );

            return Results.NoContent();  
        });

        //DELETE /games/id
        group.MapDelete("/{id}", (int id) =>
        {
            games.RemoveAll(game => game.Id == id);

            return Results.NoContent();   
        });
    }
}