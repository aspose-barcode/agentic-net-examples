// Title: Australia Post Barcode to JSON Converter with Custom Decoder
// Description: Demonstrates generating an Australia Post barcode, decoding it with a custom customer information decoder, and outputting the extracted data as formatted JSON.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes, BarCodeReader for decoding, and the custom AustraliaPostCustomerInformationDecoder interface for handling customer‑specific fields. Developers often need to generate barcodes for shipping, scan them in automated workflows, and transform the raw data into structured formats such as JSON for downstream processing.
// Prompt: Develop a utility that converts decoded Australia Post barcode data to JSON using a custom decoder.
// Tags: australia post,barcode,generation,recognition,custom decoder,json,aspose.barcode

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
    /// Custom decoder implementing the interface for customer information field.
    /// For demonstration it simply returns the raw field data.
    /// </summary>
    public class MyCustomerInfoDecoder : AustraliaPostCustomerInformationDecoder
    {
        // Real implementation would decode based on CTable/NTable rules.
        public string Decode(string customerInformationField)
        {
            return customerInformationField ?? string.Empty;
        }
    }

    /// <summary>
    /// Entry point for the Australia Post barcode utility.
    /// Generates a barcode, decodes it with a custom decoder, and prints JSON output.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Main method that orchestrates barcode generation, decoding, and JSON serialization.
        /// </summary>
        /// <param name="args">Command‑line arguments (not used).</param>
        static void Main(string[] args)
        {
            // Sample data: FCC = "59", DPID = "12345678", customer info = "AB" (CTable, up to 5 chars)
            string sampleCodeText = "5912345678AB";

            // Path for temporary barcode image
            string imagePath = Path.Combine(Path.GetTempPath(), "australiapost.png");

            // Generate Australia Post barcode and save as PNG
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, sampleCodeText))
            {
                // Use CTable interpreting type for customer information
                generator.Parameters.Barcode.AustralianPost.EncodingTable = CustomerInformationInterpretingType.CTable;
                generator.Save(imagePath, BarCodeImageFormat.Png);
            }

            // Verify the image was created
            if (!File.Exists(imagePath))
            {
                Console.WriteLine($"Failed to create barcode image at {imagePath}");
                return;
            }

            // Read and decode the barcode using the same interpreting type
            using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.AustraliaPost))
            {
                reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = CustomerInformationInterpretingType.CTable;
                // Assign custom decoder
                reader.BarcodeSettings.AustraliaPost.CustomerInformationDecoder = new MyCustomerInfoDecoder();

                // Process results (expecting a single barcode)
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    if (string.IsNullOrEmpty(result.CodeText))
                    {
                        Console.WriteLine("No CodeText detected.");
                        continue;
                    }

                    // Parse FCC (first 2 chars) and DPID (next 8 chars)
                    string fcc = result.CodeText.Substring(0, 2);
                    string dpid = result.CodeText.Substring(2, 8);
                    string rawCustomerInfo = result.CodeText.Length > 10 ? result.CodeText.Substring(10) : string.Empty;

                    // Use the custom decoder to interpret the customer information field
                    string decodedCustomerInfo = reader.BarcodeSettings.AustraliaPost.CustomerInformationDecoder.Decode(rawCustomerInfo);

                    // Build an anonymous object for JSON serialization
                    var jsonObject = new
                    {
                        FCC = fcc,
                        DPID = dpid,
                        RawCustomerInfo = rawCustomerInfo,
                        DecodedCustomerInfo = decodedCustomerInfo,
                        Symbology = result.CodeType.ToString()
                    };

                    // Serialize to JSON with indentation and output to console
                    string json = JsonSerializer.Serialize(jsonObject, new JsonSerializerOptions { WriteIndented = true });
                    Console.WriteLine(json);
                }
            }

            // Clean up temporary image file
            try
            {
                File.Delete(imagePath);
            }
            catch
            {
                // Ignored – file may be locked or already removed
            }
        }
    }
}