// Title: Encode and Verify MaxiCode Mode 2 Postal Code
// Description: Demonstrates encoding a numeric postal code into the primary message of a MaxiCode Mode 2 barcode, generating the image, and decoding it to confirm the data matches the original.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation and recognition category. It showcases the use of ComplexBarcodeGenerator, MaxiCodeCodetextMode2, and BarCodeReader to create and read MaxiCode symbols. Developers working with shipping, logistics, or retail applications often need to encode structured data such as postal codes, country codes, and service categories into MaxiCode barcodes and verify their integrity.
// Prompt: Encode a numeric postal code in the primary message of a MaxiCode Mode 2 and verify decoding accuracy.
// Tags: maxicode, mode2, barcode, encoding, decoding, aspose.barcode, complexbarcode, c#

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates a MaxiCode Mode 2 barcode containing a numeric postal code,
/// then reads the barcode back to verify the encoded data.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a MaxiCode barcode, decodes it, and prints verification results.
    /// </summary>
    static void Main()
    {
        // Define the numeric postal code (9 digits) and related MaxiCode fields.
        const string postalCode = "123456789";
        const int countryCode = 840; // USA numeric country code
        const int serviceCategory = 999; // Example service category

        // Build the complex codetext for MaxiCode Mode 2 using the provided values.
        var maxiCodeData = new MaxiCodeCodetextMode2
        {
            PostalCode = postalCode,
            CountryCode = countryCode,
            ServiceCategory = serviceCategory,
            // Optional second message; can contain any additional information.
            SecondMessage = new MaxiCodeStandardSecondMessage { Message = "Sample data" }
        };

        // Generate the barcode image and store it in a memory stream.
        using (var generator = new ComplexBarcodeGenerator(maxiCodeData))
        {
            using (var bitmap = generator.GenerateBarCodeImage())
            {
                using (var ms = new MemoryStream())
                {
                    // Save the generated bitmap as PNG into the memory stream.
                    bitmap.Save(ms, ImageFormat.Png);
                    ms.Position = 0; // Reset stream position for reading.

                    // Decode the barcode from the memory stream using MaxiCode decoder.
                    using (var reader = new BarCodeReader(ms, DecodeType.MaxiCode))
                    {
                        foreach (var result in reader.ReadBarCodes())
                        {
                            // Attempt to decode the complex codetext based on the mode reported by the reader.
                            var decoded = ComplexCodetextReader.TryDecodeMaxiCode(result.Extended.MaxiCode.Mode, result.CodeText);
                            if (decoded is MaxiCodeCodetextMode2 decodedMode2)
                            {
                                // Verify that the decoded postal code matches the original value.
                                bool isMatch = decodedMode2.PostalCode == postalCode;
                                Console.WriteLine($"Decoded PostalCode: {decodedMode2.PostalCode}");
                                Console.WriteLine($"Match original: {isMatch}");
                            }
                            else
                            {
                                Console.WriteLine("Decoded codetext is not MaxiCode Mode 2.");
                            }
                        }
                    }
                }
            }
        }
    }
}