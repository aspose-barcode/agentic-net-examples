using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generation and recognition of an Australia Post barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode, saves it to a file, then reads it back and displays the results.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image.
        string imagePath = "australia_post.png";

        // -------------------------------------------------
        // Generate an Australia Post barcode with CTable interpreting type.
        // -------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, "5912345678AB"))
        {
            // Configure the generator to use the CTable encoding table.
            generator.Parameters.Barcode.AustralianPost.AustralianPostEncodingTable = CustomerInformationInterpretingType.CTable;

            // Save the generated barcode image to the specified file.
            generator.Save(imagePath);
        }

        // -------------------------------------------------
        // Recognize the previously generated barcode.
        // Enable ignoring ending filling patterns for CTable.
        // -------------------------------------------------
        using (var reader = new BarCodeReader(imagePath, DecodeType.AustraliaPost))
        {
            // Set the decoding interpreting type to CTable.
            reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = CustomerInformationInterpretingType.CTable;

            // Enable the flag to ignore filler "z" symbols at the end of the barcode.
            reader.BarcodeSettings.AustraliaPost.IgnoreEndingFillingPatternsForCTable = true;

            // Iterate through all detected barcodes and output their details.
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"BarCode Type: {result.CodeType}");
                Console.WriteLine($"BarCode CodeText: {result.CodeText}");
            }
        }
    }
}