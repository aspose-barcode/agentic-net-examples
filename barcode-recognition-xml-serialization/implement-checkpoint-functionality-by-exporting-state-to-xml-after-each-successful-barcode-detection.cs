// Title: Barcode generation, detection, and checkpoint export example
// Description: Demonstrates creating Code128 barcodes, reading them, and exporting detection state to XML after each successful read.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes, BarCodeReader for detecting them, and the ExportToXml method for checkpointing. Developers often need to generate barcodes, process scanned images, and persist recognition state for auditing or debugging, making this pattern common in inventory and logistics applications.
// Prompt: Implement checkpoint functionality by exporting the state to XML after each successful barcode detection.
// Tags: barcode generation, barcode recognition, code128, xml export, checkpoint, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Generates sample Code128 barcodes, reads them back, and exports a checkpoint XML file
/// after each successful detection using Aspose.BarCode APIs.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Handles barcode creation, detection, and checkpoint export.
    /// </summary>
    static void Main()
    {
        // Ensure the output folder exists
        string imagesFolder = "Barcodes";
        if (!Directory.Exists(imagesFolder))
        {
            Directory.CreateDirectory(imagesFolder);
        }

        // Sample texts to encode into barcodes
        string[] sampleTexts = new string[] { "12345", "ABCDEF", "9876543210" };

        // Generate barcode images from the sample texts
        int index = 0;
        foreach (string text in sampleTexts)
        {
            string imagePath = Path.Combine(imagesFolder, $"barcode_{index}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, text))
            {
                generator.Save(imagePath, BarCodeImageFormat.Png);
            }
            index++;
        }

        // Retrieve all generated PNG files for processing
        string[] imageFiles = Directory.GetFiles(imagesFolder, "*.png");
        int checkpointCounter = 0;

        // Iterate over each image file and attempt barcode detection
        foreach (string file in imageFiles)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            using (var reader = new BarCodeReader(file, DecodeType.AllSupportedTypes))
            {
                // Read all barcodes present in the current image
                foreach (var result in reader.ReadBarCodes())
                {
                    // Successful detection when CodeText is not null or empty
                    if (!string.IsNullOrEmpty(result.CodeText))
                    {
                        Console.WriteLine($"Detected Barcode: Type={result.CodeTypeName}, Text={result.CodeText}");

                        // Export the reader's state to an XML checkpoint file
                        string checkpointPath = $"checkpoint_{checkpointCounter}.xml";
                        try
                        {
                            reader.ExportToXml(checkpointPath);
                            Console.WriteLine($"Checkpoint exported to: {checkpointPath}");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Failed to export checkpoint: {ex.Message}");
                        }

                        checkpointCounter++;
                    }
                }
            }
        }

        Console.WriteLine("Processing completed.");
    }
}