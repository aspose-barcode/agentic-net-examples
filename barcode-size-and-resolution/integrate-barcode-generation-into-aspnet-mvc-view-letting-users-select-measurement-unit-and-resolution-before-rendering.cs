// Title: Barcode generation with selectable unit and resolution for ASP.NET MVC
// Description: Demonstrates creating a barcode image where the measurement unit and DPI resolution are configurable, suitable for rendering in an MVC view.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use BarcodeGenerator, set resolution, measurement units (points, pixels, millimeters), and image dimensions. Developers often need to generate barcodes dynamically in web applications, customize size, and serve the image from a view. The snippet shows typical API usage for such scenarios.
// Prompt: Integrate barcode generation into ASP.NET MVC view, letting users select measurement unit and resolution before rendering.
// Tags: barcode generation, aspnet mvc, measurement unit, resolution, code128, png, aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates core barcode generation logic that can be integrated into an ASP.NET MVC view.
/// The example shows how to configure measurement units, resolution, and image dimensions before saving the barcode image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the console application.
    /// In a real MVC scenario, the same logic would be invoked from a controller action and the image streamed to the view.
    /// </summary>
    static void Main()
    {
        // Sample input parameters (in a real MVC app these would be bound from user input)
        string codeText = "Sample123";
        BaseEncodeType encodeType = EncodeTypes.Code128; // 1D barcode symbology
        float resolutionDpi = 300f; // User‑selected DPI resolution

        // Measurement unit selection: Points (could also be Pixels or Millimeters)
        // All size‑related properties are set using the .Point member.

        // Initialize the barcode generator with the chosen symbology and data
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Apply the user‑selected resolution (DPI)
            generator.Parameters.Resolution = resolutionDpi;

            // Use interpolation mode to ensure the image size matches the specified dimensions exactly
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Define the output image size in points
            generator.Parameters.ImageWidth.Point = 300f;   // Width in points
            generator.Parameters.ImageHeight.Point = 150f;  // Height in points

            // Configure barcode-specific dimensions in points
            generator.Parameters.Barcode.XDimension.Point = 2f;      // Module (X) width
            generator.Parameters.Barcode.BarHeight.Point = 40f;     // Bar height for 1D barcode

            // Optional: set foreground (bars) and background colors
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Save the generated barcode image to a file (could be streamed instead in MVC)
            string outputPath = "barcode.png";
            generator.Save(outputPath, BarCodeImageFormat.Png);

            // Informational output for debugging or logging purposes
            Console.WriteLine($"Barcode generated and saved to '{outputPath}'.");
            Console.WriteLine($"Resolution: {generator.Parameters.Resolution} DPI");
            Console.WriteLine($"Image size: {generator.Parameters.ImageWidth.Point}pt x {generator.Parameters.ImageHeight.Point}pt");
        }
    }
}