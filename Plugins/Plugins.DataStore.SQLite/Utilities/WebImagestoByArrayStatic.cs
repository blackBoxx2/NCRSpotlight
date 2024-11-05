using EntitiesLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.EntityFrameworkCore;
using Plugins.DataStore.SQLite;
using System.Net.Http;
using System.Threading.Tasks;
namespace NCRSPOTLIGHT.Plugins.Plugins.SQLite
{
    public static class WebImagestoByArrayStatic
    {


        public static async Task<IFormFile> DownloadImageAsync(string imageUrl)
        {
            //using (HttpClient httpClient = new HttpClient())
            //{
            //    httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/85.0.4183.121 Safari/537.36");
            //    IFormFile imageBytes = await httpClient.GetByteArrayAsync(imageUrl);
            //    return imageBytes;
            //}
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/85.0.4183.121 Safari/537.36"); //stop blocking my request! grrrr

                try
                {
                    byte[] imageBytes = await httpClient.GetByteArrayAsync(imageUrl);

                    var stream = new MemoryStream(imageBytes);

                    var formFile = new FormFile(stream, 0, stream.Length, "file", Path.GetFileName(imageUrl))
                    {
                        Headers = new HeaderDictionary(), 
                        ContentType = "image/jpeg"
                    };

                    return formFile;
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine($"Error downloading image: {ex.Message}");
                    return null;
                }
            }
        }

        public static async Task SeedProductPictures(string imgUrl, Product product)
        {

            string imageUrl = imgUrl;
            IFormFile img = await DownloadImageAsync(imageUrl);

                    string mimeType = img.ContentType;
                    string fileName = Path.GetFileName(img.FileName);
                    long fileLength = img.Length;
                    if (!(fileName == "" || fileLength == 0))
                    {
                        ProductPicture p = new ProductPicture();
                        using (var memoryStream = new MemoryStream())
                        {
                            await img.CopyToAsync(memoryStream);

                            p.FileContent.Content = memoryStream.ToArray();

                        }
                        p.MimeType = mimeType;
                        p.FileName = fileName;
                        product.ProductPictures.Add(p);
                    };
        }




                //var productPicture = new ProductPicture
                //{
                //ProductID = 1, // Example product ID
                //FileName = "image.jpg",
                //ContentType = "image/jpeg",
                //FileContent = imageBytes
                //};
                //return imageBytes;

//    }
}
}
