// Title: Serialize barcode generation settings to XML in memory
// Description: Demonstrates exporting Aspose.BarCode generator settings to a MemoryStream using ExportToXml.
// Prompt: Serialize barcode generation settings to a MemoryStream by calling ExportToXml(Stream) method directly.
// Tags: barcode, serialization, xml, memorystream, aspose.barcode, export

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeExportExample
{
    /// <summary>
    /// Provides an example of exporting barcode generation settings to a MemoryStream as XML.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point of the example. Creates a barcode generator, customizes settings,
        /// and serializes those settings to a memory stream using ExportToXml.
        /// </summary>
        static void Main()
        {
            // Initialize a barcode generator for Code128 symbology with sample data.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
            {
                // Customize generation parameters (e.g., bar color and module size).
                generator.Parameters.Barcode.BarColor = Color.Blue;          // Set the barcode bars to blue.
                generator.Parameters.Barcode.XDimension.Point = 2f;        // Define the X-dimension (module width) in points.

                // Prepare a memory stream to hold the exported XML.
                using (var memoryStream = new MemoryStream())
                {
                    // Export the current generator settings to the memory stream as XML.
                    bool exportResult = generator.ExportToXml(memoryStream);

                    // Output the result of the export operation and the size of the generated XML.
                    Console.WriteLine($"Export successful: {exportResult}");
                    Console.WriteLine($"XML size in bytes: {memoryStream.Length}");
                }
            }
        }
    }
}