// Title: Adjust DPI for Accurate Barcode Region Detection
// Description: Demonstrates generating a Code128 barcode, adjusting image DPI, and recognizing the barcode with region details.
// Prompt: Adjust DPI settings when loading images to ensure accurate barcode region detection.
// Tags: barcode, code128, dpi, region detection, generation, recognition, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a barcode, adjusts image DPI, and reads the barcode region.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode image, sets its DPI, and reads barcode information.
    /// </summary>
    static void Main()
    {
        // Define the file path for the generated barcode image
        string imagePath = "sample.png";

        // ------------------------------------------------------------
        // Generate a simple Code128 barcode and save it to disk
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Optional: set generation resolution (DPI) if needed
            generator.Parameters.Resolution = 96;
            generator.Save(imagePath);
        }

        // Verify that the image file was created successfully
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Error: File not found - {imagePath}");
            return;
        }

        // ------------------------------------------------------------
        // Load the image, adjust its DPI, and perform barcode recognition
        // ------------------------------------------------------------
        using (var bitmap = new Bitmap(imagePath))
        {
            // Adjust DPI to 300x300 for more accurate region detection
            bitmap.SetResolution(300f, 300f);

            // Initialize the reader to detect all supported barcode types
            using (var reader = new BarCodeReader(bitmap, DecodeType.AllSupportedTypes))
            {
                // Iterate through all detected barcodes
                foreach (var result in reader.ReadBarCodes())
                {
                    // Retrieve the detected barcode region (rectangle)
                    var region = result.Region.Rectangle;

                    // Output barcode details and region coordinates
                    Console.WriteLine($"Detected Barcode:");
                    Console.WriteLine($"  Type: {result.CodeTypeName}");
                    Console.WriteLine($"  Text: {result.CodeText}");
                    Console.WriteLine($"  Region - X: {region.X}, Y: {region.Y}, Width: {region.Width}, Height: {region.Height}");
                }
            }
        }
    }
}