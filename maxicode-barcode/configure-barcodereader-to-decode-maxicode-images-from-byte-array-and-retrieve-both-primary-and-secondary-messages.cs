// Title: Decode MaxiCode from byte array and extract primary & secondary messages
// Description: Demonstrates generating a MaxiCode (Mode 2) image, converting it to a byte array, and using BarCodeReader to decode both the primary postal information and the secondary message.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, focusing on complex barcode types such as MaxiCode. It showcases the use of ComplexBarcodeGenerator, BarCodeReader, and ComplexCodetextReader to handle encoding and decoding of structured MaxiCode data, a common requirement for shipping and logistics applications where both address and custom messages are embedded.
// Prompt: Configure BarcodeReader to decode MaxiCode images from a byte array and retrieve both primary and secondary messages.
// Tags: maxicode, barcode, decoding, byte array, primary message, secondary message, aspnet.barcode, complexbarcode, codetext

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a MaxiCode barcode, converting it to a byte array,
/// and decoding it to retrieve both primary (postal) and secondary (custom) messages.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example.
    /// </summary>
    static void Main()
    {
        // Create a MaxiCode codetext (Mode 2) with a standard second message
        var maxiCodeData = new MaxiCodeCodetextMode2
        {
            PostalCode = "524032140",   // 9‑digit US postal code
            CountryCode = 56,           // Example country code
            ServiceCategory = 999       // Example service category
        };
        var secondMessage = new MaxiCodeStandardSecondMessage
        {
            Message = "Test message"
        };
        maxiCodeData.SecondMessage = secondMessage;

        // Generate the MaxiCode image into a memory stream
        using (var generator = new ComplexBarcodeGenerator(maxiCodeData))
        {
            using (var imageStream = new MemoryStream())
            {
                // Save the generated barcode as PNG into the stream
                generator.Save(imageStream, BarCodeImageFormat.Png);
                byte[] imageBytes = imageStream.ToArray();

                // Decode the image from the byte array
                using (var inputStream = new MemoryStream(imageBytes))
                {
                    using (var reader = new BarCodeReader(inputStream, DecodeType.MaxiCode))
                    {
                        // Iterate through all detected barcodes (should be one)
                        foreach (var result in reader.ReadBarCodes())
                        {
                            // Retrieve the MaxiCode mode from the extended parameters
                            var mode = result.Extended.MaxiCode.Mode;

                            // Decode the raw codetext into a structured object
                            var decoded = ComplexCodetextReader.TryDecodeMaxiCode(mode, result.CodeText);

                            // Output primary (postal) and secondary (message) information
                            if (decoded is MaxiCodeCodetextMode2 m2)
                            {
                                Console.WriteLine($"Postal Code: {m2.PostalCode}");
                                Console.WriteLine($"Country Code: {m2.CountryCode}");
                                Console.WriteLine($"Service Category: {m2.ServiceCategory}");

                                if (m2.SecondMessage is MaxiCodeStandardSecondMessage stdMsg)
                                {
                                    Console.WriteLine($"Second Message: {stdMsg.Message}");
                                }
                            }
                            else if (decoded is MaxiCodeCodetextMode3 m3)
                            {
                                Console.WriteLine($"Postal Code: {m3.PostalCode}");
                                Console.WriteLine($"Country Code: {m3.CountryCode}");
                                Console.WriteLine($"Service Category: {m3.ServiceCategory}");

                                if (m3.SecondMessage is MaxiCodeStandardSecondMessage stdMsg)
                                {
                                    Console.WriteLine($"Second Message: {stdMsg.Message}");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Decoded MaxiCode type is not recognized.");
                            }
                        }
                    }
                }
            }
        }
    }
}