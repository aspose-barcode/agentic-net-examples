using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Sample Australia Post barcode text (customer information part)
        const string codeText = "5912345678AB";

        // Create barcode generator for Australia Post symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, codeText))
        {
            // Use CTable interpreting type for customer information
            generator.Parameters.Barcode.AustralianPost.EncodingTable = CustomerInformationInterpretingType.CTable;

            // Generate barcode image in memory
            using (Bitmap image = generator.GenerateBarCodeImage())
            {
                // Set up reader with Australia Post decoding settings
                using (var reader = new BarCodeReader(image, DecodeType.AustraliaPost))
                {
                    // Apply same interpreting type and enable ignoring filler patterns
                    reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = CustomerInformationInterpretingType.CTable;
                    reader.BarcodeSettings.AustraliaPost.IgnoreEndingFillingPatternsForCTable = true;

                    // Read and display results
                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        Console.WriteLine("BarCode Type: " + result.CodeType);
                        Console.WriteLine("BarCode CodeText: " + result.CodeText);
                    }
                }

                // Optionally save the generated image to verify visually
                image.Save("AustraliaPost.png", Aspose.Drawing.Imaging.ImageFormat.Png);
            }
        }
    }
}