// Title: Batch generate barcodes from XML configurations
// Description: Demonstrates importing multiple barcode settings from XML files, generating corresponding PNG images, and saving them to a timestamped folder.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use BarcodeGenerator.ImportFromXml and BarcodeGenerator.Save to process batch configurations. Developers often need to automate barcode creation from predefined XML templates for inventory, shipping, or labeling workflows. The snippet illustrates folder handling, timestamped output, and error reporting for large‑scale barcode generation.
// Prompt: Batch import XML configurations, apply each to generate a barcode, and store images in a timestamped folder.
// Tags: barcode symbology, batch import, png, barcodegenerator

using System;
using System.IO;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates barcode images by importing settings from XML files located in a predefined folder.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Scans the input directory for XML configurations,
    /// creates barcodes, and saves them as PNG files in a timestamped output folder.
    /// </summary>
    static void Main()
    {
        // Define the folder that contains XML barcode configuration files.
        string inputFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BarcodesXml");
        if (!Directory.Exists(inputFolder))
        {
            // Ensure the input folder exists to avoid runtime errors.
            Directory.CreateDirectory(inputFolder);
        }

        // Define a timestamped output folder for the generated barcode images.
        string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
        string outputFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BarcodesOutput", timestamp);
        Directory.CreateDirectory(outputFolder);

        // Retrieve all XML files from the input folder.
        string[] xmlFiles = Directory.GetFiles(inputFolder, "*.xml");
        if (xmlFiles.Length == 0)
        {
            Console.WriteLine("No XML configuration files found in: " + inputFolder);
            return;
        }

        // Process each XML configuration file.
        foreach (string xmlPath in xmlFiles)
        {
            try
            {
                // Import barcode settings from the XML file.
                using (BarcodeGenerator generator = BarcodeGenerator.ImportFromXml(xmlPath))
                {
                    // Build the output image file name (same as XML file name, but with .png extension).
                    string outputFileName = Path.GetFileNameWithoutExtension(xmlPath) + ".png";
                    string outputPath = Path.Combine(outputFolder, outputFileName);

                    // Save the generated barcode image to the output folder.
                    generator.Save(outputPath);
                    Console.WriteLine($"Generated barcode saved to: {outputPath}");
                }
            }
            catch (Exception ex)
            {
                // Log any errors encountered while processing the current XML file.
                Console.WriteLine($"Failed to process '{xmlPath}': {ex.Message}");
            }
        }
    }
}