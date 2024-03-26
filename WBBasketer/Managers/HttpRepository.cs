using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using WBBasketer.Models.Dtos;
using WBBasketer.Options;

namespace WBBasketer.Managers;

public class HttpRepository
{
    private readonly HttpClient _httpClient;
    private readonly HttpOptions _httpOptions;

    public HttpRepository(HttpOptions httpOptions)
    {
        _httpOptions = httpOptions;
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", httpOptions.BearerToken);
    }

    public async Task<GetDetailsResponseDto> GetDetailsAsync(long nm)
    {
        var response = await _httpClient.GetAsync($"https://card.wb.ru/cards/v2/detail?curr=rub&nm={nm}");

        var content = await response.Content.ReadFromJsonAsync<GetDetailsResponseDto>();

        if (content is null)
        {
            throw new HttpRequestException("Не удалось получить информацию о товаре:( \nВозможно неправильная номенклатура");
        }

        return content;
    }

    public async Task AddToBasketAsync(AddToBasketRequestDto dto)
    {
        var content = JsonContent.Create(new List<AddToBasketRequestDto> { dto });
        var response = 
            await _httpClient.PostAsync($"https://cart-storage-api.wildberries.ru/api/basket/sync?ts={dto.ClientTS}&device_id={_httpOptions.SessionId}", content);

        switch (response.StatusCode)
        {
            case HttpStatusCode.OK:
                Console.WriteLine("Товар успешно добавлен в корзину!");
                break;
            case HttpStatusCode.Unauthorized:
                Console.WriteLine("Вы не авторизованы");
                break;
            case HttpStatusCode.BadRequest:
                Console.WriteLine("Что-то не так с телом запроса");
                break;
            default:
                throw new HttpRequestException("Произошла ошибка при добавлении товара");
        }
    }
}