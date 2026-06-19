using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a barcode, saving it to a memory stream,
/// and then recognizing it using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a Code128 barcode, reads it back, and outputs recognition details.
    /// </summary>
    static void Main()
    {
        // Create a barcode generator for Code128 with the sample text "Sample123"
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Set the image resolution to 300 DPI (optional configuration)
            generator.Parameters.Resolution = 300f;

            // Use a memory stream to hold the generated barcode image in PNG format
            using (var imageStream = new MemoryStream())
            {
                // Save the barcode image into the memory stream
                generator.Save(imageStream, BarCodeImageFormat.Png);
                // Reset stream position to the beginning for reading
                imageStream.Position = 0;

                // Load the PNG image into an Aspose.Drawing Bitmap
                using (var bitmap = new Bitmap(imageStream))
                {
                    // Initialize a barcode reader that can decode all supported barcode types
                    using (var reader = new BarCodeReader(bitmap, DecodeType.AllSupportedTypes))
                    {
                        // Start a stopwatch to measure recognition time
                        var stopwatch = Stopwatch.StartNew();

                        // Perform the barcode recognition; returns an array of results
                        var results = reader.ReadBarCodes();

                        // Stop the stopwatch after recognition completes
                        stopwatch.Stop();

                        // Output processing time and total number of barcodes detected
                        Console.WriteLine($"Processing Time: {stopwatch.ElapsedMilliseconds} ms");
                        Console.WriteLine($"Barcodes Detected: {reader.FoundCount}");

                        // Iterate through each recognition result and display detailed information
                        foreach (var result in results)
                        {
                            Console.WriteLine("----- Barcode Result -----");
                            Console.WriteLine($"Type: {result.CodeTypeName}");
                            Console.WriteLine($"Code Text: {result.CodeText}");
                            Console.WriteLine($"Confidence: {result.Confidence}");
                            Console.WriteLine($"Reading Quality: {result.ReadingQuality}");
                            Console.WriteLine($"Orientation Angle: {result.Region.Angle}");

                            // Extract the bounding rectangle of the detected barcode region
                            var rect = result.Region.Rectangle;
                            // Convert rectangle coordinates to integers for clearer output
                            int x = (int)Math.Round((double)rect.X);
                            int y = (int)Math.Round((double)rect.Y);
                            int width = (int)Math.Round((double)rect.Width);
                            int height = (int)Math.Round((double)rect.Height);
                            Console.WriteLine($"Region: X={x}, Y={y}, Width={width}, Height={height}");
                        }
                    }
                }
            }
        }
    }
}