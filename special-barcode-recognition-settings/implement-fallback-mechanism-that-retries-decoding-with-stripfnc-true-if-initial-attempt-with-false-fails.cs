// Title: Barcode decoding with fallback StripFNC handling
// Description: Demonstrates generating a GS1 Code128 barcode containing FNC characters and decoding it with a fallback that retries with StripFNC enabled if the first attempt fails.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator (for creating barcodes) and BarCodeReader (for decoding). Typical scenarios include handling GS1 barcodes where Function Code (FNC) characters may need to be stripped during recognition. Developers often need a reliable fallback strategy to ensure successful decoding when initial settings do not yield results.
// Prompt: Implement a fallback mechanism that retries decoding with StripFNC true if initial attempt with false fails.
// Tags: barcode, gs1code128, stripfnc, fallback, decoding, generation, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program showing how to generate a GS1 Code128 barcode and decode it with a fallback mechanism for StripFNC.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a sample barcode, decodes it with fallback, and outputs the result.
    /// </summary>
    static void Main()
    {
        // Path for the sample barcode image
        string imagePath = "sample.png";

        // Generate a barcode that contains FNC characters (GS1 Code128)
        GenerateSampleBarcode(imagePath);

        // Decode with fallback mechanism (first without stripping FNC, then with stripping)
        string decodedText = DecodeWithFallback(imagePath);

        // Output the final decoded text (or "null" if decoding failed)
        Console.WriteLine($"Final decoded text: {(decodedText ?? "null")}");
    }

    // Generates a GS1 Code128 barcode and saves it to the specified file
    static void GenerateSampleBarcode(string path)
    {
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.GS1Code128, "(01)12345678901231(10)ABC"))
        {
            generator.Save(path);
        }
    }

    // Attempts to decode the image; if the first attempt (StripFNC = false) fails,
    // it retries with StripFNC = true.
    static string DecodeWithFallback(string imagePath)
    {
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return null;
        }

        // First attempt: do not strip FNC characters
        string result = TryDecode(imagePath, stripFnc: false);
        if (!string.IsNullOrEmpty(result))
            return result;

        // Second attempt: enable StripFNC to ignore FNC characters
        return TryDecode(imagePath, stripFnc: true);
    }

    // Performs a single decode attempt with the specified StripFNC setting
    static string TryDecode(string imagePath, bool stripFnc)
    {
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            // Configure the reader to strip or retain FNC characters based on the parameter
            reader.BarcodeSettings.StripFNC = stripFnc;

            // Read all barcodes found in the image
            BarCodeResult[] results = reader.ReadBarCodes();
            foreach (BarCodeResult res in results)
            {
                if (!string.IsNullOrEmpty(res.CodeText))
                {
                    // Log the successful decode details
                    Console.WriteLine($"StripFNC={stripFnc}, Type={res.CodeTypeName}, Text={res.CodeText}");
                    return res.CodeText;
                }
            }
        }

        // No valid barcode text found in this attempt
        return null;
    }
}