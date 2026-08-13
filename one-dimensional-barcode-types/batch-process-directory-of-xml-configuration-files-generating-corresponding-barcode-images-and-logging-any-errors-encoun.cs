// Title: Batch barcode generation from XML configuration files
// Description: Demonstrates how to read barcode settings from XML files, generate PNG images, and log errors.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of BarcodeGenerator.ImportFromXml and ExportToXml for batch processing. Developers often need to automate barcode creation from configuration files, handling multiple symbologies and output formats while capturing processing errors. The snippet illustrates folder handling, image saving, and simple logging, useful for CI pipelines or bulk operations.
// Prompt: Batch process a directory of XML configuration files, generating corresponding barcode images and logging any errors encountered.
// Tags: barcode generation, xml configuration, batch processing, error logging, png output, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates batch processing of XML barcode configuration files to generate PNG images.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Scans a folder for XML files, creates barcodes per configuration, saves them, and logs any errors.
    /// </summary>
    static void Main()
    {
        // Input folder containing XML configuration files
        string inputFolder = Path.Combine(Directory.GetCurrentDirectory(), "BarcodesXml");
        // Output folder for generated barcode images
        string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "BarcodesImages");
        // Log file for errors
        string logFile = Path.Combine(outputFolder, "error.log");

        // Ensure input and output folders exist
        if (!Directory.Exists(inputFolder))
        {
            Directory.CreateDirectory(inputFolder);
        }
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Seed a sample XML if the input folder is empty (self‑contained example)
        if (Directory.GetFiles(inputFolder, "*.xml").Length == 0)
        {
            string sampleXmlPath = Path.Combine(inputFolder, "sample1.xml");
            using (var sampleGenerator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
            {
                // Export the generator settings to XML
                sampleGenerator.ExportToXml(sampleXmlPath);
            }
        }

        // Process each XML file in the input folder
        foreach (string xmlFile in Directory.GetFiles(inputFolder, "*.xml"))
        {
            try
            {
                // Load generator configuration from XML
                using (var generator = BarcodeGenerator.ImportFromXml(xmlFile))
                {
                    // Determine output image path (same name, .png extension)
                    string outputImagePath = Path.Combine(
                        outputFolder,
                        Path.GetFileNameWithoutExtension(xmlFile) + ".png");

                    // Save the barcode image as PNG
                    generator.Save(outputImagePath, BarCodeImageFormat.Png);

                    Console.WriteLine($"Generated barcode: {outputImagePath}");
                }
            }
            catch (Exception ex)
            {
                // Log error to console and file
                string message = $"Error processing '{xmlFile}': {ex.Message}";
                Console.WriteLine(message);
                try
                {
                    File.AppendAllText(logFile, $"{DateTime.Now}: {message}{Environment.NewLine}");
                }
                catch
                {
                    // Swallow logging failures to avoid crashing the batch
                }
            }
        }

        Console.WriteLine("Batch processing completed.");
    }
}