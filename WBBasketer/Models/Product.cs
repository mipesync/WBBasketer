namespace WBBasketer.Models;

public class Product
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public List<Size> Sizes { get; set; } = new();
}