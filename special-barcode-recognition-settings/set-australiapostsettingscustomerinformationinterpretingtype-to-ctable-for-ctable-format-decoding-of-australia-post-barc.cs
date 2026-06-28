using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generation and reading of an Australia Post barcode using the CTable interpreting type.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode, saves it to a memory stream,
    /// then reads and decodes it using the same interpreting type.
    /// </summary>
    static void Main()
    {
        // Create a barcode generator for Australia Post format with sample data.
        using (var generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, "5912345678AB"))
        {
            // Configure the generator to use the CTable encoding table.
            generator.Parameters.Barcode.AustralianPost.AustralianPostEncodingTable = CustomerInformationInterpretingType.CTable;

            // Prepare a memory stream to hold the generated barcode image.
            using (var ms = new MemoryStream())
            {
                // Save the generated barcode as a PNG image into the memory stream.
                generator.Save(ms, BarCodeImageFormat.Png);
                // Reset stream position to the beginning for reading.
                ms.Position = 0;

                // Initialize a barcode reader to decode the image from the memory stream.
                using (var reader = new BarCodeReader(ms, DecodeType.AustraliaPost))
                {
                    // Set the reader to interpret customer information using the CTable type.
                    reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = CustomerInformationInterpretingType.CTable;

                    // Iterate through all decoded barcodes and output their text.
                    foreach (var result in reader.ReadBarCodes())
                    {
                        Console.WriteLine("Decoded CodeText: " + result.CodeText);
                    }
                }
            }
        }
    }
}