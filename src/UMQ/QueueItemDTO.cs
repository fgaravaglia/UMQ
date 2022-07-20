using System;

namespace UMQ
{
    public class QueueItemDTO
    {
        public string ID{get; set;}

        public string QueueName {get; set;}

        public DateTime PublishedOn {get; set;}
        
        public object MessageBody {get; set;}
    }
}