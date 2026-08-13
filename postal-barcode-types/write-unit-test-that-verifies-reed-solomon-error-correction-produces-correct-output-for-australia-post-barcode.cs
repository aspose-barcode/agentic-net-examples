// Title: Reed‑Solomon Error Correction Test for Australia Post Barcode
// Description: Generates an Australia Post barcode, introduces minor image corruption, and verifies that the Aspose.BarCode decoder correctly restores the original data using Reed‑Solomon error correction.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, focusing on error‑correction capabilities. It demonstrates using BarcodeGenerator, BarCodeReader, and related settings such as CustomerInformationInterpretingType to handle Australia Post symbology. Developers often need to ensure reliable scanning of damaged barcodes in logistics and postal applications.
// Prompt: Write a unit test that verifies Reed‑Solomon error correction produces correct output for Australia Post barcode.
// Tags: australia post, reed-solomon, error correction, barcode generation, barcode recognition, unit test, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates a simple verification of Reed‑Solomon error correction for an Australia Post barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Generates an Australia Post barcode, corrupts it, and verifies that the decoder restores the original text via Reed‑Solomon error correction.
    /// </summary>
    static void Main()
    {
        // Original Australia Post barcode text (FCC=59, DPID=8 digits, 2 CTable chars)
        string originalCode = "5980123456AB";

        // Temporary file path for the barcode image
        string imagePath = Path.Combine(Path.GetTempPath(), "AustraliaPost.png");

        // Generate the barcode and save it as PNG
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, originalCode))
        {
            // Use CTable for customer information interpretation
            generator.Parameters.Barcode.AustralianPost.EncodingTable = CustomerInformationInterpretingType.CTable;
            // Do not throw on incorrect code text (not needed for this test)
            generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = false;

            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify the image was created
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("FAILED: Barcode image was not created.");
            return;
        }

        // Introduce a small amount of corruption (flip two pixels)
        using (Bitmap bitmap = new Bitmap(imagePath))
        {
            // Simple pixel corruption at two locations
            bitmap.SetPixel(0, 0, Color.White);
            bitmap.SetPixel(1, 1, Color.White);

            // Overwrite the original file with the corrupted image
            bitmap.Save(imagePath, ImageFormat.Png);
        }

        // Attempt to read and decode the corrupted barcode
        bool passed = false;
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.AustraliaPost))
        {
            // Ensure the decoder uses the same interpreting type as the generator
            reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = CustomerInformationInterpretingType.CTable;

            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                if (!string.IsNullOrEmpty(result.CodeText) && result.CodeText == originalCode)
                {
                    passed = true;
                    break;
                }
            }
        }

        // Output test result
        if (passed)
        {
            Console.WriteLine("PASSED: Reed‑Solomon error correction restored the original code text.");
        }
        else
        {
            Console.WriteLine("FAILED: Decoded code text does not match the original.");
        }

        // Clean up temporary file
        try
        {
            if (File.Exists(imagePath))
                File.Delete(imagePath);
        }
        catch
        {
            // Ignored – cleanup failure should not affect test outcome
        }
    }
}