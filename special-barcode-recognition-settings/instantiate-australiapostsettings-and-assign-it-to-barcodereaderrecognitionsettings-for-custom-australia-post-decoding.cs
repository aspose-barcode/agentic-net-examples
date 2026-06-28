using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generation and recognition of an Australia Post barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode, then reads it back and prints the results.
    /// </summary>
    static void Main()
    {
        // Sample Australia Post barcode text to encode.
        string codeText = "5912345678AB";

        // Create a barcode generator for Australia Post format with the specified text.
        using (var generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, codeText))
        {
            // Optional: set the encoding table for generation (CTable in this case).
            generator.Parameters.Barcode.AustralianPost.AustralianPostEncodingTable = CustomerInformationInterpretingType.CTable;

            // Generate the barcode image as a Bitmap.
            using (Bitmap image = generator.GenerateBarCodeImage())
            {
                // Initialize a barcode reader for the generated image, specifying Australia Post decoding.
                using (var reader = new BarCodeReader(image, DecodeType.AustraliaPost))
                {
                    // Access and customize Australia Post specific decoding settings.
                    var australiaPostSettings = reader.BarcodeSettings.AustraliaPost;
                    // Use NTable for interpreting customer information during decoding.
                    australiaPostSettings.CustomerInformationInterpretingType = CustomerInformationInterpretingType.NTable;
                    // Ignore ending filling patterns when using CTable encoding.
                    australiaPostSettings.IgnoreEndingFillingPatternsForCTable = true;

                    // Perform barcode recognition and iterate over all detected results.
                    foreach (var result in reader.ReadBarCodes())
                    {
                        // Output the type of barcode detected.
                        Console.WriteLine($"Detected Type: {result.CodeType}");
                        // Output the decoded text of the barcode.
                        Console.WriteLine($"Code Text: {result.CodeText}");
                    }
                }
            }
        }
    }
}