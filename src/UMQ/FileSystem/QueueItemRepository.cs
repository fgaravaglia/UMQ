using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;


namespace UMQ.FileSystem
{
    internal class QueueItemRepository
    {
        readonly string _FullPath;

        public QueueItemRepository(string fullpathName)
        {
            if(String.IsNullOrEmpty(fullpathName))
                throw new ArgumentNullException(nameof(fullpathName));
            this._FullPath = fullpathName;
            Console.WriteLine("[QueueItemRepository] FullPath: " + _FullPath);
        }

        public QueueItemDTO GetLastItem()
        {
            return GetAll().OrderByDescending(x => x.PublishedOn).FirstOrDefault();
        }

        public IEnumerable<QueueItemDTO> GetAll()
        {
            if(!File.Exists(Path.GetFullPath(this._FullPath)))
                return new List<QueueItemDTO>();

            var jsonContent = File.ReadAllText(Path.GetFullPath(this._FullPath));
            var options = new JsonSerializerOptions { WriteIndented = true };
            var list = JsonSerializer.Deserialize<List<QueueItemDTO>>(jsonContent);
            return list;
        }

        public void AddItem(QueueItemDTO dto)
        {        
            if(dto == null)
                throw new ArgumentNullException(nameof(dto));

            Console.WriteLine("[QueueItemRepository] getting all data in queue");
            var items = GetAll().ToList();

            Console.WriteLine("[QueueItemRepository] try to add data in queue");
            if(items.Exists(x => x.ID == dto.ID))
                throw new ApplicationException("Unable to add item to Queue: ID already exists");
            items.Add(dto);

            Console.WriteLine("[QueueItemRepository] persist Data in queue");
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(items, options);
            File.WriteAllText(_FullPath, jsonString);
        }
    }
}