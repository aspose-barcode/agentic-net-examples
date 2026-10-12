// Title: Barcode generation with selectable measurement unit and resolution
// Description: Demonstrates generating a Code128 barcode where the measurement unit for XDimension and image resolution are chosen by the user, useful for web scenarios such as ASP.NET MVC.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure barcode parameters like XDimension units and image resolution using the BarcodeGenerator class. Typical use cases include dynamic barcode creation in web applications, where developers need to adapt size and DPI for different output media. Developers often need to set measurement units (pixels, millimeters, inches, points) and resolution before saving the barcode image.
// Prompt: Integrate barcode generation into ASP.NET MVC view, letting users select measurement unit and resolution before rendering.
// Tags: barcode, code128, generation, measurement unit, resolution, aspnet mvc, aspose.barcode, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode generation with configurable measurement unit and resolution.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Simulates user selection of measurement unit and resolution, generates a barcode, and saves it to a temporary folder.
    /// </summary>
    static void Main()
    {
        // Simulated user input that would normally come from an ASP.NET MVC view.
        string codeText = "ASPOSE123";
        string selectedUnit = "Millimeters"; // Options: Pixels, Millimeters, Inches, Point
        float selectedResolution = 300f; // DPI

        // Prepare output folder in the system's temporary directory.
        string outputFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(outputFolder);
        string outputPath = Path.Combine(outputFolder, "barcode.png");

        // Generate the barcode with the selected settings.
        GenerateBarcode(codeText, selectedUnit, selectedResolution, outputPath);

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode generated at: {outputPath}");
    }

    /// <summary>
    /// Generates a Code128 barcode using the specified text, measurement unit, and resolution, then saves it to the given path.
    /// </summary>
    /// <param name="text">The data to encode in the barcode.</param>
    /// <param name="unit">The measurement unit for XDimension (Pixels, Millimeters, Inches, Point).</param>
    /// <param name="resolution">The image resolution in DPI.</param>
    /// <param name="outputPath">The file path where the barcode image will be saved.</param>
    static void GenerateBarcode(string text, string unit, float resolution, string outputPath)
    {
        // Initialize the barcode generator with Code128 symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, text))
        {
            // Set measurement unit for XDimension based on user selection.
            switch (unit?.Trim().ToLowerInvariant())
            {
                case "pixels":
                    generator.Parameters.Barcode.XDimension.Pixels = 3f;
                    break;
                case "inches":
                    generator.Parameters.Barcode.XDimension.Inches = 0.02f; // approx 2 hundredths of an inch
                    break;
                case "point":
                    generator.Parameters.Barcode.XDimension.Point = 2f;
                    break;
                case "millimeters":
                default:
                    generator.Parameters.Barcode.XDimension.Millimeters = 2f;
                    break;
            }

            // Apply the desired image resolution (dots per inch).
            generator.Parameters.Resolution = resolution;

            // Save the generated barcode as a PNG image.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }
    }
}