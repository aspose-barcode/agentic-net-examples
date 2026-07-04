// Title: Barcode detection with custom region of interest
// Description: Demonstrates limiting barcode recognition to a specific area of an image using Aspose.BarCode.
// Prompt: Use custom region of interest to limit barcode detection to a specific area of an image.
// Tags: barcode, region of interest, detection, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a barcode (if needed) and reads it using a custom region of interest.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a sample barcode image if missing, then reads barcodes within the top‑left quarter of the image.
    /// </summary>
    static void Main()
    {
        // Path for the sample barcode image
        string imagePath = "sample_barcode.png";

        // Generate a barcode image if it does not exist
        if (!File.Exists(imagePath))
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
            {
                // Set a simple black bar color
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                // Save as PNG
                generator.Save(imagePath, BarCodeImageFormat.Png);
                Console.WriteLine($"Barcode image generated: {imagePath}");
            }
        }

        // Verify the image file exists before attempting recognition
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Error: Image file '{imagePath}' not found.");
            return;
        }

        // Load the image and define a custom region of interest (top‑left quarter)
        using (var bitmap = new Bitmap(imagePath))
        {
            // Define a rectangle covering the top‑left quarter of the image
            var roi = new Rectangle(0, 0, bitmap.Width / 2, bitmap.Height / 2);

            // Create a reader and set the image with the region of interest
            using (var reader = new BarCodeReader())
            {
                reader.SetBarCodeImage(bitmap, roi);

                // Read barcodes within the specified region
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                    Console.WriteLine($"Detected Text: {result.CodeText}");
                    var bounds = result.Region.Rectangle;
                    Console.WriteLine($"Region - X:{bounds.X}, Y:{bounds.Y}, Width:{bounds.Width}, Height:{bounds.Height}");
                }
            }
        }
    }
}