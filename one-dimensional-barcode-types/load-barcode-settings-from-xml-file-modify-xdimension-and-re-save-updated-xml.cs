using System;
using System.IO;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates importing barcode settings from an XML file,
/// modifying the XDimension, and exporting the updated settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Define the input and output XML file paths.
        string inputPath = "barcodeSettings.xml";
        string outputPath = "updatedBarcodeSettings.xml";

        // Ensure the input file exists before attempting to load it.
        if (!File.Exists(inputPath))
        {
            Console.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Import barcode settings from the existing XML file.
        using (var generator = BarcodeGenerator.ImportFromXml(inputPath))
        {
            // Update the XDimension (module size) to 2.5 points.
            generator.Parameters.Barcode.XDimension.Point = 2.5f;

            // Export the modified settings to a new XML file.
            generator.ExportToXml(outputPath);
        }

        // Inform the user that the operation completed successfully.
        Console.WriteLine($"Updated barcode settings saved to: {outputPath}");
    }
}