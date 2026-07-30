// Title: Serialize barcode generation settings to XML in memory
// Description: Demonstrates exporting Aspose.BarCode generation settings to a MemoryStream as XML, useful for persisting or transmitting configuration.
// Category-Description: This example belongs to the Aspose.BarCode configuration serialization category, illustrating how to use BarcodeGenerator and its ExportToXml method to capture generation parameters. Developers often need to save or share barcode settings across services, and this pattern shows the typical API usage with MemoryStream for in‑memory handling.
// Prompt: Serialize barcode generation settings to a MemoryStream by calling ExportToXml(Stream) method directly.
// Tags: barcode, serialization, xml, memorystream, aspnet, aspnetcore, aspose.barcode, code128, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates exporting barcode generation settings to an in‑memory XML representation.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a Code128 barcode generator, configures parameters, and exports its settings to XML via a MemoryStream.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator for Code128 with sample text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Set specific generation parameters (X dimension and bar height).
            generator.Parameters.Barcode.XDimension.Point = 2f;
            generator.Parameters.Barcode.BarHeight.Point = 40f;

            // Export the generator's configuration to a MemoryStream as XML.
            using (var memoryStream = new MemoryStream())
            {
                bool success = generator.ExportToXml(memoryStream);
                Console.WriteLine($"Export to XML successful: {success}");

                // Rewind the stream to the beginning to read the XML content.
                memoryStream.Position = 0;
                using (var reader = new StreamReader(memoryStream))
                {
                    string xmlContent = reader.ReadToEnd();
                    Console.WriteLine("Exported XML:");
                    Console.WriteLine(xmlContent);
                }
            }
        }
    }
}