using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode detection in an image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Scans the specified image (or a default image) for barcodes and prints details to the console.
    /// </summary>
    /// <param name="args">Command‑line arguments; the first argument may specify the image path.</param>
    static void Main(string[] args)
    {
        // Determine the image file to process: use first argument if provided, otherwise default to "sample.jpg".
        string imagePath = args.Length > 0 ? args[0] : "sample.jpg";

        // Ensure the image file exists before attempting to read it.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Initialize a BarCodeReader to scan the image for all supported barcode types.
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Perform barcode recognition and retrieve all results.
            BarCodeResult[] results = reader.ReadBarCodes();

            // If no barcodes were detected, inform the user and exit.
            if (results.Length == 0)
            {
                Console.WriteLine("No barcodes detected.");
                return;
            }

            // Iterate over each detected barcode and output its properties.
            foreach (var result in results)
            {
                Console.WriteLine($"BarCode Type   : {result.CodeTypeName}");
                Console.WriteLine($"BarCode Text   : {result.CodeText}");
                Console.WriteLine($"Confidence     : {result.Confidence}");
                Console.WriteLine($"ReadingQuality : {result.ReadingQuality}");

                // Extract the region rectangle that defines the barcode's location in the image.
                var rect = result.Region.Rectangle;
                int x = (int)Math.Round((double)rect.X);
                int y = (int)Math.Round((double)rect.Y);
                int width = (int)Math.Round((double)rect.Width);
                int height = (int)Math.Round((double)rect.Height);
                Console.WriteLine($"Region (px)    : X={x}, Y={y}, Width={width}, Height={height}");

                // Retrieve the orientation angle of the barcode.
                double angle = result.Region.Angle;
                Console.WriteLine($"Orientation    : {angle} degrees");
                Console.WriteLine();
            }
        }
    }
}