// Title: Generate Mailmark barcode and output as Base64 string
// Description: Demonstrates creating a Mailmark barcode from JSON input and converting the image to a Base64 string, suitable for returning from a REST endpoint.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of ComplexBarcodeGenerator and MailmarkCodetext classes to produce Mailmark symbology. Developers often need to generate barcodes dynamically from request data and return them in web APIs; this snippet shows the typical workflow of deserializing JSON, configuring barcode parameters, rendering to PNG, and encoding the result for HTTP transmission.
// Prompt: Develop a REST endpoint receiving JSON Mailmark fields and returning the generated barcode as Base64 string.
// Tags: mailmark, barcode, generation, base64, json, aspnet, aspose.barcode, complexbarcodegenerator

using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Entry point for the Mailmark barcode generation example.
/// </summary>
class Program
{
    /// <summary>
    /// Represents the JSON payload containing Mailmark fields.
    /// </summary>
    public class MailmarkInput
    {
        public int Format { get; set; }
        public int VersionID { get; set; }
        public string Class { get; set; }
        public int SupplychainID { get; set; }
        public int ItemID { get; set; }
        public string DestinationPostCodePlusDPS { get; set; }
    }

    /// <summary>
    /// Parses JSON input, generates a Mailmark barcode, and outputs the image as a Base64 string.
    /// </summary>
    static void Main()
    {
        // Simulated JSON payload (in a real REST endpoint this would come from the request body)
        string json = "{\"Format\":4,\"VersionID\":1,\"Class\":\"0\",\"SupplychainID\":384224,\"ItemID\":16563762,\"DestinationPostCodePlusDPS\":\"EF61AH8T \"}";

        // Deserialize the JSON into a strongly‑typed object
        MailmarkInput input;
        try
        {
            input = JsonSerializer.Deserialize<MailmarkInput>(json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Invalid JSON: {ex.Message}");
            return;
        }

        // Validate required fields
        if (input == null ||
            string.IsNullOrEmpty(input.Class) ||
            string.IsNullOrEmpty(input.DestinationPostCodePlusDPS))
        {
            Console.WriteLine("Missing required Mailmark fields.");
            return;
        }

        // Populate the MailmarkCodetext object with the input data
        var mailmark = new MailmarkCodetext
        {
            Format = input.Format,
            VersionID = input.VersionID,
            Class = input.Class,
            SupplychainID = input.SupplychainID,
            ItemID = input.ItemID,
            DestinationPostCodePlusDPS = input.DestinationPostCodePlusDPS
        };

        // Generate the barcode using ComplexBarcodeGenerator
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        {
            // Set barcode visual parameters (e.g., module size)
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Render the barcode to a memory stream in PNG format
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);

                // Convert the PNG bytes to a Base64 string for HTTP transmission
                string base64 = Convert.ToBase64String(ms.ToArray());
                Console.WriteLine(base64);
            }
        }

        // In a real REST service the Base64 string would be returned in the HTTP response.
    }
}