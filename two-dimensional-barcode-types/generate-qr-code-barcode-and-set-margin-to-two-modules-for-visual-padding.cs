// Title: Generate QR Code with Custom Margin Using Aspose.BarCode
// Description: Demonstrates how to create a QR Code barcode, set the module size, and apply a two‑module quiet‑zone margin for visual padding.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing how to configure barcode parameters such as XDimension and padding. It uses the BarcodeGenerator and related parameter classes to produce QR Code images, a common task for developers needing to embed machine‑readable data in applications, reports, or web pages. Typical use cases include generating QR codes for URLs, contact information, or product identifiers with precise visual layout control.
// Prompt: Generate QR Code barcode and set margin to two modules for visual padding.
// Tags: qr code, barcode generation, margin, padding, aspose.barcode, png

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a QR Code barcode, configures its module size,
/// applies a two‑module quiet‑zone margin, and saves the result as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the QR Code and writes the output file path to the console.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated PNG image.
        string outputPath = "qr.png";

        // Initialize a QR Code generator within a using block to ensure proper disposal.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Set the text that will be encoded into the QR Code.
            generator.CodeText = "Sample QR";

            // Define the size of a single QR module (XDimension) – 2 points per module.
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Calculate the padding value representing two modules.
            float twoModules = 2f * generator.Parameters.Barcode.XDimension.Point;

            // Apply the calculated padding to all sides of the barcode (quiet zone).
            generator.Parameters.Barcode.Padding.Left.Point   = twoModules;
            generator.Parameters.Barcode.Padding.Top.Point    = twoModules;
            generator.Parameters.Barcode.Padding.Right.Point  = twoModules;
            generator.Parameters.Barcode.Padding.Bottom.Point = twoModules;

            // Save the configured barcode image as a PNG file.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the QR Code image has been saved.
        Console.WriteLine($"QR Code saved to: {outputPath}");
    }
}