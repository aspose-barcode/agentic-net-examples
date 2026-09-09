// Title: Generate Mailmark 2D barcode and return Base64 string
// Description: Demonstrates parsing Mailmark JSON fields, creating a Mailmark 2D barcode with Aspose.BarCode, and outputting the image as a Base64 string.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcode types such as Mailmark 2D. It showcases the use of ComplexBarcodeGenerator, Mailmark2DCodetext, and related parameter settings to produce PNG images. Developers building REST services that need to generate barcodes from JSON payloads can adapt this pattern for on‑the‑fly image creation and Base64 encoding.
// Prompt: Develop a REST endpoint receiving JSON Mailmark fields and returning the generated barcode as Base64 string.
// Tags: mailmark, barcode, generation, base64, json, aspnet, aspose.barcode, complexbarcode

using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a Mailmark 2D barcode from JSON input and outputs the image as a Base64 string.
/// </summary>
class Program
{
    // Sample JSON representing Mailmark 2D fields (in a real service this would be received via HTTP POST)
    private const string SampleJson = @"{
        ""UPUCountryID"": ""JGB "",
        ""InformationTypeID"": ""0"",
        ""VersionID"": ""1"",
        ""Class"": ""1"",
        ""SupplyChainID"": 123,
        ""ItemID"": 1234,
        ""DestinationPostCodeAndDPS"": ""EF61AH8T "",
        ""RTSFlag"": ""0"",
        ""ReturnToSenderPostCode"": ""QWE2 "",
        ""CustomerContent"": ""CUSTOM DATA"",
        ""DataMatrixType"": ""Type_9""
    }";

    /// <summary>
    /// Entry point. Parses JSON, generates a Mailmark 2D barcode, and writes the PNG image as a Base64 string.
    /// </summary>
    static void Main()
    {
        // In a real application this JSON would come from an HTTP POST request.
        // The console example parses the JSON, generates the barcode, and prints the Base64 string.
        try
        {
            // Deserialize the incoming JSON into a strongly‑typed request object
            MailmarkRequest request = JsonSerializer.Deserialize<MailmarkRequest>(SampleJson);
            if (request == null)
            {
                Console.WriteLine("Failed to deserialize request.");
                return;
            }

            // Populate the Mailmark2DCodetext object with values from the request
            Mailmark2DCodetext mailmark = new Mailmark2DCodetext
            {
                UPUCountryID = request.UPUCountryID,
                InformationTypeID = request.InformationTypeID,
                VersionID = request.VersionID,
                Class = request.Class,
                SupplyChainID = request.SupplyChainID,
                ItemID = request.ItemID,
                DestinationPostCodeAndDPS = request.DestinationPostCodeAndDPS,
                RTSFlag = request.RTSFlag,
                ReturnToSenderPostCode = request.ReturnToSenderPostCode,
                CustomerContent = request.CustomerContent
            };

            // Resolve DataMatrixType enum value via reflection (as per rules)
            var field = typeof(Mailmark2DType).GetField(request.DataMatrixType);
            if (field == null)
            {
                Console.WriteLine($"Unknown DataMatrixType: {request.DataMatrixType}");
                return;
            }
            mailmark.DataMatrixType = (Mailmark2DType)field.GetValue(null);

            // Generate the barcode using ComplexBarcodeGenerator
            using (var generator = new ComplexBarcodeGenerator(mailmark))
            {
                // Set X‑dimension (pixel size) for the barcode modules
                generator.Parameters.Barcode.XDimension.Pixels = 4f;

                // Save the barcode image to a memory stream in PNG format
                using (var ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);

                    // Convert the image bytes to a Base64 string and output it
                    string base64 = Convert.ToBase64String(ms.ToArray());
                    Console.WriteLine(base64);
                }
            }
        }
        catch (Exception ex)
        {
            // Log any unexpected errors
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    // DTO representing the expected JSON payload for Mailmark barcode generation
    private class MailmarkRequest
    {
        public string UPUCountryID { get; set; }
        public string InformationTypeID { get; set; }
        public string VersionID { get; set; }
        public string Class { get; set; }
        public int SupplyChainID { get; set; }
        public int ItemID { get; set; }
        public string DestinationPostCodeAndDPS { get; set; }
        public string RTSFlag { get; set; }
        public string ReturnToSenderPostCode { get; set; }
        public string CustomerContent { get; set; }
        public string DataMatrixType { get; set; }
    }
}