// Title: Generate Mailmark 2D barcode from JSON data
// Description: Demonstrates deserializing a Mailmark2DCodetext JSON payload and creating the corresponding 2‑D barcode image.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of Aspose.BarCode.ComplexBarcode.ComplexBarcodeGenerator together with Aspose.BarCode.ComplexBarcode.Mailmark2DCodetext to produce Mailmark 2D symbols. Developers working with postal services, logistics, or any scenario requiring Mailmark encoding can follow similar patterns for serialization, deserialization, and barcode rendering.
// Prompt: Deserialize JSON back into a Mailmark2DCodetext instance and generate the corresponding barcode.
// Tags: mailmark,2d barcode,serialization,deserialization,aspose.barcode,generation,json

using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Example program that deserializes a Mailmark2DCodetext JSON string
/// and generates a Mailmark 2D barcode image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs JSON deserialization,
    /// creates a ComplexBarcodeGenerator, and saves the barcode image.
    /// </summary>
    static void Main()
    {
        // Sample JSON representing a Mailmark2DCodetext.
        string json = @"{
            ""Class"": ""1"",
            ""CustomerContent"": ""SampleCustomer"",
            ""CustomerContentEncodeMode"": ""C40"",
            ""DestinationPostCodeAndDPS"": ""EF61AH8T "",
            ""InformationTypeID"": ""0"",
            ""ItemID"": 16563762,
            ""ReturnToSenderPostCode"": ""SW1A1AA"",
            ""RTSFlag"": ""0"",
            ""SupplyChainID"": 384224,
            ""UPUCountryID"": ""GB"",
            ""VersionID"": ""1""
        }";

        // Configure JSON options to handle enums as strings and ignore case.
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
        };

        // Deserialize JSON into the Aspose Mailmark2DCodetext object.
        Mailmark2DCodetext mailmark;
        try
        {
            mailmark = JsonSerializer.Deserialize<Mailmark2DCodetext>(json, options);
            if (mailmark == null)
                throw new InvalidOperationException("Deserialization returned null.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to deserialize JSON: {ex.Message}");
            return;
        }

        // Define the output file path for the generated barcode image.
        string outputPath = "mailmark2d.png";

        // Generate the Mailmark 2D barcode using ComplexBarcodeGenerator.
        try
        {
            using (var generator = new ComplexBarcodeGenerator(mailmark))
            {
                // Optional: set foreground and background colors.
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;

                // Save the barcode image to the specified file.
                generator.Save(outputPath);
            }

            Console.WriteLine($"Barcode generated and saved to '{Path.GetFullPath(outputPath)}'.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Barcode generation failed: {ex.Message}");
        }
    }
}