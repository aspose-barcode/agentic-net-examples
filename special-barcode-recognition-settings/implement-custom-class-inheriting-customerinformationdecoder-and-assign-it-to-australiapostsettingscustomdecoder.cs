// Title: Custom Customer Information Decoder for Australia Post Barcodes
// Description: Demonstrates implementing a custom CustomerInformationDecoder and assigning it to AustraliaPostSettings to decode the customer information field of an Australia Post barcode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, focusing on the Australia Post symbology. It showcases the use of BarcodeGenerator, BarCodeReader, AustraliaPostSettings, and the CustomerInformationDecoder interface to customize decoding of customer‑information fields. Developers working with postal barcodes often need to interpret encoded customer data beyond the default decoding, making custom decoders a common requirement.
// Prompt: Implement a custom class inheriting CustomerInformationDecoder and assign it to AustraliaPostSettings.CustomDecoder.
// Tags: australia post, barcode, custom decoder, customer information, generation, recognition, aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

namespace AustraliaPostCustomDecoderDemo
{
    /// <summary>
    /// Custom decoder that implements <see cref="AustraliaPostCustomerInformationDecoder"/>
    /// to provide bespoke decoding of the customer information field.
    /// </summary>
    public class CustomCustomerInfoDecoder : AustraliaPostCustomerInformationDecoder
    {
        /// <summary>
        /// Decodes the raw customer information field.
        /// </summary>
        /// <param name="customerInformationField">The raw field extracted from the barcode.</param>
        /// <returns>A string representing the decoded customer information.</returns>
        public string Decode(string customerInformationField)
        {
            // In a real scenario, implement CTable/NTable decoding logic here.
            return $"CustomDecoded[{customerInformationField}]";
        }
    }

    /// <summary>
    /// Demonstrates generating an Australia Post barcode, reading it, and using a custom decoder.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point of the demo application.
        /// </summary>
        static void Main()
        {
            // Sample Australia Post barcode with FCC=59, DPID=12345678, customer info "AB".
            string codeText = "5912345678AB";

            // Create a barcode generator for the Australia Post symbology and set the encoding table to CTable (allows letters).
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, codeText))
            {
                generator.Parameters.Barcode.AustralianPost.EncodingTable = CustomerInformationInterpretingType.CTable;

                // Generate the barcode image.
                using (Bitmap image = generator.GenerateBarCodeImage())
                {
                    // Initialize a reader for Australia Post barcodes.
                    using (BarCodeReader reader = new BarCodeReader(image, DecodeType.AustraliaPost))
                    {
                        // Assign the custom decoder to the Australia Post settings.
                        reader.BarcodeSettings.AustraliaPost.CustomerInformationDecoder = new CustomCustomerInfoDecoder();

                        // Ensure the interpreting type matches the generator's setting.
                        reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = CustomerInformationInterpretingType.CTable;

                        // Read all detected barcodes.
                        foreach (BarCodeResult result in reader.ReadBarCodes())
                        {
                            Console.WriteLine($"Detected Code Type: {result.CodeType}");
                            Console.WriteLine($"Detected Code Text: {result.CodeText}");

                            // Extract the raw customer information (after FCC(2) + DPID(8)).
                            string rawCustomerInfo = result.CodeText.Substring(10);
                            // Use the custom decoder directly.
                            string decodedInfo = ((AustraliaPostCustomerInformationDecoder)reader.BarcodeSettings.AustraliaPost.CustomerInformationDecoder).Decode(rawCustomerInfo);
                            Console.WriteLine($"Custom Decoded Customer Info: {decodedInfo}");
                        }
                    }
                }
            }

            // Indicate that processing has finished.
            Console.WriteLine("Processing completed.");
        }
    }
}