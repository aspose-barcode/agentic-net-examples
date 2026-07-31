// Title: Barcode detection with custom region of interest
// Description: Demonstrates limiting barcode recognition to a specific area of an image using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode image processing and barcode recognition category. It shows how to use BarCodeReader with a region of interest to improve detection performance and accuracy. Developers often need to restrict scanning to a portion of an image when multiple barcodes are present or when background noise is high. Key classes include BarCodeReader, DecodeType, and Rectangle.
// Prompt: Use custom region of interest to limit barcode detection to a specific area of an image.
// Tags: barcode, region of interest, detection, code128, aspose.barcode, image processing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a barcode image (if missing) and then
/// detects the barcode using a custom region of interest to limit the scanning area.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Path to the sample barcode image.
        string imagePath = "barcode.png";

        // Generate a barcode image if it does not already exist.
        if (!File.Exists(imagePath))
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
            {
                // Optional: set barcode foreground and background colors.
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;

                // Save the generated barcode as a PNG file.
                generator.Save(imagePath, BarCodeImageFormat.Png);
                Console.WriteLine($"Generated barcode image at: {Path.GetFullPath(imagePath)}");
            }
        }

        // Load the image into a Bitmap object for processing.
        using (var bitmap = new Bitmap(imagePath))
        {
            // Define a custom region of interest (top‑left quarter of the image).
            int roiWidth = bitmap.Width / 2;
            int roiHeight = bitmap.Height / 2;
            var region = new Rectangle(0, 0, roiWidth, roiHeight);

            // Initialize the barcode reader.
            using (var reader = new BarCodeReader())
            {
                // Restrict decoding to the Code128 symbology.
                reader.BarCodeReadType = DecodeType.Code128;

                // Assign the bitmap and the region of interest to the reader.
                reader.SetBarCodeImage(bitmap, new Rectangle[] { region });

                // Perform barcode recognition within the specified region.
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                    Console.WriteLine($"Detected Text: {result.CodeText}");

                    // Output the bounds of the region where the barcode was found.
                    var rect = result.Region.Rectangle;
                    Console.WriteLine($"Region - X:{rect.X}, Y:{rect.Y}, Width:{rect.Width}, Height:{rect.Height}");
                    Console.WriteLine($"Angle: {result.Region.Angle}");
                }
            }
        }
    }
}