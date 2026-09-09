// Title: Iterate over BarCodeResult collection and log details
// Description: Demonstrates reading a generated QR barcode image and logging each detected barcode's type, text, and region information.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, showcasing how to use BarCodeGenerator to create a barcode, BarCodeReader to detect barcodes, and BarCodeResult to access metadata such as code type, text, and region. Typical use cases include batch processing of scanned images, extracting barcode data for inventory or tracking systems, and debugging recognition results. Developers often need to iterate over multiple results to handle composite images containing several barcodes.
// Prompt: Iterate over BarCodeResult collection to log each barcode's type, text, and region.
// Tags: barcode symbology, barcode generation, barcode recognition, qrcode, result iteration, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Sample program that generates a QR code, reads it back, and logs each detected barcode's details.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a temporary QR barcode image, reads it, and prints barcode type, text, and region data.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the sample files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeSample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the full path for the generated barcode image
        string barcodePath = Path.Combine(tempFolder, "sample.png");

        // Generate a QR barcode with sample text and save it as PNG
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "SampleText"))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 4f; // Set module size for better readability
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the image file was created successfully before attempting to read it
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Initialize a reader that can decode all supported barcode types from the image
        using (BarCodeReader reader = new BarCodeReader(barcodePath, DecodeType.AllSupportedTypes))
        {
            // Read all barcodes found in the image
            BarCodeResult[] results = reader.ReadBarCodes();

            // Iterate over each detection result and output its details
            foreach (BarCodeResult result in results)
            {
                Console.WriteLine($"Code Type Name: {result.CodeTypeName}");
                Console.WriteLine($"Code Text: {result.CodeText}");

                // Extract the bounding rectangle of the barcode region
                var rect = result.Region.Rectangle;
                Console.WriteLine($"Region - X: {rect.X}, Y: {rect.Y}, Width: {rect.Width}, Height: {rect.Height}");
                Console.WriteLine($"Angle: {result.Region.Angle}");
                Console.WriteLine(new string('-', 40));
            }
        }

        // Attempt to clean up temporary files and folder; ignore any errors during cleanup
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Cleanup failures are non‑critical; they do not affect program logic
        }
    }
}