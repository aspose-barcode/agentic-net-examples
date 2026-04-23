using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

namespace AustraliaPostDecoderTest
{
    // Custom decoder that captures the raw customer information field
    public class MyCustomerInfoDecoder : AustraliaPostCustomerInformationDecoder
    {
        private string _lastRaw;

        public string LastRaw => _lastRaw;

        public string Decode(string customerInformationField)
        {
            _lastRaw = customerInformationField;
            // Return any dummy decoded string; the test cares about the raw input
            return "decoded";
        }
    }

    class Program
    {
        static void Main()
        {
            const string barcodeFile = "australiapost.png";

            // Generate an Australia Post barcode
            using (var generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, "5912345678AB"))
            {
                generator.Parameters.Barcode.AustralianPost.AustralianPostEncodingTable = CustomerInformationInterpretingType.CTable;
                generator.Save(barcodeFile);
            }

            // Prepare the custom decoder
            var decoder = new MyCustomerInfoDecoder();

            // Read the barcode and assign the custom decoder
            using (var reader = new BarCodeReader(barcodeFile, DecodeType.AustraliaPost))
            {
                reader.BarcodeSettings.AustraliaPost.CustomerInformationDecoder = decoder;
                reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = CustomerInformationInterpretingType.CTable;

                foreach (var result in reader.ReadBarCodes())
                {
                    // The result is not used; the decoder is invoked during reading
                }
            }

            // Verify that the decoder received raw bar values (only characters 0‑3)
            bool passed = !string.IsNullOrEmpty(decoder.LastRaw) &&
                          System.Text.RegularExpressions.Regex.IsMatch(decoder.LastRaw, @"^[0-3]+$");

            Console.WriteLine(passed
                ? "Test passed: Decoder received raw customer information field."
                : "Test failed: Decoder did not receive expected raw data.");

            // Clean up the generated file
            if (File.Exists(barcodeFile))
            {
                File.Delete(barcodeFile);
            }
        }
    }
}