// Title: Australia Post NTable Barcode to JSON Converter
// Description: Generates an Australia Post barcode, decodes its customer information using a custom N‑Table decoder, and outputs the result as formatted JSON.
// Category-Description: This example demonstrates Aspose.BarCode generation and recognition for Australia Post symbology. It showcases the use of BarcodeGenerator, BarCodeReader, and a custom CustomerInformationDecoder to interpret N‑Table encoded data. Developers working with postal services often need to extract and transform barcode payloads into machine‑readable formats such as JSON.
// Prompt: Develop a utility that converts decoded Australia Post barcode data to JSON using a custom decoder.
// Tags: australia post,barcode,decoding,json,custom decoder,aspose.barcode

using System;
using System.IO;
using System.Text;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Custom decoder that interprets the N‑Table customer information field of an Australia Post barcode.
/// </summary>
class NTableDecoder : AustraliaPostCustomerInformationDecoder
{
    // Mapping table for N‑Table encoding (pairs of digits to single numeric values)
    private readonly string[] _nTable = { "00", "01", "02", "10", "11", "12", "20", "21", "22", "30" };

    /// <summary>
    /// Decodes the supplied customer information field using the N‑Table mapping.
    /// </summary>
    /// <param name="customerInformationField">Raw customer information string extracted from the barcode.</param>
    /// <returns>Decoded numeric string.</returns>
    public string Decode(string customerInformationField)
    {
        var sb = new StringBuilder();

        // Process the field two characters at a time
        for (int i = 0; i < customerInformationField.Length; i += 2)
        {
            if (i + 2 <= customerInformationField.Length)
            {
                string pair = customerInformationField.Substring(i, 2);

                // Find the pair in the N‑Table and append its index
                for (int j = 0; j < _nTable.Length; j++)
                {
                    if (_nTable[j] == pair)
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
/// Demonstrates generation, reading, custom decoding, and JSON serialization of an Australia Post barcode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, decodes it with a custom N‑Table decoder, and prints JSON output.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare a temporary working folder for barcode image generation.
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "AusPostDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "AustraliaPost.png");

        // --------------------------------------------------------------------
        // Generate a sample Australia Post barcode using the N‑Table encoding.
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, "620123456701234"))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Parameters.Barcode.BarHeight.Pixels = 50f;
            generator.Parameters.Barcode.AustralianPost.EncodingTable = CustomerInformationInterpretingType.NTable;
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // --------------------------------------------------------------------
        // Set up the barcode reader with the custom N‑Table decoder.
        // --------------------------------------------------------------------
        BaseDecodeType decodeType = DecodeType.AustraliaPost;
        using (var reader = new BarCodeReader(barcodePath, decodeType))
        {
            reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = CustomerInformationInterpretingType.NTable;
            reader.BarcodeSettings.AustraliaPost.CustomerInformationDecoder = new NTableDecoder();

            // ----------------------------------------------------------------
            // Read each barcode, extract and decode the customer information,
            // then serialize the result to formatted JSON.
            // ----------------------------------------------------------------
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                string fullCode = result.CodeText ?? string.Empty;

                // Customer information starts after the first 10 characters of the full code.
                string customerInfoField = fullCode.Length > 10 ? fullCode.Substring(10) : string.Empty;

                // Use the custom decoder to interpret the N‑Table field.
                string decodedCustomerInfo = ((NTableDecoder)reader.BarcodeSettings.AustraliaPost.CustomerInformationDecoder)
                    .Decode(customerInfoField);

                var jsonObj = new
                {
                    CodeType = result.CodeTypeName,
                    CodeText = fullCode,
                    DecodedCustomerInfo = decodedCustomerInfo
                };

                string json = JsonSerializer.Serialize(jsonObj, new JsonSerializerOptions { WriteIndented = true });
                Console.WriteLine(json);
            }
        }

        // --------------------------------------------------------------------
        // Clean up temporary files and directories.
        // --------------------------------------------------------------------
        try
        {
            if (File.Exists(barcodePath))
                File.Delete(barcodePath);
            if (Directory.Exists(tempFolder))
                Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored – cleanup failures should not affect program exit.
        }
    }
}