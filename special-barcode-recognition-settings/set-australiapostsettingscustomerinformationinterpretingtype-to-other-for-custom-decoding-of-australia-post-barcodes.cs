// Title: Custom decoding of Australia Post barcodes using CustomerInformationInterpretingType.Other
// Description: Demonstrates how to generate an Australia Post barcode and decode it with the CustomerInformationInterpretingType set to Other, enabling custom interpretation of the customer information field.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader for decoding them, focusing on the Australia Post symbology. Developers often need to customize how the customer information segment is interpreted; this snippet shows the key API classes (BarcodeGenerator, BarCodeReader, CustomerInformationInterpretingType) and typical steps for such scenarios.
// Prompt: Set AustraliaPostSettings.CustomerInformationInterpretingType to Other for custom decoding of Australia Post barcodes.
// Tags: australia post, barcode, custom decoding, customerinformationinterpretingtype, generation, recognition, c#, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates setting <c>CustomerInformationInterpretingType</c> to <c>Other</c> for Australia Post barcodes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // Prepare a temporary directory and file path for the barcode image
        // ------------------------------------------------------------
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeBarCodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string imagePath = Path.Combine(tempDir, "AustraliaPostOther.png");

        // ------------------------------------------------------------
        // Generate an Australia Post barcode with CustomerInformationInterpretingType set to Other
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, "6201234567321032103210"))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Parameters.Barcode.BarHeight.Pixels = 50f;
            generator.Parameters.Barcode.AustralianPost.EncodingTable = CustomerInformationInterpretingType.Other;
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // ------------------------------------------------------------
        // Verify that the barcode image was successfully created
        // ------------------------------------------------------------
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // ------------------------------------------------------------
        // Read the barcode and configure the decoder to interpret customer information as Other
        // ------------------------------------------------------------
        using (var reader = new BarCodeReader(imagePath, DecodeType.AustraliaPost))
        {
            reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = CustomerInformationInterpretingType.Other;
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"CodeType: {result.CodeTypeName}");
                Console.WriteLine($"CodeText: {result.CodeText}");
            }
        }

        // ------------------------------------------------------------
        // Clean up temporary files and directory
        // ------------------------------------------------------------
        try
        {
            File.Delete(imagePath);
            Directory.Delete(tempDir);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program outcome
        }
    }
}