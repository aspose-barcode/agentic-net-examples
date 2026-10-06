// Title: Check PDF417 IsLinked extended parameter
// Description: Demonstrates generating a PDF417 barcode with the IsLinked flag set and reading the barcode to verify the extended IsLinked property.
// Category-Description: This example belongs to the Aspose.BarCode PDF417 barcode manipulation category. It shows how to use BarcodeGenerator to set PDF417-specific parameters and BarCodeReader with DecodeType.Pdf417 to access extended PDF417 properties such as IsLinked. Developers working with PDF417 symbology often need to control segment linking for multi‑segment barcodes and validate those settings during scanning.
// Prompt: Access PDF417 extended parameters to check if the barcode is linked to another segment.
// Tags: pdf417, barcode, extended-parameters, islinked, generation, recognition, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating and reading a PDF417 barcode with the IsLinked extended parameter.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a PDF417 barcode with IsLinked=true, saves it, and reads back the IsLinked state.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary output directory and define the barcode image path
        string outputDir = Path.Combine(Path.GetTempPath(), "Pdf417LinkedDemo");
        Directory.CreateDirectory(outputDir);
        string barcodePath = Path.Combine(outputDir, "linkedPdf417.png");

        // Generate a PDF417 barcode and set the IsLinked flag to true
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Pdf417, "Sample linked barcode"))
        {
            // Set barcode dimensions (pixel size of X dimension)
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Enable linking of this PDF417 segment
            generator.Parameters.Barcode.Pdf417.IsLinked = true;

            // Save the generated barcode as a PNG image
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was successfully created
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Read the barcode from the saved image and output the extended IsLinked property
        using (BarCodeReader reader = new BarCodeReader(barcodePath, DecodeType.Pdf417))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"CodeType: {result.CodeTypeName}");
                Console.WriteLine($"CodeText: {result.CodeText}");
                Console.WriteLine($"IsLinked: {result.Extended.Pdf417.IsLinked}");
            }
        }
    }
}