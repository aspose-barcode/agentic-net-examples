// Title: Capture Barcode Region and Convert to Pixel Coordinates
// Description: Demonstrates how to generate a QR barcode, read it, obtain the barcode region in points, and convert those coordinates to absolute pixel values based on image resolution.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes, BarCodeReader for detecting them, and Aspose.Drawing.Rectangle for handling region data. Typical scenarios include extracting barcode locations for cropping, overlaying graphics, or aligning UI elements. Developers working with barcode imaging often need to translate region coordinates from points to pixels to integrate with pixel‑based workflows.
/// Prompt: Capture barcode region as a rectangle object and convert coordinates to absolute pixel values.
/// Tags: barcode, qr, region, coordinates, pixel, generation, recognition, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a QR barcode, reads it back, and converts the detected region
/// from point units to absolute pixel values using the image's DPI.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the barcode generation, detection, and coordinate conversion.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder to store the generated image.
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeRegionDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the full path for the barcode image file.
        string barcodePath = Path.Combine(tempFolder, "barcode.png");

        // Generate a sample QR barcode and save it as a PNG file.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "123456789"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Load the saved image to retrieve its resolution (DPI).
        using (Bitmap bitmap = new Bitmap(barcodePath))
        {
            float dpiX = bitmap.HorizontalResolution;
            float dpiY = bitmap.VerticalResolution;

            // Initialize a barcode reader for QR codes.
            using (BarCodeReader reader = new BarCodeReader(barcodePath, DecodeType.QR))
            {
                // Iterate through all detected barcodes in the image.
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    // Obtain the region rectangle (coordinates are expressed in points).
                    Rectangle rect = result.Region.Rectangle;

                    // Convert the rectangle's point coordinates to absolute pixel values.
                    int pixelX = (int)Math.Round((double)rect.X * dpiX / 72.0);
                    int pixelY = (int)Math.Round((double)rect.Y * dpiY / 72.0);
                    int pixelWidth = (int)Math.Round((double)rect.Width * dpiX / 72.0);
                    int pixelHeight = (int)Math.Round((double)rect.Height * dpiY / 72.0);

                    // Output barcode details and both point and pixel region information.
                    Console.WriteLine($"Code Type: {result.CodeTypeName}");
                    Console.WriteLine($"Code Text: {result.CodeText}");
                    Console.WriteLine($"Region (points) - X:{rect.X}, Y:{rect.Y}, Width:{rect.Width}, Height:{rect.Height}");
                    Console.WriteLine($"Region (pixels) - X:{pixelX}, Y:{pixelY}, Width:{pixelWidth}, Height:{pixelHeight}");
                    Console.WriteLine($"Angle: {result.Region.Angle}");
                }
            }
        }

        // Clean up temporary files (optional). Errors during deletion are ignored.
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Suppress any exceptions thrown while attempting to delete the temporary folder.
        }
    }
}