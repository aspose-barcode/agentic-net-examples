// Title: Australia Post barcode generation and CTable decoding example
// Description: Demonstrates generating an Australia Post barcode with CTable encoding and then decoding it using the CTable format.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader for extracting information. Typical use cases include postal automation, logistics, and document processing where Australia Post barcodes need to be encoded and decoded. Developers often work with EncodeTypes, DecodeType, and specific settings like AustralianPost.EncodingTable and AustraliaPost.CustomerInformationInterpretingType.
// Prompt: Set AustraliaPostSettings.CustomerInformationInterpretingType to CTable for CTable format decoding of Australia Post barcodes.
// Tags: barcode symbology, australia post, ctable, generation, recognition, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates how to generate an Australia Post barcode with CTable encoding
/// and subsequently decode it using the CTable format.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, reads it back, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for the barcode image
        string tempDir = Path.Combine(Path.GetTempPath(), "AustraliaPostDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string barcodePath = Path.Combine(tempDir, "AustraliaPostCTable.png");

        // Generate Australia Post barcode with CTable encoding
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, "6201234567ASPO"))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Parameters.Barcode.BarHeight.Pixels = 50f;
            generator.Parameters.Barcode.AustralianPost.EncodingTable = CustomerInformationInterpretingType.CTable;
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was created successfully
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Barcode image was not created.");
            return;
        }

        // Read the barcode using CTable decoding settings
        using (BarCodeReader reader = new BarCodeReader(barcodePath, DecodeType.AustraliaPost))
        {
            reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = CustomerInformationInterpretingType.CTable;
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"CodeType: {result.CodeTypeName}");
                Console.WriteLine($"CodeText: {result.CodeText}");
            }
        }

        // Cleanup temporary files and directory
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempDir);
        }
        catch
        {
            // Ignore cleanup errors
        }
    }
}