// Title: Generate QR Code with Zero Margin and Save as JPEG
// Description: Demonstrates how to create a QR Code barcode using Aspose.BarCode, remove all padding, and export the image as a JPEG file.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category. It shows how to configure barcode parameters such as padding using the BarcodeGenerator class, a common task when developers need tightly‑cropped images for UI or printing. Typical use cases include generating QR codes for web links, product IDs, or authentication tokens where surrounding whitespace must be minimized.
// Prompt: Generate a QR Code barcode with margin set to zero and save as JPEG image.
// Tags: qr code, zero margin, jpeg, aspose.barcode, generation, barcode symbology

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a QR Code with no margin and saves it as a JPEG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Define the output file path in the current working directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "qr_zero_margin.jpg");

        // Initialize the QR Code generator with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello World"))
        {
            // Remove all padding (margin) by setting each side to zero points.
            generator.Parameters.Barcode.Padding.Left.Point = 0f;
            generator.Parameters.Barcode.Padding.Top.Point = 0f;
            generator.Parameters.Barcode.Padding.Right.Point = 0f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 0f;

            // Save the generated barcode as a JPEG image to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        // Inform the user where the QR Code image was saved.
        Console.WriteLine($"QR Code saved to: {outputPath}");
    }
}