using EntitiesLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.EntityFrameworkCore;
using Plugins.DataStore.SQLite;
using System.Net.Http;
using System.Threading.Tasks;
namespace Plugins.DataStore.SQLite.Utilities
{
    public static class WebImagestoByArrayStatic
    {


        public static async Task<byte[]> DownloadImageAsync(string filePath)
        {


            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                return null;
            }

            try
            {
                // Read the file into a byte array
                byte[] imageBytes = await File.ReadAllBytesAsync(filePath);
                return imageBytes;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading file: {ex.Message}");
                return null;
            }
        }
  
        public static async Task SeedProductPictures(string filePath, Product product)
        {


            // Download the image as a byte array
            byte[] imageBytes = await DownloadImageAsync(filePath);

            if (imageBytes != null && imageBytes.Length > 0)
            {
                // Create a new ProductPicture instance and set its properties
                ProductPicture productPicture = new ProductPicture
                {
                    MimeType = "image/jpeg", // or detect dynamically if required
                    FileName = Path.GetFileName(filePath),
                    FileContent = new FileContent
                    {
                        Content = imageBytes // Store the byte array directly
                    }
                };

                // Add the ProductPicture to the product
                product.ProductPictures.Add(productPicture);
            }
        }





    }
}
