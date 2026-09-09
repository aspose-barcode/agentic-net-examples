// Title: Export multiple barcode configurations to a single XML file using Append mode
// Description: Demonstrates how to use ExportToXml with a FileStream opened in Append mode to concatenate several barcode configuration snapshots into one XML document.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the ExportToXml API for persisting barcode generator settings. It illustrates creating different barcode types, adjusting parameters, and writing their configurations sequentially to a single XML file. Developers working with barcode configuration management, backup, or versioning often need to serialize multiple settings, and this pattern provides a straightforward solution.
// Prompt: Use ExportToXml with a FileStream opened in Append mode to concatenate multiple configuration snapshots.
// Tags: barcode, export, xml, append, configuration, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode.Generation;

/// <summary>
/// Shows how to export several barcode generator configurations to one XML file by appending each snapshot.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates three barcode generators, customizes their X‑dimension, and appends their XML configurations to a single file.
    /// </summary>
    static void Main()
    {
        // Define the temporary output file path
        string outputPath = Path.Combine(Path.GetTempPath(), "BarcodesConfig.xml");

        // Ensure a clean start by deleting any existing file
        if (File.Exists(outputPath))
        {
            File.Delete(outputPath);
        }

        // Open a FileStream in Append mode so each ExportToXml call adds to the same file
        using (FileStream fs = new FileStream(outputPath, FileMode.Append, FileAccess.Write, FileShare.Read))
        {
            // First barcode configuration (QR)
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "SampleQR"))
            {
                generator.Parameters.Barcode.XDimension.Pixels = 4f;
                generator.ExportToXml(fs);
            }

            // Second barcode configuration (Code128)
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "SampleCode128"))
            {
                generator.Parameters.Barcode.XDimension.Pixels = 2f;
                generator.ExportToXml(fs);
            }

            // Third barcode configuration (DataMatrix)
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "SampleDM"))
            {
                generator.Parameters.Barcode.XDimension.Pixels = 3f;
                generator.ExportToXml(fs);
            }
        }

        // Output result information
        Console.WriteLine($"Exported barcode configurations to: {outputPath}");
        Console.WriteLine($"File size: {new FileInfo(outputPath).Length} bytes");
    }
}