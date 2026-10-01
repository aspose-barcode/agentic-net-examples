// Title: Extract GS1 Composite Component Count and Application Identifiers from PNG
// Description: Demonstrates generating a GS1 Composite barcode, saving it as a PNG image, reading it back, and extracting both the component count and the application identifiers (AIs) present in the barcode.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, focusing on GS1 Composite barcodes. It showcases the use of BarcodeGenerator for creating GS1‑128 + PDF417 composite symbols, BarCodeReader for decoding, and the Extended.GS1CompositeBar properties for accessing component details. Developers working with GS1 data, inventory systems, or logistics often need to generate composite barcodes and later parse their AI information for downstream processing.
// Prompt: Extract GS1 Composite component count and application identifiers from a PNG image.
// Tags: gs1, composite, barcode, generation, recognition, png, aspose.barcode

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Sample program that creates a GS1 Composite barcode, saves it as PNG,
/// reads it back, and extracts component count and application identifiers.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the sample. Generates a barcode, decodes it, and prints extracted data.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary folder for the sample barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "Gs1CompositeSample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "gs1composite.png");

        // Create a GS1 Composite barcode image
        string linearPart = "(01)12345678901231"; // GTIN-14 (AI 01)
        string twoDPart = "(21)ABC123";          // Serial number (AI 21)
        string codeText = $"{linearPart}|{twoDPart}";

        using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codeText))
        {
            // Configure linear component (GS1-128)
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;
            // Configure 2D component (full PDF417)
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_C;
            // Allow non‑GS1 data in the 2D component (required for AI 21)
            generator.Parameters.Barcode.GS1CompositeBar.AllowOnlyGS1Encoding = false;
            // Adjust PDF417 columns (optional)
            generator.Parameters.Barcode.Pdf417.Columns = 30;

            // Save as PNG
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify the image file exists before attempting to read
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create the barcode image.");
            return;
        }

        // Read the barcode from the image
        BaseDecodeType decodeType = DecodeType.GS1CompositeBar;
        using (var reader = new BarCodeReader(barcodePath, decodeType))
        {
            // Read all detected barcodes (should be one)
            BarCodeResult[] results = reader.ReadBarCodes();

            if (results.Length == 0)
            {
                Console.WriteLine("No barcode detected in the image.");
                return;
            }

            foreach (var result in results)
            {
                // Extract component count (linear + 2D). If the API provides a direct property, use it; otherwise compute.
                int componentCount = GetComponentCount(result);

                // Collect application identifiers from both components
                var ais = ExtractApplicationIdentifiers(result);

                Console.WriteLine($"Component Count: {componentCount}");
                Console.WriteLine("Application Identifiers found:");
                foreach (var ai in ais)
                {
                    Console.WriteLine($"  {ai}");
                }
            }
        }

        // Clean up temporary files (optional)
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored – cleanup failure should not affect program outcome
        }
    }

    // Attempts to get component count from the result; falls back to counting non‑empty parts.
    private static int GetComponentCount(BarCodeResult result)
    {
        // The extended parameters may expose ComponentCount; use reflection to avoid compile errors if absent.
        var ext = result.Extended?.GS1CompositeBar;
        if (ext == null)
            return 0;

        var prop = ext.GetType().GetProperty("ComponentCount");
        if (prop != null && prop.PropertyType == typeof(int))
        {
            return (int)prop.GetValue(ext);
        }

        // Fallback: count linear and 2D parts that are not empty
        int count = 0;
        var linearProp = ext.GetType().GetProperty("LinearCodeText");
        var twoDProp = ext.GetType().GetProperty("TwoDCodeText");
        if (linearProp != null && linearProp.GetValue(ext) is string lin && !string.IsNullOrEmpty(lin))
            count++;
        if (twoDProp != null && twoDProp.GetValue(ext) is string two && !string.IsNullOrEmpty(two))
            count++;
        return count;
    }

    // Extracts distinct AI strings like "(01)" from linear and 2D code texts.
    private static HashSet<string> ExtractApplicationIdentifiers(BarCodeResult result)
    {
        var ais = new HashSet<string>();
        var ext = result.Extended?.GS1CompositeBar;
        if (ext == null)
            return ais;

        var linearProp = ext.GetType().GetProperty("LinearCodeText");
        var twoDProp = ext.GetType().GetProperty("TwoDCodeText");

        string[] parts = new string[2];
        if (linearProp != null && linearProp.GetValue(ext) is string lin)
            parts[0] = lin;
        if (twoDProp != null && twoDProp.GetValue(ext) is string two)
            parts[1] = two;

        var regex = new Regex(@"\(\d{2,4}\)");
        foreach (var part in parts)
        {
            if (string.IsNullOrEmpty(part))
                continue;

            foreach (Match match in regex.Matches(part))
            {
                ais.Add(match.Value);
            }
        }

        return ais;
    }
}