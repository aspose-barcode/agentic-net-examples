// Title: Mailmark barcode decoding with simulated Reed‑Solomon errors
// Description: Demonstrates generating Mailmark barcodes, corrupting the image to simulate Reed‑Solomon errors, and successfully decoding them using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, focusing on complex barcode symbologies such as Mailmark. It showcases the use of ComplexBarcodeGenerator, BarCodeReader, and related classes to create, corrupt, and decode barcodes, a common task for developers testing error‑correction capabilities and robustness of barcode scanning solutions.
// Prompt: Write unit tests verifying successful decoding of Mailmark barcodes with intentional Reed‑Solomon error patterns.
// Tags: mailmark, barcode, decoding, error-correction, reed-solomon, aspose.barcode, complexbarcode, unit-test

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating, corrupting, and decoding Mailmark barcodes using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that runs a series of Mailmark decoding tests and reports results.
    /// </summary>
    static void Main()
    {
        // Prepare test cases with different ItemID values
        var testCases = new List<MailmarkCodetext>
        {
            CreateMailmark(16563762),
            CreateMailmark(16563763),
            CreateMailmark(16563764)
        };

        int passed = 0;
        int failed = 0;

        // Execute each test case
        for (int i = 0; i < testCases.Count; i++)
        {
            bool result = RunTest(testCases[i], i + 1);
            if (result)
                passed++;
            else
                failed++;
        }

        // Output summary of test results
        Console.WriteLine($"TOTAL: {testCases.Count}, PASSED: {passed}, FAILED: {failed}");
    }

    // Creates a MailmarkCodetext with known valid values
    static MailmarkCodetext CreateMailmark(int itemId)
    {
        var mailmark = new MailmarkCodetext();
        mailmark.Format = 4; // 4‑state Mailmark
        mailmark.VersionID = 1;
        mailmark.Class = "0";
        mailmark.SupplychainID = 384224;
        mailmark.ItemID = itemId;
        mailmark.DestinationPostCodePlusDPS = "EF61AH8T "; // trailing space is required
        return mailmark;
    }

    // Generates a barcode image, corrupts it, decodes it and verifies the result
    static bool RunTest(MailmarkCodetext original, int testNumber)
    {
        // Step 1: generate barcode image into a memory stream
        using (var barcodeStream = new MemoryStream())
        {
            using (var generator = new ComplexBarcodeGenerator(original))
            {
                generator.Save(barcodeStream, BarCodeImageFormat.Png);
            }

            // Step 2: corrupt the image to simulate Reed‑Solomon errors
            using (var corruptedStream = CorruptImage(barcodeStream))
            {
                // Step 3: decode the corrupted image
                using (var reader = new BarCodeReader(corruptedStream, DecodeType.Mailmark))
                {
                    // Allow engine to try to read even if some data is damaged
                    reader.QualitySettings.AllowIncorrectBarcodes = true;

                    foreach (var result in reader.ReadBarCodes())
                    {
                        // Verify that a code text was obtained
                        if (string.IsNullOrEmpty(result.CodeText))
                        {
                            Console.WriteLine($"Test {testNumber}: FAILED – empty CodeText.");
                            return false;
                        }

                        // Decode the complex codetext back to a MailmarkCodetext object
                        var decoded = ComplexCodetextReader.TryDecodeMailmark(result.CodeText);
                        if (decoded == null)
                        {
                            Console.WriteLine($"Test {testNumber}: FAILED – ComplexCodetextReader returned null.");
                            return false;
                        }

                        // Compare all fields
                        bool match =
                            decoded.Format == original.Format &&
                            decoded.VersionID == original.VersionID &&
                            decoded.Class == original.Class &&
                            decoded.SupplychainID == original.SupplychainID &&
                            decoded.ItemID == original.ItemID &&
                            decoded.DestinationPostCodePlusDPS == original.DestinationPostCodePlusDPS;

                        if (match)
                        {
                            Console.WriteLine($"Test {testNumber}: PASS");
                            return true;
                        }
                        else
                        {
                            Console.WriteLine($"Test {testNumber}: FAILED – decoded values do not match original.");
                            return false;
                        }
                    }

                    Console.WriteLine($"Test {testNumber}: FAILED – no barcode detected.");
                    return false;
                }
            }
        }
    }

    // Introduces random pixel errors into the PNG image to mimic Reed‑Solomon corruption
    static MemoryStream CorruptImage(MemoryStream originalStream)
    {
        // Load the original image
        originalStream.Position = 0;
        using (var bitmap = new Bitmap(originalStream))
        {
            // Simple deterministic "random" corruption
            var rand = new Random(12345);
            int errorCount = Math.Max(1, bitmap.Width * bitmap.Height / 2000); // ~0.05% of pixels

            for (int i = 0; i < errorCount; i++)
            {
                int x = rand.Next(bitmap.Width);
                int y = rand.Next(bitmap.Height);
                // Flip the pixel color (black <-> white)
                var current = bitmap.GetPixel(x, y);
                var newColor = (current.ToArgb() == Aspose.Drawing.Color.Black.ToArgb())
                    ? Aspose.Drawing.Color.White
                    : Aspose.Drawing.Color.Black;
                bitmap.SetPixel(x, y, newColor);
            }

            // Save the corrupted image to a new memory stream
            var corruptedStream = new MemoryStream();
            bitmap.Save(corruptedStream, ImageFormat.Png);
            corruptedStream.Position = 0;
            return corruptedStream;
        }
    }
}