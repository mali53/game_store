using GameStore.Api;
using GameStore.Api.Dtos;

var builder = WebApplication.CreateBuilder(args);


var app = builder.Build();

const string GetGameEndpointName = "GetGame";

List<GameDto> games = [
    new (
         1,
       "The Legend of Zelda: Breath of the Wild",
        "Action-adventure",
        59.99m,
        new DateOnly(2017, 3, 3)
    ),
    new (
        2,
        "Super Mario Odyssey",
        "Platformer",
        59.99m,
        new DateOnly(2017, 10, 27)
    ),
    new (
        3,
        "Mario Kart 8 Deluxe",
        "Racing",
        59.99m,
        new DateOnly(2017, 4, 28)
    )

];

// Get games
app.MapGet("games",()=>games);

// Get game by id
app.MapGet("games/{id}",(int id)=>games.Find(game=>game.Id==id))
.WithName(GetGameEndpointName);


// POST /games
app.MapPost("games",(CreateGameDto newGame)=>
{
    GameDto game = new(
        games.Count+1,
        newGame.Name,
        newGame.Genre,
        newGame.Price,
        newGame.ReleaseDate

        
    );

    games.Add(game);
    return Results.CreatedAtRoute(GetGameEndpointName, new{id = game.Id},game);
  
});


app.MapGet("/", () => "Hello World!");

app.Run();
