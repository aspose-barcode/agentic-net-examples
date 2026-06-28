using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a MaxiCode barcode (Mode 6) with a transparent background
/// and writing it to a memory stream.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates the barcode, saves it to a memory stream, and outputs its size.
    /// </summary>
    static void Main()
    {
        // Initialize a MaxiCode barcode generator for Mode 6
        using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode))
        {
            // Set the text to be encoded in the barcode
            generator.CodeText = "Test message";

            // Configure MaxiCode specific parameters: select Mode 6
            generator.Parameters.Barcode.MaxiCode.Mode = MaxiCodeMode.Mode6;

            // Set the background color to transparent to support PNG transparency
            generator.Parameters.BackColor = Color.Transparent;

            // Create a memory stream to hold the generated PNG image
            using (var memoryStream = new MemoryStream())
            {
                // Save the barcode image to the memory stream in PNG format
                generator.Save(memoryStream, BarCodeImageFormat.Png);

                // Reset the stream position to the beginning for any subsequent reads
                memoryStream.Position = 0;

                // Output the size of the generated image in bytes
                Console.WriteLine($"Generated barcode size: {memoryStream.Length} bytes");
            }
        }
    }
}