using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a DotCode barcode and returning its Base64 representation.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a DotCode barcode from sample text and prints its Base64 string.
    /// </summary>
    static void Main()
    {
        // Sample codetext for the DotCode barcode
        string codeText = "Hello DotCode";

        // Generate the barcode and obtain its Base64 representation
        string base64Image = GenerateDotCodeBase64(codeText);

        // Output the Base64 string (can be used client‑side for rendering)
        Console.WriteLine(base64Image);
    }

    static string GenerateDotCodeBase64(string codeText)
    {
        // MemoryStream will hold the generated PNG image
        using (var memoryStream = new MemoryStream())
        {
            // Create a BarcodeGenerator for DotCode symbology
            using (var generator = new BarcodeGenerator(EncodeTypes.DotCode, codeText))
            {
                // Optional: configure DotCode specific parameters here
                // generator.Parameters.Barcode.DotCode.ECIEncoding = ECIEncodings.UTF8;

                // Save the barcode image to the memory stream in PNG format
                generator.Save(memoryStream, BarCodeImageFormat.Png);
            }

            // Reset stream position before reading
            memoryStream.Position = 0;

            // Convert the image bytes to a Base64 string
            byte[] imageBytes = memoryStream.ToArray();
            return Convert.ToBase64String(imageBytes);
        }
    }
}