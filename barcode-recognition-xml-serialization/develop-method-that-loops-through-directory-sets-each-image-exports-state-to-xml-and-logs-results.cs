// Title: Barcode batch processing – generate, recognize, and export state to XML
// Description: Demonstrates creating sample barcode images, reading each image, exporting the recognition state to XML, and logging the outcome.
// Category-Description: This example belongs to the Aspose.BarCode batch processing category, illustrating how to use BarcodeGenerator for image creation, BarCodeReader for recognition, and ExportToXml for state export. Typical use cases include automated barcode validation pipelines, bulk image processing, and audit logging. Developers often need to generate barcodes, read them in bulk, and persist recognition details for further analysis.
// Prompt: Develop a method that loops through a directory, sets each image, exports state to XML, and logs results.
// Tags: barcode generation, barcode recognition, xml export, batch processing, code128, pdf417, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates batch generation of barcode images, recognition of each image, and exporting the recognition state to XML.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates sample barcodes, processes each image, and logs results.
    /// </summary>
    static void Main()
    {
        // Create a dedicated temporary folder for generated barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "Batch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Generate sample barcode images and collect their file paths
        List<string> imageFiles = new List<string>();
        string[] sampleTexts = { "ABC123", "XYZ789", "HELLO" };
        for (int i = 0; i < sampleTexts.Length; i++)
        {
            string imagePath = Path.Combine(tempFolder, $"barcode_{i + 1}.png");
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, sampleTexts[i]))
            {
                // Save each barcode as a PNG image
                generator.Save(imagePath, BarCodeImageFormat.Png);
            }
            imageFiles.Add(imagePath);
        }

        // Process each generated image: set image for recognition, export state to XML, and log the result
        foreach (string imagePath in imageFiles)
        {
            if (!File.Exists(imagePath))
            {
                Console.WriteLine($"File not found: {imagePath}");
                continue;
            }

            string xmlPath = Path.ChangeExtension(imagePath, ".xml");
            try
            {
                using (BarCodeReader reader = new BarCodeReader())
                {
                    // Load the barcode image into the reader
                    reader.SetBarCodeImage(imagePath);
                    // Optionally specify a decode type (using a generic type here)
                    reader.SetBarCodeReadType(DecodeType.Pdf417);
                    // Export the internal recognition state to an XML file
                    reader.ExportToXml(xmlPath);
                }

                Console.WriteLine($"Processed '{Path.GetFileName(imagePath)}' -> XML saved at '{Path.GetFileName(xmlPath)}'");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Warning: Unable to process '{Path.GetFileName(imagePath)}' - {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing '{Path.GetFileName(imagePath)}' - {ex.Message}");
            }
        }

        // Optional cleanup: delete generated files and temporary folder
        // Uncomment the following lines if automatic cleanup is desired
        // foreach (var file in Directory.GetFiles(tempFolder))
        //     File.Delete(file);
        // Directory.Delete(tempFolder);
    }
}