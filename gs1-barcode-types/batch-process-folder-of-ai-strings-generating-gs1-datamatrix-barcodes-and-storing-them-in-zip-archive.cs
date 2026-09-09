// Title: Batch generate GS1 DataMatrix barcodes from AI strings and archive them
// Description: Demonstrates how to read AI (Application Identifier) strings from text files, create GS1 DataMatrix barcodes using Aspose.BarCode, and package the resulting images into a ZIP file.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on GS1 DataMatrix symbology. It showcases the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes for bulk processing scenarios such as inventory labeling, where multiple AI strings are converted to barcodes and saved as image files. Developers often need to automate barcode creation and bundle outputs for distribution, making this pattern common in logistics and retail applications.
// Prompt: Batch process a folder of AI strings, generating GS1 DataMatrix barcodes and storing them in a ZIP archive.
// Tags: gs1 datamatrix, batch processing, zip archive, barcode generation, aspose.barcode, aspose.barcode.generation

using System;
using System.IO;
using System.IO.Compression;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates batch processing of AI strings to generate GS1 DataMatrix barcodes and archive them.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Reads AI strings from temporary text files, creates barcode images, and compresses them into a ZIP file.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for input AI strings
        string inputFolder = Path.Combine(Path.GetTempPath(), "GS1Input_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(inputFolder);

        // Sample AI strings (must include AI (01) with 14 digits)
        string[] sampleTexts = new string[]
        {
            "(01)00123456789012(21)ITEM001",
            "(01)12345678901231(21)ITEM002",
            "(01)00012345678905(21)ITEM003",
            "(01)98765432109876(21)ITEM004",
            "(01)55555555555555(21)ITEM005"
        };

        // Write each sample to a separate .txt file
        for (int i = 0; i < sampleTexts.Length; i++)
        {
            string filePath = Path.Combine(inputFolder, $"sample{i + 1}.txt");
            File.WriteAllText(filePath, sampleTexts[i]);
        }

        // Create a folder for generated barcode images
        string outputFolder = Path.Combine(Path.GetTempPath(), "GS1Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // List to hold paths of generated image files
        var generatedFiles = new System.Collections.Generic.List<string>();

        // Process each input file and generate a barcode image
        for (int i = 0; i < sampleTexts.Length; i++)
        {
            string txtFile = Path.Combine(inputFolder, $"sample{i + 1}.txt");
            if (!File.Exists(txtFile))
            {
                Console.WriteLine($"File not found: {txtFile}");
                continue;
            }

            string codeText = File.ReadAllText(txtFile).Trim();
            if (string.IsNullOrEmpty(codeText))
            {
                Console.WriteLine($"Empty code text in file: {txtFile}");
                continue;
            }

            string imagePath = Path.Combine(outputFolder, $"barcode{i + 1}.png");

            try
            {
                // Initialize the barcode generator for GS1 DataMatrix
                using (var generator = new BarcodeGenerator(EncodeTypes.GS1DataMatrix, codeText))
                {
                    // Optional parameters to control appearance and validation
                    generator.Parameters.Barcode.XDimension.Pixels = 8f;
                    generator.Parameters.Barcode.FilledBars = false;
                    generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = false;

                    // Save the generated barcode as a PNG image
                    generator.Save(imagePath, BarCodeImageFormat.Png);
                }

                generatedFiles.Add(imagePath);
                Console.WriteLine($"Generated barcode: {imagePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating barcode for file '{txtFile}': {ex.Message}");
            }
        }

        // Create a ZIP archive containing all generated barcode images
        string zipPath = Path.Combine(Path.GetTempPath(), "GS1Barcodes_" + Guid.NewGuid().ToString("N") + ".zip");
        try
        {
            using (var zip = ZipFile.Open(zipPath, ZipArchiveMode.Create))
            {
                foreach (var file in generatedFiles)
                {
                    if (File.Exists(file))
                    {
                        zip.CreateEntryFromFile(file, Path.GetFileName(file));
                    }
                }
            }

            Console.WriteLine($"ZIP archive created at: {zipPath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating ZIP archive: {ex.Message}");
        }
    }
}