// Title: Initialize BarcodeGenerator from XML string using ImportFromXml
// Description: Demonstrates reading barcode configuration XML from a string, creating a MemoryStream, and initializing a BarcodeGenerator via ImportFromXml. The example also shows exporting a generator's settings, modifying a property, and saving the resulting barcode image.
// Category-Description: This example belongs to the Aspose.BarCode configuration management category, illustrating how to export a BarcodeGenerator's settings to XML, import those settings back, and adjust parameters before rendering. Key API classes include BarcodeGenerator, EncodeTypes, BarCodeImageFormat, and the ImportFromXml/ExportToXml methods. Developers often need this pattern to persist barcode configurations, share them across services, or apply dynamic changes without recreating the generator from scratch.
// Prompt: Develop a function that reads XML from a string and initializes a BarcodeGenerator using ImportFromXml(Stream).
// Tags: barcode, symbology, import, export, xml, aspose.barcode, generation, png, csharp

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Provides an example of exporting a barcode configuration to XML,
/// importing it back into a new <see cref="BarcodeGenerator"/>,
/// modifying a property, and saving the generated image.
/// </summary>
class Program
{
    /// <summary>
    /// Reads XML from a string, creates a <see cref="MemoryStream"/>,
    /// and initializes a <see cref="BarcodeGenerator"/> using <c>ImportFromXml</c>.
    /// </summary>
    /// <param name="xml">The XML string containing barcode configuration.</param>
    /// <returns>A new <see cref="BarcodeGenerator"/> initialized from the XML.</returns>
    static BarcodeGenerator InitializeGeneratorFromXml(string xml)
    {
        // Convert the XML string to a UTF‑8 byte array.
        byte[] xmlBytes = Encoding.UTF8.GetBytes(xml);

        // ImportFromXml reads the stream immediately, so it can be disposed after the call.
        using (var stream = new MemoryStream(xmlBytes))
        {
            return BarcodeGenerator.ImportFromXml(stream);
        }
    }

    /// <summary>
    /// Executes the export‑import workflow and saves the resulting barcode image.
    /// </summary>
    static void Main()
    {
        // Step 1: Create a sample barcode generator and export its configuration to XML.
        using (var originalGenerator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Export the current configuration to an XML string via a MemoryStream.
            string exportedXml;
            using (var ms = new MemoryStream())
            {
                originalGenerator.ExportToXml(ms);
                exportedXml = Encoding.UTF8.GetString(ms.ToArray());
            }

            // Step 2: Initialize a new generator from the exported XML string.
            using (var importedGenerator = InitializeGeneratorFromXml(exportedXml))
            {
                // Optional: modify properties after import if desired.
                importedGenerator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Blue;

                // Save the generated barcode image to a temporary file.
                string outputPath = Path.Combine(Path.GetTempPath(), "imported_barcode.png");
                importedGenerator.Save(outputPath, BarCodeImageFormat.Png);

                Console.WriteLine($"Barcode generated and saved to: {outputPath}");
            }
        }
    }
}