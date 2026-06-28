using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Code128 barcode, converting it to a Base64 string, and outputting it.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode, encodes it as PNG, converts to Base64, and writes to console.
    /// </summary>
    static void Main()
    {
        // Specify the barcode symbology and the text to encode.
        BaseEncodeType encodeType = EncodeTypes.Code128;
        string codeText = "Sample123";

        // Create a BarcodeGenerator instance with the defined type and text.
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Use a memory stream to hold the generated PNG image.
            using (var ms = new MemoryStream())
            {
                // Save the barcode image to the memory stream in PNG format.
                generator.Save(ms, BarCodeImageFormat.Png);

                // Reset stream position to the beginning before reading.
                ms.Position = 0;

                // Convert the image bytes from the memory stream to a Base64 string.
                string base64 = Convert.ToBase64String(ms.ToArray());

                // Write the Base64 string to the console (suitable for embedding in HTML as a data URI).
                Console.WriteLine(base64);
            }
        }
    }
}