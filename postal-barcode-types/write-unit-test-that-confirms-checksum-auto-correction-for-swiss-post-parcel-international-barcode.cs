// Title: Checksum Auto‑Correction Test for Swiss Post Parcel Barcode
// Description: Demonstrates generating a Swiss Post Parcel barcode with an incorrect checksum, allowing the Aspose.BarCode library to auto‑correct it, and verifies the corrected value by reading the barcode back.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, showcasing how to work with EncodeTypes.SwissPostParcel and DecodeType.SwissPostParcel. It illustrates typical use cases such as barcode creation, automatic checksum handling, and validation through reading, which developers often need when integrating postal barcode standards into their applications.
// Prompt: Write a unit test that confirms checksum auto‑correction for Swiss Post Parcel international barcode.
// Tags: barcode, swisspost, checksum, auto-correction, generation, recognition, unit-test, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Contains a simple console‑based test that verifies automatic checksum correction
/// for the Swiss Post Parcel barcode symbology using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the test application.
    /// Generates barcodes with both incorrect and correct checksums,
    /// reads them back, and prints the verification results.
    /// </summary>
    static void Main()
    {
        // Create a temporary directory for barcode image files
        string tempDir = Path.Combine(Path.GetTempPath(), "SwissPostTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Input strings: one with an erroneous checksum, one already correct
        string wrongInput = "RM999605017CH";
        string correctInput = "RM999605013CH";
        string expectedCorrect = "RM999605013CH";

        // Paths for the generated PNG images
        string wrongPath = Path.Combine(tempDir, "wrong.png");
        string correctPath = Path.Combine(tempDir, "correct.png");

        // Generate barcode with erroneous checksum (library should auto‑correct it)
        using (var gen = new BarcodeGenerator(EncodeTypes.SwissPostParcel, wrongInput))
        {
            gen.Parameters.Barcode.XDimension.Pixels = 2;
            gen.Parameters.Barcode.BarHeight.Pixels = 40;
            gen.Save(wrongPath, BarCodeImageFormat.Png);
        }

        // Generate barcode with correct checksum (should remain unchanged)
        using (var gen = new BarcodeGenerator(EncodeTypes.SwissPostParcel, correctInput))
        {
            gen.Parameters.Barcode.XDimension.Pixels = 2;
            gen.Parameters.Barcode.BarHeight.Pixels = 40;
            gen.Save(correctPath, BarCodeImageFormat.Png);
        }

        // Read back the barcode generated from the erroneous input
        string readFromWrong = null;
        using (var reader = new BarCodeReader(wrongPath, DecodeType.SwissPostParcel))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                readFromWrong = result.CodeText;
                break; // Only need the first result
            }
        }

        // Read back the barcode generated from the correct input
        string readFromCorrect = null;
        using (var reader = new BarCodeReader(correctPath, DecodeType.SwissPostParcel))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                readFromCorrect = result.CodeText;
                break; // Only need the first result
            }
        }

        // Verify that the auto‑corrected barcode matches the expected correct value
        bool test1 = readFromWrong == expectedCorrect;
        // Verify that the correctly generated barcode remains unchanged
        bool test2 = readFromCorrect == expectedCorrect;

        // Output test results
        Console.WriteLine($"Test erroneous checksum auto‑correction: {(test1 ? "PASS" : "FAIL")}");
        Console.WriteLine($"Read from wrong input: {readFromWrong ?? "null"}");
        Console.WriteLine($"Test correct checksum unchanged: {(test2 ? "PASS" : "FAIL")}");
        Console.WriteLine($"Read from correct input: {readFromCorrect ?? "null"}");

        // Cleanup temporary files and directory
        try
        {
            if (File.Exists(wrongPath)) File.Delete(wrongPath);
            if (File.Exists(correctPath)) File.Delete(correctPath);
            Directory.Delete(tempDir, true);
        }
        catch
        {
            // Ignored – cleanup failures should not affect test outcome
        }
    }
}