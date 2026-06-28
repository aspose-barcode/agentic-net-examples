using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a MaxiCode barcode and outputting its Base64 representation.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a MaxiCode barcode from sample text, encodes it as PNG,
    /// converts the image to a Base64 string, and writes it to the console.
    /// </summary>
    static void Main()
    {
        // Sample data to encode in the MaxiCode barcode
        const string codeText = "Test MaxiCode";

        // Resolve the MaxiCode symbology as a BaseEncodeType
        BaseEncodeType encodeType = EncodeTypes.MaxiCode;

        // Create a memory stream to hold the generated barcode image
        using (var memoryStream = new MemoryStream())
        {
            // Initialize the barcode generator with the specified type and text
            using (var generator = new BarcodeGenerator(encodeType, codeText))
            {
                // Save the generated barcode as a PNG image into the memory stream
                generator.Save(memoryStream, BarCodeImageFormat.Png);
            }

            // Reset stream position to the beginning before reading its contents
            memoryStream.Position = 0;

            // Convert the image bytes from the stream to a Base64-encoded string
            string base64 = Convert.ToBase64String(memoryStream.ToArray());

            // Output the Base64 string (e.g., for embedding in JSON)
            Console.WriteLine(base64);
        }
    }
}