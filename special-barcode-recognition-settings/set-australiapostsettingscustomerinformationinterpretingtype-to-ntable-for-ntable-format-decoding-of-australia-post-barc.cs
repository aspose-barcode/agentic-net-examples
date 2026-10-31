// Title: Demonstrate NTable decoding for Australia Post barcodes
// Description: Shows how to generate an Australia Post barcode using NTable encoding and then decode it by setting CustomerInformationInterpretingType to NTable. Useful for processing Australia Post barcodes that contain customer information in NTable format.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It illustrates the use of BarcodeGenerator with EncodeTypes.AustraliaPost and BarCodeReader with DecodeType.AustraliaPost, focusing on the CustomerInformationInterpretingType property to control NTable format handling. Developers working with postal services often need to encode and decode specialized symbologies such as Australia Post, and this snippet demonstrates the key API classes and typical workflow for such tasks.
// Prompt: Set AustraliaPostSettings.CustomerInformationInterpretingType to NTable for NTable format decoding of Australia Post barcodes.
// Tags: australia post, barcode, ntable, generation, recognition, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates an Australia Post barcode with NTable encoding,
/// then reads it back using NTable decoding settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, decodes it, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary folder for the demo files
        string tempFolder = Path.Combine(Path.GetTempPath(), "AustraliaPostDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "AustraliaPostNTable.png");

        // Generate an Australia Post barcode with NTable encoding
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, "620123456701234"))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 4;
            generator.Parameters.Barcode.BarHeight.Pixels = 50;
            // Set the encoding table to NTable for customer information
            generator.Parameters.Barcode.AustralianPost.EncodingTable = CustomerInformationInterpretingType.NTable;
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Read the barcode and set the decoding format to NTable
        using (BarCodeReader reader = new BarCodeReader(barcodePath, DecodeType.AustraliaPost))
        {
            // Configure the reader to interpret customer information using NTable
            reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = CustomerInformationInterpretingType.NTable;
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"CodeType: {result.CodeTypeName}");
                Console.WriteLine($"CodeText: {result.CodeText}");
            }
        }

        // Clean up temporary files (optional)
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignore any cleanup errors
        }
    }
}