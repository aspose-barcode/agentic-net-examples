// Title: Barcode decoding with fallback StripFNC setting
// Description: Demonstrates decoding a Code128 barcode and retrying with StripFNC enabled if the first attempt fails.
// Category-Description: This example belongs to the Aspose.BarCode decoding category, showcasing the use of BarCodeReader, BarcodeSettings, and the StripFNC property. It illustrates a common scenario where developers need to handle barcodes containing Function (FNC) characters by first attempting a standard decode and then falling back to a mode that strips those characters. Ideal for applications that must robustly read Code128 barcodes with optional FNC symbols.
// Prompt: Implement a fallback mechanism that retries decoding with StripFNC true if initial attempt with false fails.
// Tags: barcode symbology, decoding, fallback, stripfnc, code128, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates barcode generation and a fallback decoding strategy using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a Code128 barcode with FNC symbols, attempts to decode it, and retries with StripFNC enabled if needed.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for the sample barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeFallback_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string imagePath = Path.Combine(tempFolder, "code128_fnc.png");

        // Generate a Code128 barcode (including FNC symbols for demonstration)
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "Aspose" + "\u00F1" + "\u00F2" + "\u00F3"))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 2;
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // First attempt: StripFNC = false
        Console.WriteLine("Attempt 1: StripFNC = false");
        if (TryDecode(imagePath, false, out BarCodeResult[] results1))
        {
            PrintResults(results1);
        }
        else
        {
            // Fallback attempt: StripFNC = true
            Console.WriteLine("Attempt 2 (fallback): StripFNC = true");
            if (TryDecode(imagePath, true, out BarCodeResult[] results2))
            {
                PrintResults(results2);
            }
            else
            {
                Console.WriteLine("Failed to decode barcode in both attempts.");
            }
        }

        // Clean up temporary files
        try
        {
            if (File.Exists(imagePath))
                File.Delete(imagePath);
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program exit
        }
    }

    /// <summary>
    /// Attempts to decode a barcode image with the specified StripFNC setting.
    /// </summary>
    /// <param name="imagePath">Path to the barcode image file.</param>
    /// <param name="stripFnc">Whether to strip FNC characters during decoding.</param>
    /// <param name="results">Array of decoding results.</param>
    /// <returns>True if at least one barcode was successfully decoded; otherwise, false.</returns>
    static bool TryDecode(string imagePath, bool stripFnc, out BarCodeResult[] results)
    {
        results = Array.Empty<BarCodeResult>();
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return false;
        }

        // Initialize the reader for Code128 barcodes
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            // Apply the StripFNC setting as requested
            reader.BarcodeSettings.StripFNC = stripFnc;
            results = reader.ReadBarCodes();
            return results != null && results.Length > 0;
        }
    }

    /// <summary>
    /// Prints decoding results to the console.
    /// </summary>
    /// <param name="results">Array of barcode decoding results.</param>
    static void PrintResults(BarCodeResult[] results)
    {
        foreach (BarCodeResult result in results)
        {
            Console.WriteLine($"CodeType: {result.CodeTypeName}");
            Console.WriteLine($"CodeText: {result.CodeText}");
            Console.WriteLine($"ReadingQuality: {result.ReadingQuality}");
        }
    }
}