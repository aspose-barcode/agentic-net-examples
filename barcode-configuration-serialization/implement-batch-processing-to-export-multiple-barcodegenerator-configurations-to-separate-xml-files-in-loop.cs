// Title: Batch export of multiple barcode configurations to XML
// Description: Demonstrates how to generate several barcodes with different symbologies and export each configuration to a separate XML file using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation and export category, showing how to use BarcodeGenerator together with its Parameters to configure barcode appearance and then serialize the settings to XML. Typical use cases include batch processing of barcode definitions for later reuse, integration with external systems, or version‑controlled storage. Developers often need to loop through multiple encode types, set visual properties, and call ExportToXml, which this sample illustrates.
// Prompt: Implement batch processing to export multiple BarcodeGenerator configurations to separate XML files in a loop.
// Tags: barcode generation, batch processing, xml export, aspose.barcode, encode types, configuration

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates several barcode generators with different symbologies
/// and exports each generator's configuration to an individual XML file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Sets up an output directory, defines barcode configurations,
    /// and iterates through them to generate and export XML files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the exported XML files
        string outputFolder = Path.Combine(Path.GetTempPath(), "BatchExport_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);
        Console.WriteLine("Export folder: " + outputFolder);

        // Define a list of barcode configurations: encode type, text, and target file name
        var configs = new List<(BaseEncodeType encodeType, string codeText, string fileName)>
        {
            (EncodeTypes.Code128, "12345678", "code128.xml"),
            (EncodeTypes.QR, "https://example.com", "qr.xml"),
            (EncodeTypes.DataMatrix, "DM12345", "datamatrix.xml"),
            (EncodeTypes.Pdf417, "PDF417 Sample", "pdf417.xml"),
            (EncodeTypes.Aztec, "Aztec Text", "aztec.xml")
        };

        // Process each configuration in the list
        for (int i = 0; i < configs.Count; i++)
        {
            var cfg = configs[i];
            string xmlPath = Path.Combine(outputFolder, cfg.fileName);

            try
            {
                // Initialize the barcode generator with the specified type and text
                using (var generator = new BarcodeGenerator(cfg.encodeType, cfg.codeText))
                {
                    // Set visual parameters for the barcode
                    generator.Parameters.Barcode.XDimension.Pixels = 2f;
                    generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                    generator.Parameters.BackColor = Aspose.Drawing.Color.White;

                    // Export the generator's configuration to an XML file
                    generator.ExportToXml(xmlPath);
                }

                Console.WriteLine($"Exported {cfg.fileName}");
            }
            catch (Exception ex)
            {
                // Log any errors that occur during export
                Console.WriteLine($"Failed to export {cfg.fileName}: {ex.Message}");
            }
        }
    }
}