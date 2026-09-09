// Title: Australia Post barcode generation and NTable decoding example
// Description: Demonstrates generating an Australia Post barcode using NTable encoding and decoding it with NTable format interpretation.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader for decoding them. Typical use cases include postal automation, logistics, and inventory systems where Australia Post barcodes are processed. Developers often need to configure encoding tables and decoding settings via the AustraliaPostSettings and related API classes.
/// Prompt: Set AustraliaPostSettings.CustomerInformationInterpretingType to NTable for NTable format decoding of Australia Post barcodes.
/// Tags: australia post, barcode, ntable, generation, recognition, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Provides a simple console application that generates an Australia Post barcode
/// with NTable encoding and then reads it back using NTable decoding settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode, saves it to a temporary file, reads and decodes it,
    /// then cleans up the temporary resources.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare a unique temporary folder for the barcode image
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "AustraliaPostDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "AustraliaPostNTable.png");

        // --------------------------------------------------------------------
        // Generate an Australia Post barcode using NTable encoding
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, "620123456701234"))
        {
            // Set visual parameters
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Parameters.Barcode.BarHeight.Pixels = 50f;

            // Configure the encoding table to NTable
            generator.Parameters.Barcode.AustralianPost.EncodingTable = CustomerInformationInterpretingType.NTable;

            // Save the barcode image as PNG
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // --------------------------------------------------------------------
        // Verify that the barcode image was created successfully
        // --------------------------------------------------------------------
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // --------------------------------------------------------------------
        // Read the barcode and set decoding format to NTable
        // --------------------------------------------------------------------
        using (var reader = new BarCodeReader(barcodePath, DecodeType.AustraliaPost))
        {
            // Apply NTable decoding settings
            reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = CustomerInformationInterpretingType.NTable;

            // Iterate through all detected barcodes (should be one)
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"CodeType: {result.CodeTypeName}");
                Console.WriteLine($"CodeText: {result.CodeText}");
            }
        }

        // --------------------------------------------------------------------
        // Clean up temporary files and directory
        // --------------------------------------------------------------------
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program outcome
        }
    }
}