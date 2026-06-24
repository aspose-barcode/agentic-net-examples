using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates how to set the X‑dimension (module width) of a barcode
/// based on a desired physical size (millimetres) and DPI.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Calculates the pixel value for a 2 mm module width at 300 dpi,
    /// configures the barcode generator accordingly, and saves the image.
    /// </summary>
    static void Main()
    {
        // Desired module (X‑dimension) width in millimetres.
        const float moduleWidthMm = 2f;

        // Target resolution in dots per inch.
        const float dpi = 300f;

        // Convert the module width from millimetres to inches.
        // 1 inch = 25.4 mm.
        float inches = moduleWidthMm / 25.4f;

        // Calculate the X‑dimension in pixels using the DPI.
        float xDimensionPixels = inches * dpi;

        // Create a barcode generator for Code128.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            // Set the data to encode.
            generator.CodeText = "123456";

            // Apply the calculated X‑dimension (pixel value).
            generator.Parameters.Barcode.XDimension.Pixels = xDimensionPixels;

            // Ensure the generator's resolution matches the DPI used for the calculation.
            generator.Parameters.Resolution = dpi;

            // Save the generated barcode as a PNG file.
            generator.Save("barcode.png");
        }

        // Output the calculated pixel value for verification.
        Console.WriteLine($"XDimension set to {xDimensionPixels:F2} pixels (2 mm at {dpi} dpi).");
    }
}