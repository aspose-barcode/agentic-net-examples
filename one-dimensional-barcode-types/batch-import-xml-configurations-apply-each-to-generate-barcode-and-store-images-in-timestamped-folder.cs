// Title: Batch generate barcodes from XML configurations
// Description: Demonstrates importing multiple Aspose.BarCode XML configuration files, generating corresponding barcodes, and saving them as PNG images in a timestamped folder.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to work with BarcodeGenerator.ImportFromXml, export configurations, and batch process images. Developers often need to automate barcode creation from predefined settings, especially when handling large volumes or integrating with external configuration pipelines.
// Prompt: Batch import XML configurations, apply each to generate a barcode, and store images in a timestamped folder.
// Tags: barcode, batch processing, xml, generation, png, aspose.barcode, generation, symbology

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates batch processing of barcode XML configurations:
/// 1. Creates sample XML config files.
/// 2. Imports each config, generates a barcode, and saves it as PNG.
/// 3. Stores results in a timestamped output directory.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the batch import, generation, and saving workflow.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder to hold generated XML configuration files.
        string configFolder = Path.Combine(Path.GetTempPath(), "BarcodeXmlConfigs_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(configFolder);

        // Generate sample XML configurations for demonstration purposes.
        CreateSampleXmlConfigs(configFolder);

        // Create a timestamped folder where barcode images will be saved.
        string outputFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + DateTime.Now.ToString("yyyyMMdd_HHmmss"));
        Directory.CreateDirectory(outputFolder);

        // Retrieve all XML configuration files from the temporary folder.
        string[] xmlFiles = Directory.GetFiles(configFolder, "*.xml");

        Console.WriteLine($"Found {xmlFiles.Length} XML configuration(s). Generating barcodes...");

        // Process each XML file: import, generate barcode, and save as PNG.
        foreach (string xmlPath in xmlFiles)
        {
            try
            {
                // Import barcode settings from the XML configuration.
                using (BarcodeGenerator generator = BarcodeGenerator.ImportFromXml(xmlPath))
                {
                    // Build the output image file name based on the XML file name.
                    string fileName = Path.GetFileNameWithoutExtension(xmlPath) + ".png";
                    string imagePath = Path.Combine(outputFolder, fileName);

                    // Save the generated barcode image in PNG format.
                    generator.Save(imagePath, BarCodeImageFormat.Png);
                    Console.WriteLine($"Generated barcode saved to: {imagePath}");
                }
            }
            catch (Exception ex)
            {
                // Log any errors encountered while processing a specific XML file.
                Console.WriteLine($"Error processing '{xmlPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch processing completed.");
    }

    /// <summary>
    /// Generates sample barcode XML configuration files for QR, Code128, and PDF417 symbologies.
    /// Each configuration is saved to the specified folder.
    /// </summary>
    /// <param name="folderPath">The directory where XML files will be written.</param>
    static void CreateSampleXmlConfigs(string folderPath)
    {
        // Define sample data: a tuple of EncodeTypes member and the corresponding code text.
        var samples = new List<(BaseEncodeType encodeType, string codeText)>
        {
            (EncodeTypes.QR, "Sample QR Code"),
            (EncodeTypes.Code128, "CODE12812345"),
            (EncodeTypes.Pdf417, "PDF417 Sample Text")
        };

        int index = 1;
        // Iterate over each sample, generate a barcode, and export its configuration to XML.
        foreach (var (encodeType, codeText) in samples)
        {
            using (var generator = new BarcodeGenerator(encodeType, codeText))
            {
                // Set a common property for demonstration (pixel size of X-dimension).
                generator.Parameters.Barcode.XDimension.Pixels = 2f;

                // Determine the XML file path and export the configuration.
                string xmlPath = Path.Combine(folderPath, $"config{index}.xml");
                generator.ExportToXml(xmlPath);
                Console.WriteLine($"Exported XML configuration: {xmlPath}");
            }
            index++;
        }
    }
}