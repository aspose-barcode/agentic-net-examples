using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code128 barcode, adjusting its DPI, and reading it back using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode image in memory, modifies its resolution, and reads the barcode data.
    /// </summary>
    static void Main()
    {
        // Create a barcode generator for Code128 with the sample text "1234567890"
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Optional: set a higher resolution for the generated image to improve quality
            generator.Parameters.Resolution = 200f;

            // Store the generated barcode image in a memory stream
            using (var ms = new MemoryStream())
            {
                // Save the barcode as a PNG image into the memory stream
                generator.Save(ms, BarCodeImageFormat.Png);
                // Reset stream position to the beginning for reading
                ms.Position = 0;

                // Load the PNG image from the memory stream into a bitmap object
                using (var bitmap = new Bitmap(ms))
                {
                    // Increase DPI to 300x300 to aid barcode detection accuracy
                    bitmap.SetResolution(300f, 300f);

                    // Initialize a barcode reader that can decode all supported barcode types
                    using (var reader = new BarCodeReader(bitmap, DecodeType.AllSupportedTypes))
                    {
                        // Iterate through all detected barcodes in the image
                        foreach (var result in reader.ReadBarCodes())
                        {
                            // Output the type and text of the detected barcode
                            Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                            Console.WriteLine($"Code Text: {result.CodeText}");

                            // Retrieve the bounding rectangle of the barcode region
                            var region = result.Region.Rectangle;
                            int x = (int)Math.Round((double)region.X);
                            int y = (int)Math.Round((double)region.Y);
                            int width = (int)Math.Round((double)region.Width);
                            int height = (int)Math.Round((double)region.Height);

                            // Output the region coordinates and size
                            Console.WriteLine($"Region: X={x}, Y={y}, Width={width}, Height={height}");
                            // Output the orientation angle of the barcode region
                            Console.WriteLine($"Orientation Angle: {result.Region.Angle}");
                        }
                    }
                }
            }
        }
    }
}