// Title: Read Australia Post Barcodes with CTable Settings from Network Share
// Description: Demonstrates reading Australia Post barcodes from image files located on a network share and configuring the reader to ignore ending filling patterns for CTable interpretation.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category. It showcases the use of BarCodeReader, DecodeType.AustraliaPost, and related settings such as CustomerInformationInterpretingType and IgnoreEndingFillingPatternsForCTable. Typical use cases include batch processing of postal barcodes stored on shared storage, where developers need to customize decoding behavior for specific symbology requirements.
// Prompt: Develop a service that reads Australia Post barcodes from a network share and applies IgnoreEndingFillingPatternsForCTable.
// Tags: australia post, barcode reading, ctable, ignoreendingfillingpatterns, aspose.barcode, image processing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates sample Australia Post barcodes, stores them in a folder
/// simulating a network share, and reads them using Aspose.BarCode with specific CTable settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcodes, then reads each image applying
    /// CustomerInformationInterpretingType.CTable and ignoring ending filling patterns.
    /// </summary>
    static void Main()
    {
        // Define the folder that simulates a network share.
        string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        EnsureFolderExists(folderPath);

        // Generate a few sample Australia Post barcodes.
        GenerateSampleBarcodes(folderPath, 3);

        // Process each barcode image in the folder.
        string[] imageFiles = Directory.GetFiles(folderPath, "*.png");
        foreach (string imagePath in imageFiles)
        {
            if (!File.Exists(imagePath))
            {
                Console.WriteLine($"File not found: {imagePath}");
                continue;
            }

            // Read the barcode using AustraliaPost settings.
            using (var reader = new BarCodeReader(imagePath, DecodeType.AustraliaPost))
            {
                // Apply CTable interpreting type and ignore ending filling patterns.
                reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = CustomerInformationInterpretingType.CTable;
                reader.BarcodeSettings.AustraliaPost.IgnoreEndingFillingPatternsForCTable = true;

                // Iterate through all detected barcodes in the image.
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"File: {Path.GetFileName(imagePath)}");
                    Console.WriteLine($"  BarCode Type: {result.CodeType}");
                    Console.WriteLine($"  BarCode CodeText: {result.CodeText}");
                }
            }
        }
    }

    /// <summary>
    /// Ensures that the specified folder exists, creating it if necessary.
    /// </summary>
    /// <param name="path">The folder path to verify.</param>
    static void EnsureFolderExists(string path)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }

    /// <summary>
    /// Generates a set of sample Australia Post barcode images and saves them as PNG files.
    /// </summary>
    /// <param name="folder">The folder where barcode images will be saved.</param>
    /// <param name="count">The number of barcodes to generate (up to the number of sample texts).</param>
    static void GenerateSampleBarcodes(string folder, int count)
    {
        // Sample code texts for Australia Post barcodes.
        string[] sampleTexts = new string[]
        {
            "5912345678AB",
            "5912345678CD",
            "5912345678EF"
        };

        for (int i = 0; i < count && i < sampleTexts.Length; i++)
        {
            string codeText = sampleTexts[i];
            string fileName = $"AustraliaPost_{i + 1}.png";
            string filePath = Path.Combine(folder, fileName);

            // Create a barcode generator for the Australia Post symbology.
            using (var generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, codeText))
            {
                // Use CTable interpreting type for the generated barcode.
                generator.Parameters.Barcode.AustralianPost.EncodingTable = CustomerInformationInterpretingType.CTable;

                // Save the barcode image as PNG.
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
        }
    }
}