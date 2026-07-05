// Title: Code128 Barcode Generation and Recognition with AllowIncorrectBarcodes
// Description: Demonstrates generating a Code128 barcode, saving it as PNG, and reading it back while allowing recognition of potentially unreadable barcodes for debugging.
// Prompt: Enable QualitySettings.AllowIncorrectBarcodes to capture potentially unreadable barcodes during debugging sessions.
// Tags: barcode, code128, generation, recognition, allowincorrectbarcodes, aspnet, c#

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that creates a Code128 barcode image and then reads it back,
/// enabling the AllowIncorrectBarcodes setting to capture barcodes that may be damaged or unreadable during debugging.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode, saves it, and then reads it while allowing incorrect barcodes.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // Generate a simple Code128 barcode and save it to a PNG file.
        // ------------------------------------------------------------
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Save the generated barcode image as "barcode.png".
            generator.Save("barcode.png");
        }

        // ------------------------------------------------------------
        // Read the generated barcode image and output its details.
        // ------------------------------------------------------------
        using (BarCodeReader reader = new BarCodeReader("barcode.png", DecodeType.Code128))
        {
            // Enable recognition of potentially incorrect or damaged barcodes.
            reader.QualitySettings.AllowIncorrectBarcodes = true;

            // Iterate through all detected barcodes in the image.
            foreach (var result in reader.ReadBarCodes())
            {
                // Output the type of the detected barcode.
                Console.WriteLine($"Detected barcode type: {result.CodeTypeName}");
                // Output the decoded text contained in the barcode.
                Console.WriteLine($"Decoded text: {result.CodeText}");
            }
        }
    }
}