// Title: Generate MaxiCode Mode 6 Barcode with Transparent Background
// Description: Creates a MaxiCode Mode 6 barcode, applies a transparent background, and writes the PNG image to a memory stream.
// Category-Description: This example demonstrates the use of Aspose.BarCode's ComplexBarcodeGenerator to produce MaxiCode symbols, a 2‑D barcode used in logistics and shipping. It showcases setting barcode parameters such as mode and background color, and saving the result to a stream in PNG format. Developers working with advanced barcode symbologies, custom rendering options, or in‑memory image handling will find this pattern useful.
// Prompt: Create a MaxiCode Mode 6 barcode, apply a transparent background, and write the file to a memory stream.
// Tags: maxicode, mode6, transparent background, memory stream, png, aspose.barcode, barcode generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a MaxiCode Mode 6 barcode with a transparent background
/// and saving it to a memory stream as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Builds the barcode, configures rendering options,
    /// and writes the image to a <see cref="MemoryStream"/>.
    /// </summary>
    static void Main()
    {
        // Initialize MaxiCode codetext for Mode 6 and set the message payload.
        var maxiCodeCodetext = new MaxiCodeStandardCodetext
        {
            Mode = MaxiCodeMode.Mode6,
            Message = "Test message"
        };

        // Create a ComplexBarcodeGenerator using the prepared codetext.
        using (var generator = new ComplexBarcodeGenerator(maxiCodeCodetext))
        {
            // Configure the barcode to have a transparent background.
            generator.Parameters.BackColor = Color.Transparent;

            // Save the generated barcode image to a memory stream in PNG format.
            using (var memoryStream = new MemoryStream())
            {
                generator.Save(memoryStream, BarCodeImageFormat.Png);
                Console.WriteLine($"Barcode generated. Stream length: {memoryStream.Length} bytes.");
            }
        }
    }
}