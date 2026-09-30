// Title: Export Barcode Generator Configuration to XML with Proper Resource Disposal
// Description: Demonstrates how to generate a Code128 barcode, configure its appearance, and export the generator settings to an XML file while ensuring streams are correctly disposed.
// Category-Description: This example belongs to the Aspose.BarCode configuration export category. It showcases the use of BarcodeGenerator, its Parameters, and the ExportToXml method to persist settings. Developers often need to save barcode configurations for reuse, version control, or automated testing, and proper disposal of FileStream objects prevents file locks.
// Prompt: Ensure proper disposal of FileStream objects after calling ExportToXml to prevent file locks.
// Tags: barcode, code128, export, xml, configuration, aspose.barcode, filestream, disposal

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode;
using Aspose.Drawing;

/// <summary>
/// Demonstrates exporting a barcode generator's configuration to an XML file with correct resource handling.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a barcode, configures visual properties, exports settings to XML, and displays a preview of the file.
    /// </summary>
    static void Main()
    {
        // Define the temporary path for the exported XML configuration file
        string xmlPath = Path.Combine(Path.GetTempPath(), "barcodeConfig.xml");

        // Ensure a clean start by deleting any existing file with the same name
        if (File.Exists(xmlPath))
        {
            File.Delete(xmlPath);
        }

        // Initialize the barcode generator with Code128 symbology and sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Configure visual appearance of the barcode
            generator.Parameters.Barcode.BarColor = Color.Black;
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Helvetica";
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 12f;

            // Export the generator's configuration to XML using a FileStream wrapped in a using block
            using (var fs = new FileStream(xmlPath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                generator.ExportToXml(fs);
            }
        }

        // Verify that the XML file was created and output the first few lines for confirmation
        if (File.Exists(xmlPath))
        {
            Console.WriteLine($"Exported XML successfully to: {xmlPath}");
            using (var sr = new StreamReader(xmlPath))
            {
                for (int i = 0; i < 5; i++)
                {
                    string line = sr.ReadLine();
                    if (line == null) break;
                    Console.WriteLine(line);
                }
            }
        }
        else
        {
            Console.WriteLine("Failed to create XML file.");
        }
    }
}