using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static async Task Main(string[] args)
    {
        const string outputPath = "ean13.png";
        const string codeText = "1234567890128"; // 13‑digit EAN13 value (including checksum)

        // Create a barcode generator for EAN13 with the specified code text
        using (var generator = new BarcodeGenerator(EncodeTypes.EAN13, codeText))
        {
            // Example: set image resolution (optional)
            generator.Parameters.Resolution = 300;

            // Open a FileStream for asynchronous writing
            using (var fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write, FileShare.None, 4096, useAsync: true))
            {
                // Save the barcode image directly to the stream in PNG format
                generator.Save(fileStream, BarCodeImageFormat.Png);

                // Ensure all data is flushed asynchronously
                await fileStream.FlushAsync();
            }
        }
    }
}