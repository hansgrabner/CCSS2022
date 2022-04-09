using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure; // Namespace for CloudConfigurationManager
using Microsoft.Azure.Storage; // Namespace for CloudStorageAccount
using Microsoft.Azure.Storage.Queue; // Namespace for Queue storage types

namespace ConsoleAppForAzureQueue
{
    //https://docs.microsoft.com/en-us/azure/storage/queues/storage-dotnet-how-to-use-queues
    class Program
    {
        static void Main(string[] args)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
    CloudConfigurationManager.GetSetting("StorageConnectionString"));
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();
            // Retrieve storage account from connection string.
   
            // Retrieve a reference to a container.
            CloudQueue queue = queueClient.GetQueueReference("mycampus02queue");

            // Create the queue if it doesn't already exist
            queue.CreateIfNotExists();

            CloudQueueMessage message = new CloudQueueMessage("Hello, World V2");
            queue.AddMessage(message);

            CloudQueueMessage peekedMessage = queue.PeekMessage();

            // Display message.
            Console.WriteLine(peekedMessage.AsString);

  
            CloudQueueMessage getMessage = queue.GetMessage();
            getMessage.SetMessageContent2("Updated contents.", false);
            queue.UpdateMessage(getMessage,
                TimeSpan.FromSeconds(60.0),  // Make it invisible for another 60 seconds.
                MessageUpdateFields.Content | MessageUpdateFields.Visibility);

            CloudQueueMessage retrievedMessage = queue.GetMessage();
            queue.FetchAttributes();

            // Retrieve the cached approximate message count.
            int? cachedMessageCount = queue.ApproximateMessageCount;

            // Display number of messages.
            Console.WriteLine("Number of messages in queue: " + cachedMessageCount);

            //Process the message in less than 30 seconds, and then delete the message
            queue.DeleteMessage(retrievedMessage);
        }
    }
}
