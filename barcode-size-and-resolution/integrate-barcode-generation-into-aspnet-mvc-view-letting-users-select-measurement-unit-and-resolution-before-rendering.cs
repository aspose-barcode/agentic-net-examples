// Title: Barcode generation with selectable measurement units and resolution
// Description: Demonstrates creating Code128 barcodes while allowing the caller to choose measurement units (pixels, mm, inches, points) and image resolution (dpi). The images are saved as PNG files.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to configure XDimension units and image resolution using the BarcodeGenerator class. Typical use cases include dynamic barcode creation in web applications where users can specify size preferences. Developers often need to adjust measurement units and DPI to meet printing or display requirements.
// Prompt: Integrate barcode generation into ASP.NET MVC view, letting users select measurement unit and resolution before rendering.
// Tags: code128, generation, png, barcodegenerator, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates barcode generation with configurable measurement units and resolution.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates sample barcodes for each unit and resolution combination and saves them as PNG files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory to store generated barcode images.
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Sample barcode data and configuration options.
        string codeText = "ASPOSE123";
        string[] units = { "Pixels", "Millimeters", "Inches", "Points" };
        float[] resolutions = { 96f, 300f };

        int index = 1;
        // Iterate over each measurement unit.
        foreach (string unit in units)
        {
            // Iterate over each resolution (dpi) value.
            foreach (float res in resolutions)
            {
                // Build a descriptive file name for the generated image.
                string fileName = $"Barcode_{unit}_{res}dpi.png";
                string filePath = Path.Combine(outputDir, fileName);

                // Initialize the barcode generator with Code128 symbology and the sample text.
                using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
                {
                    // Configure the XDimension based on the selected measurement unit.
                    switch (unit)
                    {
                        case "Pixels":
                            generator.Parameters.Barcode.XDimension.Pixels = 3f;
                            break;
                        case "Millimeters":
                            generator.Parameters.Barcode.XDimension.Millimeters = 2f;
                            break;
                        case "Inches":
                            generator.Parameters.Barcode.XDimension.Inches = 0.05f;
                            break;
                        case "Points":
                            generator.Parameters.Barcode.XDimension.Point = 2f;
                            break;
                    }

                    // Set the image resolution (dots per inch).
                    generator.Parameters.Resolution = res;

                    // Save the generated barcode as a PNG file.
                    generator.Save(filePath, BarCodeImageFormat.Png);
                }

                // Output the result path to the console for verification.
                Console.WriteLine($"[{index}] Generated barcode: {filePath}");
                index++;
            }
        }

        // Summarize the location of all generated barcode images.
        Console.WriteLine($"All barcodes saved to: {outputDir}");
    }
}