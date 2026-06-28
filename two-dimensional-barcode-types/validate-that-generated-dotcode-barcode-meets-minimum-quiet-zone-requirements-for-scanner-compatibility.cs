using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a DotCode barcode with a specified quiet zone,
/// saving it to an image file, and then validating the quiet zone by reading the barcode back.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a DotCode barcode, saves it, and validates its quiet zone.
    /// </summary>
    static void Main()
    {
        // Path for the generated barcode image
        string outputPath = "dotcode.png";

        // Minimum quiet zone required (in points)
        float minQuietZonePoints = 10f;

        // Create a DotCode barcode generator with the desired data
        BaseEncodeType encodeType = EncodeTypes.DotCode;
        using (var generator = new BarcodeGenerator(encodeType, "1234567890"))
        {
            // Apply the same quiet zone (padding) to all four sides
            generator.Parameters.Barcode.Padding.Left.Point   = minQuietZonePoints;
            generator.Parameters.Barcode.Padding.Top.Point    = minQuietZonePoints;
            generator.Parameters.Barcode.Padding.Right.Point  = minQuietZonePoints;
            generator.Parameters.Barcode.Padding.Bottom.Point = minQuietZonePoints;

            // Save the generated barcode image to the specified file
            generator.Save(outputPath);
        }

        // Ensure the image file was created successfully
        if (!File.Exists(outputPath))
        {
            Console.WriteLine($"Failed to generate barcode image at '{outputPath}'.");
            return;
        }

        // Load the saved image to obtain its dimensions (width & height in pixels)
        using (var image = Image.FromFile(outputPath))
        {
            int imageWidth  = image.Width;
            int imageHeight = image.Height;

            // Initialize a barcode reader for DotCode type on the saved image
            using (var reader = new BarCodeReader(outputPath, DecodeType.DotCode))
            {
                // Attempt to read all barcodes present in the image
                var results = reader.ReadBarCodes();

                // If no barcode is detected, report and exit
                if (results.Length == 0)
                {
                    Console.WriteLine("No DotCode barcode detected in the image.");
                    return;
                }

                // Assume the first detected barcode corresponds to the one we generated
                var result = results[0];
                var region = result.Region.Rectangle; // RectangleF representing barcode bounds

                // Compute quiet zones: distance from barcode edges to image edges
                float leftQuiet   = region.X;
                float topQuiet    = region.Y;
                float rightQuiet  = imageWidth  - (region.X + region.Width);
                float bottomQuiet = imageHeight - (region.Y + region.Height);

                // Determine the smallest quiet zone among all sides
                float smallestQuiet = Math.Min(
                    Math.Min(leftQuiet, rightQuiet),
                    Math.Min(topQuiet, bottomQuiet));

                // Convert required quiet zone from points to pixels (assuming 96 DPI)
                // 1 point = 1/72 inch; 96 DPI => 1 point = 96/72 = 1.3333 pixels
                float pointsToPixels = 96f / 72f;
                float requiredPixels = minQuietZonePoints * pointsToPixels;

                // Output diagnostic information
                Console.WriteLine($"Image size: {imageWidth}x{imageHeight} pixels");
                Console.WriteLine($"Detected barcode region: X={region.X}, Y={region.Y}, Width={region.Width}, Height={region.Height}");
                Console.WriteLine($"Quiet zones (pixels) - Left: {leftQuiet}, Top: {topQuiet}, Right: {rightQuiet}, Bottom: {bottomQuiet}");
                Console.WriteLine($"Smallest quiet zone: {smallestQuiet} pixels");

                // Validate whether the smallest quiet zone meets the required minimum
                if (smallestQuiet >= requiredPixels)
                {
                    Console.WriteLine("Validation passed: Quiet zone meets the minimum requirement.");
                }
                else
                {
                    Console.WriteLine($"Validation failed: Quiet zone is smaller than the required {minQuietZonePoints} points.");
                }
            }
        }
    }
}