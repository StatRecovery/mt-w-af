namespace MyFirstAzureFunction;

public record Welcome
{
    public string Name { get; set; }
}

public record Goodbye
{
    public string InstanceId { get; init; }

    public string ReceivedName { get; set; }
}