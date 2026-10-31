// Title: Australia Post barcode generation and CTable decoding example
// Description: Demonstrates generating an Australia Post barcode and decoding it using the CTable customer information interpreting type.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader for extracting information. Developers working with postal services often need to encode and decode Australia Post barcodes, adjusting settings such as CustomerInformationInterpretingType to match specific data formats like CTable.
// Prompt: Set AustraliaPostSettings.CustomerInformationInterpretingType to CTable for CTable format decoding of Australia Post barcodes.
// Tags: australia post, barcode generation, barcode recognition, ctable, customer information interpreting, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates how to generate an Australia Post barcode and decode it using the CTable format.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, reads it back with CTable decoding, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // Prepare a temporary folder and file path for the barcode image
        // ------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "AustraliaPostDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "AustraliaPostCTable.png");

        // ------------------------------------------------------------
        // Define sample Australia Post barcode text (FCC 62 + DPID + customer info)
        // ------------------------------------------------------------
        string codeText = "6201234567ASPO";

        // ------------------------------------------------------------
        // Generate the barcode with CTable decoding format
        // ------------------------------------------------------------
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, codeText))
        {
            // Set visual parameters
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Parameters.Barcode.BarHeight.Pixels = 50f;

            // Configure the encoding table to CTable
            generator.Parameters.Barcode.AustralianPost.EncodingTable = CustomerInformationInterpretingType.CTable;

            // Save the barcode image as PNG
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // ------------------------------------------------------------
        // Read the generated barcode and set decoding format to CTable
        // ------------------------------------------------------------
        using (BarCodeReader reader = new BarCodeReader(barcodePath, DecodeType.AustraliaPost))
        {
            // Apply CTable interpretation for customer information
            reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = CustomerInformationInterpretingType.CTable;

            // Iterate through detected barcodes and output results
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"CodeType: {result.CodeTypeName}");
                Console.WriteLine($"CodeText: {result.CodeText}");
            }
        }

        // ------------------------------------------------------------
        // Cleanup temporary files (optional)
        // ------------------------------------------------------------
        try
        {
            if (File.Exists(barcodePath))
                File.Delete(barcodePath);
            if (Directory.Exists(tempFolder))
                Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignore any cleanup errors
        }
    }
}