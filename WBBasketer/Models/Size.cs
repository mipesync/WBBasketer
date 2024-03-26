using System.Text.Json.Serialization;

namespace WBBasketer.Models;

public class Size
{
    [JsonPropertyName("optionId")]
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    [JsonPropertyName("origName")]
    public string OrigName { get; set; } = "0";
}