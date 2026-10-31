// Title: Encode numeric postal code in MaxiCode Mode 2 and verify decoding
// Description: Demonstrates how to generate a MaxiCode Mode 2 barcode with a numeric postal code in the primary message, add a structured secondary message, and validate the encoded data by decoding the image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, focusing on complex barcode types such as MaxiCode. It showcases the use of ComplexBarcodeGenerator, MaxiCodeCodetextMode2, and BarCodeReader to create, customize, and read MaxiCode symbols—common tasks for shipping, logistics, and inventory systems that require high‑density data encoding.
// Prompt: Encode a numeric postal code in the primary message of a MaxiCode Mode 2 and verify decoding accuracy.
// Tags: maxicode, mode2, barcode generation, barcode recognition, complex barcode, aspnet, csharp, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates encoding a numeric postal code in a MaxiCode Mode 2 barcode,
/// adding a structured secondary message, and verifying the encoded data by decoding the generated image.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a MaxiCode Mode 2 barcode, saves it to a temporary file,
    /// decodes it, and compares the decoded values with the original input.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary file path for the generated barcode image
        string tempPath = Path.Combine(Path.GetTempPath(), "MaxiCodeMode2.png");

        // Create MaxiCode codetext for Mode 2 with a numeric postal code and other required fields
        var maxiCodeCodetext = new MaxiCodeCodetextMode2
        {
            PostalCode = "123456789", // 9 numeric digits
            CountryCode = 56,
            ServiceCategory = 1
        };

        // Build a structured secondary message (address lines, state, and year)
        var secondMessage = new MaxiCodeStructuredSecondMessage();
        secondMessage.Add("123 MAIN STREET");
        secondMessage.Add("ANYTOWN");
        secondMessage.Add("CA");
        secondMessage.Year = 23;
        maxiCodeCodetext.SecondMessage = secondMessage;

        // Generate the barcode using ComplexBarcodeGenerator
        using (var generator = new ComplexBarcodeGenerator(maxiCodeCodetext))
        {
            // Set the MaxiCode mode to 2
            generator.Parameters.Barcode.MaxiCode.Mode = MaxiCodeMode.Mode2;
            // Optionally adjust the module size (pixel dimension)
            generator.Parameters.Barcode.XDimension.Pixels = 10;
            // Save the barcode image as PNG
            generator.Save(tempPath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image file was created successfully
        if (!File.Exists(tempPath))
        {
            Console.WriteLine("Failed to generate barcode image.");
            return;
        }

        // Decode the generated barcode image
        using (var reader = new BarCodeReader(tempPath, DecodeType.MaxiCode))
        {
            var results = reader.ReadBarCodes();
            if (results.Length == 0)
            {
                Console.WriteLine("No barcode detected during decoding.");
                return;
            }

            // Process each decoded result
            foreach (var result in results)
            {
                // Attempt to decode complex MaxiCode codetext
                var decodedCodetext = ComplexCodetextReader.TryDecodeMaxiCode(result.Extended.MaxiCode.Mode, result.CodeText);
                if (decodedCodetext is MaxiCodeCodetextMode2 decodedMode2)
                {
                    // Compare primary message fields
                    bool postalMatch = decodedMode2.PostalCode == maxiCodeCodetext.PostalCode;
                    bool countryMatch = decodedMode2.CountryCode == maxiCodeCodetext.CountryCode;
                    bool serviceMatch = decodedMode2.ServiceCategory == maxiCodeCodetext.ServiceCategory;

                    Console.WriteLine($"PostalCode match: {postalMatch}");
                    Console.WriteLine($"CountryCode match: {countryMatch}");
                    Console.WriteLine($"ServiceCategory match: {serviceMatch}");

                    // Verify the structured secondary message if present
                    if (decodedMode2.SecondMessage is MaxiCodeStructuredSecondMessage decodedSecond)
                    {
                        bool linesMatch = decodedSecond.Identifiers.Count == secondMessage.Identifiers.Count;
                        if (linesMatch)
                        {
                            for (int i = 0; i < decodedSecond.Identifiers.Count; i++)
                            {
                                if (decodedSecond.Identifiers[i] != secondMessage.Identifiers[i])
                                {
                                    linesMatch = false;
                                    break;
                                }
                            }
                        }
                        Console.WriteLine($"Structured second message match: {linesMatch}");
                        Console.WriteLine($"Year match: {decodedSecond.Year == secondMessage.Year}");
                    }
                }
                else
                {
                    Console.WriteLine("Decoded codetext is not of expected Mode2 type.");
                }
            }
        }

        // Clean up the temporary barcode image file
        try
        {
            File.Delete(tempPath);
        }
        catch
        {
            // Ignore any cleanup errors
        }
    }
}