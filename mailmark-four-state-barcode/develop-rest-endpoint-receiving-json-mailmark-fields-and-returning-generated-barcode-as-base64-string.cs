// Title: Generate Mailmark Barcode and Return Base64 via REST Simulation
// Description: Demonstrates creating a Mailmark 4‑state barcode from JSON input and encoding the PNG image as a Base64 string, suitable for returning from a REST endpoint.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It shows how to use Aspose.BarCode.ComplexBarcode and Aspose.BarCode.Generation classes to build Mailmark codetext, configure barcode parameters, render a PNG image, and convert it to Base64 for web APIs. Developers creating REST services that need to produce barcodes on‑the‑fly will find this pattern useful.
// Prompt: Develop a REST endpoint receiving JSON Mailmark fields and returning the generated barcode as Base64 string.
// Tags: mailmark, barcode generation, png, aspose.barcode, complexbarcodegenerator, json, base64

using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Sample console program that simulates a REST endpoint generating a Mailmark barcode from JSON input
/// and returning a Base64‑encoded PNG.
/// </summary>
class Program
{
    // Sample JSON representing Mailmark 4‑state fields
    private const string SampleJson = @"{
        ""Format"": 4,
        ""VersionID"": 1,
        ""Class"": ""0"",
        ""SupplychainID"": 384224,
        ""ItemID"": 16563762,
        ""DestinationPostCodePlusDPS"": ""EF61AH8T ""
    }";

    /// <summary>
    /// Entry point: parses JSON, builds Mailmark codetext, generates PNG barcode,
    /// and writes Base64 string to console (simulating HTTP response).
    /// </summary>
    static void Main()
    {
        // In a real REST service the JSON would come from the request body.
        // Here we simulate it with a hard‑coded string.
        MailmarkInput input;
        try
        {
            input = JsonSerializer.Deserialize<MailmarkInput>(SampleJson);
            if (input == null)
                throw new ArgumentException("Deserialized input is null.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to parse JSON: {ex.Message}");
            return;
        }

        // Build the Mailmark codetext object from the deserialized input
        var mailmark = new MailmarkCodetext
        {
            Format = input.Format,
            VersionID = input.VersionID,
            Class = input.Class,
            SupplychainID = input.SupplychainID,
            ItemID = input.ItemID,
            DestinationPostCodePlusDPS = input.DestinationPostCodePlusDPS
        };

        // Generate the barcode and obtain a Base64‑encoded PNG image
        string base64;
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        {
            // Set barcode visual parameters (e.g., module size)
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            using (var ms = new MemoryStream())
            {
                // Render the barcode to the memory stream as PNG
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0;

                // Convert the PNG bytes to a Base64 string
                byte[] bytes = ms.ToArray();
                base64 = Convert.ToBase64String(bytes);
            }
        }

        // Output the Base64 string (simulating a REST response payload)
        Console.WriteLine(base64);
    }

    // DTO for deserializing the incoming JSON payload
    private class MailmarkInput
    {
        public int Format { get; set; }
        public int VersionID { get; set; }
        public string Class { get; set; } = string.Empty;
        public int SupplychainID { get; set; }
        public int ItemID { get; set; }
        public string DestinationPostCodePlusDPS { get; set; } = string.Empty;
    }
}