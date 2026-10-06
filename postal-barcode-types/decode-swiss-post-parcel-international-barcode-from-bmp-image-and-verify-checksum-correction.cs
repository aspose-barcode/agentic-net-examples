// Title: Decode Swiss Post Parcel barcode and verify checksum correction
// Description: Demonstrates generating Swiss Post Parcel barcodes with an incorrect and correct checksum, decoding them from BMP images, and confirming that the library corrects the checksum during recognition.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It shows how to use BarcodeGenerator to create SwissPostParcel barcodes, BarCodeReader to decode them, and how the API automatically validates and corrects checksums. Developers working with postal symbologies often need to generate barcodes for shipping labels and ensure accurate decoding even when the source image contains checksum errors.
// Prompt: Decode a Swiss Post Parcel international barcode from a BMP image and verify checksum correction.
// Tags: swisspost, parcel, barcode, checksum, generation, recognition, bmp, aspnet.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates Swiss Post Parcel barcodes with and without a checksum,
/// decodes them from BMP images, and verifies that the decoded values match after checksum correction.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs barcode generation, decoding, comparison, and cleanup.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for generated images
        string tempFolder = Path.Combine(Path.GetTempPath(), "SwissPostDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define file paths for the barcode images
        string wrongImagePath = Path.Combine(tempFolder, "SwissPostWrongChecksum.bmp");
        string correctImagePath = Path.Combine(tempFolder, "SwissPostCorrectChecksum.bmp");

        // ------------------------------------------------------------
        // Generate a barcode with an intentionally wrong checksum
        // ------------------------------------------------------------
        using (var genWrong = new BarcodeGenerator(EncodeTypes.SwissPostParcel, "RM999605017CH"))
        {
            genWrong.Parameters.Barcode.XDimension.Pixels = 2f;
            genWrong.Parameters.Barcode.BarHeight.Pixels = 40f;
            genWrong.Save(wrongImagePath, BarCodeImageFormat.Bmp);
        }

        // ------------------------------------------------------------
        // Generate a barcode without a checksum; the library adds the correct one
        // ------------------------------------------------------------
        using (var genCorrect = new BarcodeGenerator(EncodeTypes.SwissPostParcel, "RM99960501CH"))
        {
            genCorrect.Parameters.Barcode.XDimension.Pixels = 2f;
            genCorrect.Parameters.Barcode.BarHeight.Pixels = 40f;
            genCorrect.Save(correctImagePath, BarCodeImageFormat.Bmp);
        }

        // ------------------------------------------------------------
        // Decode the barcode image that contains the wrong checksum
        // ------------------------------------------------------------
        string decodedWrong = null;
        if (File.Exists(wrongImagePath))
        {
            using (var readerWrong = new BarCodeReader(wrongImagePath, DecodeType.SwissPostParcel))
            {
                foreach (BarCodeResult result in readerWrong.ReadBarCodes())
                {
                    decodedWrong = result.CodeText;
                    Console.WriteLine($"Decoded from image with wrong checksum: {decodedWrong}");
                }
            }
        }
        else
        {
            Console.WriteLine("Wrong checksum image not found.");
        }

        // ------------------------------------------------------------
        // Decode the barcode image that contains the correct checksum
        // ------------------------------------------------------------
        string decodedCorrect = null;
        if (File.Exists(correctImagePath))
        {
            using (var readerCorrect = new BarCodeReader(correctImagePath, DecodeType.SwissPostParcel))
            {
                foreach (BarCodeResult result in readerCorrect.ReadBarCodes())
                {
                    decodedCorrect = result.CodeText;
                    Console.WriteLine($"Decoded from image with correct checksum: {decodedCorrect}");
                }
            }
        }
        else
        {
            Console.WriteLine("Correct checksum image not found.");
        }

        // ------------------------------------------------------------
        // Verify that the library corrected the checksum by comparing results
        // ------------------------------------------------------------
        if (decodedWrong != null && decodedCorrect != null)
        {
            if (decodedWrong == decodedCorrect)
            {
                Console.WriteLine("Checksum correction verified: decoded values match.");
            }
            else
            {
                Console.WriteLine("Checksum correction failed: decoded values do not match.");
            }
        }

        // ------------------------------------------------------------
        // Cleanup temporary files and folder (optional)
        // ------------------------------------------------------------
        try
        {
            if (File.Exists(wrongImagePath)) File.Delete(wrongImagePath);
            if (File.Exists(correctImagePath)) File.Delete(correctImagePath);
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore any errors during cleanup
        }
    }
}