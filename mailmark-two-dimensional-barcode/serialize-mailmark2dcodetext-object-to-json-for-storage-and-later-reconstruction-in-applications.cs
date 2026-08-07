// Title: Serialize Mailmark2D Code Text to JSON and Generate Barcode
// Description: This example creates a Mailmark2DCodetext object, serializes it to JSON for persistence, deserializes it back, and generates a Mailmark 2D barcode image.
// Category-Description: Demonstrates Aspose.BarCode complex barcode generation workflow, covering object serialization with System.Text.Json, deserialization, and barcode image creation using ComplexBarcodeGenerator. Developers working with Mailmark 2D symbology often need to store code text configurations and later reconstruct them for barcode rendering in applications.
// Prompt: Serialize a Mailmark2DCodetext object to JSON for storage and later reconstruction in applications.
// Tags: mailmark2d, json, serialization, deserialization, barcode generation, complexbarcode, aspnet.barcode, system.text.json, png

using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates serialization of Mailmark2DCodetext to JSON and barcode generation.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates, serializes, deserializes Mailmark2DCodetext and generates a barcode image.
    /// </summary>
    static void Main()
    {
        // Create a sample Mailmark2DCodetext with required fields
        var mailmark2d = new Mailmark2DCodetext
        {
            // Single‑character string values as required by the API
            VersionID = "1",
            InformationTypeID = "0",
            Class = "1",
            RTSFlag = "0",

            // Integer identifiers
            ItemID = 16563762,
            SupplyChainID = 384224,

            // Destination postcode with DPS (trailing space is required)
            DestinationPostCodeAndDPS = "EF61AH8T ",

            // Optional fields (left as defaults or set as needed)
            // CustomerContent = "Optional customer data",
            // CustomerContentEncodeMode = DataMatrixEncodeMode.C40,
            // DataMatrixType = Mailmark2DType.Auto,
            // ReturnToSenderPostCode = "SW1A1AA",
            // UPUCountryID = "GB"
        };

        // Serialize the object to JSON with indentation for readability
        var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(mailmark2d, jsonOptions);

        // Store JSON to a file
        const string jsonPath = "mailmark2d.json";
        File.WriteAllText(jsonPath, json);
        Console.WriteLine($"Mailmark2DCodetext serialized to {jsonPath}");

        // Read JSON back from the file
        string jsonFromFile = File.ReadAllText(jsonPath);
        var deserializedMailmark2d = JsonSerializer.Deserialize<Mailmark2DCodetext>(jsonFromFile);
        if (deserializedMailmark2d == null)
        {
            Console.WriteLine("Deserialization failed.");
            return;
        }
        Console.WriteLine("Mailmark2DCodetext deserialized successfully.");

        // Generate a barcode image from the deserialized object
        const string imagePath = "mailmark2d.png";
        using (var generator = new ComplexBarcodeGenerator(deserializedMailmark2d))
        {
            // Save the barcode image to a PNG file
            generator.Save(imagePath);
        }
        Console.WriteLine($"Barcode image saved to {imagePath}");
    }
}