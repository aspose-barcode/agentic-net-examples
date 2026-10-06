// Title: Export barcode generation state to XML using a configurable directory
// Description: Demonstrates reading a simple configuration file to determine the output folder and exporting the Aspose.BarCode generation state and image to XML and PNG formats.
// Category-Description: This example belongs to the Aspose.BarCode generation and export category. It showcases how to use the BarcodeGenerator class, configure barcode parameters, and persist the generation state with ExportToXml. Typical use cases include archiving barcode settings, debugging, or integrating with downstream systems that consume XML representations of barcode configurations. Developers often need to manage output locations via configuration files and save both the barcode image and its metadata.
// Prompt: Design a configuration file that specifies the default XML export directory and integrates it with ExportToXml calls.
// Tags: barcode symbology, export, xml, configuration, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode.Generation;

/// <summary>
/// Sample program that reads a configuration file to determine the export directory,
/// generates a QR barcode, and exports its generation state to XML along with the image file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // Load or create a simple configuration file containing the export directory
        // ------------------------------------------------------------
        string configFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.txt");

        // If the config file does not exist, create it with a default export directory
        if (!File.Exists(configFile))
        {
            string defaultDir = "ExportXml";
            File.WriteAllText(configFile, $"ExportDir={defaultDir}");
            Console.WriteLine($"Created default config file at '{configFile}'.");
        }

        // ------------------------------------------------------------
        // Parse the configuration file to obtain the ExportDir value
        // ------------------------------------------------------------
        string exportDir = null;
        foreach (string line in File.ReadAllLines(configFile))
        {
            if (line.StartsWith("ExportDir=", StringComparison.OrdinalIgnoreCase))
            {
                exportDir = line.Substring("ExportDir=".Length).Trim();
                break;
            }
        }

        // Validate that the export directory was found in the configuration
        if (string.IsNullOrEmpty(exportDir))
        {
            Console.WriteLine("ExportDir not found in configuration. Exiting.");
            return;
        }

        // ------------------------------------------------------------
        // Resolve the export directory to an absolute path and ensure it exists
        // ------------------------------------------------------------
        if (!Path.IsPathRooted(exportDir))
        {
            exportDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, exportDir);
        }
        Directory.CreateDirectory(exportDir);
        Console.WriteLine($"Using XML export directory: {exportDir}");

        // ------------------------------------------------------------
        // Generate a sample QR barcode and export its state to XML
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "SampleCodeText"))
        {
            // Set a simple barcode parameter (X dimension in points)
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Build the full path for the XML export file
            string xmlPath = Path.Combine(exportDir, "barcodeState.xml");

            // Export the generation state to XML
            generator.ExportToXml(xmlPath);
            Console.WriteLine($"Barcode generation state exported to: {xmlPath}");

            // Also save the barcode image for reference
            string imagePath = Path.Combine(exportDir, "barcodeImage.png");
            generator.Save(imagePath, BarCodeImageFormat.Png);
            Console.WriteLine($"Barcode image saved to: {imagePath}");
        }

        Console.WriteLine("Operation completed successfully.");
    }
}