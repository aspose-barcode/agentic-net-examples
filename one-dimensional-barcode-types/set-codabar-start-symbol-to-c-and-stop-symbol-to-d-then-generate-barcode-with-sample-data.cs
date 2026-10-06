// Title: Generate Codabar Barcode with Custom Start/Stop Symbols
// Description: Demonstrates how to set the Codabar start symbol to C and stop symbol to D, then generate a PNG barcode image using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of the BarcodeGenerator class with Codabar symbology. It shows how to configure barcode parameters such as start/stop symbols and X‑dimension, a common requirement when creating machine‑readable labels for inventory, shipping, or point‑of‑sale systems.
// Prompt: Set Codabar start symbol to C and stop symbol to D, then generate barcode with sample data.
// Tags: codabar, start-stop-symbol, png, barcodegenerator, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a Codabar barcode with custom start and stop symbols.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a Codabar barcode image and saves it to the output folder.
    /// </summary>
    static void Main()
    {
        // Define the output directory and ensure it exists.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "output");
        Directory.CreateDirectory(outputDir);

        // Full path for the generated PNG file.
        string outPath = Path.Combine(outputDir, "Codabar_C_D.png");

        // Create a BarcodeGenerator for Codabar with sample data "123456".
        using (var generator = new BarcodeGenerator(EncodeTypes.Codabar, "123456"))
        {
            // Set the start and stop symbols to C and D respectively.
            generator.Parameters.Barcode.Codabar.StartSymbol = CodabarSymbol.C;
            generator.Parameters.Barcode.Codabar.StopSymbol = CodabarSymbol.D;

            // Adjust the X-dimension (module width) to 2 pixels for better readability.
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Save the generated barcode as a PNG image.
            generator.Save(outPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode saved to {outPath}");
    }
}