// Title: Swiss Post Parcel Barcode checksum auto‑correction unit test
// Description: Demonstrates how to generate a Swiss Post Parcel barcode, let Aspose.BarCode auto‑correct the checksum, and verify the corrected value by reading the barcode back.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It shows usage of BarcodeGenerator with EncodeTypes.SwissPostParcel and BarCodeReader with DecodeType.SwissPostParcel to validate checksum handling. Developers working with postal symbologies often need to ensure correct checksum calculation for compliance and scanning reliability.
// Prompt: Write a unit test that confirms checksum auto‑correction for Swiss Post Parcel international barcode.
// Tags: swisspostparcel, checksum, barcode generation, barcode recognition, unit test, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Contains the entry point and helper methods for testing checksum auto‑correction of Swiss Post Parcel barcodes.
/// </summary>
class Program
{
    /// <summary>
    /// Generates barcodes with potentially incorrect or missing checksums, reads them back, and verifies that Aspose.BarCode auto‑corrects the checksum.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for test artifacts
        string tempDir = Path.Combine(Path.GetTempPath(), "SwissPostTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Test data: input strings with wrong or missing checksum and the expected corrected value
        string wrongChecksumInput = "RM999605017CH";
        string missingChecksumInput = "RM99960501CH";
        string expectedCorrect = "RM999605013CH";

        // Execute the checksum auto‑correction tests
        bool test1 = RunChecksumAutoCorrectionTest(tempDir, wrongChecksumInput, expectedCorrect, "WrongChecksum");
        bool test2 = RunChecksumAutoCorrectionTest(tempDir, missingChecksumInput, expectedCorrect, "MissingChecksum");

        // Output test results
        Console.WriteLine($"Test Wrong Checksum: {(test1 ? "PASS" : "FAIL")}");
        Console.WriteLine($"Test Missing Checksum: {(test2 ? "PASS" : "FAIL")}");
    }

    /// <summary>
    /// Generates a Swiss Post Parcel barcode from the supplied input, saves it as an image, reads it back, and checks whether the read code matches the expected corrected value.
    /// </summary>
    /// <param name="folder">Folder where the barcode image will be saved.</param>
    /// <param name="inputCode">Barcode text to encode (may have wrong or missing checksum).</param>
    /// <param name="expectedCode">The correct barcode text expected after auto‑correction.</param>
    /// <param name="testName">Identifier used for the generated image file name.</param>
    /// <returns>True if the read barcode matches the expected corrected code; otherwise, false.</returns>
    static bool RunChecksumAutoCorrectionTest(string folder, string inputCode, string expectedCode, string testName)
    {
        // Determine the full path for the barcode image
        string imagePath = Path.Combine(folder, $"{testName}.png");

        // Generate the barcode; Aspose.BarCode automatically corrects the checksum during encoding
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, inputCode))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            generator.Parameters.Barcode.BarHeight.Pixels = 40f;
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Read the generated barcode image and extract the decoded text
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.SwissPostParcel))
        {
            BarCodeResult[] results = reader.ReadBarCodes();
            if (results.Length == 0)
                return false;

            string readCode = results[0].CodeText;
            return string.Equals(readCode, expectedCode, StringComparison.Ordinal);
        }
    }
}