using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generation and recognition of a Han Xin barcode,
/// and validates the required quiet zone around the detected barcode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a Han Xin barcode, reads it back, and checks quiet zone dimensions.
    /// </summary>
    static void Main()
    {
        const string codeText = "1234567890";

        // Generate Han Xin barcode into a memory stream
        using (MemoryStream ms = new MemoryStream())
        {
            float requiredQuiet; // Holds the calculated quiet zone size (in points)

            // Create a barcode generator for Han Xin type with the specified text
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.HanXin, codeText))
            {
                // Set module size (XDimension) – 2 points per module
                generator.Parameters.Barcode.XDimension.Point = 2f;

                // Store required quiet zone for later validation (2 * XDimension)
                requiredQuiet = generator.Parameters.Barcode.XDimension.Point * 2f;

                // Use automatic version selection for the Han Xin barcode
                generator.Parameters.Barcode.HanXin.Version = HanXinVersion.Auto;

                // Set error correction level (example: L2)
                generator.Parameters.Barcode.HanXin.ErrorLevel = HanXinErrorLevel.L2;

                // Save the generated barcode image as PNG into the memory stream
                generator.Save(ms, BarCodeImageFormat.Png);
            }

            // Reset stream position to the beginning for reading
            ms.Position = 0;

            // Load the generated image as a Bitmap (required by BarCodeReader)
            using (Bitmap bitmap = new Bitmap(ms))
            {
                // Initialize a barcode reader for Han Xin type
                using (BarCodeReader reader = new BarCodeReader(bitmap, DecodeType.HanXin))
                {
                    // Read all detected barcodes from the image
                    var results = reader.ReadBarCodes();

                    // If no barcode is found, output a message and exit
                    if (results.Length == 0)
                    {
                        Console.WriteLine("No Han Xin barcode detected.");
                        return;
                    }

                    // Use the first detected barcode result
                    var result = results[0];
                    var region = result.Region.Rectangle; // Bounding rectangle of the barcode

                    // Calculate quiet zone sizes (in pixels) on each side of the barcode
                    int leftQuiet   = (int)Math.Round((double)region.X);
                    int topQuiet    = (int)Math.Round((double)region.Y);
                    int rightQuiet  = bitmap.Width  - (int)Math.Round((double)region.Right);
                    int bottomQuiet = bitmap.Height - (int)Math.Round((double)region.Bottom);

                    // Verify that each quiet zone meets or exceeds the required size
                    bool quietOk = leftQuiet   >= requiredQuiet &&
                                   topQuiet    >= requiredQuiet &&
                                   rightQuiet  >= requiredQuiet &&
                                   bottomQuiet >= requiredQuiet;

                    // Output diagnostic information
                    Console.WriteLine($"Image size: {bitmap.Width}x{bitmap.Height} pixels");
                    Console.WriteLine($"Detected barcode region: X={region.X}, Y={region.Y}, Width={region.Width}, Height={region.Height}");
                    Console.WriteLine($"Quiet zones (pixels) - Left: {leftQuiet}, Top: {topQuiet}, Right: {rightQuiet}, Bottom: {bottomQuiet}");
                    Console.WriteLine($"Required quiet zone (pixels): {requiredQuiet}");
                    Console.WriteLine(quietOk ? "Quiet zone validation passed." : "Quiet zone validation failed.");
                }
            }
        }
    }
}