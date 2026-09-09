// Title: Australia Post barcode generation and CTable decoding example
// Description: Demonstrates generating an Australia Post barcode with CTable encoding and decoding it using the CTable format.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader for decoding them, focusing on Australia Post symbology. Developers often need to encode customer information in CTable format and later interpret it, making this pattern common in logistics and mailing applications.
// Prompt: Set AustraliaPostSettings.CustomerInformationInterpretingType to CTable for CTable format decoding of Australia Post barcodes.
// Tags: barcode, australia post, ctable, generation, recognition, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Generates an Australia Post barcode with CTable encoding, decodes it, and cleans up temporary files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a temporary directory, generates a barcode, reads it, and deletes the artifacts.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for the demo files
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeBarCodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string imagePath = Path.Combine(tempDir, "AustraliaPostCTable.png");

        // Generate an Australia Post barcode using CTable encoding
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, "6201234567ASPOSE"))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Parameters.Barcode.BarHeight.Pixels = 50f;
            generator.Parameters.Barcode.AustralianPost.EncodingTable = CustomerInformationInterpretingType.CTable;
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was created successfully
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Barcode image was not created.");
            return;
        }

        // Read the barcode using CTable decoding settings
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.AustraliaPost))
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
            File.Delete(imagePath);
            Directory.Delete(tempDir);
        }
        catch
        {
            // Ignored: cleanup failures are non‑critical for the demo
        }
    }
}