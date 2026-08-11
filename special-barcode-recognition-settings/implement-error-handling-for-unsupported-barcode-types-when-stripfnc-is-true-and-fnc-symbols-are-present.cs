// Title: StripFNC handling for unsupported barcode types in Aspose.BarCode
// Description: Demonstrates generating a GS1‑128 barcode, reading it with StripFNC enabled, and handling cases where the barcode type does not support stripping FNC symbols.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the BarcodeGenerator, BarCodeReader, and BarcodeSettings classes for creating GS1‑128 barcodes, configuring decoding options such as StripFNC, and implementing error handling for unsupported symbologies. Developers working with barcode preprocessing, data sanitization, or compliance with GS1 standards can use these patterns when integrating Aspose.BarCode into .NET applications.
// Prompt: Implement error handling for unsupported barcode types when StripFNC is true and FNC symbols are present.
// Tags: barcode, gs1-128, stripfnc, error-handling, generation, recognition, aspose.barcode, .net

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode generation, reading with StripFNC, and error handling for unsupported barcode types.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a GS1‑128 barcode, saves it, and attempts to read it with StripFNC enabled.
    /// </summary>
    static void Main()
    {
        // Prepare output directory and file path.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir);
        string imagePath = Path.Combine(outputDir, "barcode.png");

        // Generate a GS1‑128 barcode that contains an implicit FNC1 (via AI parentheses).
        try
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.GS1Code128, "(01)12345678901231"))
            {
                // Save the generated barcode image to disk.
                generator.Save(imagePath);
                Console.WriteLine($"Barcode image saved to: {imagePath}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during barcode generation: {ex.Message}");
            return;
        }

        // Attempt to read the barcode with StripFNC enabled.
        // DecodeType.Code128 (non‑GS1) is used to simulate an unsupported scenario.
        try
        {
            using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
            {
                // Enable stripping of FNC characters during decoding.
                reader.BarcodeSettings.StripFNC = true;

                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    // If StripFNC is true but control characters remain, treat this as an unsupported barcode type.
                    if (reader.BarcodeSettings.StripFNC && ContainsControlCharacters(result.CodeText))
                    {
                        throw new ArgumentException(
                            $"StripFNC is not supported for barcode type '{result.CodeTypeName}' when FNC symbols are present.");
                    }

                    Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                    Console.WriteLine($"CodeText      : {result.CodeText}");
                }
            }
        }
        catch (ArgumentException argEx)
        {
            Console.WriteLine($"Argument error: {argEx.Message}");
        }
        catch (BarCodeException bcEx)
        {
            Console.WriteLine($"Barcode library error: {bcEx.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error: {ex.Message}");
        }
    }

    // Helper method: checks for control characters (e.g., FNC1 = 0x1D) in the decoded text.
    private static bool ContainsControlCharacters(string text)
    {
        if (string.IsNullOrEmpty(text))
            return false;

        foreach (char ch in text)
        {
            // ASCII control range 0x00‑0x1F (excluding common whitespace characters).
            if (ch < 0x20 && ch != '\r' && ch != '\n' && ch != '\t')
                return true;
        }
        return false;
    }
}