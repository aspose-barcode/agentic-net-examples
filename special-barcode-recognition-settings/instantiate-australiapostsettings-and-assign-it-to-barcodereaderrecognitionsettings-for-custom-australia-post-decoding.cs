// Title: Custom Australia Post barcode decoding with Aspose.BarCode
// Description: Demonstrates how to assign a custom AustraliaPostCustomerInformationDecoder to the BarCodeReader's RecognitionSettings for tailored decoding of Australia Post barcodes.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, focusing on customizing decoding behavior for specific symbologies. It showcases the use of BarCodeReader, AustraliaPostSettings, and the AustraliaPostCustomerInformationDecoder interface to implement custom logic, a common requirement when default decoding does not meet business needs. Developers can adapt this pattern for other symbologies requiring specialized post‑processing.
// Prompt: Instantiate AustraliaPostSettings and assign it to BarCodeReader.RecognitionSettings for custom Australia Post decoding.
// Tags: australia post, barcode decoding, custom decoder, aspose.barcode, recognitionsettings

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

namespace AsposeBarcodeAustraliaPostDemo
{
    // Custom decoder implementing the AustraliaPostCustomerInformationDecoder interface
    public class MyAustraliaPostDecoder : AustraliaPostCustomerInformationDecoder
    {
        // Simple implementation that returns a fixed string for demonstration
        public string Decode(string barValues)
        {
            // In a real scenario, decode the barValues according to custom logic
            return "CustomDecodedInfo";
        }
    }

    /// <summary>
    /// Demonstrates custom decoding of Australia Post barcodes using Aspose.BarCode.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point that generates a sample barcode, configures custom decoding, and outputs results.
        /// </summary>
        static void Main()
        {
            // Generate a sample Australia Post barcode image
            using (var generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, "5912345678AB"))
            {
                // Use CTable interpreting type for the customer information field
                generator.Parameters.Barcode.AustralianPost.AustralianPostEncodingTable = CustomerInformationInterpretingType.CTable;

                // Create the barcode image
                using (Bitmap barcodeImage = generator.GenerateBarCodeImage())
                {
                    // Initialize a reader for the generated image, specifying Australia Post decode type
                    using (var reader = new BarCodeReader(barcodeImage, DecodeType.AustraliaPost))
                    {
                        // Access the AustraliaPost decoding settings
                        AustraliaPostSettings auPostSettings = reader.BarcodeSettings.AustraliaPost;

                        // Assign a custom decoder implementation
                        auPostSettings.CustomerInformationDecoder = new MyAustraliaPostDecoder();

                        // Ensure the interpreting type matches the generation settings
                        auPostSettings.CustomerInformationInterpretingType = CustomerInformationInterpretingType.CTable;

                        // Perform barcode recognition and output results
                        foreach (var result in reader.ReadBarCodes())
                        {
                            Console.WriteLine($"Detected Type: {result.CodeType}");
                            Console.WriteLine($"Code Text: {result.CodeText}");
                            // The custom decoder does not affect CodeText directly; it would be used internally
                        }
                    }
                }
            }
        }
    }
}