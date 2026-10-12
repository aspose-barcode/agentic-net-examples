// Title: Decode MaxiCode from a byte array and read primary & secondary messages
// Description: Demonstrates how to generate a MaxiCode barcode, convert it to a PNG byte array, and then decode it using BarCodeReader to extract both primary fields and the structured secondary message.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, focusing on complex barcode types such as MaxiCode. It showcases the use of ComplexBarcodeGenerator for creating MaxiCode, BarCodeReader for decoding, and ComplexCodetextReader for parsing the complex codetext. Developers working with shipping, logistics, or inventory systems often need to encode and decode MaxiCode data, including postal information and structured secondary messages.
// Prompt: Configure BarcodeReader to decode MaxiCode images from a byte array and retrieve both primary and secondary messages.
// Tags: maxicode, barcode, decoding, byte array, complex barcode, aspose.barcode, generation, recognition

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a MaxiCode barcode, converting it to a byte array,
/// and decoding it to retrieve primary and secondary message data.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a MaxiCode image, obtains its byte array,
    /// and decodes the barcode to display extracted information.
    /// </summary>
    static void Main()
    {
        // Generate a MaxiCode image and obtain its byte array
        byte[] imageBytes = GenerateMaxiCodeImage();

        // Decode the MaxiCode from the byte array and display messages
        DecodeMaxiCodeFromBytes(imageBytes);
    }

    /// <summary>
    /// Creates a MaxiCode (Mode 2) with a structured secondary message,
    /// renders it to PNG, and returns the image as a byte array.
    /// </summary>
    /// <returns>Byte array containing the PNG representation of the MaxiCode.</returns>
    private static byte[] GenerateMaxiCodeImage()
    {
        // Prepare MaxiCode codetext (Mode 2) with a structured secondary message
        var codetext = new MaxiCodeCodetextMode2
        {
            PostalCode = "524032140", // 9 digits
            CountryCode = 56,
            ServiceCategory = 999
        };

        // Build the structured second message (address lines, state, year, etc.)
        var secondMessage = new MaxiCodeStructuredSecondMessage();
        secondMessage.Add("634 ALPHA DRIVE");
        secondMessage.Add("PITTSBURGH");
        secondMessage.Add("PA");
        secondMessage.Year = 99;

        codetext.SecondMessage = secondMessage;

        // Create the complex barcode generator for the MaxiCode codetext
        using (var generator = new ComplexBarcodeGenerator(codetext))
        {
            // Set MaxiCode mode to 2 (structured secondary message)
            generator.Parameters.Barcode.MaxiCode.Mode = MaxiCodeMode.Mode2;

            // Optional: adjust module size for better readability
            generator.Parameters.Barcode.XDimension.Pixels = 15f;

            // Save the generated barcode to a memory stream as PNG
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                return ms.ToArray(); // Return the PNG bytes
            }
        }
    }

    /// <summary>
    /// Decodes a MaxiCode barcode from a byte array, extracts primary fields,
    /// and prints any structured or standard secondary message.
    /// </summary>
    /// <param name="imageBytes">Byte array containing the PNG image of the MaxiCode.</param>
    private static void DecodeMaxiCodeFromBytes(byte[] imageBytes)
    {
        // Create a seekable memory stream from the byte array
        using (var ms = new MemoryStream(imageBytes))
        {
            // Initialize the reader for MaxiCode decoding
            using (var reader = new BarCodeReader(ms, DecodeType.MaxiCode))
            {
                // Iterate through all detected barcodes (should be one in this case)
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    // Decode complex MaxiCode codetext into a strongly‑typed object
                    var complex = ComplexCodetextReader.TryDecodeMaxiCode(
                        result.Extended.MaxiCode.Mode,
                        result.CodeText);

                    // Process Mode 2 codetext which contains primary fields and a secondary message
                    if (complex is MaxiCodeCodetextMode2 mode2)
                    {
                        Console.WriteLine($"PostalCode: {mode2.PostalCode}");
                        Console.WriteLine($"CountryCode: {mode2.CountryCode}");
                        Console.WriteLine($"ServiceCategory: {mode2.ServiceCategory}");

                        // Handle structured secondary message
                        if (mode2.SecondMessage is MaxiCodeStructuredSecondMessage structured)
                        {
                            Console.WriteLine("Structured Second Message:");
                            foreach (string line in structured.Identifiers)
                            {
                                Console.WriteLine(line);
                            }
                            Console.WriteLine($"Year: {structured.Year}");
                        }
                        // Handle standard (plain text) secondary message
                        else if (mode2.SecondMessage is MaxiCodeStandardSecondMessage standard)
                        {
                            Console.WriteLine($"Standard Second Message: {standard.Message}");
                        }
                    }
                }
            }
        }
    }
}