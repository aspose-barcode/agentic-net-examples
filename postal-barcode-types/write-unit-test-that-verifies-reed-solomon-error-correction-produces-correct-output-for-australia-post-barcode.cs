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
    /// then reads it back and verifies that the decoded text matches the original.
    /// </summary>
    static void Main()
    {
        // Sample Australia Post barcode data
        const string originalCodeText = "5912345678ABCde";

        // Create a barcode generator for Australia Post format with the sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, originalCodeText))
        {
            // Set the interpreting type to CTable for customer information
            generator.Parameters.Barcode.AustralianPost.AustralianPostEncodingTable = CustomerInformationInterpretingType.CTable;

            // Prepare a memory stream to hold the generated image
            using (var ms = new MemoryStream())
            {
                // Save the barcode as a PNG image into the memory stream
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading

                // Load the PNG image from the memory stream into a bitmap
                using (var bitmap = new Bitmap(ms))
                {
                    // Initialize a barcode reader for Australia Post type
                    using (var reader = new BarCodeReader(bitmap, DecodeType.AustraliaPost))
                    {
                        // Configure the reader to use the same CTable interpreting type
                        reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = CustomerInformationInterpretingType.CTable;

                        // Perform barcode recognition and retrieve all results
                        var results = reader.ReadBarCodes();

                        // Ensure at least one barcode was detected
                        if (results.Length == 0)
                        {
                            Console.WriteLine("FAILED: No barcode detected.");
                            return;
                        }

                        // Check if any decoded result matches the original text
                        bool match = false;
                        foreach (var result in results)
                        {
                            if (result.CodeText == originalCodeText)
                            {
                                match = true;
                                break;
                            }
                        }

                        // Output verification result
                        if (match)
                        {
                            Console.WriteLine("PASSED: Decoded text matches original.");
                        }
                        else
                        {
                            Console.WriteLine($"FAILED: Decoded text does not match. Expected '{originalCodeText}'.");
                        }
                    }
                }
            }
        }
    }
}