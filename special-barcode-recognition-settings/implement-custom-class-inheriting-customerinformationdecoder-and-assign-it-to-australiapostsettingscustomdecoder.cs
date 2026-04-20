using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

namespace AustraliaPostCustomDecoderDemo
{
    // Custom decoder implementing the interface required by AustraliaPostSettings
    public class MyCustomerInformationDecoder : AustraliaPostCustomerInformationDecoder
    {
        // Simple example: just return the raw bar values prefixed with a label
        public string Decode(string customerInformationField)
        {
            return $"CustomDecoded[{customerInformationField}]";
        }
    }

    class Program
    {
        static void Main()
        {
            // Sample code text for Australia Post barcode (postal code + customer info)
            const string codeText = "5912345678ABCde";

            // Create barcode generator for Australia Post
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, codeText))
            {
                // Set interpreting type for customer information (CTable in this example)
                generator.Parameters.Barcode.AustralianPost.EncodingTable = CustomerInformationInterpretingType.CTable;

                // Generate barcode image
                using (Bitmap barcodeImage = generator.GenerateBarCodeImage())
                {
                    // Create reader for Australia Post barcode
                    using (BarCodeReader reader = new BarCodeReader(barcodeImage, DecodeType.AustraliaPost))
                    {
                        // Assign custom decoder to the settings
                        reader.BarcodeSettings.AustraliaPost.CustomerInformationDecoder = new MyCustomerInformationDecoder();

                        // Optionally set the interpreting type to match generation
                        reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = CustomerInformationInterpretingType.CTable;

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