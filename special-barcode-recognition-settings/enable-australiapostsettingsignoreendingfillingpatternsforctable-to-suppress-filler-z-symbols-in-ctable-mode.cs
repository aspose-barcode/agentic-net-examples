// Title: Suppress filler symbols in Australia Post CTable barcode decoding
// Description: Demonstrates generating an Australia Post barcode with CTable customer information and decoding it while ignoring the trailing filler "z" symbols.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader for extracting data. Typical use cases include postal automation and custom data encoding where developers need to control decoding behavior, such as ignoring filler patterns in CTable mode. The key API classes are BarcodeGenerator, BarCodeReader, and related settings classes.
// Prompt: Enable AustraliaPostSettings.IgnoreEndingFillingPatternsForCTable to suppress filler "z" symbols in CTable mode.
// Tags: australia post, barcode, ctable, ignore filler, generation, recognition, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates an Australia Post barcode with CTable customer information,
/// saves it as an image, and then reads it while ignoring the ending filler
/// patterns ("z") in CTable mode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs barcode generation, saving,
    /// and recognition with specific decoding settings.
    /// </summary>
    static void Main()
    {
        // Sample Australia Post code text:
        // FCC = 59, DPID = 12345678, Customer info = "AB" (CTable, 2 chars)
        const string codeText = "5912345678AB";

        // Initialize the barcode generator for Australia Post symbology
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, codeText))
        {
            // Configure the generator to use CTable interpreting type for customer information
            generator.Parameters.Barcode.AustralianPost.EncodingTable = CustomerInformationInterpretingType.CTable;

            // Generate the barcode image as a bitmap
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Save the generated image to disk for verification
                const string imagePath = "AustraliaPost.png";
                bitmap.Save(imagePath, ImageFormat.Png);
                Console.WriteLine($"Barcode image saved to: {Path.GetFullPath(imagePath)}");

                // Initialize a barcode reader to decode the generated image
                using (BarCodeReader reader = new BarCodeReader(bitmap, DecodeType.AustraliaPost))
                {
                    // Set the reader to interpret customer information using CTable
                    reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = CustomerInformationInterpretingType.CTable;

                    // Enable ignoring of ending filling patterns (the "z" filler) in CTable mode
                    reader.BarcodeSettings.AustraliaPost.IgnoreEndingFillingPatternsForCTable = true;

                    // Iterate through all detected barcodes and display their type and decoded text
                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        Console.WriteLine($"Detected Type: {result.CodeType}");
                        Console.WriteLine($"Decoded Text : {result.CodeText}");
                    }
                }
            }
        }
    }
}