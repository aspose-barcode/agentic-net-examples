using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static async Task Main(string[] args)
    {
        // Output file path
        string outputPath = "qr.png";

        // Create QR Code generator
        using (var generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Set the text to encode
            generator.CodeText = "https://example.com";

            // Set high error correction level
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Generate the barcode image
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            // Save image to a memory stream in PNG format
            using (var memoryStream = new MemoryStream())
            {
                bitmap.Save(memoryStream, ImageFormat.Png);
                // Ensure the stream is positioned at the beginning
                memoryStream.Position = 0;
                // Asynchronously write the image bytes to the file system
                await File.WriteAllBytesAsync(outputPath, memoryStream.ToArray());
            }
        }

        Console.WriteLine($"QR Code saved to {Path.GetFullPath(outputPath)}");
    }
}