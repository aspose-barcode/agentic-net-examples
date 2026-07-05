// Title: Barcode Generation and Confidence Retrieval
// Description: Generates a Code128 barcode, saves it, then reads it allowing incorrect barcodes and outputs the detection confidence for each result.
// Prompt: Retrieve BarCodeResult.Confidence after allowing incorrect barcodes to assess detection reliability.
// Tags: code128, generation, recognition, confidence, allowincorrectbarcodes, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates creating a barcode, saving it to an image file, and then reading it back
/// while allowing detection of incorrect barcodes. The confidence level of each detection
/// is displayed to assess reliability.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, verifies its creation,
    /// reads it with relaxed validation, and prints the code text and confidence.
    /// </summary>
    static void Main()
    {
        // Define the path for the generated barcode image
        string imagePath = "barcode.png";

        // Create a simple Code128 barcode and save it to a file
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345"))
        {
            generator.Save(imagePath);
        }

        // Verify that the image file was created
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Initialize a barcode reader for the saved image, specifying Code128 symbology
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            // Enable detection of incorrect barcodes (e.g., damaged or with wrong checksum)
            reader.QualitySettings.AllowIncorrectBarcodes = true;

            // Perform recognition and retrieve all detected barcode results
            BarCodeResult[] results = reader.ReadBarCodes();

            // Output confidence and code text for each detected barcode
            foreach (var result in results)
            {
                Console.WriteLine($"CodeText: {result.CodeText}");
                Console.WriteLine($"Confidence: {result.Confidence}");
            }
        }
    }
}