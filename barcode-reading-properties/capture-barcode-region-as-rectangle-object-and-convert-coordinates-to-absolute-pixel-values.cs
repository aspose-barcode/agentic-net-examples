// Title: Capture barcode region as rectangle and get absolute pixel coordinates
// Description: Demonstrates generating a Code128 barcode, reading it, and extracting the barcode region as a rectangle with pixel coordinates.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It shows how to use BarcodeGenerator to create a barcode image and BarCodeReader to detect the barcode and retrieve its Region information, including the bounding rectangle and orientation angle. Developers working with barcode scanning, image analysis, or layout calculations often need to obtain absolute pixel positions of detected barcodes for further processing such as overlaying graphics or extracting sub‑images.
// Prompt: Capture barcode region as a rectangle object and convert coordinates to absolute pixel values.
// Tags: barcode generation, barcode recognition, code128, region rectangle, pixel coordinates, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Code128 barcode, reads it back,
/// and outputs the barcode region as an absolute‑pixel rectangle.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a temporary barcode image,
    /// reads the barcode to obtain its region, prints the rectangle data,
    /// and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the demo files
        string tempDir = Path.Combine(Path.GetTempPath(), "BarcodeRegionDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Define the full path for the generated barcode image
        string barcodePath = Path.Combine(tempDir, "code128.png");

        // Generate a sample Code128 barcode and save it as a PNG image
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            generator.Parameters.Resolution = 300; // Set high resolution for better detection
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was created successfully
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Read the barcode image and obtain the region rectangle for each detected barcode
        using (var reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // The Region property provides the bounding rectangle and orientation
                var rect = result.Region.Rectangle;

                // Output the decoded text and rectangle details in absolute pixel units
                Console.WriteLine($"CodeText: {result.CodeText}");
                Console.WriteLine($"Region Rectangle (absolute pixels): X={rect.X}, Y={rect.Y}, Width={rect.Width}, Height={rect.Height}");
                Console.WriteLine($"Orientation Angle: {result.Region.Angle}");
            }
        }

        // Clean up temporary files and directory; ignore any errors during cleanup
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempDir, true);
        }
        catch
        {
            // Cleanup failures are non‑critical for this demo
        }
    }
}