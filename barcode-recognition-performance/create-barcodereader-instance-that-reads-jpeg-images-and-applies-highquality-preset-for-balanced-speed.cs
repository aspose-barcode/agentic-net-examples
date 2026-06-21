using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates how to read all supported barcodes from a JPEG image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the path to the JPEG image that contains barcodes.
        string imagePath = "barcode.jpg";

        // Ensure the specified file exists before attempting to process it.
        if (!File.Exists(imagePath))
        {
            // Inform the user that the file could not be found and exit.
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Initialize a BarCodeReader for the image, configured to detect all supported symbologies.
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Set the quality preset to HighQuality to improve detection of low‑quality barcodes.
            reader.QualitySettings = QualitySettings.HighQuality;

            // Iterate over each detected barcode in the image.
            foreach (var result in reader.ReadBarCodes())
            {
                // Output the type of the barcode (e.g., QR, Code128).
                Console.WriteLine($"Type: {result.CodeTypeName}");
                // Output the decoded text/value of the barcode.
                Console.WriteLine($"CodeText: {result.CodeText}");
                // Output the confidence level of the detection (0.0 to 1.0).
                Console.WriteLine($"Confidence: {result.Confidence}");
                // Add an empty line for readability between results.
                Console.WriteLine();
            }
        }
    }
}