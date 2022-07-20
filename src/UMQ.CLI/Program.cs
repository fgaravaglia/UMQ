using System;
using UMQ;
using UMQ.FileSystem;

namespace UMQ.CLI
{
    public class StructuredData
    {
        public string Text{get; set;}
        public DateTime TimeStamp {get; set;}
        public int Counter {get; set;}
        public double? Number {get; set;}

        public StructuredData()
        {
            Text = "Hello world";
            TimeStamp = DateTime.Now;
            Counter = 22;
            Number = 100.3;
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var queueDefinition = new FileSystemQueueDefinition("TEST", @"C:\Temp\Umbrella.MessageQueue");
                var service = new QueueService();
                service.AddQueue(queueDefinition);
                service.CreateQueues();
                Console.WriteLine("Queue statup completed");

                service.AddItem(queueDefinition.Name, "IT IS A TEXT!");
                service.AddItem(queueDefinition.Name, new StructuredData());

                var item = service.GetLastItem(queueDefinition.Name);
                Console.WriteLine("Lat Item: " + item.ID);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }

        }
    }
}
