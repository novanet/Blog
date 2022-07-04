using System.Text.Json.Serialization;

namespace Slack;
public record Payload([property: JsonPropertyName("text")] string Text);