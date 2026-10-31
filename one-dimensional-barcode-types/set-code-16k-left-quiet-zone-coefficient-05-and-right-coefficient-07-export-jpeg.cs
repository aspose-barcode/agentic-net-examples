// Title: Set Code 16K quiet zone coefficients and export as JPEG
// Description: Demonstrates how to configure left and right quiet zone coefficients for a Code 16K barcode and save the result as a JPEG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of the BarcodeGenerator class and its Parameters.Barcode.Code16K properties to control quiet zone settings. Typical use cases include customizing barcode margins for scanner compatibility and visual layout requirements. Developers working with barcode rendering often need to adjust quiet zones, image dimensions, and output formats, and this snippet shows a concise pattern for doing so.
// Prompt: Set Code 16K left quiet zone coefficient 0.5 and right coefficient 0.7, export JPEG.
// Tags: code16k, quietzone, barcode, generation, jpeg, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a Code 16K barcode with custom quiet zone coefficients
/// and saves it as a JPEG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Desired quiet zone coefficients (as double for demonstration)
        double desiredLeftCoef = 0.5;
        double desiredRightCoef = 0.7;

        // Validate coefficients against API constraints (int >=10 for left, >=1 for right)
        if (desiredLeftCoef < 10 || desiredRightCoef < 1)
        {
            Console.WriteLine("Error: Code 16K quiet zone coefficients must be integers with Left >= 10 and Right >= 1.");
            Console.WriteLine($"Requested values: Left={desiredLeftCoef}, Right={desiredRightCoef}");
            return;
        }

        // Convert to integer values (API expects int)
        int leftCoef = (int)desiredLeftCoef;
        int rightCoef = (int)desiredRightCoef;

        // Prepare output directory and file path
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir);
        string outputPath = Path.Combine(outputDir, "Code16K_QuietZone.jpg");

        // Create and configure the barcode generator
        using (var generator = new BarcodeGenerator(EncodeTypes.Code16K, "Aspose.Barcode"))
        {
            // Set basic barcode appearance
            generator.Parameters.Barcode.XDimension.Pixels = 2;

            // Apply custom quiet zone coefficients
            generator.Parameters.Barcode.Code16K.QuietZoneLeftCoef = leftCoef;
            generator.Parameters.Barcode.Code16K.QuietZoneRightCoef = rightCoef;

            // Save the barcode as a JPEG image
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}