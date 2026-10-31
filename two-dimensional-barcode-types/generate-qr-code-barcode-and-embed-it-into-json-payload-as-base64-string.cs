using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Sample data to encode
        string codeText = "Hello, Aspose QR!";

        // Create QR Code generator
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            // Optional: set module size
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Save barcode to a memory stream as PNG
            using (MemoryStream ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0;
                byte[] imageBytes = ms.ToArray();
                string base64 = Convert.ToBase64String(imageBytes);

                // Create JSON payload with the Base64 string
                string json = $"{{\"qrCode\":\"{base64}\"}}";

                // Output JSON to console
                Console.WriteLine(json);
            }
        }
    }
}