// Title: Batch generate barcodes from XML configuration files
// Description: This example exports sample barcode settings to XML files, then imports each XML to generate PNG barcode images, illustrating a typical batch workflow.
// Category-Description: Demonstrates Aspose.BarCode's XML export/import capabilities, using BarcodeGenerator, BarcodeGenerator.ImportFromXml, and image saving. Common for applications that need to store barcode definitions, share them across systems, or process large sets of configurations in a batch. Developers often use these APIs to automate barcode creation from predefined settings.
// Prompt: Batch process a directory of XML configuration files, generating corresponding barcode images and logging any errors encountered.
// Tags: barcode, symbology, batch, xml, image generation, aspose.barcode, png, error logging

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates batch processing of XML barcode configuration files to generate PNG images and log errors.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates sample XML configurations, imports them, generates barcode images, and writes a summary.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary working directory for the demo
        string workDir = Path.Combine(Path.GetTempPath(), "BarcodeXmlBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workDir);

        // Subdirectory where generated barcode images will be saved
        string outputDir = Path.Combine(workDir, "Output");
        Directory.CreateDirectory(outputDir);

        // Path to a simple log file that will capture any errors during processing
        string logPath = Path.Combine(workDir, "error.log");

        // Collect the full paths of the XML configuration files that will be generated
        List<string> xmlFiles = new List<string>();

        // Define a few sample barcode configurations to be exported as XML
        var samples = new List<(BaseEncodeType type, string text, string fileName)>
        {
            (EncodeTypes.Code128, "Sample123", "code128.xml"),
            (EncodeTypes.QR, "https://example.com", "qr.xml"),
            (EncodeTypes.DataMatrix, "DM12345", "datamatrix.xml")
        };

        // Export each sample configuration to an individual XML file
        foreach (var sample in samples)
        {
            string xmlPath = Path.Combine(workDir, sample.fileName);
            try
            {
                using (var generator = new BarcodeGenerator(sample.type, sample.text))
                {
                    // Example of setting a simple parameter (black barcode color)
                    generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                    generator.ExportToXml(xmlPath);
                }

                // Keep track of the successfully created XML file
                xmlFiles.Add(xmlPath);
            }
            catch (Exception ex)
            {
                // Log any export errors to the log file
                File.AppendAllText(logPath, $"Error exporting {sample.fileName}: {ex.Message}{Environment.NewLine}");
            }
        }

        // Import each XML configuration and generate the corresponding barcode image
        foreach (string xmlFile in xmlFiles)
        {
            try
            {
                using (var generator = BarcodeGenerator.ImportFromXml(xmlFile))
                {
                    string imageFileName = Path.GetFileNameWithoutExtension(xmlFile) + ".png";
                    string imagePath = Path.Combine(outputDir, imageFileName);
                    generator.Save(imagePath, BarCodeImageFormat.Png);
                }
            }
            catch (Exception ex)
            {
                // Log any import or image generation errors to the log file
                File.AppendAllText(logPath, $"Error processing {Path.GetFileName(xmlFile)}: {ex.Message}{Environment.NewLine}");
            }
        }

        // Output a brief summary of the batch operation
        Console.WriteLine($"Processing completed. Images saved to: {outputDir}");
        Console.WriteLine($"Log file (if any errors): {logPath}");
    }
}