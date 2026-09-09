// Title: Generate Mailmark 2D barcode from JSON
// Description: Demonstrates deserializing a Mailmark2DCodetext JSON payload and creating a Mailmark 2‑D barcode image.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It shows how to use the ComplexBarcodeGenerator with Mailmark2DCodetext to produce a 2‑D barcode, a common task when integrating postal services or logistics systems. Developers often need to convert structured data (e.g., JSON) into Mailmark barcodes for mailing automation, and this snippet illustrates the typical workflow using Aspose.BarCode classes.
// Prompt: Deserialize JSON back into a Mailmark2DCodetext instance and generate the corresponding barcode.
// Tags: mailmark, complexbarcode, json, deserialization, png, aspose.barcode, barcode-generation

using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates deserialization of Mailmark2DCodetext JSON and barcode generation.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Deserializes JSON, creates a Mailmark 2D barcode, and saves it as PNG.
    /// </summary>
    static void Main()
    {
        // Sample JSON representing a Mailmark2DCodetext
        string json = @"{
            ""UPUCountryID"": ""JGB "",
            ""InformationTypeID"": ""0"",
            ""VersionID"": ""1"",
            ""Class"": ""1"",
            ""SupplyChainID"": 384224,
            ""ItemID"": 16563762,
            ""DestinationPostCodeAndDPS"": ""EF61AH8T "",
            ""RTSFlag"": ""0"",
            ""ReturnToSenderPostCode"": ""QWE2"",
            ""CustomerContent"": ""CUSTOM"",
            ""CustomerContentEncodeMode"": ""C40"",
            ""DataMatrixType"": ""Type_7""
        }";

        // Attempt to deserialize the JSON into a Mailmark2DCodetext object
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
            Console.WriteLine($"Failed to deserialize JSON: {ex.Message}");
            return;
        }

        // Determine the output file path for the generated barcode image
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "Mailmark2D.png");

        // Create the barcode generator using the deserialized Mailmark2DCodetext
        using (var generator = new ComplexBarcodeGenerator(mailmark2D))
        {
            // Optionally set the module (pixel) size for the barcode
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"Barcode generated and saved to: {outputPath}");
    }
}