using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a barcode image and converting it to a Base64 string.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a Code128 barcode for a sample value and prints its Base64 representation.
    /// </summary>
    static void Main()
    {
        // Generate a barcode and convert it to a Base64 string
        string base64 = GenerateBarcodeBase64(EncodeTypes.Code128, "123456");
        // Output the Base64 string to the console
        Console.WriteLine(base64);
    }

    /// <summary>
    /// Generates a barcode image of the specified type and text, then returns the image as a Base64-encoded string.
    /// </summary>
    /// <param name="type">The barcode encoding type (e.g., Code128).</param>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <returns>Base64 string representing the generated barcode image in PNG format.</returns>
    static string GenerateBarcodeBase64(BaseEncodeType type, string codeText)
    {
        // Create a memory stream to hold the barcode image data
        using (var ms = new MemoryStream())
        {
            // Initialize the barcode generator with the specified type and text
            using (var generator = new BarcodeGenerator(type, codeText))
            {
                // Save the generated barcode to the memory stream in PNG format
                generator.Save(ms, BarCodeImageFormat.Png);
            }

            // Reset the stream position to the beginning before reading
            ms.Position = 0;

            // Retrieve the image bytes from the memory stream
            byte[] imageBytes = ms.ToArray();

            // Convert the image bytes to a Base64 string and return it
            return Convert.ToBase64String(imageBytes);
        }
    }
}