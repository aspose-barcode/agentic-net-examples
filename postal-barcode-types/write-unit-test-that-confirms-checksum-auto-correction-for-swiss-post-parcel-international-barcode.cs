// Title: Verify checksum auto‑correction for Swiss Post Parcel barcode
// Description: Demonstrates generating a Swiss Post Parcel barcode without a checksum, letting the library auto‑correct it, and confirming the correction via recognition.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, focusing on checksum handling for Swiss Post Parcel symbology. It showcases the use of BarcodeGenerator, BarCodeReader, and related settings such as ThrowExceptionWhenCodeTextIncorrect and ChecksumValidation. Developers often need to ensure barcodes are valid even when input data lacks required check digits, making this pattern useful for automated validation and correction workflows.
// Prompt: Write a unit test that confirms checksum auto‑correction for Swiss Post Parcel international barcode.
// Tags: swisspostparcel, checksum, auto-correction, barcode generation, barcode recognition, unit-test

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a Swiss Post Parcel barcode without a checksum,
/// enables automatic checksum correction, and verifies the correction via recognition.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, corrects the checksum,
    /// reads it back, and reports whether the auto‑correction succeeded.
    /// </summary>
    static void Main()
    {
        // Base code without checksum (placeholder data)
        string baseCode = "123456789012";

        // Create a generator for Swiss Post Parcel barcode with the base code
        using (var generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, baseCode))
        {
            // Allow the generator to correct an incorrect codetext instead of throwing
            generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = false;

            // Save the generated barcode image to a memory stream (PNG format)
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading

                // Retrieve the possibly corrected code text (checksum added)
                string correctedCode = generator.CodeText;

                // Initialize a reader to recognize the barcode from the memory stream
                using (var reader = new BarCodeReader(ms, DecodeType.SwissPostParcel))
                {
                    // Enable checksum validation during recognition
                    reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

                    bool passed = false;

                    // Iterate through all detected barcodes and compare with corrected code
                    foreach (var result in reader.ReadBarCodes())
                    {
                        if (result.CodeText == correctedCode)
                        {
                            passed = true;
                            break;
                        }
                    }

                    // Output the verification result
                    Console.WriteLine(passed
                        ? "PASS: Checksum auto‑correction verified."
                        : "FAILED: Checksum auto‑correction not verified.");
                }
            }
        }
    }
}