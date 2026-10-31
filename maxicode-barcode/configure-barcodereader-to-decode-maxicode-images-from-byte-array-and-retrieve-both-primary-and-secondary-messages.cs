// Title: Decode MaxiCode from a byte array and extract primary and secondary messages
// Description: Demonstrates how to generate a MaxiCode barcode, store it in a byte array, and use BarCodeReader to decode both the primary structured data and the secondary message.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode operations category, focusing on MaxiCode generation and recognition. It showcases the ComplexBarcodeGenerator for creating MaxiCode with structured secondary messages and the BarCodeReader with DecodeType.MaxiCode for extracting detailed codetext. Developers working with shipping, logistics, or inventory systems often need to encode and decode MaxiCode data, making this pattern essential for handling both primary and secondary information.
// Prompt: Configure BarcodeReader to decode MaxiCode images from a byte array and retrieve both primary and secondary messages.
// Tags: maxicode, barcode, decoding, bytearray, aspose.barcode, complexbarcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a MaxiCode barcode, converting it to a byte array,
/// and decoding it to retrieve primary and secondary messages.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a sample MaxiCode and decodes it.
    /// </summary>
    static void Main()
    {
        // Generate a sample MaxiCode image with a structured secondary message
        byte[] barcodeBytes = GenerateSampleMaxiCode();

        // Decode the MaxiCode from the byte array
        DecodeMaxiCode(barcodeBytes);
    }

    /// <summary>
    /// Creates a MaxiCode barcode (Mode 2) with a structured secondary message
    /// and returns the image as a PNG byte array.
    /// </summary>
    /// <returns>Byte array containing the generated PNG image.</returns>
    static byte[] GenerateSampleMaxiCode()
    {
        // Build the structured secondary message
        var secondMessage = new MaxiCodeStructuredSecondMessage();
        secondMessage.Add("634 ALPHA DRIVE");
        secondMessage.Add("PITTSBURGH");
        secondMessage.Add("PA");
        secondMessage.Year = 99;

        // Define the primary codetext for Mode 2
        var codetext = new MaxiCodeCodetextMode2
        {
            PostalCode = "524032140", // 9 digits for Mode 2
            CountryCode = 56,
            ServiceCategory = 999,
            SecondMessage = secondMessage
        };

        // Generate the barcode using ComplexBarcodeGenerator
        using (var generator = new ComplexBarcodeGenerator(codetext))
        {
            // Set MaxiCode mode to 2
            generator.Parameters.Barcode.MaxiCode.Mode = MaxiCodeMode.Mode2;

            // Optional visual settings
            generator.Parameters.Barcode.XDimension.Pixels = 15f;
            generator.Parameters.Barcode.CodeTextParameters.TwoDDisplayText = "Sample MaxiCode";

            // Save the barcode to a memory stream as PNG and return the byte array
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                return ms.ToArray();
            }
        }
    }

    /// <summary>
    /// Decodes a MaxiCode image supplied as a byte array and prints both primary
    /// and secondary messages to the console.
    /// </summary>
    /// <param name="imageBytes">Byte array containing the MaxiCode image.</param>
    static void DecodeMaxiCode(byte[] imageBytes)
    {
        // Wrap the byte array in a seekable memory stream
        using (var ms = new MemoryStream(imageBytes))
        {
            // Create a BarCodeReader configured for MaxiCode
            using (var reader = new BarCodeReader(ms, DecodeType.MaxiCode))
            {
                // Iterate through all detected barcodes
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    // Retrieve the MaxiCode mode from extended parameters
                    var mode = result.Extended.MaxiCode.Mode;

                    // Attempt to decode the complex codetext based on the mode
                    MaxiCodeCodetext complexCodetext = ComplexCodetextReader.TryDecodeMaxiCode(mode, result.CodeText);
                    if (complexCodetext == null)
                        continue;

                    // Handle Mode 2 structured codetext
                    if (complexCodetext is MaxiCodeCodetextMode2 mode2)
                    {
                        Console.WriteLine("Primary Message:");
                        Console.WriteLine($"PostalCode: {mode2.PostalCode}");
                        Console.WriteLine($"CountryCode: {mode2.CountryCode}");
                        Console.WriteLine($"ServiceCategory: {mode2.ServiceCategory}");

                        if (mode2.SecondMessage is MaxiCodeStructuredSecondMessage structured)
                        {
                            Console.WriteLine("Secondary Structured Message:");
                            foreach (string line in structured.Identifiers)
                            {
                                Console.WriteLine(line);
                            }
                            Console.WriteLine($"Year: {structured.Year}");
                        }
                    }
                    // Handle Mode 3 unstructured codetext
                    else if (complexCodetext is MaxiCodeCodetextMode3 mode3)
                    {
                        Console.WriteLine("Primary Message:");
                        Console.WriteLine($"PostalCode: {mode3.PostalCode}");
                        Console.WriteLine($"CountryCode: {mode3.CountryCode}");
                        Console.WriteLine($"ServiceCategory: {mode3.ServiceCategory}");

                        if (mode3.SecondMessage is MaxiCodeStandardSecondMessage standard)
                        {
                            Console.WriteLine("Secondary Unstructured Message:");
                            Console.WriteLine(standard.Message);
                        }
                    }
                }
            }
        }
    }
}