// Title: Generate and Read Barcodes with Aspose.BarCode
// Description: This example creates a Code128 barcode image, saves it to a temporary location, then reads all supported barcodes from that image and logs each barcode's text and type.
// Category-Description: Demonstrates Aspose.BarCode generation and recognition APIs. It uses BarcodeGenerator to encode data, BarCodeReader to decode images, and BarCodeResult to access decoded information. Typical scenarios include creating barcodes for product labeling, shipping, and then validating or extracting data from scanned images. Developers often need quick samples that show end‑to‑end barcode creation and reading in C#.
// Prompt: Invoke ReadBarCodes and iterate over the BarCodeResult array to log each barcode's text and type.
// Tags: barcode generation, barcode recognition, readbarcodes, codetext, codetype, aspose.barcode, csharp, console

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a barcode image and reading it using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Code128 barcode, reads it back, and writes each detected barcode's text and type to the console.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for the demo files
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Define the full path for the barcode image
        string imagePath = Path.Combine(tempDir, "sample.png");

        // Generate a sample Code128 barcode image
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "123456789"))
        {
            // Set the X-dimension (module width) in pixels for better readability
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            // Save the generated barcode as a PNG file
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the image was created successfully
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Read all supported barcodes from the generated image
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            BarCodeResult[] results = reader.ReadBarCodes();

            // Iterate over each detected barcode and log its text and type
            foreach (BarCodeResult result in results)
            {
                Console.WriteLine($"CodeText: {result.CodeText}, CodeType: {result.CodeTypeName}");
            }
        }

        // Cleanup temporary files and directory
        try
        {
            File.Delete(imagePath);
            Directory.Delete(tempDir, true);
        }
        catch
        {
            // Ignore any errors that occur during cleanup
        }
    }
}