using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a QR code with Aspose.BarCode and converting it to a data URL.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a QR code, encodes it as a Base64 data URL, and writes it to the console.
    /// </summary>
    static void Main()
    {
        // Text to encode in the QR code
        const string codeText = "Hello, Aspose QR!";

        // Variable to hold the resulting data URL
        string dataUrl;

        // Create a QR code generator with the specified text
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            // Optional: set error correction level (e.g., Medium)
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Save the barcode image to a memory stream in PNG format
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                byte[] imageBytes = ms.ToArray();

                // Convert the image bytes to a Base64 string and build the data URL
                string base64 = Convert.ToBase64String(imageBytes);
                dataUrl = $"data:image/png;base64,{base64}";
            }
        }

        // Output the data URL; it can be used directly in a Blazor component's <img src="..."/>
        Console.WriteLine(dataUrl);
    }
}