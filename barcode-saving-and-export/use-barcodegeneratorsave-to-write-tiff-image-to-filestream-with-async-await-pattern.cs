using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode.Generation;

namespace BarcodeTiffAsyncExample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Output file path
            string outputPath = "barcode.tiff";

            // Create an asynchronous file stream for writing
            using (var fileStream = new FileStream(
                outputPath,
                FileMode.Create,
                FileAccess.Write,
                FileShare.None,
                bufferSize: 4096,
                useAsync: true))
            {
                // Initialize the barcode generator with Code128 symbology
                using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
                {
                    // Set the text to be encoded
                    generator.CodeText = "1234567890";

                    // Save the barcode image to the stream in TIFF format using async/await
                    await Task.Run(() => generator.Save(fileStream, BarCodeImageFormat.Tiff));
                }
            }

            Console.WriteLine($"Barcode saved to {Path.GetFullPath(outputPath)}");
        }
    }
}