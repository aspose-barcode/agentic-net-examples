using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Code128 barcode, converting it to a Base64 string,
/// and outputting the result to the console.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode, encodes it as PNG, converts to Base64, and writes to console.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator for Code128 with the sample text "123ABC"
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123ABC"))
        {
            // Create a memory stream to hold the generated PNG image
            using (var ms = new MemoryStream())
            {
                // Save the barcode image into the memory stream in PNG format
                generator.Save(ms, BarCodeImageFormat.Png);

                // Reset stream position to the beginning for reading
                ms.Position = 0;

                // Convert the image bytes from the memory stream to a Base64 string
                string base64 = Convert.ToBase64String(ms.ToArray());

                // Write the Base64 string to the console (simulating a JSON API response)
                Console.WriteLine(base64);
            }
        }
    }
}