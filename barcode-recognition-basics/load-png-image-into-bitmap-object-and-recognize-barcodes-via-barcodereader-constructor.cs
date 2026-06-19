using System;
using System.IO;
using Aspose.Drawing;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates how to read all supported barcode types from a PNG image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Loads an image file, scans it for barcodes, and prints the results to the console.
    /// </summary>
    static void Main()
    {
        // Define the path to the PNG image that contains barcodes.
        string imagePath = "barcode.png";

        // Ensure the specified file exists before attempting to load it.
        if (!File.Exists(imagePath))
        {
            // Inform the user that the file could not be found and exit.
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Load the image into an Aspose.Drawing.Bitmap within a using block to guarantee disposal.
        using (Bitmap bitmap = new Bitmap(imagePath))
        {
            // Initialize a BarCodeReader to detect all supported barcode types in the bitmap.
            using (BarCodeReader reader = new BarCodeReader(bitmap, DecodeType.AllSupportedTypes))
            {
                // Iterate over each detected barcode result.
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
}