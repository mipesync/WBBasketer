using WBBasketer.Models.Dtos;
using WBBasketer.Options;
using WBBasketer.Repositories;

var bearerToken = string.Empty;
var sessionId = string.Empty;

while (true)
{
    if (string.IsNullOrEmpty(bearerToken))
        bearerToken = InitializeToken();
    
    if (string.IsNullOrEmpty(sessionId))
        sessionId = InitializeSessionId();
    
    var httpOptions = new HttpOptions(bearerToken, sessionId);
    var httpRepository = new HttpRepository(httpOptions);

    var nomenclature = GetNomenclature();
    var productDetails = await httpRepository.GetDetailsAsync(nomenclature);
    
    PrintDetails(productDetails);
    
    await httpRepository.AddToBasketAsync(new AddToBasketRequestDto
    {
        ChrtId = GetSizeId(),
        Cod1s = nomenclature,
        ClientTS = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
        Quantity = GetQuantity()
    });
}

string InitializeToken()
{
    while (true)
    {
        Console.Write("Введите токен доступа (находится в локальном хранилище сайта с ключом \"wbx__tokenData\"): ");
        var token = Console.ReadLine();

        if (string.IsNullOrEmpty(token))
        {
            Console.WriteLine("Необходимо ввести токен доступа");
            continue;
        }

        if (token.Split('.').Length != 3)
        {
            Console.WriteLine("Введён некорректный токен доступа");
        }
        else
        {
            return token;
        }
    }
}

string InitializeSessionId()
{
    while (true)
    {
        Console.Write("Введите id сессии (находится в локальном хранилище сайта с ключом \"wbx__sessionID\"): ");
        var session = Console.ReadLine();

        if (string.IsNullOrEmpty(session))
        {
            Console.WriteLine("Необходимо ввести id сессии");
            continue;
        }

        if (!session.Contains("site_"))
        {
            Console.WriteLine("Введён некорректный id сессии");
        }
        else
        {
            return session;
        }
    }
}

long GetNomenclature()
{
    while (true)
    {
        Console.Write("Укажите номенклатуру товара, который необходимо добавить в корзину: ");
        var inputData = Console.ReadLine();

        if (!long.TryParse(inputData, out var nm))
        {
            Console.WriteLine("Номерклатура должна состоять из цифр!");
            continue;
        }

        return nm;
    }
}

long GetSizeId()
{
    while (true)
    {
        Console.Write("Укажите id размера товара, который необходимо добавить в корзину: ");
        var inputData = Console.ReadLine();

        if (!long.TryParse(inputData, out var sizeId))
        {
            Console.WriteLine("Id размера должен состоять из цифр!");
            continue;
        }

        return sizeId;
    }
}

int GetQuantity()
{
    while (true)
    {
        Console.Write("Какое кол-во товара добавить? ");
        var inputData = Console.ReadLine();

        if (!int.TryParse(inputData, out var quantity))
        {
            Console.WriteLine("Кол-во товара должно быть цифрой!");
            continue;
        }

        return quantity;
    }
}

void PrintDetails(GetDetailsResponseDto productDetails)
{
    foreach (var product in productDetails.Data.Products)
    {
        Console.WriteLine($"Название: {product.Name}");

        foreach (var productSize in product.Sizes)
        {
            Console.WriteLine($"\tId. {productSize.Id}\t Описание: {productSize.Name} ({productSize.OrigName})");
        }
    }
}