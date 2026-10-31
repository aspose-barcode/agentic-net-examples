// Title: Custom Customer Information Decoder for Australia Post Barcodes
// Description: Demonstrates how to create a custom decoder by inheriting AustraliaPostCustomerInformationDecoder and assign it to the barcode reader settings to interpret the customer information field.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, focusing on custom decoding of Australia Post barcodes. It showcases the use of BarcodeGenerator, BarCodeReader, and the AustraliaPost settings classes. Developers often need to extract and interpret the customer information segment of Australia Post barcodes, and custom decoders provide flexibility for proprietary or simplified decoding logic.
// Prompt: Implement a custom class inheriting CustomerInformationDecoder and assign it to AustraliaPostSettings.CustomDecoder.
// Tags: australia post, barcode, custom decoder, customer information, generation, recognition, aspnet, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Custom decoder that simply returns the raw customer information field unchanged.
/// Inherit from <see cref="AustraliaPostCustomerInformationDecoder"/> to integrate with Aspose.BarCode's Australia Post settings.
/// </summary>
public class MyDecoder : AustraliaPostCustomerInformationDecoder
{
    /// <summary>
    /// Decodes the supplied customer information field.
    /// </summary>
    /// <param name="customerInformationField">The raw customer information string extracted from the barcode.</param>
    /// <returns>The decoded (or in this case, unchanged) customer information.</returns>
    public string Decode(string customerInformationField)
    {
        // Simple pass‑through decoder – no transformation applied.
        return customerInformationField;
    }
}

class Program
{
    /// <summary>
    /// Entry point of the example. Generates an Australia Post barcode, reads it back,
    /// applies the custom decoder, and displays the results.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for the demo files.
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeBarCodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string barcodePath = Path.Combine(tempDir, "AustraliaPost.png");

        // ------------------------------------------------------------
        // Generate an Australia Post barcode using CTable encoding.
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, "6201234567AB"))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Parameters.Barcode.BarHeight.Pixels = 50f;
            generator.Parameters.Barcode.AustralianPost.EncodingTable = CustomerInformationInterpretingType.CTable;
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // ------------------------------------------------------------
        // Read the barcode and assign the custom decoder.
        // ------------------------------------------------------------
        using (var reader = new BarCodeReader(barcodePath, DecodeType.AustraliaPost))
        {
            // Attach the custom decoder to the Australia Post settings.
            reader.BarcodeSettings.AustraliaPost.CustomerInformationDecoder = new MyDecoder();

            // Iterate through all detected barcodes (only one in this demo).
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"CodeType: {result.CodeTypeName}");
                Console.WriteLine($"Full CodeText: {result.CodeText}");

                // Extract the customer information field (characters after the first 10).
                string customerInfo = result.CodeText.Length > 10 ? result.CodeText.Substring(10) : string.Empty;

                // Use the custom decoder to interpret the extracted field.
                string decodedInfo = ((MyDecoder)reader.BarcodeSettings.AustraliaPost.CustomerInformationDecoder).Decode(customerInfo);
                Console.WriteLine($"Decoded Customer Information: {decodedInfo}");
            }
        }

        // ------------------------------------------------------------
        // Clean up temporary files and directories.
        // ------------------------------------------------------------
        try
        {
            if (File.Exists(barcodePath))
                File.Delete(barcodePath);
            Directory.Delete(tempDir, true);
        }
        catch
        {
            // Ignored – cleanup failure should not affect program exit.
        }
    }
}