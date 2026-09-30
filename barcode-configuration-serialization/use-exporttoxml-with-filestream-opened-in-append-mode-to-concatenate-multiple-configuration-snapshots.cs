// Title: Export Multiple Barcode Configurations to a Single XML File
// Description: Demonstrates how to use ExportToXml with a FileStream opened in Append mode to concatenate several barcode configuration snapshots into one XML document.
// Category-Description: This example belongs to the Aspose.BarCode configuration export category. It shows how to work with the BarcodeGenerator class and its Parameters to customize barcodes, then serialize each configuration to XML using ExportToXml. Typical use cases include persisting barcode settings for later reuse, auditing, or batch processing. Developers often need to combine multiple configuration snapshots into a single file for easy storage or version control.
// Prompt: Use ExportToXml with a FileStream opened in Append mode to concatenate multiple configuration snapshots.
// Tags: barcode, export, xml, append, configuration, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Shows how to export several barcode generator configurations to a single XML file
/// by appending each snapshot using a FileStream in Append mode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates three barcode generators, customizes them,
    /// and writes their XML representations sequentially to one file.
    /// </summary>
    static void Main()
    {
        // Define the path for the concatenated XML file in the temporary folder.
        string xmlPath = Path.Combine(Path.GetTempPath(), "BarcodesConfig.xml");

        // Ensure the output file starts empty by deleting any existing file.
        if (File.Exists(xmlPath))
        {
            File.Delete(xmlPath);
        }

        // Prepare a collection of sample barcode configurations to export.
        var configs = new[]
        {
            new { Encode = EncodeTypes.Code128, Text = "ABC123" },
            new { Encode = EncodeTypes.QR, Text = "Hello World" },
            new { Encode = EncodeTypes.DataMatrix, Text = "DM001" }
        };

        // Iterate over each configuration, generate a barcode, customize it,
        // and append its XML representation to the same file.
        foreach (var cfg in configs)
        {
            using (var generator = new BarcodeGenerator(cfg.Encode, cfg.Text))
            {
                // Example of customizing a barcode property (set bar color to blue).
                generator.Parameters.Barcode.BarColor = Color.Blue;

                // Open a FileStream in Append mode and write the XML snapshot.
                using (var fs = new FileStream(xmlPath, FileMode.Append, FileAccess.Write, FileShare.Read))
                {
                    generator.ExportToXml(fs);
                }
            }
        }

        // Inform the user where the concatenated XML file was saved.
        Console.WriteLine($"Exported {configs.Length} barcode configurations to:");
        Console.WriteLine(xmlPath);
    }
}