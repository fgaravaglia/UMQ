using System;
using System.IO;
using UMQ;

namespace UMQ.FileSystem
{
    /// <summary>
    /// Class to generate the infrastructure below the given File system Queue
    /// </summary>
    internal class QueueCreator
    {
        public QueueCreator()
        {

        }
        /// <summary>
        /// Creates the required infrastructure items to support this queue
        /// </summary>
        /// <param name="definition">definition of file system queue</param>
        public void CreateQueue(FileSystemQueueDefinition definition)
        {
            // verify queu structure exists
            if(!QueueExists(definition))
            {
                // create
                CreateQueueInfrastructure(definition);
            }
            
            // check for index file
            CheckQueueInfrastructure(definition);
        }

        private bool QueueExists(FileSystemQueueDefinition definition)
        {
            var fullPath = Path.Combine(definition.FolderPath, definition.Name);
           
            if(Directory.Exists( Path.GetFullPath(fullPath)))
                return true;
            return false;
        }

        void CreateQueueInfrastructure(FileSystemQueueDefinition definition)
        {
            var fullPath = Path.Combine(definition.FolderPath, definition.Name);
            DirectoryInfo di = new DirectoryInfo(fullPath);
            if(!di.Exists)
                di.Create();
        }

        void CheckQueueInfrastructure(FileSystemQueueDefinition definition)
        {

        }
    }
}