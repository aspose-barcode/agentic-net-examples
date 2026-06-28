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
        // Text to encode in QR code
        string codeText = "https://example.com";

        // Create QR code generator with specified text
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            // Set high error correction level
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Use interpolation auto-size and define image dimensions
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 300f;

            // Generate barcode image
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Save image to memory stream as PNG
                using (var ms = new MemoryStream())
                {
                    bitmap.Save(ms, ImageFormat.Png);
                    // Convert image bytes to Base64 string
                    string base64 = Convert.ToBase64String(ms.ToArray());
                    // Build data URI for embedding in MVC view
                    string imgSrc = $"data:image/png;base64,{base64}";
                    // Output the data URI (can be copied into a view)
                    Console.WriteLine(imgSrc);
                }
            }
        }
    }
}