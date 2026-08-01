// Title: Barcode Generation, Recognition, and Confidence Warning
// Description: Demonstrates generating a Code128 barcode, reading it back, and logging a warning when recognition confidence is moderate.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader for decoding them. Developers often need to assess recognition confidence and provide guidance for image quality improvement, especially when confidence is moderate.
// Prompt: Log a warning when BarCodeResult.Confidence equals Confidence.Moderate and suggest image enhancement to the user.
// Tags: barcode, code128, generation, recognition, confidence, moderate, image enhancement, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Generates a Code128 barcode, saves it as an image, reads it back,
/// and logs a warning if the recognition confidence is moderate.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example.
    /// </summary>
    static void Main()
    {
        // Define the output path for the generated barcode image.
        string imagePath = "sample_barcode.png";

        // Create a barcode generator for Code128 with the data "12345".
        // Set a moderate resolution (300 DPI) to improve recognition confidence.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345"))
        {
            generator.Parameters.Resolution = 300; // DPI
            generator.Save(imagePath); // Save the barcode image to the specified path.
        }

        // Verify that the image file was created before attempting to read it.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Error: Barcode image not found at '{imagePath}'.");
            return;
        }

        // Initialize a barcode reader for Code128 and read the saved image.
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            // Iterate through all detected barcode results.
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Type: {result.CodeTypeName}");
                Console.WriteLine($"Text: {result.CodeText}");
                Console.WriteLine($"Confidence: {result.Confidence}");

                // If the confidence level is moderate, log a warning and suggest image enhancement.
                if (result.Confidence == BarCodeConfidence.Moderate)
                {
                    Console.WriteLine("Warning: Barcode confidence is moderate. Consider enhancing the image (e.g., increase resolution, improve lighting).");
                }
            }
        }
    }
}