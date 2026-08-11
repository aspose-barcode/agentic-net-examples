// Title: Set Code 16K quiet‑zone coefficients and export as JPEG
// Description: Demonstrates how to configure left and right quiet‑zone coefficients for a Code 16K barcode using Aspose.BarCode and save the result as a JPEG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and barcode parameter settings such as quiet‑zone coefficients. Typical use cases include customizing barcode appearance for printing or digital display, where precise quiet‑zone control is required. Developers often need to adjust these settings to meet scanner specifications or layout constraints.
// Prompt: Set Code 16K left quiet zone coefficient 0.5 and right coefficient 0.7, export JPEG.
// Tags: code16k, quiet zone, jpeg, barcode generation, aspose.barcode, aspose.drawing

using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode;
using Aspose.Drawing.Imaging;

/// <summary>
/// Entry point for the Code16K quiet‑zone demonstration.
/// </summary>
class Program
{
    /// <summary>
    /// Configures quiet‑zone coefficients for a Code16K barcode and saves it as a JPEG file.
    /// </summary>
    static void Main()
    {
        // Desired quiet‑zone coefficients (the API expects integers, so non‑integer values are invalid)
        double leftCoefRequested = 0.5;
        double rightCoefRequested = 0.7;

        // Validate that the coefficients are whole numbers because the properties are of type int
        if (leftCoefRequested % 1 != 0 || rightCoefRequested % 1 != 0)
        {
            Console.WriteLine("Error: Code16K quiet‑zone coefficients must be integer values. " +
                              $"Requested values: left={leftCoefRequested}, right={rightCoefRequested}");
            return;
        }

        // Sample codetext for Code16K (any non‑empty string is acceptable for demonstration)
        string codeText = "1234567890";

        // Create the barcode generator for Code16K
        using (var generator = new BarcodeGenerator(EncodeTypes.Code16K, codeText))
        {
            // Apply integer quiet‑zone coefficients
            generator.Parameters.Barcode.Code16K.QuietZoneLeftCoef = (int)leftCoefRequested;
            generator.Parameters.Barcode.Code16K.QuietZoneRightCoef = (int)rightCoefRequested;

            // Optional: set a simple appearance (black bars on white background)
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Define output file path and save the barcode as a JPEG image
            string outputPath = "code16k.jpg";
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
            Console.WriteLine($"Barcode saved to {outputPath}");
        }
    }
}