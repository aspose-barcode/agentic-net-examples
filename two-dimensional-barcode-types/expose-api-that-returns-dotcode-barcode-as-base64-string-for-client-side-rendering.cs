using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Simulated request payload
        string codeText = "Hello DotCode!";
        string base64 = GenerateDotCodeBase64(codeText);
        Console.WriteLine(base64);
    }

    static string GenerateDotCodeBase64(string codeText)
    {
        using (var generator = new BarcodeGenerator(EncodeTypes.DotCode, codeText))
        {
            // Set encode mode (optional, default is Auto)
            generator.Parameters.Barcode.DotCode.DotCodeEncodeMode = DotCodeEncodeMode.Auto;

            // Generate the barcode image
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                using (var ms = new MemoryStream())
                {
                    // Save image to memory stream in PNG format
                    bitmap.Save(ms, ImageFormat.Png);
                    // Convert image bytes to Base64 string
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }
    }
}