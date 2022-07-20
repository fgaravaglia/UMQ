using System;
using System.Collections.Generic;
using System.IO;

namespace UMQ.FileSystem
{
    internal class FileQueueClient : QueueClient
    {
        readonly QueueItemRepository _Repository;

        public FileQueueClient(FileSystemQueueDefinition definition) : base(definition)
        {
            // build full path for json file
            var fullPath = Path.Combine(definition.FolderPath, definition.Name);
            _Repository = new QueueItemRepository(Path.Combine(fullPath, "queue.json"));
        }

        public override QueueItemDTO GetLastItem()
        {
            return this._Repository.GetLastItem();
        }

        public override IEnumerable<QueueItemDTO> GetAll()
        {
            return this._Repository.GetAll();
        }

        protected override void PersistNewItem(QueueItemDTO dto)
        {
            this._Repository.AddItem(dto);

        }
    }
}