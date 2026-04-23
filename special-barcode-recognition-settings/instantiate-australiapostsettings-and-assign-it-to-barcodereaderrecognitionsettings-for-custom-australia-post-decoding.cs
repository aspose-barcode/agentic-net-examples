using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

namespace AustraliaPostDemo
{
    // Simple implementation of the customer information decoder interface
    class SimpleAustraliaPostDecoder : AustraliaPostCustomerInformationDecoder
    {
        public string Decode(string customerInformationField)
        {
            // For demonstration, just return the raw field prefixed
            return $"Decoded:{customerInformationField}";
        }
    }

    class Program
    {
        static void Main()
        {
            // Sample Australia Post barcode text (customer information part included)
            const string barcodeText = "5912345678AB";

            // Generate the barcode image
            using (var generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, barcodeText))
            {
                // Use CTable interpreting type for generation
                generator.Parameters.Barcode.AustralianPost.AustralianPostEncodingTable = CustomerInformationInterpretingType.CTable;

                using (var image = generator.GenerateBarCodeImage())
                {
                    // Create a reader for the generated image, specifying AustraliaPost decoding
                    using (var reader = new BarCodeReader(image, DecodeType.AustraliaPost))
                    {
                        // Obtain the AustraliaPostSettings instance from the reader
                        AustraliaPostSettings settings = reader.BarcodeSettings.AustraliaPost;

                        // Assign a custom decoder
                        settings.CustomerInformationDecoder = new SimpleAustraliaPostDecoder();

                        // Optionally change interpreting type for recognition
                        settings.CustomerInformationInterpretingType = CustomerInformationInterpretingType.NTable;

                        // Read barcodes
                        foreach (BarCodeResult result in reader.ReadBarCodes())
                        {
                            Console.WriteLine($"BarCode Type: {result.CodeType}");
                            Console.WriteLine($"BarCode CodeText: {result.CodeText}");
                        }
                    }
                }
            }
        }
    }
}