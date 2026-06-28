using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates serialization of a Mailmark2DCodetext object to JSON,
/// deserialization back to an object, and generation of a barcode image
/// using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // 1. Create a sample Mailmark2DCodetext object with valid data
        // ------------------------------------------------------------
        var mailmark2d = new Mailmark2DCodetext
        {
            UPUCountryID = "JGB ",
            InformationTypeID = "0",
            VersionID = "1",
            Class = "1",
            SupplyChainID = 123,
            ItemID = 1234,
            DestinationPostCodeAndDPS = "EF61AH8T ",
            RTSFlag = "0",
            ReturnToSenderPostCode = "SW1A1AA",
            DataMatrixType = Mailmark2DType.Type_7,
            CustomerContent = "CUSTOM",
            CustomerContentEncodeMode = DataMatrixEncodeMode.C40
        };

        // ------------------------------------------------------------
        // 2. Serialize the object to a formatted JSON string and write to file
        // ------------------------------------------------------------
        var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(mailmark2d, jsonOptions);
        const string jsonPath = "mailmark2d.json";
        File.WriteAllText(jsonPath, json);
        Console.WriteLine($"Serialized Mailmark2DCodetext to {jsonPath}");

        // ------------------------------------------------------------
        // 3. Read the JSON file back and deserialize to a Mailmark2DCodetext instance
        // ------------------------------------------------------------
        if (!File.Exists(jsonPath))
        {
            Console.WriteLine("JSON file not found. Exiting.");
            return;
        }

        string jsonRead = File.ReadAllText(jsonPath);
        var deserialized = JsonSerializer.Deserialize<Mailmark2DCodetext>(jsonRead);
        if (deserialized == null)
        {
            Console.WriteLine("Failed to deserialize JSON. Exiting.");
            return;
        }
        Console.WriteLine("Deserialized Mailmark2DCodetext from JSON.");

        // ------------------------------------------------------------
        // 4. Generate a barcode image from the deserialized object and save as PNG
        // ------------------------------------------------------------
        using (var generator = new ComplexBarcodeGenerator(deserialized))
        {
            // Save barcode to a memory stream in PNG format
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading

                const string imagePath = "mailmark2d.png";
                // Write the memory stream contents to a file
                using (var fileStream = new FileStream(imagePath, FileMode.Create, FileAccess.Write))
                {
                    ms.CopyTo(fileStream);
                }
                Console.WriteLine($"Barcode image saved to {imagePath}");
            }
        }
    }
}