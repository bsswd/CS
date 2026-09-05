using GameStore.Api.Dtos;

const string GetGameEndpointName = "GetGame";

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

List <GameDto> games = [
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

//GET /games
app.MapGet("/games", () => games);


// GET /games/1
app.MapGet("/games/{id}", (int id) => games.Find(game => game.Id == id))
   .WithName(GetGameEndpointName);

//POST /games
app.MapPost("/games", (CreateGameDto newGame) =>
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


//PUT /games/1
app.MapPut("/games/{id}", (int id, UpdateGameDto updatedGame) =>
{
   var index = games.FindIndex(game => game.Id == id);

   games[index] = new GameDto(
        id,
        updatedGame.Name,
        updatedGame.Genre,
        updatedGame.Price,
        updatedGame.ReleaseDate
    );

    return Results.NoContent();  
});

app.Run();