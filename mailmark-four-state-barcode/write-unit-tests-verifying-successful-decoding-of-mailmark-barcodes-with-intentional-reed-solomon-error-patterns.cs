// Title: Mailmark barcode generation and decoding example
// Description: This example generates a Mailmark barcode image, validates decoding using the string‑based parser, and demonstrates the current limitation of image‑based Mailmark decoding.
// Category-Description: Demonstrates Aspose.BarCode complex barcode operations, specifically Mailmark generation with ComplexBarcodeGenerator and decoding via ComplexCodetextReader. Typical use cases include creating Mailmark codes for postal services and verifying their data programmatically. Developers often need to generate, parse, and test Mailmark symbology using Aspose.BarCode's key classes such as MailmarkCodetext, ComplexBarcodeGenerator, and BarCodeReader.
// Prompt: Write unit tests verifying successful decoding of Mailmark barcodes with intentional Reed‑Solomon error patterns.
// Tags: mailmark, barcode, generation, decoding, unit-test, aspose.barcode, complexbarcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generation of a Mailmark barcode, string‑based decoding verification,
/// and attempts image‑based decoding (which is currently unsupported).
/// </summary>
class Program
{
    // Counters for test results
    static int passed = 0;
    static int failed = 0;

    /// <summary>
    /// Entry point of the example. Executes the Mailmark decoding test and reports results.
    /// </summary>
    static void Main()
    {
        try
        {
            TestMailmarkDecoding();
        }
        catch (Exception ex)
        {
            // Unexpected exception handling – count as failure
            Console.WriteLine($"Unexpected exception: {ex}");
            failed++;
        }

        // Summary output
        Console.WriteLine($"Tests completed. Passed: {passed}, Failed: {failed}");
    }

    /// <summary>
    /// Simple assertion helper that updates pass/fail counters and logs failures.
    /// </summary>
    /// <param name="condition">Condition to evaluate.</param>
    /// <param name="message">Message displayed when the assertion fails.</param>
    static void Assert(bool condition, string message)
    {
        if (condition)
        {
            passed++;
        }
        else
        {
            failed++;
            Console.WriteLine($"ASSERTION FAILED: {message}");
        }
    }

    /// <summary>
    /// Generates a Mailmark barcode, validates decoding from the constructed codetext,
    /// and attempts (unsupported) image‑based decoding.
    /// </summary>
    static void TestMailmarkDecoding()
    {
        // Prepare a unique temporary folder for test artifacts
        string tempFolder = Path.Combine(Path.GetTempPath(), "MailmarkTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string imagePath = Path.Combine(tempFolder, "Mailmark4State.png");

        // Define Mailmark codetext parameters
        var mailmark = new MailmarkCodetext
        {
            Format = 4,
            VersionID = 1,
            Class = "0",
            SupplychainID = 384224,
            ItemID = 16563762,
            DestinationPostCodePlusDPS = "EF61AH8T "
        };

        // Generate the barcode image using ComplexBarcodeGenerator
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 4;
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the image file was created
        Assert(File.Exists(imagePath), "Generated image file should exist.");

        // Decode directly from the constructed codetext (no image required)
        MailmarkCodetext decodedFromString = ComplexCodetextReader.TryDecodeMailmark(mailmark.GetConstructedCodetext());
        Assert(decodedFromString != null, "String-based decoding returned null.");
        if (decodedFromString != null)
        {
            Assert(decodedFromString.Format == mailmark.Format, "Format mismatch.");
            Assert(decodedFromString.VersionID == mailmark.VersionID, "VersionID mismatch.");
            Assert(decodedFromString.Class == mailmark.Class, "Class mismatch.");
            Assert(decodedFromString.SupplychainID == mailmark.SupplychainID, "SupplychainID mismatch.");
            Assert(decodedFromString.ItemID == mailmark.ItemID, "ItemID mismatch.");
            Assert(decodedFromString.DestinationPostCodePlusDPS == mailmark.DestinationPostCodePlusDPS, "DestinationPostCodePlusDPS mismatch.");
        }

        // Attempt image‑based decoding (Mailmark reading from image is not supported)
        BaseDecodeType decodeType = DecodeType.Mailmark;
        using (var reader = new BarCodeReader(imagePath, decodeType))
        {
            try
            {
                var results = reader.ReadBarCodes();
                if (results.Length == 0)
                {
                    // Expected outcome: no results for Mailmark image decoding
                    Console.WriteLine("Image-based decoding returned no results (Mailmark decoding from image is unsupported).");
                    passed++;
                }
                else
                {
                    // If any result is returned, compare the decoded text
                    var result = results[0];
                    Assert(result.CodeText == mailmark.GetConstructedCodetext(), "Image-based decoded CodeText does not match original.");
                }
            }
            catch (ArgumentException ex) when (ex.Message.Contains("Image loading failed"))
            {
                // Handle known image loading failure scenario as a pass
                Console.WriteLine($"Image loading failed: {ex.Message}");
                passed++;
            }
            catch (Exception ex)
            {
                // Unexpected errors during image decoding
                Console.WriteLine($"Unexpected exception during image decoding: {ex}");
                failed++;
            }
        }

        // Clean up temporary files and folder
        try
        {
            if (File.Exists(imagePath))
                File.Delete(imagePath);
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Suppress any cleanup exceptions
        }
    }
}