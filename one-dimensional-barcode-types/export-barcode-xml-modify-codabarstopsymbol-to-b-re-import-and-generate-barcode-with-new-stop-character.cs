using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates creating a Codabar barcode, exporting its settings to XML,
/// modifying the stop symbol in the XML, and regenerating the barcode with the new settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define file paths for the temporary XML configuration and the final barcode image.
        string xmlPath = "barcode.xml";
        string outputPath = "barcode.png";

        // ------------------------------------------------------------
        // Step 1: Generate a Codabar barcode with default start/stop symbols (A) and export its settings to XML.
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Codabar, "123456"))
        {
            // Explicitly set start and stop symbols to 'A' (default values, shown for clarity).
            generator.Parameters.Barcode.Codabar.StartSymbol = CodabarSymbol.A;
            generator.Parameters.Barcode.Codabar.StopSymbol = CodabarSymbol.A;

            // Export the current generator configuration to an XML file.
            generator.ExportToXml(xmlPath);
        }

        // ------------------------------------------------------------
        // Step 2: Load the exported XML, change the stop symbol to 'B', and save the modified XML.
        // ------------------------------------------------------------
        if (!File.Exists(xmlPath))
        {
            Console.WriteLine("Failed to create XML file.");
            return;
        }

        // Load the XML document containing the barcode configuration.
        XDocument doc = XDocument.Load(xmlPath);

        // Locate the <CodabarStopSymbol> element within the XML hierarchy.
        XElement stopSymbolElement = doc.Root?.Descendants("CodabarStopSymbol").FirstOrDefault();

        if (stopSymbolElement != null)
        {
            // Update the stop symbol value to 'B'.
            stopSymbolElement.Value = "B";
        }
        else
        {
            Console.WriteLine("CodabarStopSymbol element not found in XML.");
            return;
        }

        // Save the modified XML back to the same file.
        doc.Save(xmlPath);

        // ------------------------------------------------------------
        // Step 3: Import the modified XML configuration, set the code text, and generate the barcode image.
        // ------------------------------------------------------------
        using (var generatorModified = BarcodeGenerator.ImportFromXml(xmlPath))
        {
            // Assign the same code text (without explicit start/stop characters).
            generatorModified.CodeText = "123456";

            // Save the generated barcode image to the specified output path.
            generatorModified.Save(outputPath);
        }

        // Inform the user that the barcode has been generated with the new stop symbol.
        Console.WriteLine($"Barcode generated with new stop symbol. Image saved to: {outputPath}");
    }
}