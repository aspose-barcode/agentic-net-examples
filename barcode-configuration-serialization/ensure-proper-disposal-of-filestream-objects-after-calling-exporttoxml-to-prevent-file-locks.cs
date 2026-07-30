// Title: Export Barcode Generator Settings to XML with Proper Resource Disposal
// Description: Demonstrates exporting Aspose.BarCode generator settings to an XML file while ensuring FileStream is correctly disposed to avoid file locks.
// Category-Description: This example belongs to the Aspose.BarCode configuration export category, showcasing how to use BarcodeGenerator and its ExportToXml method. It highlights best practices for resource management with FileStream, a common requirement when persisting barcode settings for later reuse or analysis. Developers working with barcode generation often need to serialize settings for configuration sharing or debugging.
// Prompt: Ensure proper disposal of FileStream objects after calling ExportToXml to prevent file locks.
// Tags: barcode symbology, export, xml, filestream disposal, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates exporting barcode generator settings to XML and saving a barcode image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a Code128 barcode, configures parameters,
    /// exports settings to XML with proper disposal, and saves the barcode image.
    /// </summary>
    static void Main()
    {
        // Initialize a BarcodeGenerator for Code128 symbology with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Optional: adjust visual parameters
            generator.Parameters.Barcode.XDimension.Point = 2f;
            generator.Parameters.Barcode.BarHeight.Point = 40f;

            // Export the generator's configuration to an XML file.
            // The FileStream is wrapped in a using block to guarantee disposal.
            using (var stream = new FileStream("barcode_settings.xml", FileMode.Create, FileAccess.Write, FileShare.None))
            {
                bool exported = generator.ExportToXml(stream);
                Console.WriteLine($"Export to XML successful: {exported}");
            } // FileStream disposed here, releasing any file lock.

            // Save the generated barcode image to verify the generator works.
            generator.Save("barcode.png");
        } // BarcodeGenerator disposed here.

        Console.WriteLine("Operation completed.");
    }
}