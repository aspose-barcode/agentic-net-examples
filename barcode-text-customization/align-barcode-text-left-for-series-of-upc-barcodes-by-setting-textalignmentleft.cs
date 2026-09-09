// Title: Align UPC‑A barcode text to the left
// Description: Demonstrates how to generate a series of UPC‑A barcodes with the human‑readable text aligned to the left side of the image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and TextAlignment to control text placement. Typical use cases include customizing barcode appearance for retail labels or inventory systems where left‑aligned text is required. Developers often need to adjust code‑text parameters such as alignment, font, and color to meet branding or layout specifications.
// Prompt: Align barcode text left for a series of UPC‑A barcodes by setting TextAlignment.Left.
// Tags: upc-a, barcode generation, text alignment, left alignment, aspnet, aspose.barcode, png output

using System;
using System.IO;
using System.Collections.Generic;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates multiple UPC‑A barcode images with left‑aligned text and saves them as PNG files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the output directory, generates barcodes, and saves the images.
    /// </summary>
    static void Main()
    {
        // Prepare a unique temporary output directory
        string outputDir = Path.Combine(Path.GetTempPath(), "UPCSeries_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // List of sample UPC‑A codes (each must contain 12 digits)
        List<string> upcCodes = new List<string>
        {
            "012345678905",
            "123456789012",
            "036000291452",
            "042100005264",
            "070123456789"
        };

        // Resolve the EncodeTypes.UPCA value via reflection (ensures compatibility with different library versions)
        FieldInfo upcField = typeof(EncodeTypes).GetField("UPCA");
        if (upcField == null)
        {
            Console.WriteLine("EncodeTypes does not contain UPCA symbology.");
            return;
        }
        BaseEncodeType upcEncodeType = (BaseEncodeType)upcField.GetValue(null);

        int index = 1;
        // Iterate through each UPC‑A code, generate the barcode, and save it
        foreach (string code in upcCodes)
        {
            using (var generator = new BarcodeGenerator(upcEncodeType, code))
            {
                // Set the human‑readable text alignment to the left side of the barcode
                generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Left;

                // Build the output file path and save the image as PNG
                string filePath = Path.Combine(outputDir, $"UPC_{index}_Left.png");
                generator.Save(filePath, BarCodeImageFormat.Png);
                Console.WriteLine($"Saved: {filePath}");
            }
            index++;
        }

        Console.WriteLine("Barcode generation completed.");
    }
}