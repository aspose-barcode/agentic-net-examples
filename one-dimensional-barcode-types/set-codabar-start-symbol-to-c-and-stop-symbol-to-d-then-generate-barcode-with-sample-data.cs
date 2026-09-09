// Title: Generate Codabar barcode with custom start/stop symbols
// Description: Demonstrates how to set Codabar start symbol to C and stop symbol to D, then generate a PNG barcode image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and Codabar settings. Developers often need to customize start/stop symbols for Codabar symbology in inventory, library, or logistics applications. The snippet shows typical configuration and image saving steps for quick integration.
// Prompt: Set Codabar start symbol to C and stop symbol to D, then generate barcode with sample data.
// Tags: codabar, start-symbol, stop-symbol, barcode-generation, png, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a Codabar barcode with custom start and stop symbols
/// and saves it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a Codabar barcode using start symbol 'C' and stop symbol 'D',
    /// then writes the output file path to the console.
    /// </summary>
    static void Main()
    {
        // Define the full path for the output PNG file in the current directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "Codabar_C_D.png");

        // Create a BarcodeGenerator for Codabar with sample data "12345".
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Codabar, "12345"))
        {
            // Set the X-dimension (module width) to 2 pixels for better readability.
            generator.Parameters.Barcode.XDimension.Pixels = 2;

            // Configure Codabar-specific settings: start symbol 'C' and stop symbol 'D'.
            generator.Parameters.Barcode.Codabar.StartSymbol = CodabarSymbol.C;
            generator.Parameters.Barcode.Codabar.StopSymbol = CodabarSymbol.D;

            // Save the generated barcode as a PNG image to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Codabar barcode saved to: {outputPath}");
    }
}