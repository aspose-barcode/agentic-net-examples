using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates loading a barcode configuration from XML, modifying its bar color,
/// and saving the updated configuration back to XML.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the input and output file paths.
        string inputPath = "barcodeConfig.xml";
        string outputPath = "barcodeConfig_modified.xml";

        // Verify that the input file exists before proceeding.
        if (!File.Exists(inputPath))
        {
            Console.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the barcode configuration from the XML file.
        using (var generator = BarcodeGenerator.ImportFromXml(inputPath))
        {
            // Change the barcode's foreground (bar) color to red.
            generator.Parameters.Barcode.BarColor = Color.Red;

            // Export the modified configuration to a new XML file.
            generator.ExportToXml(outputPath);
        }

        // Inform the user that the modified configuration has been saved.
        Console.WriteLine($"Modified barcode configuration saved to: {outputPath}");
    }
}