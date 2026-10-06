// Title: Batch generate barcodes from XML configurations and save to timestamped folder
// Description: The sample creates XML barcode configuration files, imports each configuration, generates the corresponding barcode, and stores the images in a folder named with the current timestamp.
// Category-Description: This example belongs to the Aspose.BarCode generation and configuration category, demonstrating how to use BarcodeGenerator to export and import XML configuration files, apply them to create barcodes, and save images. Typical use cases include batch processing of barcode settings, dynamic generation based on stored configurations, and integration with external configuration management systems. Developers often need to serialize generator parameters, reuse them across environments, and automate image output.
// Prompt: Batch import XML configurations, apply each to generate a barcode, and store images in a timestamped folder.
// Tags: qr,code128,datamatrix,batch,xml,import,barcodegenerator,png

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates batch creation of barcode images by exporting configurations to XML,
/// importing them back, and saving the generated barcodes to a timestamped directory.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the workflow of creating sample XML configurations,
    /// importing each configuration, generating the barcode, and saving the image files.
    /// </summary>
    static void Main()
    {
        // Step 1: Create a temporary folder to hold the XML configuration files.
        string configFolder = Path.Combine(Path.GetTempPath(), "BarcodeXmlConfigs_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(configFolder);

        // Step 2: Define sample barcode configurations and export each to an XML file.
        var sampleConfigs = new List<(BaseEncodeType type, string text, string fileName, Color color)>
        {
            (EncodeTypes.QR, "Sample QR Code", "qr_config.xml", Color.Blue),
            (EncodeTypes.Code128, "SampleCode128", "code128_config.xml", Color.Green),
            (EncodeTypes.DataMatrix, "DM", "datamatrix_config.xml", Color.Red)
        };

        foreach (var cfg in sampleConfigs)
        {
            string xmlPath = Path.Combine(configFolder, cfg.fileName);
            using (var generator = new BarcodeGenerator(cfg.type, cfg.text))
            {
                // Apply custom visual settings.
                generator.Parameters.Barcode.BarColor = cfg.color;
                generator.Parameters.Barcode.XDimension.Pixels = 3f;

                // Export the current generator settings to an XML file.
                generator.ExportToXml(xmlPath);
            }
        }

        // Step 3: Create a timestamped folder for the generated barcode images.
        string outputFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + DateTime.Now.ToString("yyyyMMdd_HHmmss"));
        Directory.CreateDirectory(outputFolder);

        // Step 4: Import each XML configuration, generate the barcode, and save the image as PNG.
        string[] xmlFiles = Directory.GetFiles(configFolder, "*.xml");
        foreach (string xmlFile in xmlFiles)
        {
            try
            {
                using (var generator = BarcodeGenerator.ImportFromXml(xmlFile))
                {
                    string imageFileName = Path.GetFileNameWithoutExtension(xmlFile) + ".png";
                    string imagePath = Path.Combine(outputFolder, imageFileName);

                    // Save the generated barcode image.
                    generator.Save(imagePath, BarCodeImageFormat.Png);
                    Console.WriteLine($"Generated barcode saved to: {imagePath}");
                }
            }
            catch (Exception ex)
            {
                // Log any errors encountered while processing a configuration file.
                Console.WriteLine($"Failed to process '{xmlFile}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch barcode generation completed.");
    }
}