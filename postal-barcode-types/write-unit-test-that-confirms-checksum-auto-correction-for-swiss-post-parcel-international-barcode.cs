// Title: Checksum Auto‑Correction Test for Swiss Post Parcel Barcode
// Description: Demonstrates generating a Swiss Post Parcel barcode with an intentionally incorrect code text, allowing the library to auto‑correct the checksum, and verifies the correction by reading the barcode back.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, focusing on checksum handling for symbologies such as Swiss Post Parcel. It showcases the use of BarcodeGenerator with ThrowExceptionWhenCodeTextIncorrect set to false, and BarCodeReader with ChecksumValidation enabled. Developers often need to ensure barcodes are valid even when input data is incomplete or contains errors, making auto‑correction essential for robust applications.
// Prompt: Write a unit test that confirms checksum auto‑correction for Swiss Post Parcel international barcode.
// Tags: barcode, swisspostparcel, checksum, autocorrection, generation, recognition, unit-test, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a Swiss Post Parcel barcode with an incorrect code text,
/// relies on auto‑correction of the checksum, and validates the correction by reading the barcode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, reads it back, and reports whether
    /// the checksum auto‑correction succeeded.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary file path for the generated barcode image.
        string filePath = Path.Combine(Path.GetTempPath(), "SwissPostParcel.png");
        if (File.Exists(filePath))
        {
            // Ensure a clean start by deleting any existing file.
            File.Delete(filePath);
        }

        // Intentionally incorrect code text (missing checksum or wrong format).
        string incorrectCode = "1234567890";

        // Generate the barcode with auto‑correction enabled (no exception on incorrect code).
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, incorrectCode))
        {
            // Do not throw an exception; let the generator correct the code text automatically.
            generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = false;

            // Save the generated barcode image to the temporary file.
            generator.Save(filePath);
        }

        // Verify that the barcode was corrected by reading it back.
        bool testPassed = false;
        using (BarCodeReader reader = new BarCodeReader(filePath, DecodeType.SwissPostParcel))
        {
            // Enable checksum validation to ensure accurate recognition results.
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // If the recognized code text differs from the original incorrect one,
                // the auto‑correction succeeded.
                if (!string.Equals(result.CodeText, incorrectCode, StringComparison.Ordinal))
                {
                    testPassed = true;
                }

                Console.WriteLine($"Recognized CodeText: {result.CodeText}");
                break; // Only need the first result.
            }
        }

        // Clean up the temporary file.
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }

        // Output the test result.
        if (testPassed)
        {
            Console.WriteLine("TEST PASSED: Checksum auto‑correction succeeded.");
        }
        else
        {
            Console.WriteLine("TEST FAILED: Checksum auto‑correction did not occur.");
        }
    }
}