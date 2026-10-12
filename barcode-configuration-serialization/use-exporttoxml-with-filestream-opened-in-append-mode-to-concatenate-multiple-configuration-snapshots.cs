// Title: Export multiple barcode configurations to a single XML file using Append mode
// Description: Demonstrates how to export several Aspose.BarCode generator settings into one XML document by opening a FileStream in Append mode, enabling concatenation of configuration snapshots.
// Category-Description: This example belongs to the Aspose.BarCode configuration management category. It showcases the ExportToXml method of the BarcodeGenerator class to persist generator settings. Typical use cases include saving barcode configuration templates, versioning settings, or aggregating multiple configurations for later analysis. Developers working with barcode generation often need to serialize generator parameters for reuse or auditing, and this pattern provides a straightforward way to collect them in a single XML file.
// Prompt: Use ExportToXml with a FileStream opened in Append mode to concatenate multiple configuration snapshots.
// Tags: barcode, symbology, export, xml, configuration, append, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Shows how to concatenate barcode generator configuration snapshots into a single XML file
/// by using <c>ExportToXml</c> with a <c>FileStream</c> opened in Append mode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates several barcode generators with distinct settings,
    /// exports each configuration to the same XML file, and prints the combined result.
    /// </summary>
    static void Main()
    {
        // Define the temporary XML file that will store concatenated configuration snapshots
        string xmlPath = Path.Combine(Path.GetTempPath(), "BarcodesConfig.xml");

        // Ensure a clean start by deleting any existing file with the same name
        if (File.Exists(xmlPath))
        {
            File.Delete(xmlPath);
        }

        // Prepare an array of barcode generators, each configured with different symbology and appearance
        var generators = new BarcodeGenerator[3];

        // QR Code generator configuration
        generators[0] = new BarcodeGenerator(EncodeTypes.QR, "SampleQR");
        generators[0].Parameters.Barcode.XDimension.Pixels = 4f;
        generators[0].Parameters.Barcode.BarColor = Color.Black;

        // Code128 generator configuration
        generators[1] = new BarcodeGenerator(EncodeTypes.Code128, "CODE128123");
        generators[1].Parameters.Barcode.XDimension.Pixels = 2f;
        generators[1].Parameters.Barcode.BarColor = Color.Blue;

        // DataMatrix generator configuration
        generators[2] = new BarcodeGenerator(EncodeTypes.DataMatrix, "DataMatrixText");
        generators[2].Parameters.Barcode.XDimension.Pixels = 3f;
        generators[2].Parameters.Barcode.BarColor = Color.Green;

        // Open a FileStream in Append mode so each ExportToXml call adds to the same file
        using (FileStream fs = new FileStream(xmlPath, FileMode.Append, FileAccess.Write, FileShare.Read))
        {
            // Export each generator's configuration to the XML file
            foreach (var gen in generators)
            {
                using (gen)
                {
                    gen.ExportToXml(fs);
                }
            }
        }

        // Output the concatenated XML content to the console for verification
        Console.WriteLine("Concatenated barcode configuration XML:");
        Console.WriteLine(File.ReadAllText(xmlPath));
    }
}