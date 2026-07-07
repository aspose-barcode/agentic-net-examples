// Title: Generate Codabar Barcode with Custom Start/Stop Symbols
// Description: Demonstrates how to set Codabar start and stop symbols and generate a PNG barcode image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and Codabar parameters to customize barcode symbology. Typical use cases include creating printable labels, inventory tags, or scanning-friendly images where specific start/stop characters are required. Developers often need to adjust these symbols to meet industry standards or legacy system requirements.
// Prompt: Set Codabar start symbol to C and stop symbol to D, then generate barcode with sample data.
// Tags: codabar, barcode, generation, png, startsymbol, stopsymbol, aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a Codabar barcode with custom start and stop symbols.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a Codabar barcode using sample data,
    /// sets the start symbol to 'C' and the stop symbol to 'D', and saves the image as PNG.
    /// </summary>
    static void Main()
    {
        // Sample codetext (without start/stop symbols; they are defined via parameters)
        const string sampleCode = "123456";

        // Initialize a Codabar barcode generator with the sample codetext
        using (var generator = new BarcodeGenerator(EncodeTypes.Codabar, sampleCode))
        {
            // Configure the Codabar start and stop symbols
            generator.Parameters.Barcode.Codabar.StartSymbol = CodabarSymbol.C; // start symbol 'C'
            generator.Parameters.Barcode.Codabar.StopSymbol = CodabarSymbol.D;  // stop symbol 'D'

            // Save the generated barcode image to a PNG file
            generator.Save("codabar.png");
        }

        // Inform the user that the barcode has been created
        Console.WriteLine("Codabar barcode generated and saved as 'codabar.png'.");
    }
}