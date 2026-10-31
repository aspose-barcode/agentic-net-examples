// Title: Reed‑Solomon Error Correction Test for Australia Post Barcode
// Description: Demonstrates generating an Australia Post barcode, intentionally corrupting it, and verifying that the built‑in Reed‑Solomon error correction restores the original data.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, focusing on error‑correction capabilities. It showcases the BarcodeGenerator, BarCodeReader, and related parameter classes for Australia Post symbology, a common use case when scanning damaged mail items. Developers looking for unit‑test patterns for barcode resilience will find this snippet useful.
// Prompt: Write a unit test that verifies Reed‑Solomon error correction produces correct output for Australia Post barcode.
// Tags: barcode, australia post, reed-solomon, error correction, unit test, generation, recognition, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating an Australia Post barcode, corrupting the image,
/// and using Reed‑Solomon error correction to recover the original data.
/// </summary>
class Program
{
    /// <summary>
    /// Generates an Australia Post barcode, corrupts it, and verifies that
    /// Reed‑Solomon error correction can recover the original code text.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for test files.
        string tempFolder = Path.Combine(Path.GetTempPath(), "AusPostTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define file paths for the original and corrupted barcode images.
        string barcodePath = Path.Combine(tempFolder, "barcode.png");
        string corruptedPath = Path.Combine(tempFolder, "barcode_corrupted.png");

        // Sample Australia Post code (FCC 62, 8‑digit DPID, 2 CTable chars).
        string codeText = "6201234567AB";

        // -------------------- Barcode Generation --------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, codeText))
        {
            // Set visual parameters.
            generator.Parameters.Barcode.XDimension.Pixels = 4;
            generator.Parameters.Barcode.BarHeight.Pixels = 50;
            // Use the CTable encoding for Australian Post.
            generator.Parameters.Barcode.AustralianPost.EncodingTable = CustomerInformationInterpretingType.CTable;
            // Save the barcode as a PNG image.
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // -------------------- Image Corruption --------------------
        using (var bitmap = new Bitmap(barcodePath))
        {
            using (var graphics = Graphics.FromImage(bitmap))
            {
                // Draw a white rectangle over the top‑left quarter of the image.
                var rect = new Rectangle(0, 0, bitmap.Width / 4, bitmap.Height / 4);
                graphics.FillRectangle(Brushes.White, rect);
            }
            // Save the corrupted image.
            bitmap.Save(corruptedPath, ImageFormat.Png);
        }

        // Verify that the corrupted image file was created successfully.
        if (!File.Exists(corruptedPath))
        {
            Console.WriteLine("Corrupted image file not found.");
            return;
        }

        // -------------------- Barcode Reading & Error Correction --------------------
        BaseDecodeType decodeType = DecodeType.AustraliaPost;
        using (var reader = new BarCodeReader(corruptedPath, decodeType))
        {
            var results = reader.ReadBarCodes();
            if (results.Length == 0)
            {
                Console.WriteLine("No barcode detected.");
                return;
            }

            // Iterate through all detected barcodes (normally one for this test).
            foreach (var result in results)
            {
                Console.WriteLine($"Decoded CodeText: {result.CodeText}");
                if (result.CodeText == codeText)
                {
                    Console.WriteLine("Reed‑Solomon error correction succeeded.");
                }
                else
                {
                    Console.WriteLine("Decoding failed or error correction insufficient.");
                }
            }
        }
    }
}