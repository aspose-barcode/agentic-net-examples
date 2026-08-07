// Title: Generate Mailmark 2D Barcode from JSON
// Description: Demonstrates deserializing a Mailmark2DCodetext JSON payload and creating a Mailmark 2D barcode image using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of ComplexBarcodeGenerator and Mailmark2DCodetext to produce Mailmark 2D symbols, a common requirement for postal and logistics applications. Developers often need to convert structured data (e.g., JSON) into barcode images for printing or electronic transmission.
// Prompt: Deserialize JSON back into a Mailmark2DCodetext instance and generate the corresponding barcode.
// Tags: mailmark, 2d barcode, json deserialization, aspose.barcode, complexbarcode, png, generation

using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that deserializes a Mailmark2DCodetext object from JSON
/// and generates a Mailmark 2D barcode image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs JSON deserialization and barcode generation.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // 1. Define a JSON string that represents a Mailmark2DCodetext instance.
        //    Adjust the values as needed for your specific scenario.
        // --------------------------------------------------------------------
        string json = @"{
            ""VersionID"": ""1"",
            ""InformationTypeID"": ""0"",
            ""Class"": ""1"",
            ""RTSFlag"": ""0"",
            ""ItemID"": 16563762,
            ""SupplyChainID"": 384224,
            ""DestinationPostCodeAndDPS"": ""EF61AH8T "",
            ""UPUCountryID"": ""GB"",
            ""CustomerContent"": ""SampleCustomerData"",
            ""CustomerContentEncodeMode"": 0,
            ""DataMatrixType"": 0
        }";

        // --------------------------------------------------------------------
        // 2. Deserialize the JSON into a Mailmark2DCodetext object.
        //    Handle possible errors and null results gracefully.
        // --------------------------------------------------------------------
        Mailmark2DCodetext mailmark;
        try
        {
            mailmark = JsonSerializer.Deserialize<Mailmark2DCodetext>(json);
            if (mailmark == null)
            {
                Console.WriteLine("Deserialization returned null.");
                return;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to deserialize JSON: {ex.Message}");
            return;
        }

        // --------------------------------------------------------------------
        // 3. Generate the Mailmark 2D barcode using ComplexBarcodeGenerator.
        //    The barcode is saved as a PNG image to the specified path.
        // --------------------------------------------------------------------
        const string outputPath = "mailmark2d.png";
        try
        {
            using (var generator = new ComplexBarcodeGenerator(mailmark))
            {
                // Save the barcode image as PNG.
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }

            Console.WriteLine($"Barcode generated and saved to '{Path.GetFullPath(outputPath)}'.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Barcode generation failed: {ex.Message}");
        }
    }
}