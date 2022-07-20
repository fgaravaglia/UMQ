using System;

namespace UMQ
{
    public class QueueDefinition
    {
        public enum QueueDriverEnum
        {
            FileSystem
        }

        public  string Name {get; private set; }
        public  QueueDriverEnum Driver {get; private set; }


        public QueueDefinition()
        {

        }

        public QueueDefinition(string name, QueueDriverEnum driver) : this()
        {
            if(String.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));
            this.Name = name;
            this.Driver = QueueDriverEnum.FileSystem;
        }

    }
}
