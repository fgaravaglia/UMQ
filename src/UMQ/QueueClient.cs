using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace UMQ
{
    public abstract class QueueClient
    {
        protected readonly string _QueueName;

        public QueueClient(QueueDefinition definition)
        {
            _QueueName = definition.Name;
        }

        public abstract QueueItemDTO GetLastItem();

        public abstract IEnumerable<QueueItemDTO> GetAll();

        public QueueItemDTO AddItem(object messageBody)
        {        
            if(messageBody == null)
                throw new ArgumentNullException(nameof(messageBody));
            
            // set dto attributes
            var dto = new QueueItemDTO(){ MessageBody = messageBody};
            dto.ID = Guid.NewGuid().ToString();
            dto.PublishedOn = DateTime.Now;
            dto.QueueName = this._QueueName;
            this.PersistNewItem(dto);
            return dto;
        }

        protected abstract void PersistNewItem(QueueItemDTO dto);
    }
}