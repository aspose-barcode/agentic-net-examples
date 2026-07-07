// Title: Decode MaxiCode from Byte Array and Retrieve Primary & Secondary Messages
// Description: Demonstrates how to generate a MaxiCode (Mode 2), save it to a memory stream, and use BarCodeReader to decode the image from a byte array, extracting both primary and secondary message data.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation and recognition category. It showcases the use of ComplexBarcodeGenerator for creating MaxiCode symbols and BarCodeReader for decoding them. Developers working with logistics, shipping, or retail often need to encode and decode MaxiCode data, including structured primary and secondary messages, using the MaxiCodeCodetextMode2, MaxiCodeStandardSecondMessage, and related API classes.
// Prompt: Configure BarcodeReader to decode MaxiCode images from a byte array and retrieve both primary and secondary messages.
// Tags: maxicode, barcode decoding, byte array, complex barcode, aspose.barcode, c#

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a MaxiCode (Mode 2), stores it in a memory stream,
/// and decodes it using <see cref="BarCodeReader"/> to retrieve both primary and secondary messages.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a MaxiCode, writes it to a PNG stream,
    /// and reads the barcode back, printing decoded information to the console.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // 1. Create a MaxiCode codetext (Mode 2) with a standard secondary message.
        // ------------------------------------------------------------
        var maxiCodeCodetext = new MaxiCodeCodetextMode2
        {
            PostalCode = "524032140",   // 9‑digit US postal code
            CountryCode = 56,           // Country code
            ServiceCategory = 999       // Service category
        };

        var secondMessage = new MaxiCodeStandardSecondMessage
        {
            Message = "Sample secondary message"
        };
        maxiCodeCodetext.SecondMessage = secondMessage;

        // ------------------------------------------------------------
        // 2. Generate the barcode image using ComplexBarcodeGenerator.
        // ------------------------------------------------------------
        using (var complexGenerator = new ComplexBarcodeGenerator(maxiCodeCodetext))
        {
            // Generate the barcode as a bitmap.
            using (var bitmap = complexGenerator.GenerateBarCodeImage())
            {
                // Save the bitmap to a memory stream in PNG format.
                using (var imageStream = new MemoryStream())
                {
                    bitmap.Save(imageStream, ImageFormat.Png);
                    imageStream.Position = 0; // Reset stream position for reading.

                    // ------------------------------------------------------------
                    // 3. Decode the MaxiCode from the byte array (memory stream).
                    // ------------------------------------------------------------
                    using (var reader = new BarCodeReader())
                    {
                        // Set the image source for the reader.
                        reader.SetBarCodeImage(imageStream);

                        // Restrict decoding to MaxiCode symbols only.
                        reader.BarCodeReadType = DecodeType.MaxiCode;

                        // Iterate through all detected barcodes.
                        foreach (var result in reader.ReadBarCodes())
                        {
                            Console.WriteLine($"Detected barcode type: {result.CodeTypeName}");
                            Console.WriteLine($"Raw CodeText: {result.CodeText}");

                            // Decode the complex codetext to obtain structured data.
                            var decoded = ComplexCodetextReader.TryDecodeMaxiCode(
                                result.Extended.MaxiCode.MaxiCodeMode,
                                result.CodeText);

                            // --------------------------------------------------------
                            // 4. Process decoded data for Mode 2 (primary & secondary messages).
                            // --------------------------------------------------------
                            if (decoded is MaxiCodeCodetextMode2 mode2)
                            {
                                Console.WriteLine("=== Primary Message ===");
                                Console.WriteLine($"Postal Code: {mode2.PostalCode}");
                                Console.WriteLine($"Country Code: {mode2.CountryCode}");
                                Console.WriteLine($"Service Category: {mode2.ServiceCategory}");

                                Console.WriteLine("=== Secondary Message ===");
                                if (mode2.SecondMessage is MaxiCodeStandardSecondMessage stdMsg)
                                {
                                    Console.WriteLine($"Message: {stdMsg.Message}");
                                }
                                else if (mode2.SecondMessage is MaxiCodeStructuredSecondMessage structMsg)
                                {
                                    Console.WriteLine("Identifiers:");
                                    foreach (var id in structMsg.Identifiers)
                                    {
                                        Console.WriteLine($"  {id}");
                                    }
                                    Console.WriteLine($"Year: {structMsg.Year}");
                                }
                            }
                            else if (decoded is MaxiCodeCodetextMode3 mode3)
                            {
                                // Handling for Mode 3 can be added here if required.
                                Console.WriteLine("Decoded as MaxiCode Mode 3 (not shown in this sample).");
                            }
                            else
                            {
                                Console.WriteLine("Unable to decode MaxiCode complex codetext.");
                            }
                        }
                    }
                }
            }
        }
    }
}