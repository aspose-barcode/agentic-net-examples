// Title: MaxiCode barcode with custom padding and rotation
// Description: Demonstrates generating a MaxiCode barcode, applying uniform padding and rotating it to fit within a 300x300 point canvas.
// Prompt: Combine custom padding and rotation settings to fit a MaxiCode barcode within a predefined canvas size.
// Tags: maxicode, padding, rotation, canvas, aspose.barcode, csharp

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a MaxiCode barcode with custom padding and rotation.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates a MaxiCode barcode, sets canvas size, padding, rotation, and saves the image.
    /// </summary>
    static void Main()
    {
        // Define sample MaxiCode data (Mode 2) with postal code, country code, service category, and a secondary message.
        var maxiCode = new MaxiCodeCodetextMode2
        {
            PostalCode = "524032140",
            CountryCode = 56,
            ServiceCategory = 999,
            SecondMessage = new MaxiCodeStandardSecondMessage { Message = "Sample MaxiCode" }
        };

        // Initialize a ComplexBarcodeGenerator using the MaxiCode codetext.
        using (var generator = new ComplexBarcodeGenerator(maxiCode))
        {
            // Set AutoSizeMode to Interpolation so ImageWidth/ImageHeight control the final size.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Define the target canvas size (300x300 points).
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 300f;

            // Apply uniform padding of 10 points on all sides.
            generator.Parameters.Barcode.Padding.Left.Point = 10f;
            generator.Parameters.Barcode.Padding.Top.Point = 10f;
            generator.Parameters.Barcode.Padding.Right.Point = 10f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 10f;

            // Rotate the barcode 90 degrees clockwise.
            generator.Parameters.RotationAngle = 90f;

            // Save the generated barcode image to a PNG file.
            generator.Save("MaxiCode.png");
        }

        // Inform the user that the barcode has been generated.
        Console.WriteLine("MaxiCode barcode generated successfully.");
    }
}