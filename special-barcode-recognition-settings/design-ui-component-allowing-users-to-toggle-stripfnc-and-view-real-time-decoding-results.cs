// Title: Demonstrate StripFNC option in barcode decoding
// Description: Shows how to generate a Code128 barcode, then decode it twice—once with StripFNC disabled and once enabled—to illustrate the effect of stripping FNC characters.
// Category-Description: This example belongs to the Aspose.BarCode decoding category, focusing on barcode recognition settings. It demonstrates the use of BarCodeReader and its BarcodeSettings.StripFNC property, a common requirement when developers need to control whether Function (FNC) characters are retained in the decoded text. Typical use cases include processing legacy barcode data where FNC symbols may be present and need to be ignored for accurate data extraction.
// Prompt: Design a UI component allowing users to toggle StripFNC and view real‑time decoding results.
// Tags: barcode, stripfnc, decoding, code128, aspose.barcode, barcodereader, barcodegeneration

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates the effect of the StripFNC setting when decoding a Code128 barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode image, decodes it with StripFNC false and true, and outputs results.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder to store the generated barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "StripFNCDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the full path for the barcode image file
        string imagePath = Path.Combine(tempFolder, "Code128FNC.png");

        // Generate a Code128 barcode (without explicit FNC symbols for simplicity)
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "Aspose"))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 2; // Set barcode module size
            generator.Save(imagePath, BarCodeImageFormat.Png); // Save as PNG
        }

        // Verify that the image was created successfully
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // -------------------------------------------------
        // Decode the barcode with StripFNC set to false
        // -------------------------------------------------
        Console.WriteLine("Read with StripFNC = false");
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            reader.BarcodeSettings.StripFNC = false; // Preserve FNC characters
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"CodeType: {result.CodeTypeName}");
                Console.WriteLine($"CodeText: {result.CodeText}");
                Console.WriteLine($"ReadingQuality: {result.ReadingQuality}");
            }
        }

        // -------------------------------------------------
        // Decode the same barcode with StripFNC set to true
        // -------------------------------------------------
        Console.WriteLine("Read with StripFNC = true");
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            reader.BarcodeSettings.StripFNC = true; // Strip FNC characters from the result
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"CodeType: {result.CodeTypeName}");
                Console.WriteLine($"CodeText: {result.CodeText}");
                Console.WriteLine($"ReadingQuality: {result.ReadingQuality}");
            }
        }

        // Clean up temporary files and folder
        try
        {
            File.Delete(imagePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignore any errors during cleanup
        }
    }
}