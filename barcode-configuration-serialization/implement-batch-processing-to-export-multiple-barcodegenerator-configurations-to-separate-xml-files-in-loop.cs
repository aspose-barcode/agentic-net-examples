// Title: Batch Export of BarcodeGenerator Configurations to XML
// Description: This example shows how to create multiple barcode generator settings and export each configuration to a separate XML file, useful for persisting barcode definitions.
// Category-Description: Aspose.BarCode batch processing example that demonstrates using BarcodeGenerator, its Parameters, and the ExportToXml method to serialize barcode configurations. Typical scenarios include preparing barcode templates, sharing settings across services, or storing them for later reuse. Developers working with barcode generation often need to automate creation of many barcode types and persist their configurations in a searchable format.
// Prompt: Implement batch processing to export multiple BarcodeGenerator configurations to separate XML files in a loop.
// Tags: barcode, symbology, export, xml, batch, aspose.barcode, generator, configuration

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates batch exporting of various BarcodeGenerator configurations to XML files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates an output folder, defines barcode configurations, and writes each configuration to its own XML file.
    /// </summary>
    static void Main()
    {
        // Create a dedicated output folder for the XML files
        string outputFolder = Path.Combine(Path.GetTempPath(), "BarcodeXmlBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // Define a list of barcode configurations (symbology + code text + target file name)
        var configs = new List<(BaseEncodeType EncodeType, string CodeText, string FileName)>
        {
            (EncodeTypes.Code128, "ABC123456", "Code128.xml"),
            (EncodeTypes.QR, "https://example.com", "QR.xml"),
            (EncodeTypes.DataMatrix, "DM12345", "DataMatrix.xml"),
            (EncodeTypes.Pdf417, "PDF417 Sample Text", "Pdf417.xml"),
            (EncodeTypes.Aztec, "AztecCode", "Aztec.xml")
        };

        // Process each configuration and export its settings to an XML file
        foreach (var (encodeType, codeText, fileName) in configs)
        {
            string xmlPath = Path.Combine(outputFolder, fileName);

            // Create and configure the BarcodeGenerator for the current configuration
            using (var generator = new BarcodeGenerator(encodeType, codeText))
            {
                // Example customizations (optional)
                generator.Parameters.Barcode.BarColor = Color.Blue;
                generator.Parameters.Barcode.XDimension.Point = 2f;
                generator.Parameters.Resolution = 300f;

                // Export the generator configuration to XML
                generator.ExportToXml(xmlPath);
            }

            Console.WriteLine($"Exported {encodeType.TypeName} configuration to: {xmlPath}");
        }

        Console.WriteLine("Batch export completed.");
    }
}