using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a Code128 barcode with configurable dimensions and unit,
/// then outputs the PNG image as a Base64 string.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Parses optional command‑line arguments for width, height, and unit,
    /// generates a barcode, and writes the image data as Base64 to the console.
    /// </summary>
    /// <param name="args">
    /// Optional arguments:
    /// <list type="bullet">
    ///   <item><description>args[0] – width (float, positive)</description></item>
    ///   <item><description>args[1] – height (float, positive)</description></item>
    ///   <item><description>args[2] – unit (e.g., pt, px, in, mm)</description></item>
    /// </list>
    /// </param>
    static void Main(string[] args)
    {
        // Default dimensions and unit
        float width = 300f;
        float height = 150f;
        string unit = "pt";

        // Override defaults with command‑line arguments when valid
        if (args.Length >= 1 && float.TryParse(args[0], out float w) && w > 0f)
            width = w;
        if (args.Length >= 2 && float.TryParse(args[1], out float h) && h > 0f)
            height = h;
        if (args.Length >= 3 && !string.IsNullOrWhiteSpace(args[2]))
            unit = args[2].Trim().ToLowerInvariant();

        // Guard against non‑positive dimensions
        if (width <= 0f) throw new ArgumentOutOfRangeException(nameof(width));
        if (height <= 0f) throw new ArgumentOutOfRangeException(nameof(height));

        // Initialize barcode generator for Code128
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            generator.CodeText = "Sample";

            // Set image size according to the specified unit
            switch (unit)
            {
                case "pt":
                case "point":
                case "points":
                    generator.Parameters.ImageWidth.Point = width;
                    generator.Parameters.ImageHeight.Point = height;
                    break;
                case "px":
                case "pixel":
                case "pixels":
                    generator.Parameters.ImageWidth.Pixels = width;
                    generator.Parameters.ImageHeight.Pixels = height;
                    break;
                case "in":
                case "inch":
                case "inches":
                    generator.Parameters.ImageWidth.Inches = width;
                    generator.Parameters.ImageHeight.Inches = height;
                    break;
                case "mm":
                case "millimeter":
                case "millimeters":
                    generator.Parameters.ImageWidth.Millimeters = width;
                    generator.Parameters.ImageHeight.Millimeters = height;
                    break;
                default:
                    // Fallback to points for unknown units
                    generator.Parameters.ImageWidth.Point = width;
                    generator.Parameters.ImageHeight.Point = height;
                    break;
            }

            // Render barcode to a memory stream in PNG format
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                byte[] imageBytes = ms.ToArray();

                // Convert image bytes to Base64 and write to console
                string base64 = Convert.ToBase64String(imageBytes);
                Console.WriteLine(base64);
            }
        }
    }
}