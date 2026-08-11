// Title: Generate Codabar barcode, modify start/stop symbols via XML, and save updated image
// Description: This example creates a Codabar barcode, exports its configuration to an XML file, edits the start/stop symbols, and re‑imports the settings to generate a new barcode image.
// Category-Description: Demonstrates barcode generation and configuration management using Aspose.BarCode. It shows how to use BarcodeGenerator to create a barcode, persist its parameters with ExportToXml, modify specific properties (Codabar start/stop symbols) in the XML, and reload the configuration with ImportFromXml to produce an updated barcode. Useful for developers who need to store, edit, or version barcode settings without rebuilding the generator each time.
// Prompt: Generate a barcode, export its XML, edit CodabarStartSymbol attribute, and re‑import to change start character.
// Tags: codabar, barcode generation, xml export, xml import, startstop symbol, aspose.barcode, c#

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates creating a Codabar barcode, exporting its settings to XML,
/// modifying the start/stop symbols, and regenerating the barcode image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example.
    /// </summary>
    static void Main()
    {
        // Step 1: Generate a Codabar barcode with default start/stop symbols (A) and save the image.
        using (var generator = new BarcodeGenerator(EncodeTypes.Codabar, "A123456A"))
        {
            // Save the original barcode image.
            generator.Save("codabar_original.png");

            // Export the current barcode configuration to an XML file for later editing.
            generator.ExportToXml("codabar.xml");
        }

        // Step 2: Load the barcode configuration from the XML file, modify the start/stop symbols, and save the new image.
        using (var generatorFromXml = BarcodeGenerator.ImportFromXml("codabar.xml"))
        {
            // Change both the start and stop symbols to 'C'.
            generatorFromXml.Parameters.Barcode.Codabar.StartSymbol = CodabarSymbol.C;
            generatorFromXml.Parameters.Barcode.Codabar.StopSymbol = CodabarSymbol.C;

            // Save the modified barcode image in PNG format.
            generatorFromXml.Save("codabar_modified.png", BarCodeImageFormat.Png);
        }
    }
}