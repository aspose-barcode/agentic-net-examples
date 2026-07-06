// Title: Barcode generation with selectable measurement unit and resolution
// Description: Demonstrates how to generate a barcode image while allowing users to choose the measurement unit and DPI resolution before rendering.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and image parameter settings. Developers often need to customize barcode size and resolution for web applications, print media, or UI integration, and this snippet shows typical API usage for those scenarios.
// Prompt: Integrate barcode generation into ASP.NET MVC view, letting users select measurement unit and resolution before rendering.
// Tags: barcode, generation, measurement unit, resolution, aspnet mvc, code128, png

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates core barcode generation logic that can be used behind an ASP.NET MVC view.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that simulates user selections, configures the barcode generator, and saves the image.
    /// </summary>
    static void Main()
    {
        // Simulated user inputs: measurement unit and DPI resolution
        string selectedUnit = "Pixel"; // Options: "Point", "Pixel", "Inch", "Millimeter"
        float selectedResolution = 300f; // DPI

        // Barcode content and symbology type
        string codeText = "Sample123";
        BaseEncodeType encodeType = EncodeTypes.Code128;

        // Create the barcode generator with the chosen type and content
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Apply the selected resolution (dots per inch)
            generator.Parameters.Resolution = selectedResolution;

            // Set image size using the chosen measurement unit
            // Example size: 300 x 150 in the selected unit
            switch (selectedUnit)
            {
                case "Point":
                    generator.Parameters.ImageWidth.Point = 300f;
                    generator.Parameters.ImageHeight.Point = 150f;
                    break;
                case "Pixel":
                    generator.Parameters.ImageWidth.Pixels = 300f;
                    generator.Parameters.ImageHeight.Pixels = 150f;
                    break;
                case "Inch":
                    generator.Parameters.ImageWidth.Inches = 3f;
                    generator.Parameters.ImageHeight.Inches = 1.5f;
                    break;
                case "Millimeter":
                    generator.Parameters.ImageWidth.Millimeters = 76.2f; // 3 inches
                    generator.Parameters.ImageHeight.Millimeters = 38.1f; // 1.5 inches
                    break;
                default:
                    throw new ArgumentException($"Unsupported unit: {selectedUnit}");
            }

            // Optional: set auto-size mode to interpolation to respect ImageWidth/Height settings
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Save the generated barcode image to a file
            string outputPath = "barcode.png";
            generator.Save(outputPath);

            Console.WriteLine($"Barcode saved to '{outputPath}' using unit '{selectedUnit}' and resolution {selectedResolution} DPI.");
        }
    }
}