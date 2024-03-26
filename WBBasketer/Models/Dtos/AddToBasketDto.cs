using System.Text.Json.Serialization;

namespace WBBasketer.Models.Dtos;

public class AddToBasketRequestDto
{
    [JsonPropertyName("chrt_id")]
    public long ChrtId { get; set; }
    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }
    [JsonPropertyName("cod_1s")]
    public long Cod1s { get; set; }
    [JsonPropertyName("client_ts")]
    public long ClientTS { get; set; }
    [JsonPropertyName("op_type")]
    public int OpType { get; set; } = 1;
    [JsonPropertyName("target_url")]
    public string TargetUrl { get; } = "EX|8|MCS|CR|popular|||||";
}