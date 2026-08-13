// Title: Apply custom background color to a MaxiCode barcode and verify decoding
// Description: Demonstrates setting a custom background color for a MaxiCode barcode using Aspose.BarCode, saving it as an image, and confirming that the barcode can still be decoded correctly.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator to customize visual appearance (background and bar colors) and BarCodeReader to decode the generated image. Typical use cases include branding barcodes with corporate colors while ensuring they remain machine‑readable. Developers often need to adjust visual parameters without breaking decoding, and this snippet illustrates that workflow.
// Prompt: Apply a custom background color to a MaxiCode barcode and verify that decoding remains successful.
// Tags: maxicode, background color, barcode generation, barcode recognition, aspose.barcode, c#

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Generates a MaxiCode barcode with a custom background color,
/// saves it to a PNG file, and then verifies that the barcode can be decoded successfully.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode, applies visual customizations,
    /// saves the image, and validates decoding.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image.
        string imagePath = "maxicode.png";

        // --------------------------------------------------------------------
        // Generate a MaxiCode barcode with custom colors.
        // --------------------------------------------------------------------
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.MaxiCode, "Sample MaxiCode"))
        {
            // Set a custom background color (light orange‑yellow).
            generator.Parameters.BackColor = Color.FromArgb(255, 255, 224, 128);

            // Optionally set the foreground (bar) color for better contrast.
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Save the customized barcode image to the specified file.
            generator.Save(imagePath);
        }

        // --------------------------------------------------------------------
        // Decode the saved barcode image to ensure the custom background does not affect readability.
        // --------------------------------------------------------------------
        BaseDecodeType decodeType = DecodeType.MaxiCode;
        using (BarCodeReader reader = new BarCodeReader(imagePath, decodeType))
        {
            // Use the highest quality preset to improve detection reliability.
            reader.QualitySettings = QualitySettings.MaxQuality;

            // Read all barcodes present in the image.
            BarCodeResult[] results = reader.ReadBarCodes();

            // Evaluate decoding results.
            bool success = false;
            foreach (BarCodeResult result in results)
            {
                if (!string.IsNullOrEmpty(result.CodeText))
                {
                    Console.WriteLine($"Decoded CodeText: {result.CodeText}");
                    success = true;
                }
            }

            Console.WriteLine(success ? "Decoding succeeded." : "Decoding failed.");
        }
    }
}