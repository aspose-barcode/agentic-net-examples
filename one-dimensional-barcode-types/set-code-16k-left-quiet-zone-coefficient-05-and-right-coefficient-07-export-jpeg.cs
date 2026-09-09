// Title: Set Code 16K quiet zone coefficients and export JPEG
// Description: Demonstrates how to configure left and right quiet zone coefficients for a Code 16K barcode using Aspose.BarCode and save the result as a JPEG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of the BarcodeGenerator class and its Parameters.Barcode.Code16K properties to adjust quiet zone settings. Typical use cases include customizing barcode margins for scanner compatibility or layout requirements. Developers often need to modify quiet zone coefficients before rendering barcodes to various image formats.
// Prompt: Set Code 16K left quiet zone coefficient 0.5 and right coefficient 0.7, export JPEG.
// Tags: code16k, quietzone, barcode, generation, jpeg, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates setting quiet zone coefficients for a Code 16K barcode and saving it as a JPEG.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the barcode with specified quiet zone coefficients and saves it.
    /// </summary>
    static void Main()
    {
        // Desired quiet zone coefficients (placeholders for invalid values)
        int leftCoef = 0;
        int rightCoef = 0;

        // The task requests 0.5 and 0.7, but the API requires integer values
        // with minimums of 10 (left) and 1 (right). Detect invalid request.
        double requestedLeft = 0.5;
        double requestedRight = 0.7;

        if (requestedLeft < 10 || requestedRight < 1)
        {
            Console.WriteLine("Error: Code 16K quiet zone coefficients must be integers with left >= 10 and right >= 1.");
            Console.WriteLine($"Requested values: left={requestedLeft}, right={requestedRight}");
            return;
        }

        // Convert to int because the API expects integer coefficients
        leftCoef = (int)requestedLeft;
        rightCoef = (int)requestedRight;

        // Prepare output directory and file path
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(outputDir);
        string outputPath = Path.Combine(outputDir, "Code16K_QuietZone.jpg");

        // Generate the barcode with the specified quiet zone coefficients
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code16K, "Aspose.BarCode"))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 2;
            generator.Parameters.Barcode.Code16K.QuietZoneLeftCoef = leftCoef;
            generator.Parameters.Barcode.Code16K.QuietZoneRightCoef = rightCoef;

            // Export the barcode as a JPEG image
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}