// Title: Decode Dutch KIX barcode from a byte array
// Description: Demonstrates generating a Dutch KIX barcode, converting it to a PNG byte array, and decoding it while handling format exceptions.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator to create a barcode image, BarCodeReader to decode barcodes from streams, and handling of potential format errors. Developers working with barcode creation, image-to-byte conversions, and runtime decoding will find these APIs essential for building scanning solutions.
// Prompt: Decode a Dutch KIX barcode from a byte array and handle potential format exceptions.
// Tags: dutch kix, barcode, decode, byte array, format exception, aspose.barcode, generation, recognition

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a Dutch KIX barcode, converts it to a byte array,
/// and then decodes it while handling possible format exceptions.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, reads it from a memory stream,
    /// and outputs the decoded information to the console.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // Generate a Dutch KIX barcode and obtain its image as a byte array
        // ------------------------------------------------------------
        byte[] barcodeBytes;
        using (var generator = new BarcodeGenerator(EncodeTypes.DutchKIX, "123456ASPOSE"))
        {
            // Configure barcode appearance
            generator.Parameters.Barcode.XDimension.Pixels = 4;
            generator.Parameters.Barcode.BarHeight.Pixels = 50;

            // Save the barcode image to a memory stream in PNG format
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                barcodeBytes = ms.ToArray(); // Extract the byte array
            }
        }

        // ------------------------------------------------------------
        // Decode the barcode from the byte array
        // ------------------------------------------------------------
        try
        {
            using (var stream = new MemoryStream(barcodeBytes))
            using (var reader = new BarCodeReader(stream, DecodeType.DutchKIX))
            {
                // Iterate through all detected barcodes (should be one in this case)
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"CodeText: {result.CodeText}");
                    Console.WriteLine($"CodeType: {result.CodeType}");
                    Console.WriteLine($"CodeTypeName: {result.CodeTypeName}");
                }
            }
        }
        catch (FormatException ex)
        {
            // Handle cases where the barcode format is invalid or unreadable
            Console.WriteLine($"Format exception: {ex.Message}");
        }
        catch (Exception ex)
        {
            // Handle any other unexpected errors during decoding
            Console.WriteLine($"Error decoding barcode: {ex.Message}");
        }
    }
}