// Title: Batch export of multiple BarcodeGenerator configurations to XML files
// Description: Demonstrates how to generate several barcodes with different symbologies and export each generator's configuration to a separate XML file.
// Category-Description: This example belongs to the Aspose.BarCode configuration export category, showcasing the use of BarcodeGenerator and its Parameters to create barcode settings and persist them as XML. Typical use cases include batch processing of barcode definitions for later reuse, integration with external systems, or version-controlled configuration storage. Developers often need to automate creation of multiple barcode configurations and export them for deployment or documentation.
// Prompt: Implement batch processing to export multiple BarcodeGenerator configurations to separate XML files in a loop.
// Tags: barcode symbology, batch processing, export, xml, barcodegenerator, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates batch processing of barcode configurations and exporting each to an XML file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates an output folder, defines a set of barcode configurations,
    /// generates each barcode, customizes a parameter, and exports the configuration to XML.
    /// </summary>
    static void Main()
    {
        // Create a dedicated temporary output folder for the generated XML files
        string outputFolder = Path.Combine(Path.GetTempPath(), "BarcodeXmlBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // Define an array of anonymous objects, each representing a barcode configuration
        var batch = new[]
        {
            new { Encode = EncodeTypes.Code128, Text = "12345678", FileName = "Code128.xml" },
            new { Encode = EncodeTypes.QR, Text = "Hello QR", FileName = "QR.xml" },
            new { Encode = EncodeTypes.Pdf417, Text = "PDF417 Sample", FileName = "Pdf417.xml" },
            new { Encode = EncodeTypes.DataMatrix, Text = "DM123", FileName = "DataMatrix.xml" },
            new { Encode = EncodeTypes.Aztec, Text = "AztecCode", FileName = "Aztec.xml" }
        };

        // Iterate over each configuration, generate the barcode, and export its settings to XML
        for (int i = 0; i < batch.Length; i++)
        {
            var config = batch[i];
            string xmlPath = Path.Combine(outputFolder, config.FileName);

            // Initialize the BarcodeGenerator with the specified symbology and text
            using (var generator = new BarcodeGenerator(config.Encode, config.Text))
            {
                // Optional: customize a generator parameter (e.g., X-dimension in pixels)
                generator.Parameters.Barcode.XDimension.Pixels = 2f;

                // Export the current generator configuration to an XML file
                generator.ExportToXml(xmlPath);
            }

            // Inform the user about the successful export
            Console.WriteLine($"Exported {config.Encode} configuration to: {xmlPath}");
        }

        // Indicate that the batch export process has finished
        Console.WriteLine("Batch export completed.");
    }
}