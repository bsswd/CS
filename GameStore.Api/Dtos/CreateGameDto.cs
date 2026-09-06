using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.Dtos;

public sealed record CreateGameDto(
	[Required][StringLength(50)] string Name,
	[Required][Range(1, 50)] int GenreId,
	[Required][Range(0.99, 299.99)] decimal Price,
	[Required] DateOnly ReleaseDate
);