// Title: Generate Mailmark 4‑state barcode and return Base64 string
// Description: Demonstrates creating a Mailmark 4‑state barcode from JSON input and encoding the image as a Base64 string, suitable for returning from a REST endpoint.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of Aspose.BarCode.ComplexBarcode.MailmarkCodetext and Aspose.BarCode.Generation.ComplexBarcodeGenerator to produce Mailmark barcodes, a common requirement in postal automation and logistics. Developers often need to convert barcode images to Base64 for web APIs, email attachments, or storage.
// Prompt: Develop a REST endpoint receiving JSON Mailmark fields and returning the generated barcode as Base64 string.
// Tags: mailmark, barcode, generation, base64, aspnet, aspose.barcode, json, rest

using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

namespace MailmarkBarcodeDemo
{
    /// <summary>
    /// Model matching the expected JSON payload for Mailmark barcode generation.
    /// </summary>
    public class MailmarkRequest
    {
        public int Format { get; set; }               // Must be 4 for 4‑state Mailmark
        public int VersionID { get; set; }            // Typically 1
        public string Class { get; set; }             // e.g. "0"
        public int SupplychainID { get; set; }        // e.g. 384224
        public int ItemID { get; set; }               // e.g. 16563762
        public string DestinationPostCodePlusDPS { get; set; } // Must include trailing space
    }

    /// <summary>
    /// Demonstrates generating a Mailmark barcode from JSON data and outputting the image as a Base64 string.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point – simulates a REST call by deserializing a JSON payload, creating a Mailmark barcode,
        /// and writing the Base64‑encoded PNG image to the console (representing the HTTP response body).
        /// </summary>
        static void Main()
        {
            // Sample JSON input – in a real REST service this would come from the request body
            string jsonInput = @"{
                ""Format"": 4,
                ""VersionID"": 1,
                ""Class"": ""0"",
                ""SupplychainID"": 384224,
                ""ItemID"": 16563762,
                ""DestinationPostCodePlusDPS"": ""EF61AH8T ""
            }";

            // Deserialize JSON to a strongly‑typed request object
            MailmarkRequest request = JsonSerializer.Deserialize<MailmarkRequest>(jsonInput);
            if (request == null)
            {
                Console.WriteLine("Invalid input.");
                return;
            }

            // Basic validation of required fields
            if (request.Format != 4)
            {
                Console.WriteLine("Only Mailmark 4‑state (Format=4) is supported.");
                return;
            }
            if (string.IsNullOrEmpty(request.Class) ||
                string.IsNullOrEmpty(request.DestinationPostCodePlusDPS))
            {
                Console.WriteLine("Missing required Mailmark fields.");
                return;
            }

            // Populate the MailmarkCodetext object with request data
            var mailmark = new MailmarkCodetext
            {
                Format = request.Format,
                VersionID = request.VersionID,
                Class = request.Class,
                SupplychainID = request.SupplychainID,
                ItemID = request.ItemID,
                DestinationPostCodePlusDPS = request.DestinationPostCodePlusDPS
            };

            // Generate the barcode image into a memory stream
            using (var ms = new MemoryStream())
            {
                using (var generator = new ComplexBarcodeGenerator(mailmark))
                {
                    // Save the barcode as PNG (BarCodeImageFormat resides in Aspose.BarCode.Generation)
                    generator.Save(ms, BarCodeImageFormat.Png);
                }

                // Convert the PNG bytes to a Base64 string
                string base64 = Convert.ToBase64String(ms.ToArray());

                // Output the Base64 string (simulating a REST response body)
                Console.WriteLine(base64);
            }
        }
    }
}