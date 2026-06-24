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
    /// Generates a barcode image using the specified encoding type, text, measurement unit, resolution, and output path.
    /// </summary>
    /// <param name="type">The barcode symbology to use.</param>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <param name="unit">The measurement unit for dimensions (Point, Pixel, Inch, Millimeter).</param>
    /// <param name="resolution">Resolution in DPI for the generated image.</param>
    /// <param name="outputPath">Full file path where the barcode image will be saved.</param>
    static void GenerateBarcode(BaseEncodeType type, string codeText, string unit, float resolution, string outputPath)
    {
        // Ensure the output directory exists before attempting to save the file.
        string directory = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        // Create a barcode generator instance and configure common parameters.
        using (var generator = new BarcodeGenerator(type, codeText))
        {
            // Set the image resolution (dots per inch) for both width and height.
            generator.Parameters.Resolution = resolution;

            // Disable automatic sizing so we can apply explicit dimensions.
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;

            // Example dimension values; adjust as needed for your use case.
            float widthValue = 300f;
            float heightValue = 150f;
            float xDimValue = 2f;

            // Apply the chosen measurement unit to image width, height, and X dimension.
            SetUnit(generator.Parameters.ImageWidth, unit, widthValue);
            SetUnit(generator.Parameters.ImageHeight, unit, heightValue);
            SetUnit(generator.Parameters.Barcode.XDimension, unit, xDimValue);

            // Save the generated barcode as a PNG file.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }
    }

    /// <summary>
    /// Assigns a numeric value to a <see cref="Unit"/> based on the specified unit name.
    /// </summary>
    /// <param name="target">The Unit object to modify.</param>
    /// <param name="unit">The unit name (case-insensitive): point, pixel, inch, or millimeter.</param>
    /// <param name="value">The numeric value to assign.</param>
    static void SetUnit(Unit target, string unit, float value)
    {
        switch (unit.ToLowerInvariant())
        {
            case "point":
                target.Point = value;
                break;
            case "pixel":
                target.Pixels = value;
                break;
            case "inch":
                target.Inches = value;
                break;
            case "millimeter":
                target.Millimeters = value;
                break;
            default:
                throw new ArgumentException($"Unsupported unit: {unit}");
        }
    }

    /// <summary>
    /// Entry point of the console application. Demonstrates barcode generation and outputs the result.
    /// </summary>
    static void Main()
    {
        // NOTE: Full ASP.NET MVC integration cannot be demonstrated in this console app.
        // The core barcode generation logic is shown below.

        // Sample parameters for barcode generation.
        BaseEncodeType symbology = EncodeTypes.Code128; // Using Code128 as an example.
        string codeText = "Sample12345";
        string measurementUnit = "Point"; // Options: Point, Pixel, Inch, Millimeter.
        float resolutionDpi = 300f; // Desired resolution in DPI.
        string outputFile = Path.Combine(Path.GetTempPath(), "barcode.png");

        try
        {
            // Generate the barcode image with the specified settings.
            GenerateBarcode(symbology, codeText, measurementUnit, resolutionDpi, outputFile);
            Console.WriteLine($"Barcode generated and saved to: {outputFile}");

            // Read the saved image and output it as a Base64 string (useful for embedding in HTML).
            byte[] imageBytes = File.ReadAllBytes(outputFile);
            string base64 = Convert.ToBase64String(imageBytes);
            Console.WriteLine("Base64 PNG:");
            Console.WriteLine(base64);
        }
        catch (Exception ex)
        {
            // Output any errors that occur during generation.
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}