// Title: Barcode Generation with Custom Measurement Unit and Resolution
// Description: Demonstrates generating a QR barcode while allowing the caller to specify the measurement unit (pixels, millimeters, points, inches) and image resolution (dpi). The barcode is saved as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to create barcodes dynamically. Typical use cases include generating barcodes on-the-fly for web applications, reports, or printing workflows where developers need control over size units and image resolution.
// Prompt: Integrate barcode generation into ASP.NET MVC view, letting users select measurement unit and resolution before rendering.
// Tags: barcode symbology, generation, qr, measurement unit, resolution, aspnet mvc, aspose.barcode, png, csharp

using System;
using System.IO;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides a console entry point that generates a QR barcode with user‑specified measurement unit and resolution.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a QR barcode image using the selected measurement unit and DPI resolution.
    /// </summary>
    /// <param name="args">
    /// Optional command‑line arguments:
    /// <list type="bullet">
    /// <item><description>args[0] – measurement unit (Pixels, Millimeters, Points, Inches). Defaults to "Pixels".</description></item>
    /// <item><description>args[1] – integer DPI resolution. Defaults to 300.</description></item>
    /// </list>
    /// </param>
    static void Main(string[] args)
    {
        // ------------------------------------------------------------
        // Simulate user selections from command‑line arguments
        // ------------------------------------------------------------
        string unitChoice = args.Length > 0 ? args[0] : "Pixels";
        int resolutionDpi = 300;
        if (args.Length > 1 && int.TryParse(args[1], out int parsedRes))
        {
            resolutionDpi = parsedRes;
        }

        // ------------------------------------------------------------
        // Prepare output folder and file path
        // ------------------------------------------------------------
        string outputFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(outputFolder);
        string outputPath = Path.Combine(outputFolder, "barcode.png");

        try
        {
            // ------------------------------------------------------------
            // Create a BarcodeGenerator for QR code with sample data
            // ------------------------------------------------------------
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Sample"))
            {
                // ------------------------------------------------------------
                // Apply the selected measurement unit to the X‑dimension
                // ------------------------------------------------------------
                switch (unitChoice.ToLowerInvariant())
                {
                    case "pixels":
                        generator.Parameters.Barcode.XDimension.Pixels = 3f;
                        break;
                    case "millimeters":
                        generator.Parameters.Barcode.XDimension.Millimeters = 2f;
                        break;
                    case "points":
                        generator.Parameters.Barcode.XDimension.Point = 1f;
                        break;
                    case "inches":
                        generator.Parameters.Barcode.XDimension.Inches = 0.1f;
                        break;
                    default:
                        // Default to pixels if the unit is unknown
                        generator.Parameters.Barcode.XDimension.Pixels = 3f;
                        break;
                }

                // ------------------------------------------------------------
                // Set the image resolution (dots per inch)
                // ------------------------------------------------------------
                generator.Parameters.Resolution = (float)resolutionDpi;

                // ------------------------------------------------------------
                // Save the generated barcode as a PNG file
                // ------------------------------------------------------------
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }

            // ------------------------------------------------------------
            // Inform the user about the successful generation
            // ------------------------------------------------------------
            Console.WriteLine($"Barcode generated with unit '{unitChoice}' and resolution {resolutionDpi} dpi.");
            Console.WriteLine($"Saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // ------------------------------------------------------------
            // Report any errors that occurred during generation
            // ------------------------------------------------------------
            Console.WriteLine($"Error generating barcode: {ex.Message}");
        }
    }
}