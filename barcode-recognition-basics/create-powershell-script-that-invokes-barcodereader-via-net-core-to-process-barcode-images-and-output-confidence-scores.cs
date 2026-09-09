// Title: Generate and Read a Code128 Barcode with Confidence Scores
// Description: This example creates a Code128 barcode image, saves it to a temporary directory, then reads the image using Aspose.BarCode's BarCodeReader to output the decoded text along with confidence and reading quality values.
// Category-Description: Demonstrates Aspose.BarCode generation and recognition APIs. The sample uses BarcodeGenerator to produce a barcode image and BarCodeReader to decode it, retrieving BarCodeResult details such as confidence scores. Typical scenarios include automated barcode validation, quality assessment, and integration into CI pipelines where confidence metrics guide downstream processing. Developers often need to generate test barcodes, read them programmatically, and evaluate reading reliability using these core classes.
// Prompt: Create a PowerShell script that invokes BarCodeReader via .NET Core to process barcode images and output confidence scores.
// Tags: barcode symbology, generation, recognition, confidence, aspnet, aspose.barcode, csharp, .net core

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates barcode generation, reading, and extraction of confidence metrics using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the sample. Generates a Code128 barcode, reads it, and prints confidence information.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the sample files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeSample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the full path for the generated barcode image
        string imagePath = Path.Combine(tempFolder, "sample.png");

        // Generate a Code128 barcode with the text "Aspose123" and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Aspose123"))
        {
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the image file was successfully created
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Set the decode type to all supported barcode symbologies
        BaseDecodeType decodeType = DecodeType.AllSupportedTypes;

        // Read the barcode image and retrieve decoding results
        using (var reader = new BarCodeReader(imagePath, decodeType))
        {
            BarCodeResult[] results = reader.ReadBarCodes();

            // If no barcodes were detected, inform the user
            if (results.Length == 0)
            {
                Console.WriteLine("No barcodes detected.");
            }
            else
            {
                // Iterate through each detected barcode and output its details
                foreach (BarCodeResult result in results)
                {
                    Console.WriteLine($"CodeText: {result.CodeText}");
                    Console.WriteLine($"CodeType: {result.CodeTypeName}");
                    Console.WriteLine($"Confidence: {result.Confidence}");
                    Console.WriteLine($"ReadingQuality: {result.ReadingQuality}");
                    Console.WriteLine();
                }
            }
        }

        // Attempt to clean up temporary files and directory
        try
        {
            File.Delete(imagePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored – cleanup failures should not affect program outcome
        }
    }
}