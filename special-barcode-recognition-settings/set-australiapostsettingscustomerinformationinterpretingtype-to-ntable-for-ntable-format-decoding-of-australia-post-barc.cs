// Title: Decode Australia Post barcode using NTable customer information interpreting
// Description: Demonstrates generating an Australia Post barcode with NTable encoding and then decoding it using the NTable format. Useful for handling customer information in Australia Post barcodes.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator, BarCodeReader, and AustraliaPostSettings to encode and decode Australia Post barcodes with specific customer information interpreting types. Developers working with postal barcode standards often need to switch between encoding tables such as NTable for accurate data extraction.
// Prompt: Set AustraliaPostSettings.CustomerInformationInterpretingType to NTable for NTable format decoding of Australia Post barcodes.
// Tags: australia post, barcode generation, barcode recognition, ntable, customer information interpreting, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating and decoding an Australia Post barcode using NTable customer information interpreting.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, saves it, reads it back with NTable decoding, and outputs the results.
    /// </summary>
    static void Main(string[] args)
    {
        // Create a unique temporary folder to store the barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "AustraliaPostDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the full file path for the generated barcode image
        string barcodePath = Path.Combine(tempFolder, "AustraliaPostNTable.png");

        // Sample Australia Post barcode text (FCC 62, DPID 01234567, customer info digits)
        string codeText = "620123456701234";

        // Generate an Australia Post barcode using NTable encoding for the customer information
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, codeText))
        {
            // Set visual parameters
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Parameters.Barcode.BarHeight.Pixels = 50f;

            // Specify NTable as the customer information interpreting type
            generator.Parameters.Barcode.AustralianPost.EncodingTable = CustomerInformationInterpretingType.NTable;

            // Save the barcode image as PNG
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Read the generated barcode image, configuring the reader to use NTable decoding
        using (BarCodeReader reader = new BarCodeReader(barcodePath, DecodeType.AustraliaPost))
        {
            // Apply NTable decoding for customer information
            reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = CustomerInformationInterpretingType.NTable;

            // Iterate through all detected barcodes (should be one) and output details
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Code Type: {result.CodeTypeName}");
                Console.WriteLine($"Code Text: {result.CodeText}");
            }
        }

        // Optional cleanup: delete the temporary folder and its contents
        // Directory.Delete(tempFolder, true);
    }
}