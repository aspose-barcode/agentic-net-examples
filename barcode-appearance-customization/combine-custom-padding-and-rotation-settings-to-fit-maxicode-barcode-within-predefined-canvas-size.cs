// Title: Generate a MaxiCode barcode with custom padding and rotation within a fixed canvas
// Description: Demonstrates how to create a MaxiCode (Mode 3) barcode, apply custom padding, rotate it, and fit it into a predefined canvas size.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcode types such as MaxiCode. It showcases the use of ComplexBarcodeGenerator, MaxiCodeCodetextMode3, and barcode parameters like ImageWidth, ImageHeight, Padding, RotationAngle, and XDimension. Developers often need to control barcode layout, size, and orientation to meet specific design or printing requirements.
// Prompt: Combine custom padding and rotation settings to fit a MaxiCode barcode within a predefined canvas size.
// Tags: maxicode, padding, rotation, png, complexbarcodegenerator, maxicodecodetextmode3

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a MaxiCode barcode with custom padding and rotation to fit a fixed canvas.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates and saves the barcode image.
    /// </summary>
    static void Main()
    {
        // Define the target canvas dimensions (in pixels)
        const float canvasWidth = 600f;
        const float canvasHeight = 400f;

        // Build the MaxiCode codetext (Mode 3) with required fields
        var maxiCodeText = new MaxiCodeCodetextMode3
        {
            PostalCode = "B1050",
            CountryCode = 56,
            ServiceCategory = 999,
            SecondMessage = new MaxiCodeStandardSecondMessage { Message = "Sample MaxiCode" }
        };

        // Initialize the generator for a complex barcode (MaxiCode)
        using (var generator = new ComplexBarcodeGenerator(maxiCodeText))
        {
            // -----------------------------------------------------------------
            // Canvas configuration
            // -----------------------------------------------------------------
            generator.Parameters.ImageWidth.Pixels = canvasWidth;   // Set canvas width
            generator.Parameters.ImageHeight.Pixels = canvasHeight; // Set canvas height
            generator.Parameters.AutoSizeMode = AutoSizeMode.Nearest; // Ensure barcode fits the canvas

            // -----------------------------------------------------------------
            // Padding configuration (left, top, right, bottom) in points
            // -----------------------------------------------------------------
            generator.Parameters.Barcode.Padding.Left.Point = 20f;
            generator.Parameters.Barcode.Padding.Top.Point = 20f;
            generator.Parameters.Barcode.Padding.Right.Point = 20f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 20f;

            // -----------------------------------------------------------------
            // Rotation configuration (allowed values: 0, 90, 180, 270 degrees)
            // -----------------------------------------------------------------
            generator.Parameters.RotationAngle = 90f;

            // -----------------------------------------------------------------
            // Module size adjustment to make the barcode fit within the padded area
            // -----------------------------------------------------------------
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // -----------------------------------------------------------------
            // Optional visual settings: resolution and colors
            // -----------------------------------------------------------------
            generator.Parameters.Resolution = 300f;
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // -----------------------------------------------------------------
            // Save the generated barcode image to a PNG file
            // -----------------------------------------------------------------
            const string outputPath = "maxicode.png";
            generator.Save(outputPath, BarCodeImageFormat.Png);
            Console.WriteLine($"MaxiCode barcode saved to: {Path.GetFullPath(outputPath)}");
        }
    }
}