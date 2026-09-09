// Title: Verify ECI encoding handling for special characters in MaxiCode barcodes
// Description: This example generates a MaxiCode barcode using ECI UTF-8 encoding with Unicode characters, then reads it back to confirm the decoded text matches the original.
// Category-Description: Demonstrates Aspose.BarCode generation and recognition of MaxiCode symbology with ECI encoding. It showcases the use of BarcodeGenerator, BarCodeReader, and related parameters for handling Unicode data, a common requirement when encoding international text in barcodes. Ideal for developers testing barcode round‑trip accuracy in automated pipelines.
// Prompt: Create unit test verifying correct handling of special characters when using ECI encoding in MaxiCode.
// Tags: maxicode, eci, unicode, barcode generation, barcode recognition, unit test, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generation and verification of a MaxiCode barcode with ECI UTF‑8 encoding containing special Unicode characters.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, saves it, reads it back, and validates the decoded text.
    /// </summary>
    static void Main()
    {
        // Prepare a unique temporary folder and file path for the barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "MaxiCodeECITest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "maxicode_eci.png");

        // Test data containing special Unicode characters (Japanese kanji for "dog" and "right")
        string originalText = "犬Right狗";

        // Generate a MaxiCode barcode with ECI (UTF‑8) encoding
        try
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, originalText))
            {
                // Set the MaxiCode specific parameters for ECI mode and UTF‑8 encoding
                generator.Parameters.Barcode.MaxiCode.EncodeMode = MaxiCodeEncodeMode.ECI;
                generator.Parameters.Barcode.MaxiCode.ECIEncoding = ECIEncodings.UTF8;

                // Save the generated barcode as a PNG image
                generator.Save(barcodePath, BarCodeImageFormat.Png);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"FAILED: Exception during barcode generation - {ex.Message}");
            return;
        }

        // Verify that the barcode image file was created successfully
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("FAILED: Barcode image file was not created.");
            return;
        }

        // Read the barcode from the image and verify the decoded text matches the original
        try
        {
            using (var reader = new BarCodeReader(barcodePath, DecodeType.MaxiCode))
            {
                var results = reader.ReadBarCodes();

                // Ensure at least one barcode was detected
                if (results == null || results.Length == 0)
                {
                    Console.WriteLine("FAILED: No barcode detected in the image.");
                    return;
                }

                bool allMatch = true;

                // Compare each decoded result with the original text
                foreach (var result in results)
                {
                    if (result.CodeText != originalText)
                    {
                        allMatch = false;
                        Console.WriteLine($"FAILED: Decoded text '{result.CodeText}' does not match original '{originalText}'.");
                    }
                }

                // Report success if all decoded texts match
                if (allMatch)
                {
                    Console.WriteLine("PASSED: ECI encoding handled correctly for special characters.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"FAILED: Exception during barcode reading - {ex.Message}");
        }
        finally
        {
            // Clean up temporary files and directories
            try
            {
                if (File.Exists(barcodePath))
                    File.Delete(barcodePath);
                if (Directory.Exists(tempFolder))
                    Directory.Delete(tempFolder, true);
            }
            catch
            {
                // Ignored – cleanup failures should not affect the test outcome
            }
        }
    }
}