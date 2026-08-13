// Title: Apply 10‑pixel margin to GS1 Code 128 barcode and save as JPEG
// Description: Demonstrates how to generate a GS1 Code 128 barcode, add a uniform 10‑pixel margin, and export it as a JPEG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes. Typical use cases include creating printable barcodes with custom padding for layout requirements. Developers often need to adjust margins to fit design constraints or scanning guidelines.
/// Prompt: Apply a 10‑pixel margin around a GS1 Code 128 barcode and save as JPEG.
/// Tags: gs1, code128, margin, jpeg, aspose.barcode, barcodegenerator

using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode;

/// <summary>
/// Demonstrates generating a GS1 Code 128 barcode with a 10‑pixel margin and saving it as a JPEG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the barcode, applies padding, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // GS1 Code 128 codetext: AI (01) with a 14‑digit GTIN
        string codeText = "(01)00123456789012";

        // Output JPEG file path
        string outputPath = "gs1code128_margin.jpg";

        // Initialize the barcode generator for GS1 Code 128
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1Code128, codeText))
        {
            // Apply a uniform 10‑pixel margin on all sides via padding
            generator.Parameters.Barcode.Padding.Left.Pixels = 10f;
            generator.Parameters.Barcode.Padding.Top.Pixels = 10f;
            generator.Parameters.Barcode.Padding.Right.Pixels = 10f;
            generator.Parameters.Barcode.Padding.Bottom.Pixels = 10f;

            // Save the generated barcode as a JPEG image
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        // Inform the user where the file was saved
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}