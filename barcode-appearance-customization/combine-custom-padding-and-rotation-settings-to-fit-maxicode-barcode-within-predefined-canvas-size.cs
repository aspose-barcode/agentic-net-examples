// Title: MaxiCode barcode with custom padding, rotation, and fixed canvas
// Description: Demonstrates how to generate a MaxiCode barcode, apply custom padding and rotation, and fit it within a predefined canvas size.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing the use of BarcodeGenerator, its Parameters, and AutoSizeMode to control layout. Developers often need to adjust padding, rotation, and image dimensions to meet design specifications for printed labels or digital media. The snippet illustrates typical steps for customizing barcode appearance while ensuring it fits a fixed-size canvas.
// Prompt: Combine custom padding and rotation settings to fit a MaxiCode barcode within a predefined canvas size.
// Tags: maxicode, padding, rotation, canvas, png, barcodegenerator, parameters, autosizemode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates a MaxiCode barcode with custom padding, rotation, and a fixed canvas size,
/// then saves it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode, configures its appearance,
    /// and writes the resulting image to a temporary file.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the system's temporary directory.
        string outputPath = Path.Combine(Path.GetTempPath(), "maxicode_custom.png");

        // Initialize a MaxiCode barcode generator with sample data.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.MaxiCode, "Sample MaxiCode"))
        {
            // Set the module size (X dimension) in pixels.
            generator.Parameters.Barcode.XDimension.Pixels = 12f;

            // Apply custom padding (left, top, right, bottom) measured in points.
            generator.Parameters.Barcode.Padding.Left.Point = 20f;
            generator.Parameters.Barcode.Padding.Top.Point = 20f;
            generator.Parameters.Barcode.Padding.Right.Point = 20f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 20f;

            // Rotate the barcode by 45 degrees.
            generator.Parameters.RotationAngle = 45f;

            // Define a fixed canvas size of 500x500 pixels.
            generator.Parameters.ImageWidth.Pixels = 500f;
            generator.Parameters.ImageHeight.Pixels = 500f;

            // Use Nearest auto-size mode to keep the barcode within the fixed canvas while respecting padding and rotation.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Nearest;

            // Save the generated barcode image as a PNG file.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"MaxiCode barcode saved to: {outputPath}");
    }
}