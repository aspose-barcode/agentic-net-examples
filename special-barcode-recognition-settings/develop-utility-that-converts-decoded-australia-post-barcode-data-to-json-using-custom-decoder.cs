// Title: Australia Post Barcode to JSON Converter Using Custom Decoder
// Description: Demonstrates decoding Australia Post barcodes and converting the extracted customer information into JSON format.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases how to use BarcodeGenerator, BarCodeReader, and custom decoder classes (AustraliaPostCustomerInformationDecoder) to process Australia Post symbology, a common requirement for logistics and mailing applications. Developers often need to extract embedded customer data from barcodes and transform it into structured formats such as JSON.
// Prompt: Develop a utility that converts decoded Australia Post barcode data to JSON using a custom decoder.
// Tags: australia post, barcode, decoding, json, custom decoder, aspose.barcode

using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

namespace AustraliaPostBarcodeUtility
{
    /// <summary>
    /// Custom decoder implementing the Aspose interface for Australia Post customer information.
    /// </summary>
    public class CustomCustomerInfoDecoder : AustraliaPostCustomerInformationDecoder
    {
        // Simple example: just return the raw bar values as decoded text.
        public string Decode(string barValues)
        {
            // In a real scenario, translate bar values (0‑3) to meaningful data here.
            return barValues;
        }
    }

    /// <summary>
    /// Example utility that generates an Australia Post barcode, reads it using a custom decoder,
    /// and outputs the decoded customer information as JSON.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point of the example. Generates a barcode, reads it, decodes customer info,
        /// and prints the JSON representation to the console.
        /// </summary>
        static void Main()
        {
            // Sample Australia Post barcode text (routing + identifier + customer info).
            const string barcodeText = "5912345678ABCde";

            // Create the barcode generator for Australia Post symbology.
            using (var generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, barcodeText))
            {
                // Use CTable interpreting type for customer information.
                generator.Parameters.Barcode.AustralianPost.EncodingTable = CustomerInformationInterpretingType.CTable;

                // Generate the barcode image in memory.
                using (var image = generator.GenerateBarCodeImage())
                {
                    // Set up the reader with the custom decoder.
                    using (var reader = new BarCodeReader(image, DecodeType.AustraliaPost))
                    {
                        // Configure reader to use CTable and the custom decoder.
                        reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = CustomerInformationInterpretingType.CTable;
                        reader.BarcodeSettings.AustraliaPost.CustomerInformationDecoder = new CustomCustomerInfoDecoder();

                        // Perform recognition and process each detected barcode.
                        foreach (var result in reader.ReadBarCodes())
                        {
                            // Full code text from the barcode (may be null).
                            string fullCode = result.CodeText ?? string.Empty;

                            // Extract the customer information part (after first 10 characters).
                            string customerInfoRaw = fullCode.Length > 10 ? fullCode.Substring(10) : string.Empty;

                            // Decode using the custom decoder.
                            string decodedInfo = ((CustomCustomerInfoDecoder)reader.BarcodeSettings.AustraliaPost.CustomerInformationDecoder)
                                .Decode(customerInfoRaw);

                            // Convert decoded information to formatted JSON.
                            string json = JsonSerializer.Serialize(
                                new { CustomerInfo = decodedInfo },
                                new JsonSerializerOptions { WriteIndented = true });

                            // Output the JSON to the console.
                            Console.WriteLine(json);
                        }
                    }
                }
            }
        }
    }
}