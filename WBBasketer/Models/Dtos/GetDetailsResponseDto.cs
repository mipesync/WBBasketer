namespace WBBasketer.Models.Dtos;

public class GetDetailsResponseDto
{
    public GetDetailsResponseDtoData Data { get; set; } = new();
}

public class GetDetailsResponseDtoData
{
    public List<Product> Products { get; set; } = new();
}