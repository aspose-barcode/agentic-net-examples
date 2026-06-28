using System;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

namespace AustraliaPostBarcodeJsonDemo
{
    /// <summary>
    /// Custom decoder implementing the Aspose <see cref="AustraliaPostCustomerInformationDecoder"/> interface.
    /// This example simply maps digits 0‑9 to letters A‑J.
    /// </summary>
    public class SimpleCustomerInfoDecoder : AustraliaPostCustomerInformationDecoder
    {
        /// <summary>
        /// Decodes the supplied data by converting numeric characters to corresponding letters.
        /// Non‑numeric characters are left unchanged.
        /// </summary>
        /// <param name="data">The raw customer information string.</param>
        /// <returns>The decoded string.</returns>
        public string Decode(string data)
        {
            // Return empty string if input is null or empty
            if (string.IsNullOrEmpty(data))
                return string.Empty;

            // Allocate a character array for the result
            char[] result = new char[data.Length];

            // Iterate over each character and apply the simple mapping
            for (int i = 0; i < data.Length; i++)
            {
                char ch = data[i];
                if (ch >= '0' && ch <= '9')
                {
                    // Map 0->A, 1->B, ..., 9->J
                    result[i] = (char)('A' + (ch - '0'));
                }
                else
                {
                    // Preserve non‑numeric characters
                    result[i] = ch;
                }
            }

            // Convert the character array back to a string
            return new string(result);
        }
    }

    class Program
    {
        /// <summary>
        /// Entry point of the demo application.
        /// Generates an Australia Post barcode, reads it back, applies a custom decoder,
        /// and outputs the results as formatted JSON.
        /// </summary>
        static void Main()
        {
            // Sample Australia Post barcode text (includes customer information part)
            const string barcodeText = "5912345678AB";

            // Create a barcode generator for the Australia Post symbology
            using (var generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, barcodeText))
            {
                // Set the interpreting type to CTable for demonstration purposes
                generator.Parameters.Barcode.AustralianPost.AustralianPostEncodingTable = CustomerInformationInterpretingType.CTable;

                // Generate the barcode image
                using (var image = generator.GenerateBarCodeImage())
                {
                    // Initialize a reader configured for Australia Post barcodes
                    using (var reader = new BarCodeReader(image, DecodeType.AustraliaPost))
                    {
                        // Assign the custom decoder and interpreting type to the reader settings
                        reader.BarcodeSettings.AustraliaPost.CustomerInformationDecoder = new SimpleCustomerInfoDecoder();
                        reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = CustomerInformationInterpretingType.CTable;

                        // Perform barcode recognition
                        var results = reader.ReadBarCodes();

                        // Process each recognized barcode
                        foreach (var result in results)
                        {
                            // Decode the raw code text using the custom decoder (demo purpose)
                            string decodedInfo = ((SimpleCustomerInfoDecoder)reader.BarcodeSettings.AustraliaPost.CustomerInformationDecoder)
                                .Decode(result.CodeText);

                            // Build an anonymous object for JSON serialization
                            var jsonObject = new
                            {
                                CodeText = result.CodeText,
                                DecodedCustomerInformation = decodedInfo,
                                InterpretingType = reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType.ToString()
                            };

                            // Serialize the object to indented JSON
                            string json = JsonSerializer.Serialize(jsonObject, new JsonSerializerOptions { WriteIndented = true });

                            // Output the JSON to the console
                            Console.WriteLine(json);
                        }
                    }
                }
            }
        }
    }
}