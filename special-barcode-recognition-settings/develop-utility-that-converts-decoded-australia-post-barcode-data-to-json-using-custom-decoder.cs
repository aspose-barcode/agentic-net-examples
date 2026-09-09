// Title: Convert Australia Post Barcode Data to JSON with a Custom Decoder
// Description: Demonstrates generating an Australia Post barcode, decoding it using a custom customer‑information decoder, and outputting the result as formatted JSON.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It shows how to use BarcodeGenerator to create a barcode, BarCodeReader to read it, and how to plug in a custom AustraliaPostCustomerInformationDecoder. Typical use cases include processing Australia Post barcodes in logistics applications, extracting embedded customer information, and converting the data to interoperable formats such as JSON. Developers often need to customize decoding logic while leveraging Aspose.BarCode's high‑level API.
// Prompt: Develop a utility that converts decoded Australia Post barcode data to JSON using a custom decoder.
// Tags: australia post,barcode,generation,recognition,custom decoder,json,aspose.barcode

using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Custom decoder for Australia Post customer information fields.
/// In this simple example the decoder returns the raw field unchanged,
/// but developers can extend it to implement proprietary parsing logic.
/// </summary>
public class CustomAustraliaPostDecoder : AustraliaPostCustomerInformationDecoder
{
    /// <summary>
    /// Decodes the supplied customer information field.
    /// </summary>
    /// <param name="customerInformationField">Raw field extracted from the barcode.</param>
    /// <returns>Decoded string (unchanged in this demo).</returns>
    public string Decode(string customerInformationField)
    {
        // Simple custom decoding: return the field unchanged.
        return customerInformationField;
    }
}

/// <summary>
/// Entry point for the Australia Post barcode generation, decoding, and JSON conversion demo.
/// </summary>
public class Program
{
    /// <summary>
    /// Generates a sample Australia Post barcode, reads it with a custom decoder,
    /// serializes the result to JSON, writes the JSON to the console, and cleans up temporary files.
    /// </summary>
    public static void Main()
    {
        // Create a unique temporary folder for generated files.
        string tempFolder = Path.Combine(Path.GetTempPath(), "AustraliaPostDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the full path for the barcode image.
        string imagePath = Path.Combine(tempFolder, "barcode.png");

        // ------------------------------------------------------------
        // Generate a sample Australia Post barcode.
        // ------------------------------------------------------------
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, "6201234567ASPOSE"))
        {
            // Set visual parameters.
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Parameters.Barcode.BarHeight.Pixels = 50f;

            // Use the C table for customer information encoding.
            generator.Parameters.Barcode.AustralianPost.EncodingTable = CustomerInformationInterpretingType.CTable;

            // Save the barcode as a PNG image.
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // ------------------------------------------------------------
        // Read the barcode using a custom decoder.
        // ------------------------------------------------------------
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.AustraliaPost))
        {
            // Assign the custom decoder to interpret the customer information field.
            reader.BarcodeSettings.AustraliaPost.CustomerInformationDecoder = new CustomAustraliaPostDecoder();

            // Iterate through all detected barcodes (normally one in this demo).
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // Prepare an anonymous object with the desired output fields.
                var output = new
                {
                    CodeType = result.CodeTypeName,
                    CodeText = result.CodeText
                };

                // Serialize the object to indented JSON.
                string json = JsonSerializer.Serialize(output, new JsonSerializerOptions { WriteIndented = true });

                // Write the JSON to the console.
                Console.WriteLine(json);
            }
        }

        // ------------------------------------------------------------
        // Clean up temporary files and directories.
        // ------------------------------------------------------------
        try
        {
            if (File.Exists(imagePath))
                File.Delete(imagePath);

            if (Directory.Exists(tempFolder))
                Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored – cleanup failure should not affect program exit.
        }
    }
}