using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Code128 barcode, saving it to a memory stream,
/// and displaying basic information about the generated image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode, writes it to a memory stream, and outputs its size and Base64 preview.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator for Code128 with the sample text "123456789"
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456789"))
        {
            // Set the image resolution to 300 DPI (optional)
            generator.Parameters.Resolution = 300f;

            // Create a memory stream to hold the generated PNG image
            using (var ms = new MemoryStream())
            {
                // Save the barcode image into the memory stream in PNG format
                generator.Save(ms, BarCodeImageFormat.Png);

                // Reset the stream position to the beginning for subsequent reading
                ms.Position = 0;

                // Retrieve the image bytes from the memory stream
                byte[] imageBytes = ms.ToArray();

                // Output the size of the generated image in bytes
                Console.WriteLine($"Generated barcode image size: {imageBytes.Length} bytes");

                // Convert the image bytes to a Base64 string and display the first 100 characters
                string base64 = Convert.ToBase64String(imageBytes);
                Console.WriteLine($"Base64 representation (first 100 chars): {base64.Substring(0, Math.Min(100, base64.Length))}");
            }
        }
    }
}