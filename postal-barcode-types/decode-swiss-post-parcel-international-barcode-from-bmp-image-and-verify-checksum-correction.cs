// Title: Decode Swiss Post Parcel barcode with checksum correction
// Description: Demonstrates decoding a Swiss Post Parcel international barcode from a BMP image, showing automatic checksum correction when the barcode contains an incorrect checksum.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It illustrates how to use BarcodeGenerator to create Swiss Post Parcel barcodes, BarCodeReader to decode them, and how the library automatically corrects checksum errors. Developers working with postal barcodes, parcel tracking, or any scenario requiring reliable barcode validation will find these APIs essential.
// Prompt: Decode a Swiss Post Parcel international barcode from a BMP image and verify checksum correction.
// Tags: swisspost, parcel, barcode, decode, checksum, bmp, aspose.barcode, generation, recognition

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates Swiss Post Parcel barcodes with both correct and incorrect checksums,
/// then decodes the erroneous barcode to demonstrate automatic checksum correction.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates temporary barcode images, decodes the one with a wrong checksum,
    /// and outputs the result to the console.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the demo files
        string tempDir = Path.Combine(Path.GetTempPath(), "SwissPostDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Define file paths for the generated images
        string wrongImagePath = Path.Combine(tempDir, "SwissPostInternational_WrongChecksum.bmp");
        string correctImagePath = Path.Combine(tempDir, "SwissPostInternational_CorrectChecksum.bmp");

        // -----------------------------------------------------------------
        // Generate a barcode with an intentionally wrong checksum
        // -----------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, "RM999605017CH"))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            generator.Parameters.Barcode.BarHeight.Pixels = 40f;
            generator.Save(wrongImagePath, BarCodeImageFormat.Bmp);
        }

        // -----------------------------------------------------------------
        // Generate a barcode with the correct checksum for comparison
        // -----------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, "RM999605013CH"))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            generator.Parameters.Barcode.BarHeight.Pixels = 40f;
            generator.Save(correctImagePath, BarCodeImageFormat.Bmp);
        }

        // Expected text after checksum correction
        const string expectedCorrectCode = "RM999605013CH";

        // -----------------------------------------------------------------
        // Decode the barcode that contains the wrong checksum
        // The library should automatically correct the checksum during decoding
        // -----------------------------------------------------------------
        using (var reader = new BarCodeReader(wrongImagePath, DecodeType.SwissPostParcel))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Decoded CodeText: {result.CodeText}");
                if (string.Equals(result.CodeText, expectedCorrectCode, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Checksum was automatically corrected during decoding.");
                }
                else
                {
                    Console.WriteLine("Decoded text does not match the expected corrected value.");
                }
            }
        }

        // -----------------------------------------------------------------
        // Clean up temporary files (optional)
        // -----------------------------------------------------------------
        try
        {
            File.Delete(wrongImagePath);
            File.Delete(correctImagePath);
            Directory.Delete(tempDir);
        }
        catch
        {
            // Ignored – cleanup failures should not affect program outcome
        }
    }
}