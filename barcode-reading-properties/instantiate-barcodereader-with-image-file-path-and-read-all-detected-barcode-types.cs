using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates reading all supported barcode types from an image file using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Path to the image that contains barcodes.
        string imagePath = "sample.png";

        // Verify that the file exists before attempting to read.
        if (!File.Exists(imagePath))
        {
            // Inform the user and exit if the file cannot be found.
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Create a BarCodeReader that will attempt to decode all supported barcode types.
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Iterate through each detected barcode in the image.
            foreach (var result in reader.ReadBarCodes())
            {
                // Output the type of the barcode (e.g., QR, Code128).
                Console.WriteLine($"BarCode Type: {result.CodeTypeName}");
                // Output the decoded text/value of the barcode.
                Console.WriteLine($"BarCode CodeText: {result.CodeText}");
            }
        }
    }
}