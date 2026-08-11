// Title: GS1 Code128 barcode generation and decoding with optional FNC stripping
// Description: The example creates a GS1 Code128 barcode containing FNC characters, saves it as PNG, then decodes it twice—once preserving and once stripping the FNC symbols—while logging the outcomes.
// Category-Description: This sample belongs to the Aspose.BarCode generation and recognition category, demonstrating how to use BarcodeGenerator to create GS1 Code128 barcodes and BarCodeReader to recognize them. It highlights handling of FNC (Function) characters via the StripFNC setting, a common requirement in GS1 applications such as product labeling and inventory tracking. Developers often need to toggle FNC stripping to meet different data processing rules, making this pattern useful across many barcode‑related projects.
// Prompt: Implement logging of each barcode decoding operation, indicating whether FNC symbols were stripped or retained.
// Tags: gs1, code128, fnc, barcode-generation, barcode-recognition, strip-fnc, aspose.barcode, png

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a GS1 Code128 barcode with FNC characters,
/// then decoding it with and without stripping those characters while logging the results.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode image, verifies its creation,
    /// and runs two decoding scenarios: preserving and stripping FNC symbols.
    /// </summary>
    static void Main()
    {
        // Prepare output directory
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "output");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Path for the generated barcode image
        string barcodePath = Path.Combine(outputDir, "gs1code128.png");

        // Sample GS1 Code128 text containing FNC characters (application identifiers)
        string sampleText = "(02)04006664241007(37)1(400)7019590754";

        // Generate the barcode image using BarcodeGenerator
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1Code128, sampleText))
        {
            // Save the generated barcode as a PNG file
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the image was successfully created
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Decode the barcode without stripping FNC characters
        DecodeAndLog(barcodePath, stripFnc: false);

        // Decode the barcode with FNC characters stripped
        DecodeAndLog(barcodePath, stripFnc: true);
    }

    /// <summary>
    /// Decodes the barcode image and logs the result, indicating whether FNC symbols were stripped.
    /// </summary>
    /// <param name="imagePath">Path to the barcode image.</param>
    /// <param name="stripFnc">If true, FNC characters will be stripped from the decoded text.</param>
    private static void DecodeAndLog(string imagePath, bool stripFnc)
    {
        Console.WriteLine($"--- Decoding (StripFNC = {stripFnc}) ---");

        // Initialize a reader for Code128 (GS1Code128 is a variant of Code128)
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            // Configure the reader to strip or retain FNC characters based on the parameter
            reader.BarcodeSettings.StripFNC = stripFnc;

            // Read all barcodes present in the image
            BarCodeResult[] results = reader.ReadBarCodes();

            if (results.Length == 0)
            {
                Console.WriteLine("No barcodes detected.");
                return;
            }

            // Log each detected barcode and its decoding details
            foreach (var result in results)
            {
                // result.CodeText reflects the StripFNC setting applied above
                Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                Console.WriteLine($"CodeText   : {result.CodeText}");
                Console.WriteLine($"StripFNC   : {stripFnc}");
                Console.WriteLine();
            }
        }
    }
}