using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generation and recognition of an Australia Post barcode using NTable encoding.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates an Australia Post barcode, saves it to a file, and then reads it back.
    /// </summary>
    static void Main()
    {
        // Sample Australia Post barcode data (digits only for NTable decoding)
        string codeText = "5912345678";
        string imagePath = "australiapost.png";

        // -------------------------------------------------
        // Generate the barcode image with NTable encoding
        // -------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, codeText))
        {
            // Set the encoding table to NTable for Australian Post barcodes
            generator.Parameters.Barcode.AustralianPost.AustralianPostEncodingTable = CustomerInformationInterpretingType.NTable;

            // Save the generated barcode image to the specified path
            generator.Save(imagePath);
        }

        // -------------------------------------------------
        // Read the barcode and set decoding to NTable format
        // -------------------------------------------------
        using (var reader = new BarCodeReader(imagePath, DecodeType.AustraliaPost))
        {
            // Configure the reader to interpret customer information using NTable
            reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = CustomerInformationInterpretingType.NTable;

            // Iterate through all detected barcodes in the image
            foreach (var result in reader.ReadBarCodes())
            {
                // Output the type of barcode detected
                Console.WriteLine($"Detected Type: {result.CodeType}");

                // Output the decoded text of the barcode
                Console.WriteLine($"CodeText: {result.CodeText}");
            }
        }
    }
}