namespace GameStore.Api.Dtos;

public sealed record CreateGameDto(
	string Name,
	string Genre,
	decimal Price,
	DateOnly ReleaseDate
);