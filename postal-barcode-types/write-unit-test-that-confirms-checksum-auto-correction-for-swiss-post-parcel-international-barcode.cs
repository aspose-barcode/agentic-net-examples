// Title: Checksum Auto‑Correction Test for Swiss Post Parcel Barcode
// Description: Demonstrates generating a Swiss Post Parcel barcode with an incorrect checksum and verifying that the library auto‑corrects it during reading, as well as generating a barcode without an explicit checksum.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, focusing on the SwissPostParcel symbology. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader for decoding them, highlighting typical scenarios where developers need to validate or auto‑correct checksums in postal barcodes. Ideal for unit‑testing barcode integrity in logistics applications.
// Prompt: Write a unit test that confirms checksum auto‑correction for Swiss Post Parcel international barcode.
// Tags: barcode, swisspostparcel, checksum, generation, recognition, unit-test, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Contains a simple console‑based test that verifies checksum auto‑correction and generation
/// for the Swiss Post Parcel barcode symbology using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the test application. Generates barcodes with erroneous or missing checksums,
    /// reads them back, and prints the verification results.
    /// </summary>
    static void Main()
    {
        // Create a temporary directory for barcode image files
        string tempDir = Path.Combine(Path.GetTempPath(), "SwissPostTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Expected correct barcode text after checksum correction/generation
        string expected = "RM999605013CH";

        // ------------------------------------------------------------
        // Test case 1: Provide a barcode string with an incorrect checksum.
        // The reader should auto‑correct the checksum and return the expected value.
        // ------------------------------------------------------------
        string erroneousInput = "RM999605017CH";
        string erroneousFile = Path.Combine(tempDir, "erroneous.png");
        string readErroneous = GenerateAndRead(erroneousInput, erroneousFile);
        Console.WriteLine($"Input with wrong checksum read as: {readErroneous}");
        Console.WriteLine(readErroneous == expected ? "Checksum auto‑correction test passed." : "Checksum auto‑correction test failed.");

        // ------------------------------------------------------------
        // Test case 2: Provide a barcode string without a checksum.
        // The generator should calculate and embed the correct checksum automatically.
        // ------------------------------------------------------------
        string withoutChecksumInput = "RM99960501CH";
        string withoutChecksumFile = Path.Combine(tempDir, "without.png");
        string readWithout = GenerateAndRead(withoutChecksumInput, withoutChecksumFile);
        Console.WriteLine($"Input without checksum read as: {readWithout}");
        Console.WriteLine(readWithout == expected ? "Checksum generation test passed." : "Checksum generation test failed.");

        // Clean up temporary files and directory
        try
        {
            Directory.Delete(tempDir, true);
        }
        catch
        {
            // Ignore cleanup errors (e.g., files still in use)
        }
    }

    /// <summary>
    /// Generates a Swiss Post Parcel barcode image from the supplied text, saves it to the specified path,
    /// then reads the barcode back using a <see cref="BarCodeReader"/> and returns the decoded text.
    /// </summary>
    /// <param name="codeText">The barcode text to encode (may include or omit checksum).</param>
    /// <param name="filePath">Full file path where the generated PNG image will be saved.</param>
    /// <returns>The decoded barcode text, or <c>null</c> if reading fails.</returns>
    static string GenerateAndRead(string codeText, string filePath)
    {
        // Initialize the generator for Swiss Post Parcel symbology with the provided text
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, codeText))
        {
            // Configure visual parameters: module size and bar height
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            generator.Parameters.Barcode.BarHeight.Pixels = 40f;

            // Save the generated barcode image to disk
            generator.Save(filePath, BarCodeImageFormat.Png);

            // Create a reader that works directly with the generated image in memory
            using (BarCodeReader reader = new BarCodeReader(generator.GenerateBarCodeImage(), DecodeType.SwissPostParcel))
            {
                // Iterate through detected barcodes (expecting a single result)
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    return result.CodeText; // Return the decoded text
                }
            }
        }

        // Return null if no barcode was read
        return null;
    }
}