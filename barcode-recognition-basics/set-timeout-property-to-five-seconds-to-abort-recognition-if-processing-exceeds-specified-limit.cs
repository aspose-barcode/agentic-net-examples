using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code128 barcode image and recognizing it using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode image if missing, then reads and displays detected barcodes.
    /// </summary>
    static void Main()
    {
        // Path to the barcode image file
        string imagePath = "sample.png";

        // If the barcode image does not exist, generate a simple Code128 barcode
        if (!File.Exists(imagePath))
        {
            // Initialize the barcode generator with Code128 symbology and sample data
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
            {
                // Save the generated barcode as a PNG file
                generator.Save(imagePath);
                Console.WriteLine($"Barcode image generated at: {Path.GetFullPath(imagePath)}");
            }
        }

        // Ensure the image file exists before attempting to read it
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Error: Barcode image file not found.");
            return;
        }

        // Create a BarCodeReader to detect all supported barcode types in the image
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Set a timeout of 5000 ms (5 seconds) for the recognition process
            reader.Timeout = 5000;

            // Iterate through all detected barcodes and output their type and text
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                Console.WriteLine($"Detected Text: {result.CodeText}");
            }
        }
    }
}