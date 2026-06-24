using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Mailmark 2D barcode from JSON data using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Sample JSON representing a Mailmark2DCodetext object.
        // In a real scenario this could be read from a file or other source.
        string json = @"{
            ""VersionID"": ""1"",
            ""InformationTypeID"": ""0"",
            ""Class"": ""1"",
            ""RTSFlag"": ""0"",
            ""SupplyChainID"": 384224,
            ""ItemID"": 16563762,
            ""DestinationPostCodeAndDPS"": ""EF61AH8T "",
            ""ReturnToSenderPostCode"": ""SW1A1AA"",
            ""UPUCountryID"": ""GB"",
            ""DataMatrixType"": 0,
            ""CustomerContent"": ""Sample customer data"",
            ""CustomerContentEncodeMode"": 0
        }";

        // Deserialize JSON into a Mailmark2DCodetext instance.
        Mailmark2DCodetext mailmark2d;
        try
        {
            mailmark2d = JsonSerializer.Deserialize<Mailmark2DCodetext>(json);
            if (mailmark2d == null)
                throw new ArgumentException("Deserialization resulted in null.");
        }
        catch (Exception ex)
        {
            // Output error and abort if JSON cannot be deserialized.
            Console.WriteLine($"Failed to deserialize JSON: {ex.Message}");
            return;
        }

        // Validate that required single‑character string properties meet API constraints.
        if (mailmark2d.VersionID?.Length != 1 ||
            mailmark2d.InformationTypeID?.Length != 1 ||
            mailmark2d.Class?.Length != 1 ||
            mailmark2d.RTSFlag?.Length != 1)
        {
            Console.WriteLine("One of the required single‑character string properties is invalid.");
            return;
        }

        // Define the output file path for the generated barcode image.
        string outputPath = "mailmark2d.png";

        // Generate the Mailmark 2D barcode using ComplexBarcodeGenerator.
        try
        {
            using (var generator = new ComplexBarcodeGenerator(mailmark2d))
            {
                // Save the barcode image to a PNG file.
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }

            // Inform the user where the barcode image was saved.
            Console.WriteLine($"Barcode generated and saved to '{Path.GetFullPath(outputPath)}'.");
        }
        catch (Exception ex)
        {
            // Output any errors that occur during barcode generation.
            Console.WriteLine($"Barcode generation failed: {ex.Message}");
        }
    }
}