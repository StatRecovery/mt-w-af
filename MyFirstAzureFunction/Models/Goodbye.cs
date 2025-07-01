namespace MyFirstAzureFunction.Models;

public record Goodbye
{
    public string InstanceId { get; init; }

    public string ReceivedName { get; set; }
}