using System;
using System.Collections.Generic;
using UMQ.FileSystem;

namespace UMQ
{
    public class QueueService
    {
        readonly Dictionary<string, QueueDefinition> _QueueRegistry;
        readonly Dictionary<string, QueueClient> _QueueClientRegistry;

        public QueueService()
        {
            this._QueueRegistry = new Dictionary<string, QueueDefinition>();
            this._QueueClientRegistry = new Dictionary<string, QueueClient>();
        }

        private void ThrowExceptionIfQueueIsNotRegistered(string queueName)
        {
            if(!this._QueueRegistry.ContainsKey(queueName))
                throw new ApplicationException($"Unable to find queue {queueName}: item not registered");

        }

        public void AddQueue(QueueDefinition definition)
        {
            if(this._QueueRegistry.ContainsKey(definition.Name))
                throw new ApplicationException($"Unable to add queue {definition.Name}: item already exists");

            this._QueueRegistry.Add(definition.Name, definition);

            switch (definition.Driver)
            {
                case QueueDefinition.QueueDriverEnum.FileSystem:
                    var fileDefinition = (FileSystemQueueDefinition)definition;
                    var client = new FileSystem.FileQueueClient(fileDefinition);
                    this._QueueClientRegistry.Add(definition.Name, client);
                    break;
                default:
                        throw new NotImplementedException($"Queue {definition.Name}: driver not supported");
            }
        }

        public void CreateQueues()
        {
            foreach (var item in this._QueueRegistry)
            {
                switch (item.Value.Driver)
                {
                    case QueueDefinition.QueueDriverEnum.FileSystem:
                        var creator = new UMQ.FileSystem.QueueCreator();
                        creator.CreateQueue((FileSystemQueueDefinition)item.Value);
                        break;
                    default:
                        throw new NotImplementedException($"Unable to create queue {item.Value.Name}: driver not supported");
                }
            }
        }

        public  QueueItemDTO AddItem(string queueName, object messageBody)
        {
            if(String.IsNullOrEmpty(queueName))
                throw new ArgumentNullException(nameof(queueName));
            if(messageBody == null)
                throw new ArgumentNullException(nameof(messageBody));

            ThrowExceptionIfQueueIsNotRegistered(queueName);
            return this._QueueClientRegistry[queueName].AddItem(messageBody);
        }

        public QueueItemDTO GetLastItem(string queueName)
        {
            if(String.IsNullOrEmpty(queueName))
                throw new ArgumentNullException(nameof(queueName));

            ThrowExceptionIfQueueIsNotRegistered(queueName);
            return this._QueueClientRegistry[queueName].GetLastItem();
        }
    }
}