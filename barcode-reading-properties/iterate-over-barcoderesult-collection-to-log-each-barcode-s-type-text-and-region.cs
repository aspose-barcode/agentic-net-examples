// Title: Iterate over BarCodeResult collection and log details
// Description: Demonstrates reading a barcode image (generating one if missing) and logging each detected barcode's type, text, and region.
// Prompt: Iterate over BarCodeResult collection to log each barcode's type, text, and region.
// Tags: barcode symbology, read, console output, aspose.barcode, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a barcode image if needed,
/// reads all barcodes from the image, and logs their details to the console.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Path to the temporary barcode image file.
        string imagePath = "sample_barcode.png";

        // If the image does not exist, generate a simple Code128 barcode for demonstration.
        if (!File.Exists(imagePath))
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345"))
            {
                // Save the generated barcode image to the specified path.
                generator.Save(imagePath);
            }
        }

        // Verify that the image file exists before attempting to read it.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Error: File '{imagePath}' not found.");
            return;
        }

        // Create a BarCodeReader that attempts to decode all supported barcode types in the image.
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Iterate over all detected barcodes.
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // Log the barcode type (symbology name).
                Console.WriteLine($"BarCode Type: {result.CodeTypeName}");
                // Log the decoded text/value of the barcode.
                Console.WriteLine($"BarCode Text: {result.CodeText}");
                // Log the region (bounding rectangle) where the barcode was found.
                Console.WriteLine($"BarCode Region: {result.Region}");
                Console.WriteLine(); // Blank line for readability between entries.
            }
        }
    }
}