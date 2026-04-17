using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

namespace BarcodeEmailDemo
{
    class Program
    {
        static void Main()
        {
            // Create a QR code generator with sample text
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
            {
                // Set high error correction level
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

                // Generate the barcode image
                using (var bitmap = generator.GenerateBarCodeImage())
                {
                    // Save the image to a memory stream in PNG format
                    using (var ms = new MemoryStream())
                    {
                        bitmap.Save(ms, ImageFormat.Png);
                        // Convert the image bytes to a Base64 string
                        string base64 = Convert.ToBase64String(ms.ToArray());

                        // Build an email HTML template with the inline Base64 image
                        string emailHtml = $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset=""UTF-8"">
    <title>QR Code Email</title>
</head>
<body>
    <p>Hello,</p>
    <p>Please scan the QR code below:</p>
    <img src=""data:image/png;base64,{base64}"" alt=""QR Code"" />
    <p>Thank you.</p>
</body>
</html>";

                        // Output the HTML to the console (replace with actual email sending in production)
                        Console.WriteLine(emailHtml);
                    }
                }
            }
        }
    }
}