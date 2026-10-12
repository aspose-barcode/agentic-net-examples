// Title: Australia Post Barcode to JSON Converter
// Description: Demonstrates generating an Australia Post barcode, decoding its customer information with a custom N‑Table decoder, and outputting the results as JSON.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator, BarCodeReader, and custom CustomerInformationDecoder implementations to process Australia Post barcodes. Developers often need to extract and transform barcode data into structured formats such as JSON for integration with downstream systems.
// Prompt: Develop a utility that converts decoded Australia Post barcode data to JSON using a custom decoder.
// Tags: australia post, barcode generation, barcode recognition, custom decoder, json output, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Custom decoder that interprets the N‑Table customer information field for Australia Post barcodes.
/// </summary>
class NTableDecoder : AustraliaPostCustomerInformationDecoder
{
    public string Decode(string customerInformationField)
    {
        // Mapping table for N‑Table values
        string[] N_Table = { "00", "01", "02", "10", "11", "12", "20", "21", "22", "30" };
        var sb = new System.Text.StringBuilder();

        // Process the field two characters at a time
        for (int i = 0; i < customerInformationField.Length; i += 2)
        {
            if (i + 2 <= customerInformationField.Length)
            {
                string tmp = customerInformationField.Substring(i, 2);
                // Find the index of the matching N‑Table entry
                for (int j = 0; j < N_Table.Length; j++)
                {
                    if (N_Table[j] == tmp)
                    {
                        sb.Append(j);
                        break;
                    }
                }
            }
        }
        return sb.ToString();
    }
}

/// <summary>
/// Simple DTO to hold barcode recognition results for JSON serialization.
/// </summary>
class ResultInfo
{
    public string CodeTypeName { get; set; }
    public string CodeText { get; set; }
    public string DecodedCustomerInfo { get; set; }
}

/// <summary>
/// Demonstrates generating an Australia Post barcode, decoding it with a custom decoder,
/// and serializing the results to JSON.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, reads it, applies the custom N‑Table decoder,
    /// and prints the JSON representation of the results.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder to store the generated barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "AustraliaPostDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string imagePath = Path.Combine(tempFolder, "barcode.png");

        // Generate an Australia Post barcode with specific encoding settings
        using (var generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, "620123456701234"))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Parameters.Barcode.BarHeight.Pixels = 50f;
            generator.Parameters.Barcode.AustralianPost.EncodingTable = CustomerInformationInterpretingType.NTable;
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        var results = new List<ResultInfo>();

        // Read the barcode image using a custom decoder for the customer information field
        using (var reader = new BarCodeReader(imagePath, DecodeType.AustraliaPost))
        {
            reader.BarcodeSettings.AustraliaPost.CustomerInformationDecoder = new NTableDecoder();

            foreach (var result in reader.ReadBarCodes())
            {
                var info = new ResultInfo
                {
                    CodeTypeName = result.CodeTypeName,
                    CodeText = result.CodeText,
                    // In this demo, CodeText already reflects the decoded customer information
                    DecodedCustomerInfo = result.CodeText
                };
                results.Add(info);
            }
        }

        // Serialize the list of results to formatted JSON
        string json = JsonSerializer.Serialize(results, new JsonSerializerOptions { WriteIndented = true });
        Console.WriteLine(json);
    }
}