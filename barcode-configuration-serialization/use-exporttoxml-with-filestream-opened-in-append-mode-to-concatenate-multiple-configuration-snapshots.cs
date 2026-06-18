using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates exporting multiple barcode configurations to a single XML file using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates barcode configurations and appends their XML representations to a file.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Define a few barcode configurations to export.
        var configurations = new (BaseEncodeType Type, string CodeText)[]
        {
            (EncodeTypes.Code128, "SampleCode128"),
            (EncodeTypes.QR, "SampleQR"),
            (EncodeTypes.DataMatrix, "SampleDataMatrix")
        };

        // Path of the XML file that will hold the exported configurations.
        string xmlFilePath = "barcodeConfigurations.xml";

        // Ensure the XML file exists by creating an empty file if it does not.
        using (var initStream = new FileStream(xmlFilePath, FileMode.Create, FileAccess.Write, FileShare.None))
        {
            // No content needed; the using block creates the file and then disposes the stream.
        }

        // Export each configuration to the same XML file using Append mode.
        foreach (var config in configurations)
        {
            // Initialize a barcode generator with the specified type and code text.
            using (var generator = new BarcodeGenerator(config.Type, config.CodeText))
            {
                // Example of setting a parameter (optional): enable checksum calculation.
                generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

                // Open the XML file in Append mode and write the current configuration.
                using (var stream = new FileStream(xmlFilePath, FileMode.Append, FileAccess.Write, FileShare.None))
                {
                    generator.ExportToXml(stream);
                }
            }
        }

        // Inform the user about the successful export.
        Console.WriteLine($"Exported {configurations.Length} barcode configurations to '{xmlFilePath}'.");
    }
}