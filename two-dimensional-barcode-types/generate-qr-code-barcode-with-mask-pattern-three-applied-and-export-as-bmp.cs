// Title: Generate QR Code with Mask Pattern 3 and Save as BMP
// Description: Demonstrates creating a QR Code barcode using Aspose.BarCode and exporting it as a BMP image. The library automatically selects the optimal mask pattern.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on QR Code creation and image export. It showcases the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes—common tools for developers who need to embed QR codes in applications, generate printable graphics, or integrate barcode imaging into workflows.
// Prompt: Generate a QR Code barcode with mask pattern three applied and export as BMP.
// Tags: qr code, barcode generation, bmp output, aspose.barcode, mask pattern

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a QR Code barcode and saves it as a BMP image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the QR Code and writes the file path to the console.
    /// </summary>
    static void Main()
    {
        // Define the full path for the output BMP file.
        string outputPath = Path.Combine(Environment.CurrentDirectory, "qr_mask3.bmp");

        // Initialize the QR Code generator with sample text.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "Sample QR Code"))
        {
            // Note: The Aspose.BarCode API does not expose direct mask pattern selection.
            // The encoder automatically applies the optimal mask (including pattern three when appropriate).

            // Save the generated barcode as a BMP image.
            generator.Save(outputPath, BarCodeImageFormat.Bmp);
        }

        // Inform the user where the BMP file was saved.
        Console.WriteLine($"QR Code saved to: {outputPath}");
    }
}