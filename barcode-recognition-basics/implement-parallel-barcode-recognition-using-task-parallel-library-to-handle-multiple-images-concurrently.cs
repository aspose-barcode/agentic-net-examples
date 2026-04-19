using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static async Task Main(string[] args)
    {
        // Folder containing barcode images (adjust as needed)
        string imagesFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Barcodes");

        if (!Directory.Exists(imagesFolder))
        {
            Console.WriteLine($"Folder not found: {imagesFolder}");
            return;
        }

        // Get up to 5 image files of common formats
        string[] supportedExtensions = new[] { ".png", ".jpg", ".jpeg", ".bmp", ".tif", ".tiff" };
        var imageFiles = Directory.GetFiles(imagesFolder)
                                  .Where(f => supportedExtensions.Contains(Path.GetExtension(f), StringComparer.OrdinalIgnoreCase))
                                  .Take(5)
                                  .ToArray();

        if (imageFiles.Length == 0)
        {
            Console.WriteLine("No barcode image files found.");
            return;
        }

        // Process each image concurrently
        var tasks = imageFiles.Select(file => ProcessImageAsync(file)).ToArray();
        await Task.WhenAll(tasks);
    }

    private static async Task ProcessImageAsync(string imagePath)
    {
        // Validate file existence (should already be true)
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        await Task.Run(() =>
        {
            // BarCodeReader implements IDisposable, use a using block
            using (var reader = new BarCodeReader(imagePath))
            {
                // Optionally set quality preset (default is NormalQuality)
                // reader.QualitySettings = QualitySettings.NormalQuality;

                // Read all barcodes from the image
                var results = reader.ReadBarCodes();

                if (results.Length == 0)
                {
                    Console.WriteLine($"No barcodes detected in: {Path.GetFileName(imagePath)}");
                }
                else
                {
                    Console.WriteLine($"Barcodes in {Path.GetFileName(imagePath)}:");
                    foreach (var result in results)
                    {
                        Console.WriteLine($"  Type: {result.CodeTypeName}, Text: {result.CodeText}");
                    }
                }
            }
        });
    }
}