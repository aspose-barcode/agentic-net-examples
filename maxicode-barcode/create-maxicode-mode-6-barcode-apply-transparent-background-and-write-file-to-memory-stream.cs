// Title: Generate MaxiCode Mode 6 Barcode with Transparent Background to Memory Stream
// Description: Demonstrates creating a MaxiCode Mode 6 barcode, setting a transparent background, and saving the image to a memory stream.
// Category-Description: Shows how to use Aspose.BarCode to generate 2‑D barcodes. The example covers the BarcodeGenerator class, EncodeTypes enumeration, and barcode parameter settings such as background color and MaxiCode mode. Typical use cases include generating shipping labels or inventory tags where a MaxiCode is required, and writing the result to a stream for further processing or transmission.
// Prompt: Create a MaxiCode Mode 6 barcode, apply a transparent background, and write the file to a memory stream.
// Tags: maxicode, barcode generation, transparent background, memory stream, aspose.barcode, png, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a MaxiCode Mode 6 barcode with a transparent background
/// and writes the PNG image to a <see cref="MemoryStream"/>.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode, configures its appearance,
    /// saves it to a memory stream, and writes the stream length to the console.
    /// </summary>
    static void Main()
    {
        // Text to encode in the MaxiCode barcode.
        string codeText = "Sample MaxiCode Mode 6";

        // Create a memory stream to hold the generated PNG image.
        using (MemoryStream memoryStream = new MemoryStream())
        {
            // Initialize the barcode generator for MaxiCode with the specified text.
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.MaxiCode, codeText))
            {
                // Set the background color to transparent.
                generator.Parameters.BackColor = Aspose.Drawing.Color.Transparent;

                // Configure the MaxiCode mode to Mode 6.
                generator.Parameters.Barcode.MaxiCode.Mode = MaxiCodeMode.Mode6;

                // Save the barcode image as PNG into the memory stream.
                generator.Save(memoryStream, BarCodeImageFormat.Png);
            }

            // Output the size of the generated image.
            Console.WriteLine($"Generated MaxiCode barcode, stream length: {memoryStream.Length} bytes");
        }
    }
}