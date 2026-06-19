using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a barcode image (if missing) and recognizing barcodes within it using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a sample barcode image if it does not exist, then reads and counts all detected barcodes.
    /// </summary>
    static void Main()
    {
        // Define the file path for the sample barcode image.
        string imagePath = "sample_barcode.png";

        // If the sample image does not exist, generate a new Code128 barcode image.
        if (!File.Exists(imagePath))
        {
            // Initialize a BarcodeGenerator with Code128 encoding and sample text.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
            {
                // Save the generated barcode image to the specified file path.
                generator.Save(imagePath);
            }
        }

        // Verify that the image file now exists before attempting to read it.
        if (!File.Exists(imagePath))
        {
            // Output an error message if the file is still missing and exit.
            Console.WriteLine($"Error: Image file '{imagePath}' not found.");
            return;
        }

        // Create a BarCodeReader to detect all supported barcode types in the image.
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Perform the barcode recognition process.
            reader.ReadBarCodes();

            // Retrieve the total number of barcodes detected in the image.
            int totalBarcodes = reader.FoundCount;

            // Output the count of found barcodes to the console.
            Console.WriteLine($"Found barcodes: {totalBarcodes}");
        }
    }
}