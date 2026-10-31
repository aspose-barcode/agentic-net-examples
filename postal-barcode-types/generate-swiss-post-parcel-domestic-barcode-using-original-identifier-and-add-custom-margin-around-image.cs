// Title: Generate Swiss Post Parcel Domestic Barcode with Custom Margins
// Description: This example creates a Swiss Post Parcel domestic barcode from an original identifier, applies a 10‑pixel margin on all sides, and saves it as a PNG image.
// Category-Description: Demonstrates Aspose.BarCode barcode generation for Swiss Post Parcel domestic symbology, covering setting module size, bar height, and custom padding. Typical use cases include creating postal barcodes for shipping labels and verifying them with the BarCodeReader. Developers working with Aspose.BarCode generation and recognition APIs often need to customize image layout and validate output.
// Prompt: Generate a Swiss Post Parcel domestic barcode using original identifier and add a custom margin around the image.
// Tags: swisspostparcel, barcode generation, custom margin, png, aspose.barcode, encode, decode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a Swiss Post Parcel domestic barcode with custom margins and verifying it.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the barcode image, saves it, and reads it back for verification.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare output directory and file path
        // --------------------------------------------------------------------
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }
        string outputPath = Path.Combine(outputDir, "SwissPostDomestic.png");

        // --------------------------------------------------------------------
        // Original Swiss Post Parcel domestic identifier to encode
        // --------------------------------------------------------------------
        string originalIdentifier = "98.34.123456.12345678";

        // --------------------------------------------------------------------
        // Generate the barcode with custom margins
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, originalIdentifier))
        {
            // Set module (X) dimension and bar height in pixels
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            generator.Parameters.Barcode.BarHeight.Pixels = 40f;

            // Apply a 10‑pixel padding on all sides of the barcode image
            generator.Parameters.Barcode.Padding.Left.Pixels = 10f;
            generator.Parameters.Barcode.Padding.Top.Pixels = 10f;
            generator.Parameters.Barcode.Padding.Right.Pixels = 10f;
            generator.Parameters.Barcode.Padding.Bottom.Pixels = 10f;

            // Save the generated barcode as a PNG file
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"Barcode saved to: {outputPath}");

        // --------------------------------------------------------------------
        // Read back the barcode to verify its type and data
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, originalIdentifier))
        {
            using (var reader = new BarCodeReader(generator.GenerateBarCodeImage(), DecodeType.SwissPostParcel))
            {
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Detected type: {result.CodeTypeName}, Data: {result.CodeText}");
                }
            }
        }
    }
}