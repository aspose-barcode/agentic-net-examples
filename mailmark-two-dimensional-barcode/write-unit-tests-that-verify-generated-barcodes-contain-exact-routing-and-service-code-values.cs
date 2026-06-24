using System;
using System.IO;
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
    /// Entry point of the application. Generates a barcode, saves it to a memory stream,
    /// then reads it back to verify the encoded text matches the original.
    /// </summary>
    static void Main()
    {
        // Sample routing and service code values for AustraliaPost barcode
        string originalCodeText = "5912345678ABCde"; // example includes routing/service info

        // Create a barcode generator for Australia Post format with the original text
        using (var generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, originalCodeText))
        {
            // Optional: set the encoding table to CTable for customer information interpretation
            generator.Parameters.Barcode.AustralianPost.AustralianPostEncodingTable = CustomerInformationInterpretingType.CTable;

            // Use a memory stream to hold the generated PNG image
            using (var ms = new MemoryStream())
            {
                // Save the barcode image into the memory stream
                generator.Save(ms, BarCodeImageFormat.Png);
                // Reset stream position to the beginning for reading
                ms.Position = 0;

                // Initialize a barcode reader to decode the image from the memory stream
                using (var reader = new BarCodeReader(ms, DecodeType.AustraliaPost))
                {
                    // Ensure checksum validation is enabled (default behavior)
                    reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

                    // Read all barcodes found in the image
                    var results = reader.ReadBarCodes();

                    // If no barcodes were detected, report failure and exit
                    if (results.Length == 0)
                    {
                        Console.WriteLine("FAIL: No barcode detected.");
                        return;
                    }

                    // Take the first detected barcode result
                    var result = results[0];

                    // Compare the recognized text with the original input
                    if (result.CodeText == originalCodeText)
                    {
                        Console.WriteLine("PASS: Recognized CodeText matches original.");
                    }
                    else
                    {
                        Console.WriteLine($"FAIL: Recognized CodeText '{result.CodeText}' does not match original '{originalCodeText}'.");
                    }

                    // Placeholder for additional verification of routing/service fields if needed
                }
            }
        }
    }
}