// Title: Generate barcodes from XML configs and save as TIFF images
// Description: This example reads up to five XML barcode configuration files from a folder, creates barcodes using Aspose.BarCode, and writes them as TIFF files.
// Category-Description: Demonstrates batch processing of barcode generation using Aspose.BarCode's BarcodeGenerator class. Typical use cases include automating barcode creation from configuration files for inventory, shipping, or labeling systems. Developers often need to import settings from XML, generate images, and store them in a designated output directory.
// Prompt: Batch process a folder of XML configuration files, generating a barcode for each and saving as TIFF images.
// Tags: barcode generation, xml import, batch processing, tiff output, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates batch processing of XML barcode configuration files to generate TIFF images.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Processes up to five XML files in the specified folder, creates barcodes, and saves them as TIFF files.
    /// </summary>
    /// <param name="args">Optional first argument specifying the input folder path.</param>
    static void Main(string[] args)
    {
        // Determine input folder (first argument or default)
        string inputFolder = args.Length > 0 ? args[0] : "BarcodesConfig";

        // Verify that the input folder exists
        if (!Directory.Exists(inputFolder))
        {
            Console.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        // Prepare output folder inside the input folder
        string outputFolder = Path.Combine(inputFolder, "Output");
        Directory.CreateDirectory(outputFolder);

        // Retrieve all XML configuration files (limit to 5 for safety)
        string[] xmlFiles = Directory.GetFiles(inputFolder, "*.xml");
        int maxFiles = Math.Min(xmlFiles.Length, 5);

        // Process each XML file
        for (int i = 0; i < maxFiles; i++)
        {
            string xmlPath = xmlFiles[i];

            // Ensure the file still exists before processing
            if (!File.Exists(xmlPath))
            {
                Console.WriteLine($"File not found: {xmlPath}");
                continue;
            }

            // Load barcode settings from the XML file
            using (BarcodeGenerator generator = BarcodeGenerator.ImportFromXml(xmlPath))
            {
                // Verify that the generator was created successfully
                if (generator == null)
                {
                    Console.WriteLine($"Failed to import XML: {xmlPath}");
                    continue;
                }

                // Build the output TIFF file name based on the XML file name
                string fileNameWithoutExt = Path.GetFileNameWithoutExtension(xmlPath);
                string tiffPath = Path.Combine(outputFolder, fileNameWithoutExt + ".tiff");

                // Save the generated barcode image as a TIFF file
                generator.Save(tiffPath, BarCodeImageFormat.Tiff);
                Console.WriteLine($"Generated barcode: {tiffPath}");
            }
        }
    }
}