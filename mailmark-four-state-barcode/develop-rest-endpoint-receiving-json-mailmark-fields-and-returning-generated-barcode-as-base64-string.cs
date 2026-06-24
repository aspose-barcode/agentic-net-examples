using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates deserialization of a Mailmark request JSON payload,
/// generation of a Mailmark barcode using Aspose.BarCode, and
/// conversion of the resulting image to a Base64 string.
/// </summary>
class Program
{
    /// <summary>
    /// Model representing the expected JSON payload for Mailmark barcode generation.
    /// </summary>
    private class MailmarkRequest
    {
        public int Format { get; set; }               // 0,1,2 etc.
        public int VersionID { get; set; }            // typically 1
        public string Class { get; set; }             // string, e.g., "0"
        public int SupplychainID { get; set; }        // max 99 or 999999
        public int ItemID { get; set; }               // max 99999999
        public string DestinationPostCodePlusDPS { get; set; } // 9‑character postcode+DP
    }

    /// <summary>
    /// Entry point of the application. Deserializes a sample JSON payload,
    /// validates required fields, generates a Mailmark barcode, and writes
    /// the Base64‑encoded image to the console.
    /// </summary>
    static void Main()
    {
        // NOTE: Full REST service cannot be hosted in the snippet runner.
        // The core logic is demonstrated by deserializing a sample JSON,
        // generating the Mailmark barcode, converting it to Base64 and
        // printing the result to the console.

        // Sample JSON payload (replace with real input as needed)
        string jsonPayload = @"{
            ""Format"": 4,
            ""VersionID"": 1,
            ""Class"": ""0"",
            ""SupplychainID"": 384224,
            ""ItemID"": 16563762,
            ""DestinationPostCodePlusDPS"": ""EF61AH8T ""
        }";

        // Deserialize JSON into request object
        MailmarkRequest request;
        try
        {
            request = JsonSerializer.Deserialize<MailmarkRequest>(jsonPayload);
            if (request == null)
                throw new ArgumentException("Deserialized request is null.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to parse JSON payload: {ex.Message}");
            return;
        }

        // Validate required string fields
        if (string.IsNullOrWhiteSpace(request.Class) ||
            string.IsNullOrWhiteSpace(request.DestinationPostCodePlusDPS))
        {
            Console.WriteLine("Class and DestinationPostCodePlusDPS must be provided.");
            return;
        }

        // Populate MailmarkCodetext object with data from the request
        var mailmark = new MailmarkCodetext
        {
            Format = request.Format,
            VersionID = request.VersionID,
            Class = request.Class,
            SupplychainID = request.SupplychainID,
            ItemID = request.ItemID,
            DestinationPostCodePlusDPS = request.DestinationPostCodePlusDPS
        };

        // Generate barcode image into a memory stream
        using (var ms = new MemoryStream())
        {
            using (var generator = new ComplexBarcodeGenerator(mailmark))
            {
                // Save as PNG; ComplexBarcodeGenerator disposes correctly after Save
                generator.Save(ms, BarCodeImageFormat.Png);
            }

            // Convert image bytes to Base64 string
            string base64 = Convert.ToBase64String(ms.ToArray());

            // Output the Base64 string (simulating a REST response body)
            Console.WriteLine(base64);
        }
    }
}