using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Add The following Namespaces to the project
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;


namespace SyncFilesToCloud
{
    class Program
    {


        static async Task Main(string[] args)
        {
            string filePath = "C:\\Users\\ali87\\Desktop\\50202.jpg";

            // Create Reference to Azure Storage Account
            String strorageconn = "DefaultEndpointsProtocol=https;AccountName=cmax;AccountKey=qM81Db3RuHKZXz+dUAvllBsS/W+K1/El8LL1wTD6PVlnI22HOQ53xyehmq6/T/rT37hvbc398EuFyFQx2BCgxw==;EndpointSuffix=core.windows.net";
            CloudStorageAccount storageacc = CloudStorageAccount.Parse(strorageconn);

            //Create Reference to Azure Blob
            CloudBlobClient blobClient = storageacc.CreateCloudBlobClient();

            //The next 2 lines create if not exists a container named "democontainer"
            CloudBlobContainer container = blobClient.GetContainerReference("cmax");

            await container.CreateIfNotExistsAsync();

            //The next 7 lines upload the file test.txt with the name DemoBlob on the container "democontainer"
            CloudBlockBlob blockBlob = container.GetBlockBlobReference("business.jpg");
            using (var filestream = System.IO.File.OpenRead(filePath))
            {

               var ado = blockBlob.UploadFromStreamAsync(filestream);
                Console.WriteLine(ado.Status); //Does Not Help Much
                _ = ado.ContinueWith(t =>
                  {
                      Console.WriteLine("It is over"); //this is working OK
                });
                Console.WriteLine(ado.Status); //Does Not Help Much
                Console.WriteLine("theEnd");
                Console.ReadKey();

            }

            Console.ReadKey();
        }

       
    }
}
