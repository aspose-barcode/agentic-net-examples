using System;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

namespace AustraliaPostBarcodeUtility
{
    // Custom decoder implementing the Aspose interface
    public class MyDecoder : AustraliaPostCustomerInformationDecoder
    {
        // Simple example: reverse the raw field string
        public string Decode(string customerInformationField)
        {
            if (customerInformationField == null)
                throw new ArgumentException("Customer information field cannot be null.", nameof(customerInformationField));

            char[] chars = customerInformationField.ToCharArray();
            Array.Reverse(chars);
            return new string(chars);
        }
    }

    class Program
    {
        static void Main()
        {
            // Sample code text for Australia Post barcode
            const string sampleCodeText = "5912345678AB";

            // Generate the barcode image in memory
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, sampleCodeText))
            {
                // Set interpreting type for customer information
                generator.Parameters.Barcode.AustralianPost.EncodingTable = CustomerInformationInterpretingType.CTable;

                using (Bitmap barcodeImage = generator.GenerateBarCodeImage())
                {
                    // Optionally save the image to verify generation (saved in the current directory)
                    const string imagePath = "AustraliaPost.png";
                    barcodeImage.Save(imagePath, ImageFormat.Png);

                    // Prepare the reader with a custom decoder
                    using (BarCodeReader reader = new BarCodeReader(barcodeImage, DecodeType.AustraliaPost))
                    {
                        MyDecoder customDecoder = new MyDecoder();

                        // Assign custom decoder and interpreting type
                        reader.BarcodeSettings.AustraliaPost.CustomerInformationDecoder = customDecoder;
                        reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = CustomerInformationInterpretingType.CTable;

                        // Read and process barcodes
                        foreach (BarCodeResult result in reader.ReadBarCodes())
                        {
                            // Use the custom decoder on the raw CodeText (for demonstration purposes)
                            string decodedInfo = customDecoder.Decode(result.CodeText);

                            // Create an object to be serialized to JSON
                            var jsonObject = new
                            {
                                CodeType = result.CodeType.ToString(),
                                CodeText = result.CodeText,
                                DecodedCustomerInfo = decodedInfo
                            };

                            // Serialize with indentation for readability
                            string json = JsonSerializer.Serialize(jsonObject, new JsonSerializerOptions { WriteIndented = true });

                            // Output the JSON to console
                            Console.WriteLine(json);
                        }
                    }
                }
            }
        }
    }
}