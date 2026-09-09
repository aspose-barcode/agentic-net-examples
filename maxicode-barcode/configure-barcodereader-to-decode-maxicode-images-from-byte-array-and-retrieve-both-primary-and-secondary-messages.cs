// Title: Decode MaxiCode from Byte Array and Retrieve Primary & Secondary Messages
// Description: Demonstrates how to generate a MaxiCode image, decode it from a byte array, and extract both primary and secondary message data.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode operations category. It showcases the use of ComplexBarcodeGenerator for creating MaxiCode symbols and BarCodeReader with ComplexCodetextReader to decode MaxiCode data. Developers working with shipping, logistics, or inventory systems often need to generate and read MaxiCode symbols, extracting structured secondary information such as address lines and year.
// Prompt: Configure BarcodeReader to decode MaxiCode images from a byte array and retrieve both primary and secondary messages.
// Tags: maxicode, barcode decoding, byte array, aspose.barcode, complex barcode, secondary message

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a MaxiCode image, then decodes it from a byte array
/// to retrieve primary fields (postal code, country code, service category) and secondary
/// messages (structured or standard).
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a sample MaxiCode, then decodes it.
    /// </summary>
    static void Main()
    {
        // Generate a MaxiCode image and obtain its binary representation.
        byte[] imageBytes = GenerateSampleMaxiCode();

        // Decode the generated image bytes and output the extracted data.
        DecodeMaxiCode(imageBytes);
    }

    /// <summary>
    /// Creates a MaxiCode (Mode 2) with a structured secondary message and returns the PNG bytes.
    /// </summary>
    /// <returns>Byte array containing the generated PNG image.</returns>
    static byte[] GenerateSampleMaxiCode()
    {
        // Build a structured secondary message (address lines and year).
        var structuredMessage = new MaxiCodeStructuredSecondMessage();
        structuredMessage.Add("634 ALPHA DRIVE");
        structuredMessage.Add("PITTSBURGH");
        structuredMessage.Add("PA");
        structuredMessage.Year = 99;

        // Define the primary fields and attach the secondary message.
        var codetext = new MaxiCodeCodetextMode2
        {
            PostalCode = "524032140",
            CountryCode = 56,
            ServiceCategory = 999,
            SecondMessage = structuredMessage
        };

        // Generate the MaxiCode using ComplexBarcodeGenerator.
        using (var generator = new ComplexBarcodeGenerator(codetext))
        {
            generator.Parameters.Barcode.MaxiCode.Mode = MaxiCodeMode.Mode2;
            generator.Parameters.Barcode.XDimension.Pixels = 15f;

            // Save the barcode to a memory stream in PNG format and return the bytes.
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                return ms.ToArray();
            }
        }
    }

    /// <summary>
    /// Decodes a MaxiCode image supplied as a byte array and prints primary and secondary data.
    /// </summary>
    /// <param name="imageData">Byte array containing the MaxiCode image.</param>
    static void DecodeMaxiCode(byte[] imageData)
    {
        // Specify that we want to decode MaxiCode symbology.
        BaseDecodeType decodeType = DecodeType.MaxiCode;

        // Create a memory stream from the image bytes and initialize the reader.
        using (var ms = new MemoryStream(imageData))
        using (var reader = new BarCodeReader(ms, decodeType))
        {
            // Iterate through all detected barcodes (should be one in this example).
            foreach (var result in reader.ReadBarCodes())
            {
                // Attempt to parse the complex MaxiCode codetext.
                var complexCodetext = ComplexCodetextReader.TryDecodeMaxiCode(
                    result.Extended.MaxiCode.Mode,
                    result.CodeText);

                // Handle Mode 2 MaxiCode.
                if (complexCodetext is MaxiCodeCodetextMode2 mode2)
                {
                    Console.WriteLine($"PostalCode: {mode2.PostalCode}");
                    Console.WriteLine($"CountryCode: {mode2.CountryCode}");
                    Console.WriteLine($"ServiceCategory: {mode2.ServiceCategory}");

                    // Structured secondary message.
                    if (mode2.SecondMessage is MaxiCodeStructuredSecondMessage structured)
                    {
                        Console.WriteLine("Structured Secondary Message:");
                        foreach (var line in structured.Identifiers)
                        {
                            Console.WriteLine($"  {line}");
                        }
                        Console.WriteLine($"Year: {structured.Year}");
                    }
                    // Unstructured secondary message.
                    else if (mode2.SecondMessage is MaxiCodeStandardSecondMessage standard)
                    {
                        Console.WriteLine($"Unstructured Secondary Message: {standard.Message}");
                    }
                }
                // Handle Mode 3 MaxiCode.
                else if (complexCodetext is MaxiCodeCodetextMode3 mode3)
                {
                    Console.WriteLine($"[Mode3] PostalCode: {mode3.PostalCode}");
                    Console.WriteLine($"[Mode3] CountryCode: {mode3.CountryCode}");
                    Console.WriteLine($"[Mode3] ServiceCategory: {mode3.ServiceCategory}");

                    if (mode3.SecondMessage is MaxiCodeStructuredSecondMessage structured)
                    {
                        Console.WriteLine("Structured Secondary Message (Mode3):");
                        foreach (var line in structured.Identifiers)
                        {
                            Console.WriteLine($"  {line}");
                        }
                        Console.WriteLine($"Year: {structured.Year}");
                    }
                    else if (mode3.SecondMessage is MaxiCodeStandardSecondMessage standard)
                    {
                        Console.WriteLine($"Unstructured Secondary Message (Mode3): {standard.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("Decoded MaxiCode is not in Mode 2 or 3.");
                }
            }
        }
    }
}