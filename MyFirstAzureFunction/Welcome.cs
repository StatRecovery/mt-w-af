namespace MyFirstAzureFunction;

public record Welcome(string Name);

public record Goodbye
{
    public string InstanceId { get; init; }
}