// Title: Set Code 16K quiet zone coefficients and export as JPEG
// Description: Demonstrates configuring left and right quiet zone coefficients for a Code 16K barcode and saving the image as a JPEG using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category. It showcases the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to customize barcode parameters such as quiet zone coefficients, which are common requirements when integrating barcodes into printed materials or digital assets. Developers often need to adjust these settings to meet scanner specifications or layout constraints.
// Prompt: Set Code 16K left quiet zone coefficient 0.5 and right coefficient 0.7, export JPEG.
// Tags: code16k, quietzone, jpeg, barcode, generation, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates a Code 16K barcode with custom quiet zone coefficients and saves it as a JPEG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Configures quiet zone coefficients, validates them, generates the barcode, and writes the output file.
    /// </summary>
    static void Main()
    {
        // Desired quiet zone coefficients (as per task)
        float leftCoef = 0.5f;
        float rightCoef = 0.7f;

        // Validate against API constraints: QuietZoneLeftCoef >= 10, QuietZoneRightCoef >= 1
        if (leftCoef < 10f || rightCoef < 1f)
        {
            Console.WriteLine("Error: Code 16K quiet zone coefficients must be integers with Left >= 10 and Right >= 1.");
            Console.WriteLine($"Provided values: Left = {leftCoef}, Right = {rightCoef}");
            return;
        }

        // Convert to integer values (API expects int)
        int leftCoefInt = (int)leftCoef;
        int rightCoefInt = (int)rightCoef;

        // Prepare output directory and file path
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }
        string outputPath = Path.Combine(outputDir, "Code16K_QuietZone.jpg");

        // Generate the barcode using Aspose.BarCode
        using (var generator = new BarcodeGenerator(EncodeTypes.Code16K, "Aspose.Barcode"))
        {
            // Optional: set X-dimension (pixel size of the smallest bar)
            generator.Parameters.Barcode.XDimension.Pixels = 2;

            // Apply the quiet zone coefficients
            generator.Parameters.Barcode.Code16K.QuietZoneLeftCoef = leftCoefInt;
            generator.Parameters.Barcode.Code16K.QuietZoneRightCoef = rightCoefInt;

            // Save the generated barcode as a JPEG image
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}