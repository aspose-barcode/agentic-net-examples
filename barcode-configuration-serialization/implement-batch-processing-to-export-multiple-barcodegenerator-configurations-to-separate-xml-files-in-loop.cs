// Title: Batch export of multiple barcode configurations to XML
// Description: Demonstrates how to generate several barcodes with different symbologies and export each generator's settings to separate XML files.
// Category-Description: This example belongs to the Aspose.BarCode configuration export category, illustrating the use of BarcodeGenerator, its Parameters, and the ExportToXml method. Developers often need to persist barcode settings for later reuse, batch processing, or integration with other systems; this snippet shows a typical loop‑based approach for handling multiple configurations in one run.
// Prompt: Implement batch processing to export multiple BarcodeGenerator configurations to separate XML files in a loop.
// Tags: barcode symbology, export, xml, batch processing, aspnet, aspose.barcode, generator

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides an example of batch processing multiple barcode configurations
/// and exporting each configuration to a separate XML file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Iterates over a set of barcode configurations,
    /// creates a <see cref="BarcodeGenerator"/> for each, and exports its settings to XML.
    /// </summary>
    static void Main()
    {
        // Define a collection of barcode configurations to be processed.
        var configurations = new (BaseEncodeType EncodeType, string CodeText, string XmlFile)[]
        {
            (EncodeTypes.Code128, "ABC123", "code128.xml"),
            (EncodeTypes.QR, "https://example.com", "qr.xml"),
            (EncodeTypes.DataMatrix, "DM12345", "datamatrix.xml"),
            (EncodeTypes.Pdf417, "PDF417 Sample Text", "pdf417.xml"),
            (EncodeTypes.Aztec, "AztecSample", "aztec.xml")
        };

        // Process each configuration in the collection.
        foreach (var config in configurations)
        {
            // Create a barcode generator with the specified symbology and data.
            using (var generator = new BarcodeGenerator(config.EncodeType, config.CodeText))
            {
                // Set a common parameter (optional) – X dimension in points.
                generator.Parameters.Barcode.XDimension.Point = 2f;

                // Build the absolute path for the output XML file.
                string xmlPath = Path.Combine(Directory.GetCurrentDirectory(), config.XmlFile);

                // Export the generator's configuration to the XML file.
                bool success = generator.ExportToXml(xmlPath);

                // Output the result of the export operation.
                Console.WriteLine($"Exported {config.EncodeType.TypeName} to '{xmlPath}': {(success ? "Success" : "Failed")}");
            }
        }
    }
}