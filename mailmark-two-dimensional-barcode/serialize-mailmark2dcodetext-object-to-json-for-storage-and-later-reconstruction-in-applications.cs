// Title: Serialize and Deserialize Mailmark2DCodetext to JSON and Generate Barcode Image
// Description: Demonstrates how to serialize a Mailmark2DCodetext object to JSON for storage, deserialize it back, and generate a barcode image using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category, showcasing the use of Mailmark2DCodetext, ComplexBarcodeGenerator, and related parameter settings. Developers working with postal and logistics solutions often need to persist barcode data, reconstruct it, and render visual representations. The snippet illustrates typical serialization, deserialization, and image generation workflows for such scenarios.
// Prompt: Serialize a Mailmark2DCodetext object to JSON for storage and later reconstruction in applications.
// Tags: mailmark, json, serialization, deserialization, barcode, complexbarcode, aspose.barcode, csharp

using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that serializes a <see cref="Mailmark2DCodetext"/> to JSON,
/// deserializes it, and generates a barcode image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs serialization, deserialization,
    /// and barcode image generation.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // Create a sample Mailmark2DCodetext object with test data.
        // ------------------------------------------------------------
        var mailmark = new Mailmark2DCodetext
        {
            InformationTypeID = "0",
            VersionID = "1",
            Class = "1",
            RTSFlag = "0",
            SupplyChainID = 384224,
            ItemID = 16563762,
            DestinationPostCodeAndDPS = "EF61AH8T ",
            CustomerContent = "CUST12", // max 6 characters
            CustomerContentEncodeMode = DataMatrixEncodeMode.C40,
            DataMatrixType = Mailmark2DType.Type_7
        };

        // ------------------------------------------------------------
        // Serialize the Mailmark2DCodetext instance to a formatted JSON string.
        // ------------------------------------------------------------
        string json = JsonSerializer.Serialize(
            mailmark,
            new JsonSerializerOptions { WriteIndented = true });

        // Write the JSON to a temporary file for later retrieval.
        string jsonPath = Path.Combine(Path.GetTempPath(), "mailmark2d.json");
        File.WriteAllText(jsonPath, json);
        Console.WriteLine($"Serialized Mailmark2DCodetext to: {jsonPath}");

        // ------------------------------------------------------------
        // Read the JSON back from the file and deserialize it into an object.
        // ------------------------------------------------------------
        string readJson = File.ReadAllText(jsonPath);
        var deserializedMailmark = JsonSerializer.Deserialize<Mailmark2DCodetext>(readJson);
        if (deserializedMailmark == null)
        {
            Console.WriteLine("Deserialization failed.");
            return;
        }
        Console.WriteLine("Deserialization succeeded.");

        // ------------------------------------------------------------
        // Generate a barcode image from the deserialized Mailmark2DCodetext.
        // ------------------------------------------------------------
        string imagePath = Path.Combine(Path.GetTempPath(), "mailmark2d.png");
        using (var generator = new ComplexBarcodeGenerator(deserializedMailmark))
        {
            // Set the X-dimension (module size) to 4 pixels for better readability.
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }
        Console.WriteLine($"Generated barcode image at: {imagePath}");
    }
}