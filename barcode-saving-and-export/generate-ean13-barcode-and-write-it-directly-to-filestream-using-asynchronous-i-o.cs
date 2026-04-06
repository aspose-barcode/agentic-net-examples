using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode.Generation;

class Program
{
    static async Task Main(string[] args)
    {
        // Path to the output PNG file
        const string outputPath = "ean13.png";

        // Create a FileStream with asynchronous options
        using (var fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write, FileShare.None, bufferSize: 4096, useAsync: true))
        {
            // Initialize the barcode generator for EAN-13
            using (var generator = new BarcodeGenerator(EncodeTypes.EAN13))
            {
                // Set the code text (12 digits; checksum will be added automatically)
                generator.CodeText = "123456789012";

                // Save the barcode image directly to the asynchronous stream in PNG format
                generator.Save(fileStream, BarCodeImageFormat.Png);
            }

            // Ensure all data is flushed asynchronously
            await fileStream.FlushAsync();
        }

        Console.WriteLine($"EAN-13 barcode saved to '{outputPath}'.");
    }
}