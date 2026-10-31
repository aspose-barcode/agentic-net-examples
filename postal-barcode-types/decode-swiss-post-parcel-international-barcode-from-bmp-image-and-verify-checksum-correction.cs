// Title: Decode Swiss Post Parcel Barcode and Verify Checksum Correction
// Description: Demonstrates decoding a Swiss Post Parcel international barcode from a BMP image and confirming that the library corrects an incorrect checksum during generation.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, showcasing how to generate a Swiss Post Parcel barcode with an intentionally wrong checksum, let the library auto-correct it, save as BMP, and then read it back using BarCodeReader. Developers working with postal symbologies can learn how to handle checksum validation, image formats, and barcode parameters using EncodeTypes, DecodeType, BarcodeGenerator, and BarCodeReader classes.
// Prompt: Decode a Swiss Post Parcel international barcode from a BMP image and verify checksum correction.
// Tags: barcode, swisspost, parcel, checksum, generation, recognition, bmp, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a Swiss Post Parcel barcode with an incorrect checksum,
/// lets the Aspose.BarCode library correct it, saves the image as BMP, and then decodes it
/// to verify the checksum correction.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs barcode generation, saving, reading, and verification.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // --------------------------------------------------------------------
        // Prepare a unique temporary folder and file path for the barcode image
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "SwissPostDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "SwissPostInternational.bmp");

        // --------------------------------------------------------------------
        // Define an intentionally wrong checksum value; the library will correct it
        // --------------------------------------------------------------------
        string wrongCode = "RM999605017CH";
        // Expected value after the library corrects the checksum
        string expectedCorrect = "RM999605013CH";

        // --------------------------------------------------------------------
        // Generate the barcode using the wrong checksum and save it as a BMP image
        // --------------------------------------------------------------------
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, wrongCode))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 2f;   // Set module width
            generator.Parameters.Barcode.BarHeight.Pixels = 40f; // Set bar height
            generator.Save(barcodePath, BarCodeImageFormat.Bmp);
        }

        // --------------------------------------------------------------------
        // Verify that the image file was successfully created
        // --------------------------------------------------------------------
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // --------------------------------------------------------------------
        // Read and decode the barcode from the BMP image
        // --------------------------------------------------------------------
        using (BarCodeReader reader = new BarCodeReader(barcodePath, DecodeType.SwissPostParcel))
        {
            bool found = false;
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                found = true;
                Console.WriteLine($"Decoded Type: {result.CodeTypeName}");
                Console.WriteLine($"Decoded Text: {result.CodeText}");

                // Compare the decoded text with the expected corrected value
                if (string.Equals(result.CodeText, expectedCorrect, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Checksum correction verified: decoded text matches expected corrected value.");
                }
                else
                {
                    Console.WriteLine("Checksum correction verification failed: decoded text does not match expected value.");
                }
            }

            if (!found)
            {
                Console.WriteLine("No barcode detected in the image.");
            }
        }

        // --------------------------------------------------------------------
        // Clean up temporary files and folder
        // --------------------------------------------------------------------
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored – cleanup failures should not affect program outcome
        }
    }
}