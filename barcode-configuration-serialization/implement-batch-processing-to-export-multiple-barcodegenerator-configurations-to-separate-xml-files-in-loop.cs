// Title: Batch Export of Barcode Configurations to XML
// Description: Demonstrates how to loop through multiple barcode generator settings and export each configuration to a separate XML file.
// Prompt: Implement batch processing to export multiple BarcodeGenerator configurations to separate XML files in a loop.
// Tags: barcode symbology, export, xml, batch processing, aspnet barcodes, generator

using System;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode;

/// <summary>
/// Example program that creates several barcode generators with different
/// symbologies and exports each configuration to its own XML file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Iterates over a collection of barcode
    /// configurations, generates each barcode, and saves the generator settings
    /// to an XML file.
    /// </summary>
    static void Main()
    {
        // Define a list of barcode configurations (symbology, codetext, output XML file)
        var configs = new List<(BaseEncodeType type, string codeText, string xmlPath)>
        {
            (EncodeTypes.Code128, "ABC123", "code128.xml"),
            (EncodeTypes.QR, "https://example.com", "qr.xml"),
            (EncodeTypes.DataMatrix, "DM12345", "datamatrix.xml"),
            (EncodeTypes.Pdf417, "PDF417 Sample Text", "pdf417.xml"),
            (EncodeTypes.Aztec, "AztecDemo", "aztec.xml")
        };

        // Process each configuration in the list
        foreach (var (type, codeText, xmlPath) in configs)
        {
            // Create a BarcodeGenerator with the specified type and codetext
            using (var generator = new BarcodeGenerator(type, codeText))
            {
                // Set common visual parameters (optional)
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;

                // Export the generator configuration to an XML file
                bool success = generator.ExportToXml(xmlPath);
                Console.WriteLine($"{xmlPath}: {(success ? "Exported" : "Failed")}");
            }
        }
    }
}