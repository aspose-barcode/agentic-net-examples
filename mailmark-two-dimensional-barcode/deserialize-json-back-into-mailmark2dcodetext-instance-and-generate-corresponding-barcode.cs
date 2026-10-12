// Title: Generate Mailmark 2D Barcode from JSON
// Description: Demonstrates deserializing a JSON string into a Mailmark2DCodetext object and creating the corresponding Mailmark 2‑D barcode image using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of ComplexBarcodeGenerator and related parameter classes to produce high‑density 2‑D barcodes such as Mailmark. Typical scenarios include converting structured data (e.g., JSON) into printable barcode images for logistics, mailing, and tracking applications. Developers often need to deserialize data, configure barcode settings, and save the output in common image formats.
// Prompt: Deserialize JSON back into a Mailmark2DCodetext instance and generate the corresponding barcode.
// Tags: barcode, mailmark, json, deserialization, complexbarcode, generation, png, aspose.barcode

using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that deserializes a JSON representation of a <see cref="Mailmark2DCodetext"/>
/// and generates a Mailmark 2‑D barcode image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Performs JSON deserialization, prepares the output folder,
    /// creates the barcode with <see cref="ComplexBarcodeGenerator"/>, and saves it as PNG.
    /// </summary>
    static void Main()
    {
        // JSON string containing sample Mailmark2DCodetext fields
        string json = @"{
            ""UPUCountryID"": ""JGB "",
            ""InformationTypeID"": ""0"",
            ""VersionID"": ""1"",
            ""Class"": ""1"",
            ""RTSFlag"": ""0"",
            ""SupplyChainID"": 384224,
            ""ItemID"": 16563762,
            ""DestinationPostCodeAndDPS"": ""EF61AH8T "",
            ""CustomerContent"": ""CUSTOMER123"",
            ""CustomerContentEncodeMode"": ""C40"",
            ""DataMatrixType"": ""Type_7""
        }";

        // Attempt to deserialize the JSON into a Mailmark2DCodetext instance
        Mailmark2DCodetext mailmark2D;
        try
        {
            mailmark2D = JsonSerializer.Deserialize<Mailmark2DCodetext>(json);
            if (mailmark2D == null)
            {
                Console.WriteLine("Deserialization returned null.");
                return;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deserializing JSON: {ex.Message}");
            return;
        }

        // Ensure the output directory exists (creates it if missing)
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "output");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Full path for the generated barcode image
        string outputPath = Path.Combine(outputDir, "Mailmark2D.png");

        // Generate the barcode using ComplexBarcodeGenerator
        using (var generator = new ComplexBarcodeGenerator(mailmark2D))
        {
            // Optional: set the module (pixel) size for better readability
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Save the barcode image in PNG format
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"Barcode generated and saved to: {outputPath}");
    }
}