// Title: Capture barcode region and convert to pixel coordinates
// Description: Demonstrates generating a Code128 barcode, reading it, retrieving the region rectangle in points, and converting those coordinates to absolute pixel values using the image DPI.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator to create barcodes, BarCodeReader to detect them, and BarCodeResult.Region to access geometric information. Developers often need to map barcode locations from point units to pixel units for image processing, UI overlay, or further analysis. The key API classes include BarcodeGenerator, BarCodeReader, BarCodeResult, and Region.
// Prompt: Capture barcode region as a rectangle object and convert coordinates to absolute pixel values.
// Tags: barcode, region, coordinate conversion, pixel, code128, aspose.barcode, generation, recognition, csharp

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a barcode, reads it back, and converts the detected region
/// from point units to absolute pixel coordinates.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Generate a sample Code128 barcode image.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Create a bitmap representation of the barcode.
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Save the bitmap to a memory stream in PNG format.
                using (var ms = new MemoryStream())
                {
                    bitmap.Save(ms, ImageFormat.Png);
                    ms.Position = 0; // Reset stream position for reading.

                    // Initialize a barcode reader for the image stream.
                    BaseDecodeType decodeType = DecodeType.Code128; // Specify the expected symbology.
                    using (var reader = new BarCodeReader(ms, decodeType))
                    {
                        // Read all barcodes present in the image.
                        BarCodeResult[] results = reader.ReadBarCodes();

                        foreach (BarCodeResult result in results)
                        {
                            // Retrieve the region rectangle (coordinates are expressed in points).
                            var regionRect = result.Region.Rectangle;

                            // Convert points to absolute pixel values using the image DPI.
                            // 1 point = 1/72 inch.
                            double dpiX = bitmap.HorizontalResolution;
                            double dpiY = bitmap.VerticalResolution;

                            int pixelX = (int)Math.Round(regionRect.X * dpiX / 72.0);
                            int pixelY = (int)Math.Round(regionRect.Y * dpiY / 72.0);
                            int pixelWidth = (int)Math.Round(regionRect.Width * dpiX / 72.0);
                            int pixelHeight = (int)Math.Round(regionRect.Height * dpiY / 72.0);

                            // Output barcode information and both point and pixel coordinates.
                            Console.WriteLine($"Barcode Text: {result.CodeText}");
                            Console.WriteLine($"Region (points): X={regionRect.X}, Y={regionRect.Y}, Width={regionRect.Width}, Height={regionRect.Height}");
                            Console.WriteLine($"Region (pixels): X={pixelX}, Y={pixelY}, Width={pixelWidth}, Height={pixelHeight}");
                            Console.WriteLine($"Angle: {result.Region.Angle}");
                            Console.WriteLine(new string('-', 40));
                        }

                        // Inform the user if no barcodes were detected.
                        if (results.Length == 0)
                        {
                            Console.WriteLine("No barcode detected in the image.");
                        }
                    }
                }
            }
        }
    }
}