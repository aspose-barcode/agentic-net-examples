// Title: Generate Mailmark barcode from JSON and return Base64
// Description: Demonstrates how to deserialize Mailmark fields from JSON, create a Mailmark barcode using Aspose.BarCode, and output the image as a Base64 string.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcode types such as Mailmark. It showcases the use of ComplexBarcodeGenerator and MailmarkCodetext classes to create barcodes from structured data, a common requirement for developers building REST services that need to return barcode images in web-friendly formats.
// Prompt: Develop a REST endpoint receiving JSON Mailmark fields and returning the generated barcode as Base64 string.
// Tags: mailmark, barcode generation, json, base64, aspose.barcode, complexbarcode

using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that deserializes Mailmark data from JSON,
/// generates a Mailmark barcode, and outputs the image as a Base64 string.
/// </summary>
class Program
{
    // Sample JSON representing Mailmark fields.
    // In a real REST service this would come from the request body.
    private const string SampleJson = @"
    {
        ""format"": 4,
        ""versionId"": 1,
        ""class"": ""0"",
        ""supplychainId"": 384224,
        ""itemId"": 16563762,
        ""destinationPostCodePlusDps"": ""EF61AH8T ""
    }";

    /// <summary>
    /// Entry point of the example. Demonstrates the core logic of
    /// JSON deserialization, barcode generation, and Base64 conversion.
    /// </summary>
    static void Main()
    {
        // NOTE: The snippet runner cannot host an HTTP server.
        // The core logic is demonstrated by reading a JSON string,
        // generating a Mailmark barcode, and outputting the image as Base64.

        // Deserialize JSON to a POCO using case‑insensitive property matching.
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        MailmarkInput input = JsonSerializer.Deserialize<MailmarkInput>(SampleJson, options);

        // Validate required fields.
        if (input == null)
        {
            Console.WriteLine("Invalid input.");
            return;
        }

        // Populate MailmarkCodetext with required properties.
        var mailmark = new MailmarkCodetext
        {
            Format = input.Format,
            VersionID = input.VersionId,
            Class = input.Class,
            SupplychainID = input.SupplychainId,
            ItemID = input.ItemId,
            DestinationPostCodePlusDPS = input.DestinationPostCodePlusDps
        };

        // Generate barcode image and convert to Base64.
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        using (var ms = new MemoryStream())
        {
            generator.Save(ms, BarCodeImageFormat.Png);
            string base64 = Convert.ToBase64String(ms.ToArray());
            Console.WriteLine(base64);
        }
    }

    // POCO matching the expected JSON structure.
    private class MailmarkInput
    {
        public int Format { get; set; }
        public int VersionId { get; set; }
        public string Class { get; set; }
        public int SupplychainId { get; set; }
        public int ItemId { get; set; }
        public string DestinationPostCodePlusDps { get; set; }
    }
}