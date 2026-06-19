using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a barcode, saving it to a TIFF stream,
/// and then reading the barcode from that stream using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a Code128 barcode, writes it to a memory stream as TIFF,
    /// then reads and prints detected barcode values.
    /// </summary>
    static void Main()
    {
        // Create a memory stream to hold the TIFF image.
        using (var tiffStream = new MemoryStream())
        {
            // Generate a Code128 barcode with the text "Sample123".
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
            {
                // Save the generated barcode image into the memory stream in TIFF format.
                generator.Save(tiffStream, BarCodeImageFormat.Tiff);
            }

            // Reset the stream position to the beginning before reading.
            tiffStream.Position = 0;

            // Initialize a barcode reader to decode all supported barcode types from the stream.
            using (var reader = new BarCodeReader(tiffStream, DecodeType.AllSupportedTypes))
            {
                // Iterate through all detected barcodes and output their text.
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine("Detected barcode: " + result.CodeText);
                }
            }
        }
    }
}