// Title: Capture QR barcode region and convert to pixel coordinates
// Description: Demonstrates generating a QR barcode, reading its region, and converting the region's point coordinates to absolute pixel values.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, illustrating how to use BarcodeGenerator, BarCodeReader, and BarCodeResult to obtain a barcode's region rectangle and translate its point-based coordinates into pixel units based on image DPI. Developers working with barcode positioning, image processing, or UI overlay can use this pattern to accurately map barcode locations.
// Prompt: Capture barcode region as a rectangle object and convert coordinates to absolute pixel values.
// Tags: qr, barcode region, coordinate conversion, pixel, aspose.barcode, generation, recognition

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a QR barcode, reads its region, and converts the region's
/// point coordinates to absolute pixel values based on image resolution.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR code, reads its region, performs coordinate conversion,
    /// and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the demo files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeRegionDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the path for the generated barcode image
        string barcodePath = Path.Combine(tempFolder, "qr.png");

        // Generate a QR barcode and save it as a PNG image
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "HelloWorld"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was successfully created
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Read the barcode from the image and obtain its region rectangle
        using (var reader = new BarCodeReader(barcodePath, DecodeType.QR))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // Region.Rectangle provides coordinates in points (1/72 inch)
                var rect = result.Region.Rectangle;

                Console.WriteLine($"Barcode Type: {result.CodeTypeName}");
                Console.WriteLine($"Code Text: {result.CodeText}");
                Console.WriteLine($"Region (points): X={rect.X}, Y={rect.Y}, Width={rect.Width}, Height={rect.Height}");

                // Convert points to absolute pixel values using image resolution (default 96 DPI)
                const double dpi = 96.0;
                const double pointsPerInch = 72.0;
                double factor = dpi / pointsPerInch;

                int pixelX = (int)Math.Round(rect.X * factor);
                int pixelY = (int)Math.Round(rect.Y * factor);
                int pixelWidth = (int)Math.Round(rect.Width * factor);
                int pixelHeight = (int)Math.Round(rect.Height * factor);

                Console.WriteLine($"Region (pixels): X={pixelX}, Y={pixelY}, Width={pixelWidth}, Height={pixelHeight}");
                Console.WriteLine($"Orientation Angle: {result.Region.Angle} degrees");
            }
        }

        // Clean up temporary files and directory
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignore any errors during cleanup
        }
    }
}