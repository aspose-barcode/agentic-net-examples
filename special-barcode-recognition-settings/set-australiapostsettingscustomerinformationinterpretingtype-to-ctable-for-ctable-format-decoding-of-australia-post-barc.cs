// Title: Australia Post barcode generation and CTable decoding example
// Description: Demonstrates generating an Australia Post barcode with customer information encoded using the CTable format and then decoding it back, interpreting the customer data as CTable.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader for decoding them. Typical use cases include printing Australia Post barcodes with custom customer information and later extracting that information programmatically. Developers often work with EncodeTypes, DecodeType, and specific settings such as AustralianPost.EncodingTable and AustraliaPost.CustomerInformationInterpretingType.
// Prompt: Set AustraliaPostSettings.CustomerInformationInterpretingType to CTable for CTable format decoding of Australia Post barcodes.
// Tags: australia post, barcode, ctable, generation, recognition, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates an Australia Post barcode with CTable‑encoded customer information
/// and then reads it back, interpreting the customer data as CTable.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode, saves to a memory stream, and reads it back using Aspose.BarCode.
    /// </summary>
    static void Main()
    {
        // Sample Australia Post code text:
        // FCC = 59 (allows customer info), DPID = 12345678, 5 CTable characters "ABCDE"
        string codeText = "5912345678ABCDE";

        // Generate the barcode with CTable encoding for customer information
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, codeText))
        {
            // Set the encoding table to CTable for the generated barcode
            generator.Parameters.Barcode.AustralianPost.EncodingTable = CustomerInformationInterpretingType.CTable;

            // Save the generated barcode to a memory stream in PNG format
            using (MemoryStream ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading

                // Recognize the barcode from the memory stream
                using (BarCodeReader reader = new BarCodeReader(ms, DecodeType.AustraliaPost))
                {
                    // Configure the reader to interpret customer information as CTable
                    reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = CustomerInformationInterpretingType.CTable;

                    // Read and output barcode information
                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        Console.WriteLine($"Code Type: {result.CodeType}");
                        Console.WriteLine($"Code Text: {result.CodeText}");
                    }
                }
            }
        }
    }
}