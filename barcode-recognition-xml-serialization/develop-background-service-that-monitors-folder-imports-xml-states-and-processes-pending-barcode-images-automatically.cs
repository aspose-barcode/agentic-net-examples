// Title: Barcode Generation, XML State Export, and Import Processing Example
// Description: Demonstrates creating Code128 barcode images, exporting their recognition state to XML, and later importing that state to read barcodes from the images.
// Category-Description: This example belongs to the Aspose.BarCode batch processing category, showcasing how to use BarcodeGenerator for image creation, BarCodeReader for recognition, and the ImportFromXml feature to persist and reuse recognition settings. Developers often need to generate barcodes, store recognition configurations, and later process large sets of images automatically, such as in background services or scheduled jobs.
// Prompt: Develop a background service that monitors a folder, imports XML states, and processes pending barcode images automatically.
// Tags: code128, generation, recognition, png, xml, import, export, barcodereader, barcodegenerator

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Sample program that generates barcode images, exports their recognition state to XML,
/// and then imports the state to read the barcodes from the images.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Create a dedicated temporary folder for generated files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        var imageFiles = new List<string>();
        var stateFiles = new List<string>();

        // ------------------------------------------------------------
        // Generate sample barcode images and export their recognition state to XML
        // ------------------------------------------------------------
        for (int i = 1; i <= 3; i++)
        {
            string codeText = $"CODE{i:D3}";
            string imagePath = Path.Combine(tempFolder, $"barcode{i}.png");
            string statePath = Path.Combine(tempFolder, $"state{i}.xml");

            // Generate barcode image using Code128 symbology
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                generator.Save(imagePath, BarCodeImageFormat.Png);
            }

            imageFiles.Add(imagePath);
            stateFiles.Add(statePath);

            // Read the generated barcode and export the reader state to XML
            using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
            {
                // Example setting: enable FNC stripping
                reader.BarcodeSettings.StripFNC = true;
                reader.ExportToXml(statePath);
            }
        }

        // ------------------------------------------------------------
        // Process pending barcode images using imported XML states
        // ------------------------------------------------------------
        for (int i = 0; i < stateFiles.Count; i++)
        {
            string xmlPath = stateFiles[i];
            string imgPath = imageFiles[i];

            // Verify that both the XML state file and the image exist
            if (!File.Exists(xmlPath) || !File.Exists(imgPath))
            {
                Console.WriteLine($"Missing file for index {i + 1}, skipping.");
                continue;
            }

            // Import the previously saved reader state from XML
            using (var importedReader = BarCodeReader.ImportFromXml(xmlPath))
            {
                // Associate the imported state with the corresponding barcode image
                importedReader.SetBarCodeImage(imgPath);
                BarCodeResult[] results = importedReader.ReadBarCodes();

                Console.WriteLine($"Processing '{Path.GetFileName(imgPath)}':");
                if (results.Length == 0)
                {
                    Console.WriteLine("  No barcode detected.");
                }
                else
                {
                    foreach (var result in results)
                    {
                        Console.WriteLine($"  Type: {result.CodeTypeName}, Text: {result.CodeText}");
                    }
                }
            }
        }

        // ------------------------------------------------------------
        // Cleanup temporary folder (optional)
        // ------------------------------------------------------------
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore any cleanup errors
        }
    }
}