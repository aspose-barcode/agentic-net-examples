// Title: Serialize Mailmark2D Code Text to JSON
// Description: Demonstrates how to serialize a Mailmark2DCodetext object to JSON, store it in a file, and later reconstruct it for barcode generation.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode operations category, focusing on Mailmark 2D code handling. It showcases the use of Aspose.BarCode.ComplexBarcode classes such as Mailmark2DCodetext and related enums, combined with .NET System.Text.Json for serialization. Developers working with postal barcodes often need to persist code text configurations, and this pattern provides a reusable approach for storage and retrieval.
// Prompt: Serialize a Mailmark2DCodetext object to JSON for storage and later reconstruction in applications.
// Tags: barcode, serialization, json, mailmark2d, aspose.barcode

using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that serializes and deserializes a <see cref="Mailmark2DCodetext"/> object using JSON.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a Mailmark2DCodetext, writes it to JSON, reads it back, and prints the values.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // 1. Create and populate a Mailmark2DCodetext instance
        // ------------------------------------------------------------
        var mailmark2D = new Mailmark2DCodetext
        {
            UPUCountryID = "JGB ",
            InformationTypeID = "0",
            VersionID = "1",
            Class = "1",
            SupplyChainID = 123,
            ItemID = 1234,
            DestinationPostCodeAndDPS = "QWE1",
            RTSFlag = "0",
            ReturnToSenderPostCode = "QWE2",
            DataMatrixType = Mailmark2DType.Type_7,
            CustomerContent = "CUSTOM",
            CustomerContentEncodeMode = DataMatrixEncodeMode.C40
        };

        // ------------------------------------------------------------
        // 2. Serialize the object to a formatted JSON string
        // ------------------------------------------------------------
        var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(mailmark2D, jsonOptions);

        // ------------------------------------------------------------
        // 3. Save the JSON string to a file
        // ------------------------------------------------------------
        const string filePath = "mailmark2d.json";
        using (var writeStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
        using (var writer = new StreamWriter(writeStream))
        {
            writer.Write(json);
        }

        // ------------------------------------------------------------
        // 4. Load the JSON from the file and deserialize back to an object
        // ------------------------------------------------------------
        Mailmark2DCodetext deserialized;
        using (var readStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        using (var reader = new StreamReader(readStream))
        {
            string jsonFromFile = reader.ReadToEnd();
            deserialized = JsonSerializer.Deserialize<Mailmark2DCodetext>(jsonFromFile);
        }

        // ------------------------------------------------------------
        // 5. Output selected fields to verify successful reconstruction
        // ------------------------------------------------------------
        Console.WriteLine($"UPUCountryID: {deserialized?.UPUCountryID}");
        Console.WriteLine($"InformationTypeID: {deserialized?.InformationTypeID}");
        Console.WriteLine($"VersionID: {deserialized?.VersionID}");
        Console.WriteLine($"Class: {deserialized?.Class}");
        Console.WriteLine($"SupplyChainID: {deserialized?.SupplyChainID}");
        Console.WriteLine($"ItemID: {deserialized?.ItemID}");
        Console.WriteLine($"DestinationPostCodeAndDPS: {deserialized?.DestinationPostCodeAndDPS}");
        Console.WriteLine($"RTSFlag: {deserialized?.RTSFlag}");
        Console.WriteLine($"ReturnToSenderPostCode: {deserialized?.ReturnToSenderPostCode}");
        Console.WriteLine($"DataMatrixType: {deserialized?.DataMatrixType}");
        Console.WriteLine($"CustomerContent: {deserialized?.CustomerContent}");
        Console.WriteLine($"CustomerContentEncodeMode: {deserialized?.CustomerContentEncodeMode}");
    }
}