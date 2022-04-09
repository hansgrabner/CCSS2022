// See https://aka.ms/new-console-template for more information

using Azure.Storage.Blobs;

class Program
{
    static async Task Main()
    {
        Console.WriteLine("Hello, World!");
        string connectionString = Environment.GetEnvironmentVariable("AZURE_STORAGE_CONNECTION_STRING");

        // Create a BlobServiceClient object which will be used to create a container client
        BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);

        //Create a unique name for the container
        string containerName = "quickstartblobs" + Guid.NewGuid().ToString();

        // Create the container and return a container client object
        BlobContainerClient containerClient = await blobServiceClient.CreateBlobContainerAsync(containerName);

        // Create a local file in the./ data / directory for uploading and downloading
        string localPath = "./data/";
        string fileName = "quickstart" + Guid.NewGuid().ToString() + ".txt";
        string localFilePath = Path.Combine(localPath, fileName);

        // Write text to the file
        await File.WriteAllTextAsync(localFilePath, "Hello, World!");

        // Get a reference to a blob
        BlobClient blobClient = containerClient.GetBlobClient(fileName);

        Console.WriteLine("Uploading to Blob storage as blob:\n\t {0}\n", blobClient.Uri);

        // Upload data from the local file
        await blobClient.UploadAsync(localFilePath, true);

    }
}


