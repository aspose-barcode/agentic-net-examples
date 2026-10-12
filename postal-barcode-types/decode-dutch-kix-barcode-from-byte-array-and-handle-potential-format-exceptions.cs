// Title: Decode Dutch KIX barcode from byte array
// Description: Demonstrates generating a Dutch KIX barcode, converting it to a byte array, and decoding it while handling format exceptions.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes, BarCodeReader for decoding, and handling common errors. Developers working with barcode automation, image processing, or data extraction can refer to this pattern for generating barcodes in memory and reading them without file I/O.
// Prompt: Decode a Dutch KIX barcode from a byte array and handle potential format exceptions.
// Tags: dutch kix, barcode generation, barcode decoding, format exception, aspose.barcode, memory stream

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Dutch KIX barcode, stores it in a memory stream,
/// and then decodes it from the resulting byte array while handling possible format exceptions.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates, encodes, and decodes a Dutch KIX barcode.
    /// </summary>
    static void Main()
    {
        // The text to encode into the barcode.
        string codeText = "123456ASPOSE";

        // Create a barcode generator for Dutch KIX symbology.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.DutchKIX, codeText))
        {
            // Set the X-dimension (module width) to 4 pixels for better readability.
            generator.Parameters.Barcode.XDimension.Pixels = 4;

            // Use a memory stream to hold the generated barcode image.
            using (MemoryStream generationStream = new MemoryStream())
            {
                // Save the barcode as a PNG image into the memory stream.
                generator.Save(generationStream, BarCodeImageFormat.Png);

                // Convert the stream contents to a byte array.
                byte[] barcodeBytes = generationStream.ToArray();

                try
                {
                    // Create a new memory stream from the byte array for decoding.
                    using (MemoryStream decodeStream = new MemoryStream(barcodeBytes))
                    {
                        // Initialize the barcode reader for Dutch KIX symbology.
                        using (BarCodeReader reader = new BarCodeReader(decodeStream, DecodeType.DutchKIX))
                        {
                            // Iterate through all detected barcodes (should be one in this case).
                            foreach (BarCodeResult result in reader.ReadBarCodes())
                            {
                                Console.WriteLine($"Decoded Text: {result.CodeText}");
                                Console.WriteLine($"Decoded Type: {result.CodeType}");
                            }
                        }
                    }
                }
                // Handle specific format errors that may arise during decoding.
                catch (FormatException ex)
                {
                    Console.WriteLine($"Format exception: {ex.Message}");
                }
                // Catch any other unexpected exceptions.
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
    }
}