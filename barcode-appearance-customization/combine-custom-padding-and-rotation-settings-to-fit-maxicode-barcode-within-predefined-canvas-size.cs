// Title: MaxiCode Barcode with Custom Padding and Rotation on Fixed Canvas
// Description: Demonstrates how to generate a MaxiCode barcode, apply custom padding and rotate it to fit within a predefined canvas size.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category, illustrating the use of ComplexBarcodeGenerator, MaxiCodeCodetextMode2, and image parameter settings. It shows typical tasks such as setting canvas dimensions, padding, rotation, and colors for MaxiCode symbols. Developers creating shipping labels or logistics solutions often need to fit MaxiCode barcodes into fixed-size graphics.
// Prompt: Combine custom padding and rotation settings to fit a MaxiCode barcode within a predefined canvas size.
// Tags: maxicode, padding, rotation, png, complexbarcode, generator, image-parameters

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Generates a MaxiCode barcode, applies custom padding and rotation, and saves it to a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Configures barcode parameters, generates the image, and writes the output path.
    /// </summary>
    static void Main()
    {
        // Define output file name and desired canvas size (points)
        string outputPath = "maxicode.png";
        float canvasWidth = 300f;
        float canvasHeight = 300f;

        // Prepare MaxiCode codetext using Mode 2 (includes postal code, country, service category, and a second message)
        var maxiCodeCodetext = new MaxiCodeCodetextMode2
        {
            PostalCode = "524032140",
            CountryCode = 56,
            ServiceCategory = 999,
            SecondMessage = new MaxiCodeStandardSecondMessage { Message = "Sample MaxiCode" }
        };

        // Initialize the complex barcode generator with the prepared codetext
        using (var generator = new ComplexBarcodeGenerator(maxiCodeCodetext))
        {
            // Set the target canvas dimensions
            generator.Parameters.ImageWidth.Point = canvasWidth;
            generator.Parameters.ImageHeight.Point = canvasHeight;

            // Use interpolation auto‑size mode so the specified width/height control the final image size
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Apply uniform padding of 10 points on all sides
            generator.Parameters.Barcode.Padding.Left.Point = 10f;
            generator.Parameters.Barcode.Padding.Top.Point = 10f;
            generator.Parameters.Barcode.Padding.Right.Point = 10f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 10f;

            // Rotate the barcode 90 degrees clockwise
            generator.Parameters.RotationAngle = 90f;

            // Optional visual settings: white background and black bars
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;

            // Save the generated barcode image as PNG
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the file was saved
        Console.WriteLine($"MaxiCode barcode saved to '{Path.GetFullPath(outputPath)}'.");
    }
}