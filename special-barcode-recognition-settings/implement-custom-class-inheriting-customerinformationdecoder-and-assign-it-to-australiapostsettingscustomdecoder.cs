using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

namespace AustraliaPostCustomDecoderDemo
{
    /// <summary>
    /// Simple implementation of the <see cref="AustraliaPostCustomerInformationDecoder"/> interface.
    /// Returns the raw data prefixed with "Decoded:" for demonstration purposes.
    /// </summary>
    public class SimpleCustomerInfoDecoder : AustraliaPostCustomerInformationDecoder
    {
        /// <summary>
        /// Decodes the supplied data by prefixing it with "Decoded:".
        /// </summary>
        /// <param name="data">The raw data to decode.</param>
        /// <returns>The decoded string.</returns>
        public string Decode(string data)
        {
            return $"Decoded:{data}";
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
            // Sample code text for an Australia Post barcode.
            const string codeText = "5912345678AB";

            // Create a barcode generator for the Australia Post format.
            using (var generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, codeText))
            {
                // Set the interpreting type (optional, using CTable as an example).
                generator.Parameters.Barcode.AustralianPost.AustralianPostEncodingTable = CustomerInformationInterpretingType.CTable;

                // Generate the barcode image as a Bitmap.
                using (Bitmap barcodeImage = generator.GenerateBarCodeImage())
                {
                    // Initialize a BarCodeReader to recognize the generated image.
                    using (var reader = new BarCodeReader(barcodeImage, DecodeType.AustraliaPost))
                    {
                        // Assign the custom decoder to the Australia Post settings.
                        reader.BarcodeSettings.AustraliaPost.CustomerInformationDecoder = new SimpleCustomerInfoDecoder();

                        // Optionally set the interpreting type on the reader side as well.
                        reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = CustomerInformationInterpretingType.CTable;

                        // Perform barcode recognition and iterate over all detected barcodes.
                        foreach (var result in reader.ReadBarCodes())
                        {
                            // Output the detected barcode type and raw code text.
                            Console.WriteLine($"BarCode Type: {result.CodeType}");
                            Console.WriteLine($"BarCode CodeText: {result.CodeText}");

                            // Demonstrate using the custom decoder directly on the raw code text.
                            var decodedInfo = ((SimpleCustomerInfoDecoder)reader.BarcodeSettings.AustraliaPost.CustomerInformationDecoder)
                                              .Decode(result.CodeText);
                            Console.WriteLine($"Custom Decoder Output: {decodedInfo}");
                        }
                    }
                }
            }
        }
    }
}