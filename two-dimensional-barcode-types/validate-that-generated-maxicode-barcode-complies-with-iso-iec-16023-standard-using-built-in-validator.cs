// Title: Validate MaxiCode barcode against ISO/IEC 16023 using Aspose.BarCode
// Description: Demonstrates generating a MaxiCode (Mode 2) barcode, decoding it, and performing a round‑trip check to ensure the data complies with the ISO/IEC 16023 specification.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation and recognition category. It showcases the use of ComplexBarcodeGenerator, MaxiCodeCodetextMode2, BarCodeReader, and related classes to create, read, and validate MaxiCode symbols—common tasks for logistics, shipping, and inventory systems that require ISO‑compliant MaxiCode barcodes.
// Prompt: Validate that the generated MaxiCode barcode complies with ISO/IEC 16023 standard using built‑in validator.
// Tags: maxicode, iso-iec-16023, validation, barcode generation, barcode recognition, complex barcode, aspnet.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Example program that creates a MaxiCode (Mode 2) barcode, reads it back,
/// and verifies that the encoded data matches the original input, providing
/// a practical compliance check with ISO/IEC 16023.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a MaxiCode barcode, decodes it, and validates the round‑trip data.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Create a unique temporary folder for the generated barcode image.
        // --------------------------------------------------------------------
        string tempDir = Path.Combine(Path.GetTempPath(), "MaxiCodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string barcodePath = Path.Combine(tempDir, "maxicode.png");

        // --------------------------------------------------------------------
        // Build MaxiCode codetext (Mode 2) with a structured second message.
        // --------------------------------------------------------------------
        var codetext = new MaxiCodeCodetextMode2
        {
            PostalCode = "524032140", // 9 digits required for Mode 2
            CountryCode = 56,
            ServiceCategory = 999
        };

        var secondMessage = new MaxiCodeStructuredSecondMessage();
        secondMessage.Add("634 ALPHA DRIVE");
        secondMessage.Add("PITTSBURGH");
        secondMessage.Add("PA");
        secondMessage.Year = 99;
        codetext.SecondMessage = secondMessage;

        // --------------------------------------------------------------------
        // Generate the MaxiCode barcode and save it as a PNG file.
        // --------------------------------------------------------------------
        using (var generator = new ComplexBarcodeGenerator(codetext))
        {
            generator.Parameters.Barcode.XDimension.Point = 2f;
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // --------------------------------------------------------------------
        // Read and decode the generated barcode.
        // --------------------------------------------------------------------
        using (var reader = new BarCodeReader(barcodePath, DecodeType.MaxiCode))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // Decode complex codetext from the raw code text.
                var complex = ComplexCodetextReader.TryDecodeMaxiCode(result.Extended.MaxiCode.Mode, result.CodeText);
                if (complex is MaxiCodeCodetextMode2 decoded)
                {
                    // Verify primary fields (postal code, country code, service category).
                    bool fieldsMatch = decoded.PostalCode == codetext.PostalCode &&
                                       decoded.CountryCode == codetext.CountryCode &&
                                       decoded.ServiceCategory == codetext.ServiceCategory;

                    // Verify the structured second message (identifiers and year).
                    bool secondMatch = false;
                    if (decoded.SecondMessage is MaxiCodeStructuredSecondMessage decodedSecond)
                    {
                        if (decodedSecond.Year == secondMessage.Year &&
                            decodedSecond.Identifiers.Count == secondMessage.Identifiers.Count)
                        {
                            secondMatch = true;
                            for (int i = 0; i < secondMessage.Identifiers.Count && secondMatch; i++)
                            {
                                if (decodedSecond.Identifiers[i] != secondMessage.Identifiers[i])
                                    secondMatch = false;
                            }
                        }
                    }

                    // Overall validation result.
                    bool validationPassed = fieldsMatch && secondMatch;
                    Console.WriteLine("Round‑trip validation passed: " + validationPassed);
                }
                else
                {
                    Console.WriteLine("Decoded codetext is not MaxiCodeMode2.");
                }
            }
        }

        // Note: Aspose.BarCode does not provide a dedicated ISO/IEC 16023 validator.
        // The round‑trip check above serves as a practical compliance verification.
    }
}