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
        string codeText = "Hello Aspose QR";
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "qr.png");

        await SaveQrCodeAsync(codeText, outputPath);

        Console.WriteLine($"QR Code saved to: {outputPath}");
    }

    static async Task SaveQrCodeAsync(string text, string outputPath)
    {
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, text))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                using (var memoryStream = new MemoryStream())
                {
                    bitmap.Save(memoryStream, ImageFormat.Png);
                    memoryStream.Position = 0;

                    using (var fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write, FileShare.None, 4096, useAsync: true))
                    {
                        await memoryStream.CopyToAsync(fileStream);
                    }
                }
            }
        }
    }
}