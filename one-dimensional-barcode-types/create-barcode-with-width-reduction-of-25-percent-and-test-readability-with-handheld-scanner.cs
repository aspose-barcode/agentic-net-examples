// Title: Generate Code128 barcode with 25% width reduction and verify readability
// Description: This example creates a Code128 barcode, reduces its bar width by 25 percent, saves it as a PNG image, and validates that it can be decoded using Aspose.BarCode's reader.
// Category-Description: Demonstrates Aspose.BarCode barcode generation and recognition. The example uses BarcodeGenerator to configure bar dimensions and BarCodeReader to decode the image. Typical scenarios include creating compact barcodes for limited space and ensuring they remain readable by handheld scanners. Developers often need to adjust XDimension and BarWidthReduction to meet printing constraints while preserving scan reliability.
// Prompt: Create a barcode with width reduction of 25 percent and test readability with a handheld scanner.
// Tags: code128, width-reduction, barcode-generation, barcode-recognition, png, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a Code128 barcode with a 25 percent bar width reduction
/// and verifying its readability using Aspose.BarCode's recognition API.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, saves it, reads it back,
    /// outputs decoding information, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare a unique temporary folder and define the output file path.
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "barcode.png");

        // --------------------------------------------------------------------
        // Generate a Code128 barcode with a 25% bar width reduction.
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Define the base bar width (XDimension) in pixels.
            generator.Parameters.Barcode.XDimension.Pixels = 10f;

            // Apply a 25% reduction to the bar width (2.5 pixels out of 10).
            generator.Parameters.Barcode.BarWidthReduction.Pixels = 2.5f;

            // Save the generated barcode as a PNG image.
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // --------------------------------------------------------------------
        // Verify that the generated barcode can be read by a handheld scanner.
        // --------------------------------------------------------------------
        if (File.Exists(barcodePath))
        {
            BaseDecodeType decodeType = DecodeType.Code128;
            using (var reader = new BarCodeReader(barcodePath, decodeType))
            {
                var results = reader.ReadBarCodes();
                if (results != null && results.Length > 0)
                {
                    foreach (var result in results)
                    {
                        Console.WriteLine($"Decoded Text: {result.CodeText}");
                        Console.WriteLine($"Symbology: {result.CodeTypeName}");
                        Console.WriteLine($"Reading Quality: {result.ReadingQuality}");
                    }
                }
                else
                {
                    Console.WriteLine("No barcode detected or unreadable.");
                }
            }
        }
        else
        {
            Console.WriteLine("Failed to generate barcode image.");
        }

        // --------------------------------------------------------------------
        // Clean up temporary files and directories.
        // --------------------------------------------------------------------
        try
        {
            if (File.Exists(barcodePath))
                File.Delete(barcodePath);
            if (Directory.Exists(tempFolder))
                Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program exit.
        }
    }
}