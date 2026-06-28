using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a Code128 barcode with a width reduction,
/// saving it to a file, and then reading it back using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode, saves it, verifies the file, and reads the barcode.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image.
        string outputPath = "barcode.png";

        // Create a BarcodeGenerator for Code128 with the specified text.
        // The using statement ensures the generator is disposed after use.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set the bar width reduction to 25 points (percentage of the original width).
            // BarWidthReduction is expressed in points; 25f corresponds to 25%.
            generator.Parameters.Barcode.BarWidthReduction.Point = 25f;

            // Save the generated barcode image to the specified file.
            generator.Save(outputPath);
        }

        // Verify that the barcode image file was successfully created.
        if (!File.Exists(outputPath))
        {
            Console.WriteLine("Failed to generate the barcode image.");
            return;
        }

        // Open the saved barcode image with BarCodeReader to simulate a handheld scanner.
        // The using statement ensures the reader is disposed after reading.
        using (var reader = new BarCodeReader(outputPath, DecodeType.Code128))
        {
            // Read all barcodes found in the image.
            var results = reader.ReadBarCodes();

            // Iterate through each detected barcode and output its details.
            foreach (var result in results)
            {
                Console.WriteLine($"Detected CodeText: {result.CodeText}");
                Console.WriteLine($"Confidence: {result.Confidence}");
            }
        }
    }
}