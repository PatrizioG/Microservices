namespace Common.Contracts;

public class CreateOrder
{
    public string UserId { get; set; } = string.Empty;
    public List<string> ProductIds { get; set; } = [];
}
