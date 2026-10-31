// Title: Validate numeric-only data in 2‑state barcodes (Planet and Postnet)
// Description: Demonstrates generating Planet and Postnet 2‑state barcodes, saving them as PNG, and verifying that the decoded text consists solely of numeric characters.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It shows how to use BarcodeGenerator to create 2‑state symbologies (Planet, Postnet) and BarCodeReader to decode them, a common task when validating postal codes or other numeric identifiers. Developers often need to ensure that generated barcodes contain only allowed characters before processing or storing them.
// Prompt: Validate that generated 2‑state barcodes contain only numeric characters by inspecting the encoded data.
// Tags: barcode, 2-state, planet, postnet, validation, numeric, generation, recognition, aspose.barcode, csharp

using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Generates Planet and Postnet barcodes, then validates that the decoded
/// text contains only numeric characters.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates temporary barcode images,
    /// decodes them, checks for numeric-only content, and cleans up.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeValidation_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Sample numeric code texts for the two symbologies
        string planetCode = "123456";
        string postnetCode = "1159628792";

        // -------------------- Generate Planet barcode --------------------
        string planetPath = Path.Combine(tempFolder, "planet.png");
        using (var planetGenerator = new BarcodeGenerator(EncodeTypes.Planet, planetCode))
        {
            // Configure visual parameters
            planetGenerator.Parameters.Barcode.XDimension.Pixels = 4f;
            planetGenerator.Parameters.Barcode.BarHeight.Pixels = 50f;
            planetGenerator.Parameters.Barcode.Postal.ShortBarHeight.Point = 20f;

            // Save as PNG
            planetGenerator.Save(planetPath, BarCodeImageFormat.Png);
        }

        // -------------------- Generate Postnet barcode --------------------
        string postnetPath = Path.Combine(tempFolder, "postnet.png");
        using (var postnetGenerator = new BarcodeGenerator(EncodeTypes.Postnet, postnetCode))
        {
            // Configure visual parameters
            postnetGenerator.Parameters.Barcode.XDimension.Pixels = 4f;
            postnetGenerator.Parameters.Barcode.BarHeight.Pixels = 50f;
            postnetGenerator.Parameters.Barcode.Postal.ShortBarHeight.Point = 20f;

            // Save as PNG
            postnetGenerator.Save(postnetPath, BarCodeImageFormat.Png);
        }

        // -------------------- Validate Planet barcode --------------------
        if (File.Exists(planetPath))
        {
            using (var planetReader = new BarCodeReader(planetPath, DecodeType.Planet))
            {
                foreach (var result in planetReader.ReadBarCodes())
                {
                    // Check that the decoded text consists only of digits
                    bool isNumeric = Regex.IsMatch(result.CodeText ?? string.Empty, @"^\d+$");
                    Console.WriteLine($"Planet barcode decoded text: '{result.CodeText}'. Numeric only: {isNumeric}");
                }
            }
        }
        else
        {
            Console.WriteLine("Planet barcode image not found.");
        }

        // -------------------- Validate Postnet barcode --------------------
        if (File.Exists(postnetPath))
        {
            using (var postnetReader = new BarCodeReader(postnetPath, DecodeType.Postnet))
            {
                foreach (var result in postnetReader.ReadBarCodes())
                {
                    // Check that the decoded text consists only of digits
                    bool isNumeric = Regex.IsMatch(result.CodeText ?? string.Empty, @"^\d+$");
                    Console.WriteLine($"Postnet barcode decoded text: '{result.CodeText}'. Numeric only: {isNumeric}");
                }
            }
        }
        else
        {
            Console.WriteLine("Postnet barcode image not found.");
        }

        // -------------------- Cleanup temporary files --------------------
        try
        {
            File.Delete(planetPath);
            File.Delete(postnetPath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignore any cleanup errors
        }
    }
}