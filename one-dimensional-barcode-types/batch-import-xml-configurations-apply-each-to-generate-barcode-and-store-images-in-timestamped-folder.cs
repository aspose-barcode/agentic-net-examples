// Title: Batch generate barcodes from XML configuration files
// Description: Demonstrates how to import barcode settings from multiple XML files, generate corresponding barcode images, and save them to a timestamped folder.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of BarcodeGenerator.ImportFromXml to load configuration, and BarcodeGenerator.Save to export images. Typical scenarios include bulk barcode creation from predefined settings, automated report generation, and integration pipelines where barcode specifications are maintained as XML. Developers often need to batch‑process configurations, manage output locations, and handle errors gracefully.
// Prompt: Batch import XML configurations, apply each to generate a barcode, and store images in a timestamped folder.
// Tags: barcode generation, batch processing, xml configuration, png, aspose.barcode, barcodegenerator

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Provides a console application that reads barcode generation settings from XML files,
/// creates corresponding barcode images, and saves them into a timestamped output directory.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Executes the batch barcode generation workflow.
    /// </summary>
    static void Main()
    {
        // Define the folder that contains XML configuration files for barcode generation.
        string inputFolder = Path.Combine(Directory.GetCurrentDirectory(), "BarCodeConfigs");
        if (!Directory.Exists(inputFolder))
        {
            Console.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Create a unique output folder using the current timestamp to avoid name collisions.
        string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
        string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), $"BarCodeImages_{timestamp}");
        Directory.CreateDirectory(outputFolder);

        // Retrieve all XML files from the input directory.
        string[] xmlFiles = Directory.GetFiles(inputFolder, "*.xml");
        if (xmlFiles.Length == 0)
        {
            Console.WriteLine("No XML configuration files found.");
            return;
        }

        // Process each XML configuration file individually.
        foreach (string xmlPath in xmlFiles)
        {
            try
            {
                // Load generator settings from the XML file.
                using (BarcodeGenerator generator = BarcodeGenerator.ImportFromXml(xmlPath))
                {
                    // Build the output image path using the XML file name (without extension).
                    string fileNameWithoutExt = Path.GetFileNameWithoutExtension(xmlPath);
                    string imagePath = Path.Combine(outputFolder, $"{fileNameWithoutExt}.png");

                    // Save the generated barcode as a PNG image.
                    generator.Save(imagePath, BarCodeImageFormat.Png);
                    Console.WriteLine($"Generated barcode saved to: {imagePath}");
                }
            }
            catch (Exception ex)
            {
                // Log any errors that occur while processing a specific XML file.
                Console.WriteLine($"Failed to process '{xmlPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch barcode generation completed.");
    }
}