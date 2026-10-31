// Title: Generate Codabar barcode with custom start and stop symbols
// Description: This example sets the Codabar start symbol to C and the stop symbol to D, then creates a PNG barcode image using sample data.
// Category-Description: Demonstrates Aspose.BarCode barcode generation for the Codabar symbology. It shows how to configure start/stop symbols, adjust module size, and save the image. Developers working with inventory, shipping, or point‑of‑sale systems often need to produce Codabar barcodes, using classes such as BarcodeGenerator, EncodeTypes, and BarCodeImageFormat.
// Prompt: Set Codabar start symbol to C and stop symbol to D, then generate barcode with sample data.
// Tags: codabar, barcode, generation, png, aspose.barcode, start-symbol, stop-symbol

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates a Codabar barcode with custom start and stop symbols.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Determine the output directory relative to the current working directory
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "output");

        // Ensure the output directory exists
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Build the full file path for the generated barcode image
        string outputPath = Path.Combine(outputDir, "Codabar_C_Start_D_Stop.png");

        // Create a BarcodeGenerator for Codabar with sample data "123456"
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Codabar, "123456"))
        {
            // Set the start symbol to C and the stop symbol to D
            generator.Parameters.Barcode.Codabar.StartSymbol = CodabarSymbol.C;
            generator.Parameters.Barcode.Codabar.StopSymbol = CodabarSymbol.D;

            // Optional: adjust the module (X) dimension for better readability
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}