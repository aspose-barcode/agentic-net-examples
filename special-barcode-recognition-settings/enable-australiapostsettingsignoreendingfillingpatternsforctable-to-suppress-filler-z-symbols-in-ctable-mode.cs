// Title: Suppress filler 'z' symbols in Australia Post CTable barcode decoding
// Description: Demonstrates how to generate an Australia Post barcode using CTable encoding and decode it while ignoring trailing filler 'z' characters.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader for decoding them. Typical use cases include postal automation and data validation where developers need to handle specific encoding tables and customize decoding behavior, such as suppressing filler patterns in CTable mode.
// Prompt: Enable AustraliaPostSettings.IgnoreEndingFillingPatternsForCTable to suppress filler "z" symbols in CTable mode.
// Tags: australia post, ctable, barcode generation, barcode recognition, ignore filler, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generation of an Australia Post barcode with CTable encoding and decoding it while suppressing trailing filler symbols.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, decodes it with filler suppression, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Prepare a unique temporary directory for the barcode image
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeBarCodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string barcodePath = Path.Combine(tempDir, "AustraliaPostCTable.png");

        // Generate an Australia Post barcode using CTable encoding
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, "6201234567END"))
        {
            // Set visual parameters
            generator.Parameters.Barcode.XDimension.Pixels = 4;
            generator.Parameters.Barcode.BarHeight.Pixels = 50;

            // Specify CTable as the customer information interpreting type
            generator.Parameters.Barcode.AustralianPost.EncodingTable = CustomerInformationInterpretingType.CTable;

            // Save the barcode image as PNG
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was created successfully
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Decode the barcode with filler suppression enabled
        using (BarCodeReader reader = new BarCodeReader(barcodePath, DecodeType.AustraliaPost))
        {
            // Configure the reader to interpret customer information using CTable
            reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = CustomerInformationInterpretingType.CTable;

            // Suppress trailing filler "z" symbols in the decoded result
            reader.BarcodeSettings.AustraliaPost.IgnoreEndingFillingPatternsForCTable = true;

            // Iterate through all decoded results and output the code text
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine("Decoded CodeText: " + result.CodeText);
            }
        }

        // Clean up temporary files and directory
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempDir);
        }
        catch
        {
            // Ignored – cleanup failures should not affect program outcome
        }
    }
}