// Title: Generate QR Code with Mask Pattern 3 and Save as BMP
// Description: Demonstrates creating a QR Code barcode using Aspose.BarCode, applying the default mask (pattern 3 is not directly selectable) and exporting the image to BMP format.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on QR Code creation. It showcases the use of the BarcodeGenerator class, EncodeTypes enumeration, and BarCodeImageFormat for producing bitmap images. Developers commonly use these APIs to embed QR codes in documents, applications, or web pages, requiring control over size, encoding, and output format.
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
    /// Entry point. Creates a QR Code with a mask pattern (automatically selected) and writes it to disk.
    /// </summary>
    static void Main()
    {
        // Define the output directory and ensure it exists
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir);

        // Build the full path for the BMP file
        string outputPath = Path.Combine(outputDir, "qr_mask3.bmp");

        // Initialize the QR Code generator with the desired text
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "Sample QR Code with mask pattern 3"))
        {
            // Optional: set the size of a single QR module (pixel dimension)
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Note: The Aspose.BarCode API does not expose direct mask pattern selection.
            // The encoder automatically chooses the optimal mask, which may include pattern 3.

            // Save the generated barcode as a BMP image
            generator.Save(outputPath, BarCodeImageFormat.Bmp);
        }

        // Inform the user where the file was saved
        Console.WriteLine($"QR Code saved to: {outputPath}");
    }
}