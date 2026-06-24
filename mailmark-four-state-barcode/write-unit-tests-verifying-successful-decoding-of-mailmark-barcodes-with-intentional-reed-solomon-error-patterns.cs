using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates Mailmark barcode generation, intentional corruption, and decoding
/// to verify Reed‑Solomon error correction capabilities.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Executes a simple unit‑test that:
    /// 1. Generates a valid Mailmark barcode.
    /// 2. Corrupts a number of pixels to simulate damage.
    /// 3. Attempts to decode the corrupted image.
    /// 4. Validates that the decoded text matches the original.
    /// </summary>
    static void Main()
    {
        // Run a simple unit‑test for Mailmark decoding with simulated Reed‑Solomon errors.
        try
        {
            // ------------------------------------------------------------
            // 1. Prepare a valid Mailmark codetext.
            // ------------------------------------------------------------
            var mailmark = new MailmarkCodetext
            {
                Format = 4,               // 4‑state default
                VersionID = 1,
                Class = "0",              // Null/Test class
                SupplychainID = 384224,
                ItemID = 16563762,
                DestinationPostCodePlusDPS = "EF61AH8T " // valid postcode+DP
            };
            string originalCodeText = mailmark.GetConstructedCodetext();

            // ------------------------------------------------------------
            // 2. Generate the barcode image into a memory stream.
            // ------------------------------------------------------------
            using (var generator = new ComplexBarcodeGenerator(mailmark))
            using (var originalStream = new MemoryStream())
            {
                generator.Save(originalStream, BarCodeImageFormat.Png);
                originalStream.Position = 0; // Reset stream for reading.

                // ------------------------------------------------------------
                // 3. Load the image for pixel manipulation.
                // ------------------------------------------------------------
                using (var bitmap = new Bitmap(originalStream))
                {
                    // ------------------------------------------------------------
                    // 4. Introduce artificial errors (pixel corruption) to simulate Reed‑Solomon damage.
                    // ------------------------------------------------------------
                    CorruptBitmap(bitmap, errorPixelCount: 15);

                    // ------------------------------------------------------------
                    // 5. Save the corrupted image to a new stream.
                    // ------------------------------------------------------------
                    using (var corruptedStream = new MemoryStream())
                    {
                        bitmap.Save(corruptedStream, Aspose.Drawing.Imaging.ImageFormat.Png);
                        corruptedStream.Position = 0; // Reset stream for decoding.

                        // ------------------------------------------------------------
                        // 6. Decode the corrupted image.
                        // ------------------------------------------------------------
                        using (var reader = new BarCodeReader(corruptedStream, DecodeType.Mailmark))
                        {
                            // Allow decoding of barcodes with incorrect data (damage).
                            reader.QualitySettings.AllowIncorrectBarcodes = true;

                            var results = reader.ReadBarCodes();
                            if (results.Length == 0)
                            {
                                Console.WriteLine("Test failed: No barcode detected.");
                                return;
                            }

                            var result = results[0];

                            // ------------------------------------------------------------
                            // 7. Verify that the decoded text matches the original codetext.
                            // ------------------------------------------------------------
                            if (result.CodeText == originalCodeText)
                            {
                                Console.WriteLine("Test passed: Decoded Mailmark matches original.");
                            }
                            else
                            {
                                Console.WriteLine($"Test failed: Decoded text mismatch.\nExpected: {originalCodeText}\nActual:   {result.CodeText}");
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Report any unexpected exceptions.
            Console.WriteLine($"Test encountered an exception: {ex.Message}");
        }
    }

    /// <summary>
    /// Corrupts a bitmap by inverting a specified number of random pixels.
    /// </summary>
    /// <param name="bitmap">The bitmap to corrupt.</param>
    /// <param name="errorPixelCount">Number of pixels to invert.</param>
    private static void CorruptBitmap(Bitmap bitmap, int errorPixelCount)
    {
        if (bitmap == null) throw new ArgumentNullException(nameof(bitmap));
        if (errorPixelCount <= 0) return;

        var rand = new Random();
        int width = bitmap.Width;
        int height = bitmap.Height;

        for (int i = 0; i < errorPixelCount; i++)
        {
            // Choose a random pixel coordinate.
            int x = rand.Next(width);
            int y = rand.Next(height);

            // Get current pixel color.
            var originalColor = bitmap.GetPixel(x, y);

            // Invert the color components.
            var invertedColor = Aspose.Drawing.Color.FromArgb(
                255 - originalColor.R,
                255 - originalColor.G,
                255 - originalColor.B);

            // Apply the inverted color back to the bitmap.
            bitmap.SetPixel(x, y, invertedColor);
        }
    }
}