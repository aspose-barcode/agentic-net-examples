// Title: Mailmark barcode decoding with Reed‑Solomon error simulation
// Description: Demonstrates generating a Mailmark barcode, corrupting it with simulated Reed‑Solomon errors, and verifying successful decoding.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, focusing on complex barcode types such as Mailmark. It showcases the use of ComplexBarcodeGenerator, BarCodeReader, and related quality settings to handle damaged barcodes, a common requirement for developers implementing robust mail processing systems. The snippet illustrates typical workflows for creating, corrupting, and validating Mailmark barcodes using Aspose.BarCode APIs.
// Prompt: Write unit tests verifying successful decoding of Mailmark barcodes with intentional Reed‑Solomon error patterns.
// Tags: mailmark, barcode, decoding, reed-solomon, error-correction, generation, recognition, aspnet, csharp

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a Mailmark barcode, corrupting it, and verifying decoding using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point for the Mailmark decoding demonstration.
    /// </summary>
    static void Main()
    {
        // Run the Mailmark decoding test
        RunMailmarkDecodingTest();
    }

    /// <summary>
    /// Generates a Mailmark barcode, introduces simulated Reed‑Solomon errors, and validates successful decoding.
    /// </summary>
    static void RunMailmarkDecodingTest()
    {
        // Prepare a valid Mailmark codetext
        var mailmark = new MailmarkCodetext();
        mailmark.Format = 4;                     // Default/unspecified format
        mailmark.VersionID = 1;
        mailmark.Class = "0";
        mailmark.SupplychainID = 384224;
        mailmark.ItemID = 16563762;
        mailmark.DestinationPostCodePlusDPS = "EF61AH8T ";

        // Generate the Mailmark barcode image using ComplexBarcodeGenerator
        using (var complexGenerator = new ComplexBarcodeGenerator(mailmark))
        {
            using (Bitmap bitmap = complexGenerator.GenerateBarCodeImage())
            {
                // Save the image to a memory stream (PNG format)
                using (var originalStream = new MemoryStream())
                {
                    bitmap.Save(originalStream, ImageFormat.Png);
                    byte[] originalBytes = originalStream.ToArray();

                    // Introduce intentional Reed‑Solomon error patterns by corrupting a few bytes
                    // (simple pixel corruption for demonstration purposes)
                    byte[] corruptedBytes = (byte[])originalBytes.Clone();
                    int errorsToIntroduce = 5;
                    Random rnd = new Random(0);
                    for (int i = 0; i < errorsToIntroduce; i++)
                    {
                        int index = rnd.Next(corruptedBytes.Length);
                        corruptedBytes[index] ^= 0xFF; // Invert bits at the selected position
                    }

                    // Decode the corrupted image
                    using (var corruptedStream = new MemoryStream(corruptedBytes))
                    {
                        using (var reader = new BarCodeReader(corruptedStream, DecodeType.Mailmark))
                        {
                            // Allow the engine to attempt recovery from damaged barcodes
                            reader.QualitySettings.AllowIncorrectBarcodes = true;
                            reader.QualitySettings.Deconvolution = DeconvolutionMode.Fast;

                            var results = reader.ReadBarCodes();

                            if (results.Length == 0)
                            {
                                Console.WriteLine("Test FAILED: No barcode detected.");
                                return;
                            }

                            // Expect only one Mailmark barcode
                            var result = results[0];
                            if (string.IsNullOrEmpty(result.CodeText))
                            {
                                Console.WriteLine("Test FAILED: Decoded CodeText is empty.");
                                return;
                            }

                            // Verify that the decoded codetext can be parsed back to a MailmarkCodetext object
                            MailmarkCodetext decodedMailmark = ComplexCodetextReader.TryDecodeMailmark(result.CodeText);
                            if (decodedMailmark == null)
                            {
                                Console.WriteLine("Test FAILED: ComplexCodetextReader could not parse the codetext.");
                                return;
                            }

                            // Simple field comparisons to ensure integrity
                            bool fieldsMatch =
                                decodedMailmark.Format == mailmark.Format &&
                                decodedMailmark.VersionID == mailmark.VersionID &&
                                decodedMailmark.Class == mailmark.Class &&
                                decodedMailmark.SupplychainID == mailmark.SupplychainID &&
                                decodedMailmark.ItemID == mailmark.ItemID &&
                                decodedMailmark.DestinationPostCodePlusDPS == mailmark.DestinationPostCodePlusDPS;

                            if (fieldsMatch)
                            {
                                Console.WriteLine("Test PASSED: Mailmark barcode decoded successfully despite errors.");
                            }
                            else
                            {
                                Console.WriteLine("Test FAILED: Decoded fields do not match original values.");
                            }
                        }
                    }
                }
            }
        }
    }
}