// Title: Combine Multiple Target Regions for Barcode Recognition
// Description: Demonstrates how to define several target regions in a single image to focus barcode recognition on distinct areas.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, illustrating the use of BarCodeReader with multiple target regions. It showcases key API classes such as BarcodeGenerator, BarCodeReader, and DecodeType, helping developers who need to scan specific parts of an image containing several barcodes, a common requirement in inventory and document processing scenarios.
// Prompt: Combine multiple target regions to focus recognition on several distinct areas within a single image file.
// Tags: code128, qr, barcode recognition, target regions, aspose.barcode, c#

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates combining two barcodes into a single image and recognizing them using multiple target regions.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a combined image with Code128 and QR barcodes,
    /// defines target regions for each, and reads the barcodes using Aspose.BarCode.
    /// </summary>
    static void Main()
    {
        // Image dimensions for the combined bitmap.
        const int imageWidth = 600;
        const int imageHeight = 300;

        // Dimensions for each individual barcode.
        const int barcodeWidth = 250;
        const int barcodeHeight = 100;

        // Create an empty bitmap that will hold both barcodes.
        using (Bitmap combinedBitmap = new Bitmap(imageWidth, imageHeight))
        {
            using (Graphics graphics = Graphics.FromImage(combinedBitmap))
            {
                // Fill the background with white.
                graphics.Clear(Color.White);

                // ----- First barcode: Code128 -----
                using (var generator1 = new BarcodeGenerator(EncodeTypes.Code128, "CODE128-123"))
                {
                    generator1.Parameters.Barcode.XDimension.Point = 2f;
                    using (MemoryStream ms1 = new MemoryStream())
                    {
                        // Save the generated barcode to a memory stream.
                        generator1.Save(ms1, BarCodeImageFormat.Png);
                        ms1.Position = 0;
                        using (Bitmap barcodeBmp1 = new Bitmap(ms1))
                        {
                            // Draw the first barcode at the left side of the combined image.
                            Rectangle destRect1 = new Rectangle(20, 20, barcodeWidth, barcodeHeight);
                            graphics.DrawImage(barcodeBmp1, destRect1);
                        }
                    }
                }

                // ----- Second barcode: QR Code -----
                using (var generator2 = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
                {
                    generator2.Parameters.Barcode.XDimension.Point = 3f;
                    using (MemoryStream ms2 = new MemoryStream())
                    {
                        // Save the generated QR code to a memory stream.
                        generator2.Save(ms2, BarCodeImageFormat.Png);
                        ms2.Position = 0;
                        using (Bitmap barcodeBmp2 = new Bitmap(ms2))
                        {
                            // Draw the second barcode at the right side of the combined image.
                            Rectangle destRect2 = new Rectangle(320, 20, barcodeWidth, barcodeHeight);
                            graphics.DrawImage(barcodeBmp2, destRect2);
                        }
                    }
                }
            }

            // Save the combined image for visual verification (optional).
            const string combinedImagePath = "combined.png";
            combinedBitmap.Save(combinedImagePath, ImageFormat.Png);
            Console.WriteLine($"Combined image saved to '{combinedImagePath}'.");

            // Define target regions that correspond to the locations of the two barcodes.
            Rectangle[] targetRegions = new Rectangle[]
            {
                new Rectangle(20, 20, barcodeWidth, barcodeHeight),   // Region for Code128
                new Rectangle(320, 20, barcodeWidth, barcodeHeight)   // Region for QR
            };

            // Use BarCodeReader with the specified regions to focus recognition.
            using (var reader = new BarCodeReader(combinedBitmap, targetRegions, DecodeType.AllSupportedTypes))
            {
                // Iterate over detected barcodes within the defined regions.
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                    Console.WriteLine($"Code Text   : {result.CodeText}");
                    // Output the region rectangle for each detected barcode.
                    var rect = result.Region.Rectangle;
                    Console.WriteLine($"Region      : X={rect.X}, Y={rect.Y}, Width={rect.Width}, Height={rect.Height}");
                    Console.WriteLine($"Angle       : {result.Region.Angle}");
                    Console.WriteLine(new string('-', 40));
                }
            }
        }
    }
}