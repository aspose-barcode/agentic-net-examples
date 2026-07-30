// Title: Clone BarcodeGenerator configuration using ExportToXml and ImportFromXml
// Description: Demonstrates exporting a BarcodeGenerator's settings to XML and importing them to create an identical clone, useful for reusing configurations across objects.
// Category-Description: This example belongs to the Aspose.BarCode configuration management category, showcasing how to serialize and deserialize barcode generator settings via XML. It highlights key API classes such as BarcodeGenerator, ExportToXml, and ImportFromXml, which developers commonly use to persist, share, or clone barcode configurations in enterprise applications.
// Prompt: Chain ExportToXml and ImportFromXml calls to clone a BarcodeGenerator configuration into a new object.
// Tags: barcode symbology, configuration cloning, xml serialization, exporttoxml, importfromxml, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode;

/// <summary>
/// Demonstrates cloning a BarcodeGenerator configuration by exporting to XML and importing back.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates an original barcode, exports its configuration to XML, imports it to a new generator, and saves both images.
    /// </summary>
    static void Main()
    {
        // Initialize the original barcode generator with Code128 symbology and sample text.
        using (var original = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Set various barcode appearance parameters.
            original.Parameters.Barcode.XDimension.Point = 2f;
            original.Parameters.Barcode.BarHeight.Point = 50f;
            original.Parameters.Barcode.FilledBars = false;
            original.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = false;
            original.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Helvetica";
            original.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 12f;

            // Export the generator's configuration to an in‑memory XML stream.
            using (var xmlStream = new MemoryStream())
            {
                bool exportSuccess = original.ExportToXml(xmlStream);
                if (!exportSuccess)
                {
                    Console.WriteLine("Failed to export barcode configuration to XML.");
                    return;
                }

                // Reset stream position to the beginning before reading.
                xmlStream.Position = 0;

                // Import the configuration from the XML stream to create a cloned generator.
                using (var cloned = BarcodeGenerator.ImportFromXml(xmlStream))
                {
                    // Save the original barcode image.
                    original.Save("original.png");

                    // Save the cloned barcode image.
                    cloned.Save("cloned.png");

                    Console.WriteLine("Original and cloned barcode images have been saved.");
                }
            }
        }
    }
}