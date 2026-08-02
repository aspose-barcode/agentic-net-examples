// Title: Demonstrate AllowIncorrectBarcodes effect on barcode confidence
// Description: Generates an EAN13 barcode with an incorrect checksum and reads it with AllowIncorrectBarcodes enabled, showing that the confidence value is null (None).
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, illustrating how to use BarcodeGenerator, BarCodeReader, and QualitySettings to handle barcodes with invalid checksums. Developers often need to process imperfect barcodes in bulk scanning scenarios, and this snippet shows the typical API usage for allowing incorrect barcodes while checking the confidence result.
// Prompt: Write unit tests verifying that AllowIncorrectBarcodes returns BarCodeResult.Confidence as null.
// Tags: ean13, incorrect checksum, allowincorrectbarcodes, confidence, barcodereader, barcodegenerator, aspnet, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates reading a barcode with AllowIncorrectBarcodes enabled and verifies that the confidence is null.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates an EAN13 barcode with an invalid checksum, reads it with AllowIncorrectBarcodes set to true, and checks the confidence value.
    /// </summary>
    static void Main()
    {
        // Generate a barcode with an intentionally incorrect checksum (EAN13)
        using (var generator = new BarcodeGenerator(EncodeTypes.EAN13, "1234567890123"))
        {
            // Create the barcode image in memory
            using (var bitmap = generator.GenerateBarCodeImage())
            {
                // Store the image in a memory stream for reading
                using (var ms = new MemoryStream())
                {
                    bitmap.Save(ms, ImageFormat.Png);
                    ms.Position = 0; // Reset stream position for reading

                    // Initialize the barcode reader for EAN13 type
                    using (var reader = new BarCodeReader(ms, DecodeType.EAN13))
                    {
                        // Enable recognition of incorrect barcodes
                        reader.QualitySettings.AllowIncorrectBarcodes = true;

                        bool testPassed = false;

                        // Iterate through all detected barcodes
                        foreach (BarCodeResult result in reader.ReadBarCodes())
                        {
                            // When AllowIncorrectBarcodes is true, Confidence should be None (value 0)
                            if (result.Confidence == BarCodeConfidence.None)
                            {
                                testPassed = true;
                                Console.WriteLine("Test Passed: Confidence is None as expected.");
                            }
                            else
                            {
                                Console.WriteLine($"Test Failed: Unexpected Confidence value {result.Confidence}.");
                            }
                        }

                        // If no results were returned, the test fails
                        if (!testPassed)
                        {
                            Console.WriteLine("Test Failed: No barcode result was returned.");
                        }
                    }
                }
            }
        }
    }
}