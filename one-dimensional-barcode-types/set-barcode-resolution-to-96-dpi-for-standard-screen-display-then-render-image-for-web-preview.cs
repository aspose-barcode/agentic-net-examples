using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Code128 barcode, converting it to PNG,
/// and outputting the image as a Base64 string.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode, saves it to a memory stream, and prints the Base64 representation.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator for Code128 with the sample text "123456"
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Set the image resolution to 96 DPI (standard screen display)
            generator.Parameters.Resolution = 96f;

            // Create a memory stream to hold the generated PNG image
            using (var ms = new MemoryStream())
            {
                // Save the barcode image into the memory stream in PNG format
                generator.Save(ms, BarCodeImageFormat.Png);

                // Retrieve the image bytes from the memory stream
                byte[] imageBytes = ms.ToArray();

                // Convert the image bytes to a Base64 string for web preview or transmission
                string base64Image = Convert.ToBase64String(imageBytes);

                // Output the Base64 string to the console
                Console.WriteLine("Base64 PNG Image:");
                Console.WriteLine(base64Image);
            }
        }
    }
}