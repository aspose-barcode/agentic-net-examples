// Title: Validate DotCode Barcode Quiet Zone
// Description: Generates a DotCode barcode, saves it, and verifies that the quiet zone around the barcode meets the minimum required size for reliable scanning.
// Category-Description: This example demonstrates Aspose.BarCode generation and recognition for DotCode symbology. It uses BarcodeGenerator to create the barcode, configures XDimension and padding to ensure adequate quiet zones, and employs BarCodeReader to detect the barcode region. Developers working with barcode printing or scanning often need to validate quiet zone dimensions to guarantee scanner compatibility, making this a common task in barcode workflow automation.
/// Prompt: Validate that generated DotCode barcode meets minimum quiet zone requirements for scanner compatibility.
/// Tags: dotcode, quiet zone, barcode generation, barcode recognition, aspose.barcode, aspose.drawing, image processing

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates how to generate a DotCode barcode, save it as an image,
/// and validate that the quiet zone around the barcode satisfies the minimum
/// size requirements for scanner compatibility.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, saves it,
    /// and checks the quiet zone dimensions.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image.
        string outputPath = "dotcode.png";

        // ------------------------------------------------------------
        // Generate a DotCode barcode with explicit XDimension and padding.
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.DotCode, "1234567890"))
        {
            // Set XDimension to ensure sufficient bar size (2 points per module).
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Configure padding (quiet zone) of at least 10 points on each side.
            generator.Parameters.Barcode.Padding.Left.Point   = 10f;
            generator.Parameters.Barcode.Padding.Top.Point    = 10f;
            generator.Parameters.Barcode.Padding.Right.Point  = 10f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 10f;

            // Save the generated barcode image to the specified path.
            generator.Save(outputPath);
        }

        // ------------------------------------------------------------
        // Verify that the barcode image file was created successfully.
        // ------------------------------------------------------------
        if (!File.Exists(outputPath))
        {
            Console.WriteLine($"Error: Barcode image not found at '{outputPath}'.");
            return;
        }

        // ------------------------------------------------------------
        // Load the image to obtain its dimensions for quiet zone calculation.
        // ------------------------------------------------------------
        using (var image = (Bitmap)Image.FromFile(outputPath))
        {
            int imageWidth  = image.Width;
            int imageHeight = image.Height;

            // --------------------------------------------------------
            // Read the barcode from the saved image using BarCodeReader.
            // --------------------------------------------------------
            using (var reader = new BarCodeReader(outputPath, DecodeType.DotCode))
            {
                var results = reader.ReadBarCodes();

                if (results.Length == 0)
                {
                    Console.WriteLine("No barcode detected.");
                    return;
                }

                // Assume the first detected result corresponds to the generated barcode.
                var result = results[0];
                var bounds = result.Region.Rectangle; // Rectangle with X, Y, Width, Height

                // --------------------------------------------------------
                // Calculate quiet zone margins based on the detected region.
                // --------------------------------------------------------
                int leftMargin   = bounds.X;
                int topMargin    = bounds.Y;
                int rightMargin  = imageWidth  - (bounds.X + bounds.Width);
                int bottomMargin = imageHeight - (bounds.Y + bounds.Height);

                const int MinimumQuietZonePoints = 10;

                bool quietZoneValid =
                    leftMargin   >= MinimumQuietZonePoints &&
                    topMargin    >= MinimumQuietZonePoints &&
                    rightMargin  >= MinimumQuietZonePoints &&
                    bottomMargin >= MinimumQuietZonePoints;

                // --------------------------------------------------------
                // Output diagnostic information.
                // --------------------------------------------------------
                Console.WriteLine($"Barcode Type: {result.CodeTypeName}");
                Console.WriteLine($"Code Text: {result.CodeText}");
                Console.WriteLine($"Image Size: {imageWidth}x{imageHeight} points");
                Console.WriteLine($"Detected Region: X={bounds.X}, Y={bounds.Y}, Width={bounds.Width}, Height={bounds.Height}");
                Console.WriteLine($"Quiet Zone Margins (points) - Left: {leftMargin}, Top: {topMargin}, Right: {rightMargin}, Bottom: {bottomMargin}");
                Console.WriteLine($"Quiet zone meets minimum requirement of {MinimumQuietZonePoints} points on each side: {quietZoneValid}");
            }
        }
    }
}