// Title: Custom Australia Post barcode decoder example
// Description: Demonstrates how to implement a custom CustomerInformationDecoder for Australia Post barcodes and apply it during recognition.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, focusing on custom decoding of Australia Post customer information fields. It showcases the use of BarcodeGenerator, BarCodeReader, AustraliaPostSettings, and the CustomerInformationDecoder interface, which developers often need when integrating Australia Post barcode processing into applications that require bespoke interpretation of encoded data.
// Prompt: Implement a custom class inheriting CustomerInformationDecoder and assign it to AustraliaPostSettings.CustomDecoder.
// Tags: barcode symbology, australia post, custom decoder, generation, recognition, aspnet.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

namespace AustraliaPostCustomDecoderDemo
{
    /// <summary>
    /// Custom decoder implementing the <see cref="AustraliaPostCustomerInformationDecoder"/> interface.
    /// Returns the raw customer information field prefixed with "Decoded:".
    /// </summary>
    public class MyCustomerInfoDecoder : AustraliaPostCustomerInformationDecoder
    {
        /// <summary>
        /// Decodes the supplied customer information field.
        /// </summary>
        /// <param name="customerInformationField">Raw field data from the barcode.</param>
        /// <returns>Decoded string prefixed with "Decoded:".</returns>
        public string Decode(string customerInformationField)
        {
            // In a real scenario, decode the bar values (0,1,2,3) into meaningful text.
            return "Decoded:" + customerInformationField;
        }
    }

    /// <summary>
    /// Demonstrates generation of an Australia Post barcode and reading it with a custom decoder.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Generates an Australia Post barcode, saves it to a file, then reads it using a custom decoder.
        /// </summary>
        static void Main()
        {
            const string outputFile = "australia_post.png";

            // Generate an Australia Post barcode with CTable interpreting type.
            using (var generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, "5912345678AB"))
            {
                // Set the encoding table for the customer information field.
                generator.Parameters.Barcode.AustralianPost.AustralianPostEncodingTable = CustomerInformationInterpretingType.CTable;

                // Create the barcode image and save it as PNG.
                using (var image = generator.GenerateBarCodeImage())
                {
                    image.Save(outputFile, Aspose.Drawing.Imaging.ImageFormat.Png);
                }
            }

            // Read the barcode and apply the custom decoder.
            using (var reader = new BarCodeReader(outputFile, DecodeType.AustraliaPost))
            {
                // Assign the custom decoder to the AustraliaPost settings.
                reader.BarcodeSettings.AustraliaPost.CustomerInformationDecoder = new MyCustomerInfoDecoder();

                // Ensure the interpreting type matches the generator's setting.
                reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = CustomerInformationInterpretingType.CTable;

                // Iterate through detected barcodes and output basic information.
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine("BarCode Type: " + result.CodeType);
                    Console.WriteLine("BarCode CodeText: " + result.CodeText);
                    // The custom decoder influences internal interpretation of the customer information field.
                }
            }
        }
    }
}