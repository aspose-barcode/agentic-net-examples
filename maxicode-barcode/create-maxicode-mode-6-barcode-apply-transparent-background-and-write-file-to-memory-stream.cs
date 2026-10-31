// Title: Generate MaxiCode Mode 6 barcode with transparent background to a memory stream
// Description: This example creates a MaxiCode barcode in Mode 6, sets a transparent background, and writes the PNG image into a MemoryStream using Aspose.BarCode.
// Category-Description: Aspose.BarCode generation examples illustrate how to produce various barcode symbologies. This sample belongs to the barcode creation category, focusing on the MaxiCode symbology, background styling, and in‑memory image output. Developers commonly use the BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to render barcodes for web, printing, or further processing.
// Prompt: Create a MaxiCode Mode 6 barcode, apply a transparent background, and write the file to a memory stream.
// Tags: maxicode, mode6, transparent background, memory stream, png, aspose.barcode, barcode generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a MaxiCode Mode 6 barcode with a transparent background and saving it to a memory stream.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode and writes it to a <see cref="MemoryStream"/>.
    /// </summary>
    static void Main()
    {
        // Sample code text for the MaxiCode barcode
        string codeText = "Sample MaxiCode Mode6";

        // Initialize the barcode generator for MaxiCode symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, codeText))
        {
            // Apply a transparent background to the generated image
            generator.Parameters.BackColor = Color.Transparent;

            // Configure the MaxiCode to operate in Mode 6
            generator.Parameters.Barcode.MaxiCode.Mode = MaxiCodeMode.Mode6;

            // Optional: define the size of a single module (pixel size)
            generator.Parameters.Barcode.XDimension.Pixels = 10f;

            // Create a memory stream to hold the PNG image data
            using (var memoryStream = new MemoryStream())
            {
                // Save the barcode image into the stream in PNG format
                generator.Save(memoryStream, BarCodeImageFormat.Png);

                // Output the length of the generated stream for verification
                Console.WriteLine($"Barcode generated. Stream length: {memoryStream.Length} bytes.");
            }
        }
    }
}