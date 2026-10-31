// Title: Serialize Mailmark2DCodetext to JSON and deserialize it
// Description: Demonstrates how to serialize a Mailmark2DCodetext object to JSON for storage and later reconstruct it in an application.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category, showcasing the use of Mailmark2DCodetext and related classes. It illustrates typical workflows such as creating barcode data objects, persisting them in JSON format, and restoring them, which developers often need when integrating barcode data with external systems or databases.
// Prompt: Serialize a Mailmark2DCodetext object to JSON for storage and later reconstruction in applications.
// Tags: barcode, serialization, json, mailmark, aspose.barcode, complexbarcode

using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that serializes a <see cref="Mailmark2DCodetext"/> instance to JSON,
/// writes it to a temporary file, reads it back, and deserializes it to verify the data.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs creation, serialization, file I/O, and deserialization
    /// of a Mailmark2DCodetext object.
    /// </summary>
    static void Main()
    {
        // Create a sample Mailmark2DCodetext with representative values
        var mailmark = new Mailmark2DCodetext
        {
            UPUCountryID = "JGB ",
            InformationTypeID = "0",
            VersionID = "1",
            Class = "1",
            SupplyChainID = 384224,
            ItemID = 16563762,
            DestinationPostCodeAndDPS = "EF61AH8T ",
            RTSFlag = "0",
            ReturnToSenderPostCode = " QWE2 ",
            CustomerContent = "CUSTOM123",
            CustomerContentEncodeMode = DataMatrixEncodeMode.C40,
            DataMatrixType = Mailmark2DType.Type_9
        };

        // Serialize the object to a formatted JSON string
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(mailmark, options);

        // Determine a temporary file path for storing the JSON
        string filePath = Path.Combine(Path.GetTempPath(), "mailmark2d.json");

        // Write the JSON content to the file
        File.WriteAllText(filePath, json);
        Console.WriteLine($"Serialized Mailmark2DCodetext saved to: {filePath}");

        // Read the JSON back from the file and deserialize it into a new object
        string readJson = File.ReadAllText(filePath);
        var deserialized = JsonSerializer.Deserialize<Mailmark2DCodetext>(readJson);

        // Output selected fields to verify successful deserialization
        if (deserialized != null)
        {
            Console.WriteLine("Deserialized Mailmark2DCodetext:");
            Console.WriteLine($"UPUCountryID: {deserialized.UPUCountryID}");
            Console.WriteLine($"InformationTypeID: {deserialized.InformationTypeID}");
            Console.WriteLine($"VersionID: {deserialized.VersionID}");
            Console.WriteLine($"Class: {deserialized.Class}");
            Console.WriteLine($"SupplyChainID: {deserialized.SupplyChainID}");
            Console.WriteLine($"ItemID: {deserialized.ItemID}");
            Console.WriteLine($"DestinationPostCodeAndDPS: {deserialized.DestinationPostCodeAndDPS}");
            Console.WriteLine($"CustomerContent: {deserialized.CustomerContent}");
        }
        else
        {
            Console.WriteLine("Failed to deserialize the JSON.");
        }
    }
}