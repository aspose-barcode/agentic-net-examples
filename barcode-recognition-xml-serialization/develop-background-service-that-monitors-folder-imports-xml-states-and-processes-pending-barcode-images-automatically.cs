// Title: Background Service for Automatic Barcode Generation and Recognition
// Description: Demonstrates monitoring a folder, importing barcode settings from XML, generating images, and reading pending barcode images.
// Category-Description: This example belongs to the Aspose.BarCode folder‑watching and batch processing category. It showcases key API classes such as BarcodeGenerator, BarCodeReader, and their XML import/export capabilities. Typical use cases include automated barcode creation pipelines, scheduled processing of incoming barcode specifications, and bulk recognition of generated images. Developers often need to integrate these operations into background services or CI workflows.
// Prompt: Develop a background service that monitors a folder, imports XML states, and processes pending barcode images automatically.
// Tags: barcode generation, barcode recognition, xml import, background service, aspose.barcode, code128, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates a simple background‑style workflow that creates sample barcodes,
/// exports their configuration to XML, re‑imports the settings to generate processed images,
/// and finally reads all barcode images in the working folder.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the workflow sequentially.
    /// </summary>
    static void Main()
    {
        // Define and ensure the working folder exists
        string workFolder = Path.Combine(Directory.GetCurrentDirectory(), "WorkFolder");
        Directory.CreateDirectory(workFolder);

        // --------------------------------------------------------------------
        // 1. Create sample barcode images and export their generator settings to XML
        // --------------------------------------------------------------------
        for (int i = 1; i <= 3; i++)
        {
            string codeText = $"Sample{i}";
            string imagePath = Path.Combine(workFolder, $"barcode{i}.png");
            string xmlPath = Path.Combine(workFolder, $"barcode{i}.xml");

            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Set unit‑based dimensions for the barcode and the image
                generator.Parameters.Barcode.XDimension.Point = 2f;
                generator.Parameters.ImageWidth.Point = 250f;
                generator.Parameters.ImageHeight.Point = 100f;

                // Save the generated barcode image
                generator.Save(imagePath, BarCodeImageFormat.Png);

                // Export the generator configuration to an XML file for later reuse
                generator.ExportToXml(xmlPath);
            }
        }

        // --------------------------------------------------------------------
        // 2. Process each XML state file: import settings and generate a new image
        // --------------------------------------------------------------------
        string[] xmlFiles = Directory.GetFiles(workFolder, "*.xml");
        foreach (string xmlFile in xmlFiles)
        {
            try
            {
                // Import generator configuration from the XML file
                using (var generator = BarcodeGenerator.ImportFromXml(xmlFile))
                {
                    // Determine output file name based on the XML file name
                    string fileName = Path.GetFileNameWithoutExtension(xmlFile);
                    string outputPath = Path.Combine(workFolder, $"processed_{fileName}.png");

                    // Optionally adjust image size before generation
                    generator.Parameters.ImageWidth.Point = 300f;
                    generator.Parameters.ImageHeight.Point = 120f;

                    // Generate and save the processed barcode image
                    generator.Save(outputPath, BarCodeImageFormat.Png);
                    Console.WriteLine($"Processed XML '{Path.GetFileName(xmlFile)}' -> '{Path.GetFileName(outputPath)}'");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing XML '{Path.GetFileName(xmlFile)}': {ex.Message}");
            }
        }

        // --------------------------------------------------------------------
        // 3. Scan for pending barcode images and attempt to read them
        // --------------------------------------------------------------------
        string[] imageFiles = Directory.GetFiles(workFolder, "*.png");
        foreach (string imageFile in imageFiles)
        {
            try
            {
                using (var reader = new BarCodeReader(imageFile))
                {
                    // Iterate through all detected barcodes in the image
                    foreach (var result in reader.ReadBarCodes())
                    {
                        Console.WriteLine($"Image '{Path.GetFileName(imageFile)}' - Detected Type: {result.CodeTypeName}, Text: {result.CodeText}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading image '{Path.GetFileName(imageFile)}': {ex.Message}");
            }
        }

        // Program completes here
    }
}