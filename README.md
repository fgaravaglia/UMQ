# UMQ
UMQ is the acronym for **Umbrella Message Queue**.
It is a library crated for PoC or Demo purpose, that lets you to emulate the behavior of one or more queue of messages.
this is veery useful to emulate a real environment in a context of Event Soursing or Domain Driven Design

The simple behavior of this library is managed by a _Service_, that exposes to outside the capability of the library itself.
in future release the possibility to instuments with custom drivers will be added.

# Supported Emulations
At the moment, the nly physical driver under the woods is:
- File System: it emulates a queue by storing messages directly ina  physical folder on your computer

## File System
here you find a simple snippet to use FileSystem Implementation:

**Setup your queue**
```
var queueDefinition = new FileSystemQueueDefinition("TEST", @"C:\Temp\Umbrella.MessageQueue");
var service = new QueueService();
service.AddQueue(queueDefinition);
service.CreateQueues();
Console.WriteLine("Queue statup completed");
```

**Add item to queue**
```
service.AddItem(queueDefinition.Name, "IT IS A TEXT!");
service.AddItem(queueDefinition.Name, new StructuredData());
```

**GetMost recet item from queue**
```
var item = service.GetLastItem(queueDefinition.Name);
```
