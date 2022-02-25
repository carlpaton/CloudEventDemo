using CloudNative.CloudEvents;
using CloudNative.CloudEvents.NewtonsoftJson;
using System.Text;

var dict = new Dictionary<Guid, Guid>();
dict.Add(
    Guid.Parse("448fe797-c664-406d-8d1d-d35d3bebe552"), 
    Guid.Parse("a40dd283-8d88-41fe-bf29-60233bcc3584"));

var cloudEvent = new CloudEvent
{
    Type = "com.github.pull.create",
    Source = new Uri("https://github.com/cloudevents/spec/pull"),
    Id = Guid.NewGuid().ToString(),
    Time = DateTime.Now,
    Data = new Root() {
        Bat = Guid.Parse("47c7def8-024b-4bd9-b337-3bee28292788"),
        Baz = Guid.Parse("ccb07ad6-ae9b-48bb-a607-0e0df94905db"),
        Foo = new Foo() {
            Active = true,
            Bar = dict,
            Email = "billy.goat@domain.com",
            Id = Guid.Parse("8448c50d-584c-48b1-8bbb-b9e180f4ff9c"),
            Name = "Billy Goat"
        }
    }
};

var key = "correlationid";
var val = "5a122a96-f0ee-4d65-bfc9-760c80c870ef";

var attCorrelationId = CloudEventAttribute.CreateExtension(key, CloudEventAttributeType.String);
var attSurrogate = CloudEventAttribute.CreateExtension("string", CloudEventAttributeType.String);

cloudEvent[attCorrelationId] = attSurrogate.Format(val);

var messageAsByteArray = new JsonEventFormatter().EncodeStructuredModeMessage(cloudEvent, out _);
var message = Encoding.UTF8.GetString(messageAsByteArray.ToArray(), 0, messageAsByteArray.Length);

Console.WriteLine(message);