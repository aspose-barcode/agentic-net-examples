// Title: Generate Postal Barcodes for Files in a Temporary Folder
// Description: Demonstrates creating a temporary input folder, generating a Postnet barcode for each file, and saving the images to an output folder.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on postal symbologies such as Postnet. It showcases the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to produce barcode images programmatically. Developers often need to automate barcode creation for mailing workflows, batch processing, or integration with document management systems.
// Prompt: Develop a Windows service that monitors a folder and generates postal barcodes for new files automatically.
// Tags: postal barcode, postnet, barcode generation, image output, aspose.barcode, csharp

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating Postnet barcodes for files placed in a temporary input folder and saving them as PNG images.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates temporary folders, processes a sample file, generates a barcode, and saves the result.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // 1. Set up a unique temporary input folder and write a sample file.
        // --------------------------------------------------------------------
        string inputFolder = Path.Combine(Path.GetTempPath(), "PostalInput_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(inputFolder);
        string sampleFilePath = Path.Combine(inputFolder, "sample.txt");
        File.WriteAllText(sampleFilePath, "Sample content for postal barcode generation.");

        // --------------------------------------------------------------------
        // 2. Set up a unique temporary output folder for the generated barcodes.
        // --------------------------------------------------------------------
        string outputFolder = Path.Combine(Path.GetTempPath(), "PostalBarcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // --------------------------------------------------------------------
        // 3. Prepare the list of files to process.
        //    (In a real service this list would be built by monitoring the folder.)
        // --------------------------------------------------------------------
        List<string> filesToProcess = new List<string> { sampleFilePath };

        // --------------------------------------------------------------------
        // 4. Iterate over each file and generate a Postnet barcode.
        // --------------------------------------------------------------------
        foreach (string filePath in filesToProcess)
        {
            // Verify that the file still exists before processing.
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                continue;
            }

            // Fixed numeric code suitable for a Postnet barcode (for demo purposes).
            string codeText = "1234567890";

            // Resolve the symbology type (Postnet) via EncodeTypes static field.
            BaseEncodeType encodeType = EncodeTypes.Postnet;

            // Create the barcode generator with the chosen symbology and data.
            using (var generator = new BarcodeGenerator(encodeType, codeText))
            {
                // Configure typical parameters for postal barcodes.
                generator.Parameters.Barcode.XDimension.Pixels = 2f;   // Width of the smallest bar element.
                generator.Parameters.Barcode.BarHeight.Pixels = 100f; // Height of the barcode.

                // Build the output file path (same name as input, but with .png extension).
                string outputFile = Path.Combine(outputFolder, Path.GetFileNameWithoutExtension(filePath) + ".png");

                // Save the generated barcode image in PNG format.
                generator.Save(outputFile, BarCodeImageFormat.Png);
                Console.WriteLine($"Generated barcode for '{filePath}' at '{outputFile}'.");
            }
        }

        // --------------------------------------------------------------------
        // 5. Optional cleanup – delete temporary folders after the demo.
        // --------------------------------------------------------------------
        // Directory.Delete(inputFolder, true);
        // Directory.Delete(outputFolder, true);
    }
}