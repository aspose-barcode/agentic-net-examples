// Title: Barcode generation, reading, and confidence warning demo
// Description: Demonstrates creating a Code128 barcode, reading it, and logging a warning when the recognition confidence is moderate, suggesting image enhancement.
// Prompt: Log a warning when BarCodeResult.Confidence equals Confidence.Moderate and suggest image enhancement to the user.
// Tags: barcode symbology, generation, recognition, confidence, warning, console

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates barcode generation, reading, and confidence handling.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode image, reads it, and logs a warning if confidence is moderate.
    /// </summary>
    static void Main()
    {
        // Define the path where the barcode image will be saved
        string imagePath = "barcode.png";

        // -------------------------------------------------
        // Generate a simple Code128 barcode and save it to file
        // -------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set a standard resolution to improve image quality
            generator.Parameters.Resolution = 300;
            generator.Save(imagePath);
        }

        // -------------------------------------------------
        // Verify that the barcode image file was created successfully
        // -------------------------------------------------
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Error: Barcode image not found at '{imagePath}'.");
            return;
        }

        // -------------------------------------------------
        // Read the barcode from the saved image file
        // -------------------------------------------------
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            bool anyResult = false;

            // Iterate through all detected barcodes
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                anyResult = true;

                // Output basic barcode information
                Console.WriteLine($"BarCode Type: {result.CodeTypeName}");
                Console.WriteLine($"BarCode CodeText: {result.CodeText}");
                Console.WriteLine($"BarCode Confidence: {result.Confidence}");

                // Log a warning if the confidence level is moderate
                if (result.Confidence == BarCodeConfidence.Moderate)
                {
                    Console.WriteLine("Warning: Moderate confidence detected. Consider enhancing the image (e.g., increase resolution, improve lighting) for better recognition.");
                }
            }

            // Inform the user if no barcodes were detected
            if (!anyResult)
            {
                Console.WriteLine("No barcodes were detected in the image.");
            }
        }

        // -------------------------------------------------
        // Clean up the generated image file (optional)
        // -------------------------------------------------
        try
        {
            File.Delete(imagePath);
        }
        catch
        {
            // Ignore any errors that occur during cleanup
        }
    }
}