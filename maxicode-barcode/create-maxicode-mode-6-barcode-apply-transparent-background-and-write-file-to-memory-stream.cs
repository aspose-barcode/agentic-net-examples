// Title: Generate a MaxiCode Mode 6 barcode with transparent background into a memory stream
// Description: Demonstrates how to create a MaxiCode Mode 6 barcode, set a transparent background, and save the image as PNG to a MemoryStream.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on 2‑D symbologies. It showcases the use of BarcodeGenerator, EncodeTypes, and MaxiCodeMode classes to produce a MaxiCode image, a common requirement for shipping and logistics applications. Developers often need to render barcodes to streams for further processing or embedding in documents.
// Prompt: Create a MaxiCode Mode 6 barcode, apply a transparent background, and write the file to a memory stream.
// Tags: maxicode, barcode, generation, png, memory-stream, transparent-background, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a MaxiCode Mode 6 barcode with a transparent background
/// and writes the PNG image to a memory stream.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates the barcode, configures its appearance, saves to a MemoryStream,
    /// and writes the generated image size to the console.
    /// </summary>
    static void Main()
    {
        // Create a memory stream that will hold the generated barcode image.
        using (MemoryStream memoryStream = new MemoryStream())
        {
            // Initialize the barcode generator for the MaxiCode symbology with sample data.
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.MaxiCode, "Sample Text"))
            {
                // Set the MaxiCode mode to Mode 6 (used for specific data encoding requirements).
                generator.Parameters.Barcode.MaxiCode.Mode = MaxiCodeMode.Mode6;

                // Make the background of the barcode transparent.
                generator.Parameters.BackColor = Color.Transparent;

                // Save the barcode as a PNG image into the memory stream.
                generator.Save(memoryStream, BarCodeImageFormat.Png);
            }

            // Output the size of the generated image (in bytes) for verification.
            Console.WriteLine($"Generated barcode image size: {memoryStream.Length} bytes");
        }
    }
}