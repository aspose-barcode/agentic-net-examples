// Title: Validate MaxiCode barcode against ISO/IEC 16023 using Aspose.BarCode validator
// Description: Demonstrates generating a MaxiCode (Mode 2) barcode, saving it to a PNG stream, and validating its contents with the built‑in MaxiCode validator to ensure compliance with ISO/IEC 16023.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, focusing on complex barcode types such as MaxiCode. It showcases the use of ComplexBarcodeGenerator for creating a MaxiCode, Image saving via Aspose.Drawing, and BarCodeReader with DecodeType.MaxiCode for decoding. Developers often need to generate MaxiCode for shipping labels and verify the encoded data programmatically; this snippet illustrates the typical workflow and key API classes (ComplexBarcodeGenerator, MaxiCodeCodetextMode2, BarCodeReader, ComplexCodetextReader).
// Prompt: Validate that the generated MaxiCode barcode complies with ISO/IEC 16023 standard using built‑in validator.
// Tags: maxicode, barcode, validation, generation, decoding, iso/iec 16023, aspnet, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a MaxiCode barcode (Mode 2), saves it to a PNG stream,
/// and validates the encoded data using Aspose.BarCode's built‑in validator to ensure
/// compliance with ISO/IEC 16023.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs barcode generation, image handling,
    /// decoding, and validation of the MaxiCode data.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // 1. Prepare MaxiCode codetext (Mode 2) with a standard second message
        // ------------------------------------------------------------
        var maxiCode = new MaxiCodeCodetextMode2
        {
            PostalCode = "524032140",   // 9‑digit US postal code
            CountryCode = 56,           // Country code
            ServiceCategory = 999       // Service category
        };

        var secondMessage = new MaxiCodeStandardSecondMessage
        {
            Message = "Test message"
        };
        maxiCode.SecondMessage = secondMessage;

        // ------------------------------------------------------------
        // 2. Generate the MaxiCode barcode image using ComplexBarcodeGenerator
        // ------------------------------------------------------------
        using (var generator = new ComplexBarcodeGenerator(maxiCode))
        {
            using (var image = generator.GenerateBarCodeImage())
            {
                // ------------------------------------------------------------
                // 3. Save the image to a memory stream in PNG format
                // ------------------------------------------------------------
                using (var ms = new MemoryStream())
                {
                    image.Save(ms, ImageFormat.Png);
                    ms.Position = 0; // Reset stream position for subsequent reading

                    // ------------------------------------------------------------
                    // 4. Validate the generated barcode by decoding it
                    // ------------------------------------------------------------
                    using (var reader = new BarCodeReader(ms, DecodeType.MaxiCode))
                    {
                        bool anyResult = false;

                        foreach (BarCodeResult result in reader.ReadBarCodes())
                        {
                            anyResult = true;

                            // Decode the complex codetext using the built‑in validator
                            var decoded = ComplexCodetextReader.TryDecodeMaxiCode(
                                result.Extended.MaxiCode.MaxiCodeMode,
                                result.CodeText);

                            if (decoded is MaxiCodeCodetextMode2 decodedMode2)
                            {
                                // Compare each field with the original data
                                bool isValid = decodedMode2.PostalCode == maxiCode.PostalCode &&
                                               decodedMode2.CountryCode == maxiCode.CountryCode &&
                                               decodedMode2.ServiceCategory == maxiCode.ServiceCategory &&
                                               ((MaxiCodeStandardSecondMessage)decodedMode2.SecondMessage).Message == secondMessage.Message;

                                Console.WriteLine(isValid
                                    ? "Validation passed: decoded data matches original."
                                    : "Validation failed: decoded data does not match original.");
                            }
                            else
                            {
                                Console.WriteLine("Validation failed: decoded codetext type is not MaxiCodeCodetextMode2.");
                            }
                        }

                        if (!anyResult)
                        {
                            Console.WriteLine("Validation failed: no barcode detected in the generated image.");
                        }
                    }
                }
            }
        }
    }
}