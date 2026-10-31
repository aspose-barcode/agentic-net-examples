// Title: Retrieve barcode pixel dimensions based on unit and resolution
// Description: Demonstrates how to generate a barcode with a specific X‑dimension unit and resolution, then obtain its actual pixel width and height.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure barcode parameters such as XDimension in millimeters, pixels, or points and set the image resolution (dpi). It shows the use of BarcodeGenerator, its Parameters, and the generated Bitmap to determine the final pixel size—common tasks when developers need precise sizing for printing or UI layout.
// Prompt: Implement method to retrieve actual pixel dimensions of generated barcode based on unit and resolution.
// Tags: barcode, dimensions, resolution, xdimension, datamatrix, aspnet, aspnetcore, aspose.barcode, image, pixel-size

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Provides functionality to generate a DataMatrix barcode and retrieve its pixel dimensions
/// based on the specified measurement unit and image resolution.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a barcode with the given unit (mm, px, pt) and resolution, then returns its width and height in pixels.
    /// </summary>
    /// <param name="unit">The measurement unit for XDimension (e.g., "mm", "px", "pt").</param>
    /// <param name="resolution">The image resolution in dots per inch (dpi).</param>
    /// <returns>A tuple containing the barcode image width and height in pixels.</returns>
    static (int Width, int Height) GetBarcodePixelDimensions(string unit, float resolution)
    {
        // Create a barcode generator for DataMatrix with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "Sample"))
        {
            // Set XDimension based on the requested unit
            if (unit.Equals("mm", StringComparison.OrdinalIgnoreCase))
                generator.Parameters.Barcode.XDimension.Millimeters = 2f;
            else if (unit.Equals("px", StringComparison.OrdinalIgnoreCase))
                generator.Parameters.Barcode.XDimension.Pixels = 3f;
            else if (unit.Equals("pt", StringComparison.OrdinalIgnoreCase))
                generator.Parameters.Barcode.XDimension.Point = 2f;
            else
                generator.Parameters.Barcode.XDimension.Millimeters = 2f; // fallback to millimeters

            // Apply the desired resolution (dpi) to the generated image
            generator.Parameters.Resolution = resolution;

            // Generate the barcode image as a bitmap
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Optionally save the image for visual verification
                string outputPath = Path.Combine(Path.GetTempPath(), $"barcode_{unit}_{resolution}.png");
                using (FileStream fs = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    bitmap.Save(fs, ImageFormat.Png);
                }

                // Return the actual pixel dimensions of the bitmap
                return (bitmap.Width, bitmap.Height);
            }
        }
    }

    /// <summary>
    /// Entry point of the program. Demonstrates retrieving barcode pixel dimensions for various units and resolutions.
    /// </summary>
    static void Main()
    {
        // Example using millimeters as the unit with 96 dpi resolution
        var resultMm = GetBarcodePixelDimensions("mm", 96f);
        Console.WriteLine($"Unit: mm, Resolution: 96 dpi => Width: {resultMm.Width}px, Height: {resultMm.Height}px");

        // Example using pixels as the unit with 300 dpi resolution
        var resultPx = GetBarcodePixelDimensions("px", 300f);
        Console.WriteLine($"Unit: px, Resolution: 300 dpi => Width: {resultPx.Width}px, Height: {resultPx.Height}px");

        // Example using points as the unit with 150 dpi resolution
        var resultPt = GetBarcodePixelDimensions("pt", 150f);
        Console.WriteLine($"Unit: pt, Resolution: 150 dpi => Width: {resultPt.Width}px, Height: {resultPt.Height}px");
    }
}