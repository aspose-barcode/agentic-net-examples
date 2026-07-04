// Title: Export and Import Barcode Generator Settings via XML
// Description: Demonstrates exporting a BarcodeGenerator's configuration to an XML file and re-importing it to generate a barcode image.
// Prompt: Ensure proper disposal of FileStream objects after calling ExportToXml to prevent file locks.
// Tags: barcode, code128, export, xml, import, aspose.barcode, filestream

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that shows how to export a barcode generator's settings to XML,
/// import them back, and create a barcode image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the export/import workflow.
    /// </summary>
    static void Main()
    {
        // Initialize a BarcodeGenerator with Code128 symbology and sample data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Export the generator's configuration to an XML file.
            // The FileStream is wrapped in a using block to guarantee disposal.
            using (var writeStream = new FileStream("barcode.xml", FileMode.Create, FileAccess.Write, FileShare.None))
            {
                generator.ExportToXml(writeStream);
            }

            // Open the previously created XML file for reading.
            using (var readStream = new FileStream("barcode.xml", FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                // Import a new BarcodeGenerator instance from the XML configuration.
                using (var importedGenerator = BarcodeGenerator.ImportFromXml(readStream))
                {
                    // Save the barcode generated from the imported settings as a PNG image.
                    importedGenerator.Save("barcode.png");
                }
            }
        }
    }
}