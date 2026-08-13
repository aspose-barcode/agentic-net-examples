// Title: Generate Codabar barcode with custom start/stop symbols
// Description: Demonstrates how to set the Codabar start symbol to C and stop symbol to D, then generate and save the barcode as a PNG image.
// Category-Description: Examples of barcode generation using Aspose.BarCode, focusing on configuring symbology-specific parameters. This collection shows how to use BarcodeGenerator, EncodeTypes, and barcode parameter objects to customize barcodes such as Codabar, QR, and Code128 for various output formats. Developers often need to set start/stop symbols, error correction levels, or visual styles before saving the image.
// Prompt: Set Codabar start symbol to C and stop symbol to D, then generate barcode with sample data.
// Tags: codabar, start-stop-symbol, png, aspose.barcode, aspose.barcode.generation

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Codabar barcode with custom start and stop symbols using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the barcode, configures symbols, and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Sample data to encode
        const string codeText = "123456";

        // Initialize a BarcodeGenerator for Codabar with the sample data
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Codabar, codeText))
        {
            // Configure the start and stop symbols (C and D respectively)
            generator.Parameters.Barcode.Codabar.StartSymbol = CodabarSymbol.C;
            generator.Parameters.Barcode.Codabar.StopSymbol = CodabarSymbol.D;

            // Save the generated barcode as a PNG image
            generator.Save("codabar.png");
        }

        // Inform the user that the barcode has been generated
        Console.WriteLine("Codabar barcode generated and saved as 'codabar.png'.");
    }
}