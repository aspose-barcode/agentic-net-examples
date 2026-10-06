// Title: Serialize and Deserialize Mailmark2D Code Text to JSON and Generate Barcode
// Description: Demonstrates how to serialize a Mailmark2DCodetext object to JSON, store it, deserialize it back, and generate a Mailmark 2‑D barcode image.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of Aspose.BarCode.ComplexBarcode and Aspose.BarCode.Generation APIs to work with Mailmark2D symbology, a postal barcode used for tracking and sorting. Developers often need to persist barcode data (e.g., in databases or files) and later reconstruct it for rendering or validation; this snippet illustrates JSON serialization/deserialization as a common approach.
// Prompt: Serialize a Mailmark2DCodetext object to JSON for storage and later reconstruction in applications.
// Tags: barcode, serialization, json, mailmark2d, aspose.barcode, complexbarcode, csharp

using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that serializes a <see cref="Mailmark2DCodetext"/> to JSON,
/// deserializes it, and generates a Mailmark 2‑D barcode image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs serialization, deserialization,
    /// and barcode image generation.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare a temporary working directory for output files
        // --------------------------------------------------------------------
        string workDir = Path.Combine(Path.GetTempPath(), "Mailmark2DExample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workDir);

        // --------------------------------------------------------------------
        // Create and populate a Mailmark2DCodetext instance with sample data
        // --------------------------------------------------------------------
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
            CustomerContent = "CUSTOM",
            CustomerContentEncodeMode = DataMatrixEncodeMode.C40,
            DataMatrixType = Mailmark2DType.Type_7
        };

        // --------------------------------------------------------------------
        // Serialize the Mailmark2DCodetext object to a formatted JSON string
        // --------------------------------------------------------------------
        string json = JsonSerializer.Serialize(mailmark, new JsonSerializerOptions { WriteIndented = true });
        string jsonPath = Path.Combine(workDir, "mailmark2d.json");
        File.WriteAllText(jsonPath, json);
        Console.WriteLine("Serialized JSON:");
        Console.WriteLine(json);

        // --------------------------------------------------------------------
        // Deserialize the JSON back into a Mailmark2DCodetext instance
        // --------------------------------------------------------------------
        var deserialized = JsonSerializer.Deserialize<Mailmark2DCodetext>(json);
        if (deserialized == null)
        {
            Console.WriteLine("Deserialization failed.");
            return;
        }

        // --------------------------------------------------------------------
        // Generate a barcode image from the deserialized object
        // --------------------------------------------------------------------
        string imagePath = Path.Combine(workDir, "mailmark2d.png");
        using (var generator = new ComplexBarcodeGenerator(deserialized))
        {
            // Adjust the X-dimension (module size) for better readability
            generator.Parameters.Barcode.XDimension.Pixels = 4;
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"Barcode image saved to: {imagePath}");
        Console.WriteLine("Program completed successfully.");
    }
}