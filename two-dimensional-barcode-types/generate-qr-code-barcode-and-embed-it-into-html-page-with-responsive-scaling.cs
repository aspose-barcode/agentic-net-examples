using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Define QR code content
        const string qrText = "https://www.example.com";

        // Generate QR code image
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, qrText))
        {
            // Set high error correction level
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Generate bitmap
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Save bitmap to memory stream as PNG
                using (var ms = new MemoryStream())
                {
                    bitmap.Save(ms, ImageFormat.Png);
                    byte[] imageBytes = ms.ToArray();
                    string base64 = Convert.ToBase64String(imageBytes);

                    // Create responsive HTML with embedded image
                    string html = $@"<!DOCTYPE html>
<html>
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>QR Code</title>
    <style>
        body {{ margin:0; display:flex; justify-content:center; align-items:center; height:100vh; background:#f0f0f0; }}
        img {{ max-width:100%; height:auto; }}
    </style>
</head>
<body>
    <img src=""data:image/png;base64,{base64}"" alt=""QR Code"" />
</body>
</html>";

                    // Write HTML file
                    File.WriteAllText("qr.html", html);
                }
            }
        }
    }
}