using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates loading a barcode configuration from an XML file,
/// falling back to a default configuration, and saving the generated
/// barcode image to disk.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Paths for the configuration XML and the output image.
        string xmlPath = "barcodeConfig.xml";
        string outputPath = "barcode.png";

        // Holds the barcode generator instance (may be null if loading fails).
        BarcodeGenerator generator = null;

        // ------------------------------------------------------------
        // Attempt to load barcode configuration from the XML file.
        // ------------------------------------------------------------
        try
        {
            if (File.Exists(xmlPath))
            {
                // Import settings from the XML configuration.
                generator = BarcodeGenerator.ImportFromXml(xmlPath);
                Console.WriteLine($"Loaded barcode configuration from '{xmlPath}'.");
            }
            else
            {
                // Inform the user that the XML file does not exist.
                Console.WriteLine($"XML configuration file not found: '{xmlPath}'.");
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occurred during import.
            Console.WriteLine($"ImportFromXml failed: {ex.Message}");
        }

        // ------------------------------------------------------------
        // If loading failed, create a default barcode configuration.
        // ------------------------------------------------------------
        if (generator == null)
        {
            Console.WriteLine("Creating default barcode configuration.");
            generator = new BarcodeGenerator(EncodeTypes.Code128, "Default123");

            // Example default settings:
            // Enable checksum calculation.
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
            // Place the code text below the barcode.
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;
        }

        // ------------------------------------------------------------
        // Save the generated barcode image to the specified file.
        // ------------------------------------------------------------
        using (generator)
        {
            generator.Save(outputPath);
        }

        Console.WriteLine($"Barcode image saved to '{outputPath}'.");
    }
}