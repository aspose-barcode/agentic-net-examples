// Title: Change Barcode Source Image Using SetBarCodeImage
// Description: Demonstrates how to switch the source image of a BarCodeReader after construction by calling SetBarCodeImage with a new file path.
// Category-Description: This example belongs to the Aspose.BarCode image manipulation category, illustrating how to work with the BarCodeReader class to read barcodes from different images without recreating the reader. Typical use cases include batch processing of scanned documents where the same reader instance can be reused for performance. Developers often need to change the source image dynamically, and SetBarCodeImage provides a convenient way to do so.
// Prompt: Change the source image after construction by calling SetBarCodeImage with a new file path.
// Tags: code128,qr,barcode generation,barcode reading,setbarcodeimage,aspose.barcode,output png

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates two barcode images, reads the first one,
/// then switches the reader's source image to the second barcode using SetBarCodeImage.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates barcode images, reads them, and demonstrates
    /// changing the source image of a BarCodeReader instance.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare output directory and file paths for the generated barcodes
        // --------------------------------------------------------------------
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        Directory.CreateDirectory(outputDir);
        string barcodePath1 = Path.Combine(outputDir, "barcode1.png");
        string barcodePath2 = Path.Combine(outputDir, "barcode2.png");

        // --------------------------------------------------------------------
        // Generate the first barcode (Code128) and save it as PNG
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "ABC123"))
        {
            generator.Save(barcodePath1, BarCodeImageFormat.Png);
        }

        // --------------------------------------------------------------------
        // Generate the second barcode (QR) and save it as PNG
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            generator.Save(barcodePath2, BarCodeImageFormat.Png);
        }

        // --------------------------------------------------------------------
        // Verify that both barcode image files were created successfully
        // --------------------------------------------------------------------
        if (!File.Exists(barcodePath1) || !File.Exists(barcodePath2))
        {
            Console.WriteLine("Failed to create barcode images.");
            return;
        }

        // --------------------------------------------------------------------
        // Create a BarCodeReader for the first image (Code128) and read its content
        // --------------------------------------------------------------------
        using (var reader = new BarCodeReader(barcodePath1, DecodeType.Code128))
        {
            Console.WriteLine("Reading from first image:");
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"  Type: {result.CodeTypeName}, Text: {result.CodeText}");
            }

            // ----------------------------------------------------------------
            // Change the source image of the existing reader to the second barcode
            // ----------------------------------------------------------------
            reader.SetBarCodeImage(barcodePath2);

            Console.WriteLine("Reading after SetBarCodeImage to second image:");
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"  Type: {result.CodeTypeName}, Text: {result.CodeText}");
            }
        }
    }
}