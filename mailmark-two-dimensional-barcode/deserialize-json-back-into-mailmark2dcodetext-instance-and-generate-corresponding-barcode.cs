// Title: Generate Mailmark2D Barcode from JSON Data
// Description: This example deserializes a JSON string into a Mailmark2DCodetext object and creates a Mailmark2D barcode image.
// Category-Description: Demonstrates Aspose.BarCode complex barcode generation for the Mailmark2D symbology. It covers deserialization of JSON into the Mailmark2DCodetext DTO, barcode creation with ComplexBarcodeGenerator, saving as PNG, and optional decoding using BarCodeReader. Developers working with postal barcodes, data matrix codes, or any Aspose.BarCode complex barcode scenarios will find this pattern useful for integrating JSON-driven barcode generation into their applications.
// Prompt: Deserialize JSON back into a Mailmark2DCodetext instance and generate the corresponding barcode.
// Tags: mailmark2d, json, deserialization, barcode-generation, aspose.barcode, complexbarcode

using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates deserialization of JSON into a Mailmark2DCodetext object and generation of a Mailmark2D barcode.
/// </summary>
class Program
{
    /// <summary>
    /// DTO matching the JSON structure for deserialization.
    /// </summary>
    private class Mailmark2DJson
    {
        public string UPUCountryID { get; set; }
        public string InformationTypeID { get; set; }
        public string VersionID { get; set; }
        public string Class { get; set; }
        public string RTSFlag { get; set; }
        public int SupplyChainID { get; set; }
        public int ItemID { get; set; }
        public string DestinationPostCodeAndDPS { get; set; }
        public string ReturnToSenderPostCode { get; set; }
        public string CustomerContent { get; set; }
        public string CustomerContentEncodeMode { get; set; }
        public string DataMatrixType { get; set; }
    }

    /// <summary>
    /// Entry point of the example. Deserializes JSON, creates a Mailmark2D barcode, saves it, and optionally decodes it.
    /// </summary>
    static void Main()
    {
        // Sample JSON representing a Mailmark2DCodetext
        string json = @"{
            ""UPUCountryID"": ""JGB "",
            ""InformationTypeID"": ""0"",
            ""VersionID"": ""1"",
            ""Class"": ""1"",
            ""RTSFlag"": ""0"",
            ""SupplyChainID"": 384224,
            ""ItemID"": 16563762,
            ""DestinationPostCodeAndDPS"": ""EF61AH8T "",
            ""ReturnToSenderPostCode"": "" QWE2 "",
            ""CustomerContent"": ""CUSTOM DATA"",
            ""CustomerContentEncodeMode"": ""C40"",
            ""DataMatrixType"": ""Type_9""
        }";

        // Deserialize JSON into the DTO
        Mailmark2DJson data = JsonSerializer.Deserialize<Mailmark2DJson>(json);
        if (data == null)
        {
            Console.WriteLine("Failed to deserialize JSON.");
            return;
        }

        // Map DTO fields to a Mailmark2DCodetext instance
        var mailmark2d = new Mailmark2DCodetext
        {
            UPUCountryID = data.UPUCountryID,
            InformationTypeID = data.InformationTypeID,
            VersionID = data.VersionID,
            Class = data.Class,
            RTSFlag = data.RTSFlag,
            SupplyChainID = data.SupplyChainID,
            ItemID = data.ItemID,
            DestinationPostCodeAndDPS = data.DestinationPostCodeAndDPS,
            ReturnToSenderPostCode = data.ReturnToSenderPostCode,
            CustomerContent = data.CustomerContent,
            CustomerContentEncodeMode = Enum.Parse<DataMatrixEncodeMode>(data.CustomerContentEncodeMode),
            DataMatrixType = Enum.Parse<Mailmark2DType>(data.DataMatrixType)
        };

        // Prepare output directory and file path
        string outputDir = Path.Combine(Path.GetTempPath(), "Mailmark2DExample");
        Directory.CreateDirectory(outputDir);
        string barcodePath = Path.Combine(outputDir, "Mailmark2D.png");

        // Generate the Mailmark2D barcode and save as PNG
        using (var generator = new ComplexBarcodeGenerator(mailmark2d))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"Barcode saved to: {barcodePath}");

        // Decode the generated barcode to verify the content
        using (var reader = new BarCodeReader(barcodePath, DecodeType.DataMatrix))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Mailmark2DCodetext decoded = ComplexCodetextReader.TryDecodeMailmark2D(result.CodeText);
                if (decoded != null)
                {
                    Console.WriteLine("Decoded Mailmark2D fields:");
                    Console.WriteLine($"UPUCountryID: {decoded.UPUCountryID}");
                    Console.WriteLine($"InformationTypeID: {decoded.InformationTypeID}");
                    Console.WriteLine($"VersionID: {decoded.VersionID}");
                    Console.WriteLine($"Class: {decoded.Class}");
                    Console.WriteLine($"RTSFlag: {decoded.RTSFlag}");
                    Console.WriteLine($"SupplyChainID: {decoded.SupplyChainID}");
                    Console.WriteLine($"ItemID: {decoded.ItemID}");
                    Console.WriteLine($"DestinationPostCodeAndDPS: {decoded.DestinationPostCodeAndDPS}");
                    Console.WriteLine($"ReturnToSenderPostCode: {decoded.ReturnToSenderPostCode}");
                    Console.WriteLine($"CustomerContent: {decoded.CustomerContent}");
                }
            }
        }
    }
}