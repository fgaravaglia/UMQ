using System;
using UMQ;

namespace UMQ.FileSystem
{
    public class FileSystemQueueDefinition : QueueDefinition
    {
        public string FolderPath {get; private set;}

        public FileSystemQueueDefinition(string name, string folderpath) : base(name, QueueDefinition.QueueDriverEnum.FileSystem)
        {
            if(String.IsNullOrEmpty(folderpath))
                throw new ArgumentNullException(nameof(folderpath));
            this.FolderPath = folderpath;
        }
    }
}