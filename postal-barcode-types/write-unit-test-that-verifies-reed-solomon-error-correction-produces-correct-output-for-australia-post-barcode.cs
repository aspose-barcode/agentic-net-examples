// Title: Reed‑Solomon Error Correction Test for Australia Post Barcode
// Description: Demonstrates generating an Australia Post barcode, corrupting it, and verifying that Reed‑Solomon error correction restores the original data.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, showcasing how to use BarcodeGenerator, BarCodeReader, and Reed‑Solomon error correction for Australia Post symbology. Typical use cases include validating barcode robustness in automated mail processing systems and ensuring data integrity after physical damage. Developers often need to generate barcodes, simulate degradation, and confirm that the built‑in error correction can recover the original payload.
// Prompt: Write a unit test that verifies Reed‑Solomon error correction produces correct output for Australia Post barcode.
// Tags: australia post, error correction, image, barcodegenerator, barcodereader

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates an Australia Post barcode, intentionally corrupts it,
/// and verifies that Reed‑Solomon error correction can recover the original code text.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the generation, corruption, and verification steps.
    /// </summary>
    static void Main()
    {
        // Original code text for the Australia Post barcode (includes numeric and alphabetic characters)
        const string originalCodeText = "5912345678AB";

        // Create a barcode generator for Australia Post symbology with the original code text
        using (var generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, originalCodeText))
        {
            // Configure the generator to use the CTable interpreting type (allows letters and digits)
            generator.Parameters.Barcode.AustralianPost.AustralianPostEncodingTable = CustomerInformationInterpretingType.CTable;

            // Generate the barcode image in memory
            using (var originalImage = generator.GenerateBarCodeImage())
            {
                // Clone the original image to simulate a damaged barcode
                using (var corruptedImage = new Bitmap(originalImage))
                {
                    // Introduce noise by setting a few pixels to white (simulating physical damage)
                    for (int i = 0; i < 5; i++)
                    {
                        // Ensure pixel coordinates stay within the image bounds
                        int x = Math.Min(i, corruptedImage.Width - 1);
                        int y = Math.Min(i, corruptedImage.Height - 1);
                        corruptedImage.SetPixel(x, y, Color.White);
                    }

                    // Initialize a barcode reader for Australia Post symbology using the corrupted image
                    using (var reader = new BarCodeReader(corruptedImage, DecodeType.AustraliaPost))
                    {
                        // Apply the same interpreting type to the reader as used during generation
                        reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = CustomerInformationInterpretingType.CTable;

                        bool success = false;

                        // Attempt to read barcodes from the corrupted image
                        foreach (BarCodeResult result in reader.ReadBarCodes())
                        {
                            // Verify that the decoded text matches the original code text
                            if (result != null && result.CodeText == originalCodeText)
                            {
                                success = true;
                                break;
                            }
                        }

                        // Output the test result
                        if (success)
                        {
                            Console.WriteLine("PASSED: Reed‑Solomon error correction recovered the original code text.");
                        }
                        else
                        {
                            Console.WriteLine("FAILED: Unable to recover the original code text from the corrupted barcode.");
                        }
                    }
                }
            }
        }
    }
}