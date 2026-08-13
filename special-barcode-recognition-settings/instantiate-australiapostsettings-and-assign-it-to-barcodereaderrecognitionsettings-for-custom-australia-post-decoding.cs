// Title: Custom Australia Post Barcode Decoding with Aspose.BarCode
// Description: Demonstrates how to configure AustraliaPostSettings on a BarCodeReader to use a custom customer information decoder for Australia Post barcodes.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, focusing on the Australia Post symbology. It showcases the use of BarcodeGenerator, BarCodeReader, and the RecognitionSettings hierarchy (AustraliaPostSettings) to customize decoding behavior. Developers working with postal services often need to interpret customer information fields, apply specific encoding tables, or plug in custom decoders; this snippet provides a clear pattern for those scenarios.
// Prompt: Instantiate AustraliaPostSettings and assign it to BarCodeReader.RecognitionSettings for custom Australia Post decoding.
// Tags: australia post, barcode, custom decoder, recognition, generation, aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

namespace AustraliaPostDemo
{
    /// <summary>
    /// Custom decoder implementing the <see cref="AustraliaPostCustomerInformationDecoder"/> interface.
    /// Returns the raw row data prefixed with a label for demonstration purposes.
    /// </summary>
    public class CustomAustraliaPostDecoder : AustraliaPostCustomerInformationDecoder
    {
        /// <summary>
        /// Decodes the supplied row data.
        /// </summary>
        /// <param name="rowData">The raw customer information row data extracted from the barcode.</param>
        /// <returns>A string containing a custom label followed by the original row data.</returns>
        public string Decode(string rowData)
        {
            return $"CustomDecoded:{rowData}";
        }
    }

    class Program
    {
        /// <summary>
        /// Entry point of the example. Generates an Australia Post barcode, configures custom decoding settings,
        /// and reads the barcode to display the detected type and text.
        /// </summary>
        static void Main()
        {
            // Sample Australia Post code text (FCC 59, 8‑digit DPID, 2 CTable chars)
            const string codeText = "5912345678AB";

            // Generate the barcode image using the Australia Post symbology
            using (var generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, codeText))
            {
                // Set the encoding table to CTable for the customer information field
                generator.Parameters.Barcode.AustralianPost.EncodingTable = CustomerInformationInterpretingType.CTable;

                // Produce the bitmap image of the barcode
                using (Bitmap barcodeImage = generator.GenerateBarCodeImage())
                {
                    // Create a reader for the generated image, specifying AustraliaPost as the decode type
                    using (var reader = new BarCodeReader(barcodeImage, DecodeType.AustraliaPost))
                    {
                        // Access the AustraliaPost decoding settings from the reader
                        var australiaPostSettings = reader.BarcodeSettings.AustraliaPost;

                        // Apply custom decoding parameters
                        australiaPostSettings.CustomerInformationInterpretingType = CustomerInformationInterpretingType.CTable;
                        australiaPostSettings.IgnoreEndingFillingPatternsForCTable = true;
                        australiaPostSettings.CustomerInformationDecoder = new CustomAustraliaPostDecoder();

                        // Perform recognition and output results
                        foreach (var result in reader.ReadBarCodes())
                        {
                            Console.WriteLine($"Detected Type : {result.CodeTypeName}");
                            Console.WriteLine($"Detected Text : {result.CodeText}");
                        }
                    }
                }
            }
        }
    }
}