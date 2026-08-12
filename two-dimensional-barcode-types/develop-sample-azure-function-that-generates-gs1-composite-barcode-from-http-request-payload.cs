// Title: Generate GS1 Composite barcode in Azure Function sample
// Description: Demonstrates creating a GS1 Composite barcode from a JSON payload using Aspose.BarCode, illustrating how the code can be adapted for an Azure Function HTTP trigger.
// Category-Description: This example belongs to the Aspose.BarCode generation category, focusing on composite symbologies (GS1 Composite). It showcases the BarcodeGenerator class with EncodeTypes.GS1CompositeBar, setting linear and 2‑D component types, and configuring visual parameters. Developers building web APIs or Azure Functions that need to return barcode images commonly use these APIs to produce printable or display‑ready barcodes.
// Prompt: Develop a sample Azure Function that generates GS1 Composite barcode from HTTP request payload.
// Tags: gs1 composite barcode, barcode generation, azure function, json, aspose.barcode, png output

using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Sample console program (adaptable to Azure Function) that generates a GS1 Composite barcode from a JSON payload.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that simulates receiving an HTTP request payload, parses it, and creates the barcode image.
    /// </summary>
    static void Main()
    {
        // Simulated HTTP request payload (JSON)
        string jsonPayload = @"{
            ""linearComponent"": ""(01)00123456789012"",
            ""twoDComponent"": ""(21)A12345678"",
            ""outputFile"": ""gs1composite.png""
        }";

        // Deserialize the payload into a strongly‑typed object
        var request = JsonSerializer.Deserialize<RequestPayload>(jsonPayload);
        if (request == null)
        {
            Console.WriteLine("Failed to parse request payload.");
            return;
        }

        // Determine output path (use temp folder to avoid permission issues)
        string outputPath = Path.Combine(Path.GetTempPath(), request.OutputFile ?? "gs1composite.png");

        try
        {
            // Generate the barcode using the provided components
            GenerateGs1CompositeBarcode(request.LinearComponent, request.TwoDComponent, outputPath);
            Console.WriteLine($"GS1 Composite barcode generated at: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error generating barcode: {ex.Message}");
        }
    }

    /// <summary>
    /// Generates a GS1 Composite barcode image and saves it to the specified path.
    /// </summary>
    /// <param name="linearComponent">The linear (1‑D) component string, e.g., "(01)00123456789012".</param>
    /// <param name="twoDComponent">The 2‑D component string, e.g., "(21)A12345678".</param>
    /// <param name="outputPath">Full file path where the PNG image will be saved.</param>
    static void GenerateGs1CompositeBarcode(string linearComponent, string twoDComponent, string outputPath)
    {
        // Validate required inputs
        if (string.IsNullOrWhiteSpace(linearComponent))
            throw new ArgumentException("Linear component is required.", nameof(linearComponent));
        if (string.IsNullOrWhiteSpace(twoDComponent))
            throw new ArgumentException("2D component is required.", nameof(twoDComponent));

        // Combine linear and 2D parts with the required '|' separator
        string codeText = $"{linearComponent}|{twoDComponent}";

        // Initialize the barcode generator for GS1 Composite symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codeText))
        {
            // Set the specific types for the linear and 2‑D components
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;

            // Optional visual settings to control appearance
            generator.Parameters.Barcode.Pdf417.AspectRatio = 3f;
            generator.Parameters.Barcode.XDimension.Point = 2f;
            generator.Parameters.Barcode.BarHeight.Point = 100f;

            // Save the generated barcode as a PNG file
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }
    }

    // Model representing the expected JSON payload structure
    private class RequestPayload
    {
        public string LinearComponent { get; set; }
        public string TwoDComponent { get; set; }
        public string OutputFile { get; set; }
    }
}