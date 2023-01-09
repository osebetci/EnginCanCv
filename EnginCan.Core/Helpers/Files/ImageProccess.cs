using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;
using System.Linq;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;
using EnginCan.Core.Middleware;

namespace EnginCan.Core.Helpers.Files
{
    public interface IImageProccess
    {
        string QueryResult(string queryString, string name);

        string CropWithOutStretch(int width, int height, int quality, string imagePath);
    }

    public class ImageProccess : IImageProccess

    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public ImageProccess(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public string QueryResult(string queryString, string name)
        {
            if (string.IsNullOrEmpty(queryString) || string.IsNullOrEmpty(name))
                return null;

            try
            {
                #region Validate

                var queries = queryString.Split(',').ToList();
                queries.Sort(); // isimlerin standart sırada olması için

                string mainImagePath = $"{_hostingEnvironment.ContentImagePath()}/{name}";
                if (!File.Exists(mainImagePath))
                    return null;

                string[] names = name.Split('.');
                string type = names[names.Length - 1].ToLower();
                string fileName = string.Join('.', names.Where(a => a != type));//
                string queryName = string.Empty;
                foreach (string query in queries)
                    queryName += $"-{query}";

                fileName += queryName;

                // Daha önceden hazırlanmış görsel var ise doğrudan onu döndürüyoruz.
                string path = $"{_hostingEnvironment.ContentImagePath()}/{fileName}.{type}";
                if (File.Exists(path))
                    return path;

                #endregion Validate

                using (MemoryStream memoryStream = new MemoryStream())
                using (Image image = Image.Load(mainImagePath))
                {
                    #region Quality

                    int? quality = null;
                    if (queryString.Contains("q_")) // kalite parametresi var ise
                        quality = int.Parse(queries.Find(a => a.Contains("q_"))?.Replace("q_", ""));

                    #endregion Quality

                    #region Width & Height

                    // Her iki değer var ise foto index noktası alınarak kenarlardan kırpılır
                    if (queryString.Contains("w_") && queryString.Contains("h_"))
                    {
                        int width = int.Parse(queries.Find(a => a.Contains("w_"))?.Replace("w_", ""));
                        int height = int.Parse(queries.Find(a => a.Contains("h_"))?.Replace("h_", ""));
                        image.Mutate(x => x.Resize(new ResizeOptions
                        {
                            Mode = ResizeMode.Crop,
                            Size = new Size(width, height)
                        }));
                    }
                    else if (queryString.Contains("w_")) // Genişlik değerine göre ölçeklendirilir ve yükseklik orantılı olarak verilir
                    {
                        decimal width = decimal.Parse(queries.Find(a => a.Contains("w_"))?.Replace("w_", "")); // genişlik değerini buluyoruz
                        decimal rate = width / image.Width; // gelen değeri orjinal değere göre oranlıyoruz, bu oranı yükseklik değerini hesaplarken kullanacağız
                        image.Mutate(x => x.Resize(new ResizeOptions
                        {
                            Mode = ResizeMode.Crop,
                            Size = new Size(
                                (int)Math.Round(image.Width * rate),
                                (int)Math.Round(image.Height * rate))
                        }));
                    }
                    else if (queryString.Contains("h_")) // Yükseklik değerine göre ölçeklendirilir ve genişlik orantılı olarak verilir
                    {
                        decimal height = decimal.Parse(queries.Find(a => a.Contains("h_"))?.Replace("h_", "")); // yükseklik değerini buluyoruz
                        decimal rate = height / image.Height; // gelen değeri orjinal değere göre oranlıyoruz, bu oranı genişlik değerini hesaplarken kullanacağız
                        image.Mutate(x => x.Resize(new ResizeOptions
                        {
                            Mode = ResizeMode.Crop,
                            Size = new Size(
                                (int)Math.Round(image.Width * rate),
                                (int)Math.Round(image.Height * rate))
                        }));
                    }

                    #endregion Width & Height

                    #region Save & Write

                    if (type == "jpg" || type == "jpeg")
                    {
                        image.SaveAsJpeg(memoryStream, new JpegEncoder { Quality = quality });
                    }
                    else if (type == "png")
                    {
                        image.SaveAsPng(memoryStream, new PngEncoder
                        {
                            BitDepth = PngBitDepth.Bit8, // Webde 8 bit yeterli
                            CompressionLevel = PngCompressionLevel.Level9, // Sıkıştırma seviyesi
                            ColorType = PngColorType.Palette // %70 oranında sıkıştırma sağlıyor
                        });
                    }

                    if (memoryStream.Length > 0)
                    {
                        using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write))
                        {
                            memoryStream.WriteTo(fs);
                            fs.Flush();
                            return path;
                        }
                    }

                    #endregion Save & Write
                }
            }
            catch { }

            return null;
        }

        /// <summary>
        /// Eko burada resmi doğrudan klasörden çektim sen bunun parametrelerini kullanacağın yere göre düzenlersin.
        /// </summary>
        /// <returns></returns>
        public string CropWithOutStretch(int width, int height, int quality, string imagePath)
        {
            // string imagePath = $"{_customDirectory.GetContentRootPath()}/images/i-not-in-the-danger-i'm-the-danger.jpg";
            using (MemoryStream memoryStream = new MemoryStream())
            using (Image image = Image.Load(imagePath))
            {
                image.Mutate(x => x.Resize(new ResizeOptions
                {
                    Mode = ResizeMode.Pad,
                    Size = new Size(width, height)
                }).BackgroundColor(Color.White));

                image.SaveAsJpeg(memoryStream, new JpegEncoder { Quality = quality });

                string base64 = Convert.ToBase64String(memoryStream.ToArray());

                return $"data:image/jpeg;base64,{base64}";
            }
        }
    }
}