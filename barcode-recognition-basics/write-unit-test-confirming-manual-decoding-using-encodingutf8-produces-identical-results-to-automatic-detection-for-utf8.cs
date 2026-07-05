// Title: UTF-8 Barcode Generation and Decoding Verification
// Description: Demonstrates generating a QR code with UTF-8 text and verifying that manual UTF-8 decoding matches automatic detection.
// Prompt: Write a unit test confirming manual decoding using Encoding.UTF8 produces identical results to automatic detection for UTF8 barcodes.
// Tags: qr, encoding, utf8, barcode, generation, recognition, unit-test

using System;
using System.IO;
using System.Text;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that creates a QR barcode containing UTF‑8 text,
/// then reads it back using both automatic encoding detection and manual UTF‑8 decoding
/// to confirm the results are identical.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR code, reads it, and validates decoding.
    /// </summary>
    static void Main()
    {
        // Sample Unicode text (UTF‑8) that will be encoded into the barcode.
        const string originalText = "Привет, мир!";

        // Create a QR barcode generator and set the code text with explicit UTF‑8 encoding.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            generator.SetCodeText(originalText, Encoding.UTF8);

            // Save the generated barcode image to a memory stream (PNG format).
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading.

                // Initialize a barcode reader for QR codes with automatic encoding detection enabled.
                using (var reader = new BarCodeReader(ms, DecodeType.QR))
                {
                    reader.BarcodeSettings.DetectEncoding = true;

                    // Read all barcodes found in the stream.
                    var results = reader.ReadBarCodes();

                    // If no barcode was detected, report and exit.
                    if (results.Length == 0)
                    {
                        Console.WriteLine("No barcode detected.");
                        return;
                    }

                    // Retrieve the first (and only) result.
                    var result = results[0];

                    // Automatic detection returns the decoded text directly.
                    string autoDecoded = result.CodeText;

                    // Manual decoding forces UTF‑8 interpretation of the raw bytes.
                    string manualDecoded = result.GetCodeText(Encoding.UTF8);

                    // Verify that both decoding methods produce identical output
                    // and that they match the original text.
                    if (autoDecoded == manualDecoded && autoDecoded == originalText)
                    {
                        Console.WriteLine("Success: Automatic and manual UTF‑8 decoding match.");
                        Console.WriteLine($"Decoded text: {autoDecoded}");
                    }
                    else
                    {
                        Console.WriteLine("Failure: Decoding results differ.");
                        Console.WriteLine($"Original text : {originalText}");
                        Console.WriteLine($"Auto decoded : {autoDecoded}");
                        Console.WriteLine($"Manual decoded (UTF‑8) : {manualDecoded}");
                    }
                }
            }
        }
    }
}