// Title: Retrieve barcode confidence after allowing incorrect barcodes
// Description: Demonstrates how to generate a Code128 barcode, read it with the MaxQuality setting that permits incorrect barcodes, and obtain the detection confidence value.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, illustrating the use of BarCodeReader with QualitySettings to assess detection reliability. It showcases key API classes such as BarcodeGenerator, BarCodeReader, BarCodeResult, and QualitySettings, which developers commonly use for generating barcodes, customizing reading parameters, and evaluating confidence scores in scanning applications.
// Prompt: Retrieve BarCodeResult.Confidence after allowing incorrect barcodes to assess detection reliability.
// Tags: barcode, code128, confidence, allowincorrectbarcodes, qualitysettings, generation, recognition

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a Code128 barcode, reads it with settings that allow incorrect barcodes,
/// and outputs the detection confidence and reading quality.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, verifies its creation, reads it with MaxQuality settings,
    /// and prints the barcode text, confidence, and reading quality.
    /// </summary>
    static void Main()
    {
        // Path where the generated barcode image will be saved
        string barcodePath = "barcode.png";

        // Generate a simple Code128 barcode and save it to the specified file
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "12345"))
        {
            generator.Save(barcodePath);
        }

        // Verify that the barcode image was successfully created
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine($"Error: Barcode file '{barcodePath}' was not found.");
            return;
        }

        // Initialize a barcode reader for the generated image, targeting Code128 symbology
        using (BarCodeReader reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            // Apply the MaxQuality preset, which enables AllowIncorrectBarcodes among other high‑quality settings
            reader.QualitySettings = QualitySettings.MaxQuality;

            // Iterate over all detected barcodes (expected to be a single entry)
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // Output the decoded text, confidence level, and reading quality for each result
                Console.WriteLine($"Detected CodeText: {result.CodeText}");
                Console.WriteLine($"Confidence Level: {result.Confidence}");
                Console.WriteLine($"Reading Quality: {result.ReadingQuality}");
            }
        }
    }
}