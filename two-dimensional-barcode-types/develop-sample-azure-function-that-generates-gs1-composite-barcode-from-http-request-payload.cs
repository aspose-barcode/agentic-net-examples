// Title: Generate GS1 Composite barcode from JSON payload
// Description: Demonstrates creating a GS1 Composite barcode by combining linear and 2D components supplied in a JSON payload and saving it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing how to configure GS1 Composite symbology using BarcodeGenerator, set X‑dimension, component types, and output format. Developers working with product identification, inventory, or logistics often need to produce GS1 Composite barcodes for packaging and scanning solutions.
// Prompt: Develop a sample Azure Function that generates GS1 Composite barcode from HTTP request payload.
// Tags: gs1-composite, barcode-generation, png, aspose.barcode, json, azure-functions

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Sample program that generates a GS1 Composite barcode from a JSON payload.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Parses a simulated HTTP request payload, builds the composite code text, and saves the barcode as a PNG file.
    /// </summary>
    static void Main()
    {
        // Simulated HTTP request payload (JSON format)
        string requestPayload = "{\"linear\":\"(01)01234567890128\",\"twod\":\"HelloWorld\"}";

        // Extract the linear component from the JSON payload
        string linear = ExtractJsonValue(requestPayload, "linear");
        // Extract the 2D component from the JSON payload
        string twod = ExtractJsonValue(requestPayload, "twod");

        // Validate that both components were found
        if (string.IsNullOrEmpty(linear) || string.IsNullOrEmpty(twod))
        {
            Console.WriteLine("Invalid payload: missing linear or 2D component.");
            return;
        }

        // Combine linear and 2D parts using the required pipe separator for GS1 Composite
        string codeText = $"{linear}|{twod}";

        // Prepare the output folder and file path
        string outputFolder = Path.Combine(Path.GetTempPath(), "GS1CompositeDemo");
        Directory.CreateDirectory(outputFolder);
        string outputPath = Path.Combine(outputFolder, "gs1composite.png");

        // Generate the GS1 Composite barcode with the specified settings
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codeText))
        {
            // Set the X-dimension (module width) in pixels
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            // Hide the human‑readable text
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;
            // Define the linear component type (GS1‑128)
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;
            // Define the 2D component type (CC‑A)
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;
            // Allow non‑GS1 encoding if needed
            generator.Parameters.Barcode.GS1CompositeBar.AllowOnlyGS1Encoding = false;

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"GS1 Composite barcode generated at: {outputPath}");
    }

    // Minimal JSON value extractor (assumes simple flat JSON with double‑quoted keys and values)
    private static string ExtractJsonValue(string json, string key)
    {
        string pattern = $"\"{key}\":\"";
        int startIdx = json.IndexOf(pattern, StringComparison.Ordinal);
        if (startIdx < 0) return null;
        startIdx += pattern.Length;
        int endIdx = json.IndexOf('\"', startIdx);
        if (endIdx < 0) return null;
        return json.Substring(startIdx, endIdx - startIdx);
    }
}