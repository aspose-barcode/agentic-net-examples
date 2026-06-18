using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates exporting a barcode configuration to XML,
/// importing it back, and verifying that the settings are preserved.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the temporary XML file path that will hold the barcode configuration.
        string xmlPath = "barcode_config.xml";

        // --------------------------------------------------------------------
        // Create a barcode generator, configure its properties, and export to XML.
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Test123"))
        {
            // Enable checksum calculation for the barcode.
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

            // Set visual dimensions: bar height and X dimension (module width).
            generator.Parameters.Barcode.BarHeight.Point = 50f;
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Configure the human‑readable text font.
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Arial";
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 12f;

            // Export the complete configuration (including namespaces and metadata) to an XML file.
            generator.ExportToXml(xmlPath);
            Console.WriteLine($"Exported barcode configuration to '{xmlPath}'.");
        }

        // --------------------------------------------------------------------
        // Verify that the XML file was successfully created.
        // --------------------------------------------------------------------
        if (!File.Exists(xmlPath))
        {
            Console.WriteLine("Error: XML file was not created.");
            return;
        }

        // --------------------------------------------------------------------
        // Import the barcode configuration from the XML file and compare key properties.
        // --------------------------------------------------------------------
        using (var importedGenerator = BarcodeGenerator.ImportFromXml(xmlPath))
        {
            // Compare each relevant property with the original settings.
            bool codeTextMatch = importedGenerator.CodeText == "Test123";
            bool checksumMatch = importedGenerator.Parameters.Barcode.IsChecksumEnabled == EnableChecksum.Yes;
            bool barHeightMatch = Math.Abs(importedGenerator.Parameters.Barcode.BarHeight.Point - 50f) < 0.001f;
            bool xDimMatch = Math.Abs(importedGenerator.Parameters.Barcode.XDimension.Point - 2f) < 0.001f;
            bool fontFamilyMatch = importedGenerator.Parameters.Barcode.CodeTextParameters.Font.FamilyName == "Arial";
            bool fontSizeMatch = Math.Abs(importedGenerator.Parameters.Barcode.CodeTextParameters.Font.Size.Point - 12f) < 0.001f;

            // Output verification results.
            Console.WriteLine("Import verification results:");
            Console.WriteLine($"CodeText matches: {codeTextMatch}");
            Console.WriteLine($"Checksum enabled matches: {checksumMatch}");
            Console.WriteLine($"BarHeight matches: {barHeightMatch}");
            Console.WriteLine($"XDimension matches: {xDimMatch}");
            Console.WriteLine($"Font family matches: {fontFamilyMatch}");
            Console.WriteLine($"Font size matches: {fontSizeMatch}");
        }

        // --------------------------------------------------------------------
        // Clean up the temporary XML file (optional).
        // --------------------------------------------------------------------
        try
        {
            File.Delete(xmlPath);
            Console.WriteLine($"Deleted temporary file '{xmlPath}'.");
        }
        catch
        {
            // Suppress any exceptions that occur during cleanup.
        }
    }
}