using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static async Task Main(string[] args)
    {
        // Create a barcode generator for Code128 with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Define the output file path
            string outputPath = "barcode.tif";

            // Open a FileStream with async support
            using (var stream = new FileStream(outputPath, FileMode.Create, FileAccess.Write, FileShare.None, bufferSize: 4096, useAsync: true))
            {
                // Save the barcode image to the stream in TIFF format using async/await
                await Task.Run(() => generator.Save(stream, BarCodeImageFormat.Tiff));
            }
        }

        Console.WriteLine("Barcode saved as TIFF.");
    }
}