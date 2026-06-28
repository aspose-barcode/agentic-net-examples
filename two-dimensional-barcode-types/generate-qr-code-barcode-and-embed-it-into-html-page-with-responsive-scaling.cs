using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates a QR code, embeds it in an HTML file, and saves the result to disk.
/// </summary>
class Program
{
    /// <summary>
    /// Application entry point. Creates a QR code image, converts it to Base64,
    /// builds an HTML page with the embedded image, and writes the page to a file.
    /// </summary>
    static void Main()
    {
        // QR code content to encode
        const string qrText = "https://example.com";

        // Create a barcode generator for a QR code with the specified text
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, qrText))
        {
            // Set the QR error correction level (optional)
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Use a memory stream to hold the generated PNG image
            using (var ms = new MemoryStream())
            {
                // Save the QR code image into the memory stream in PNG format
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading

                // Convert the image bytes to a Base64 string for embedding in HTML
                string base64Image = Convert.ToBase64String(ms.ToArray());

                // Build a responsive HTML document that displays the QR code image
                string html = $@"<!DOCTYPE html>
<html>
<head>
    <meta charset=""UTF-8"">
    <title>QR Code</title>
    <style>
        img {{ max-width: 100%; height: auto; display: block; margin: auto; }}
        body {{ margin: 0; padding: 20px; font-family: Arial, sans-serif; }}
    </style>
</head>
<body>
    <img src=""data:image/png;base64,{base64Image}"" alt=""QR Code"" />
</body>
</html>";

                // Write the HTML content to a file named "qr.html"
                File.WriteAllText("qr.html", html);
                Console.WriteLine("QR code HTML generated: qr.html");
            }
        }
    }
}