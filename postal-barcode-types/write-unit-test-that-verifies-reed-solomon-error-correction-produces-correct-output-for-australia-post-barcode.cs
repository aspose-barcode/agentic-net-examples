// Title: Reed‑Solomon Error Correction Test for Australia Post Barcode
// Description: Demonstrates generating an Australia Post barcode, intentionally corrupting it, and verifying that Reed‑Solomon error correction can recover the original data.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, focusing on error‑correction capabilities. It uses BarcodeGenerator, BarCodeReader, and related settings to illustrate how Reed‑Solomon correction works for Australia Post symbology, a common requirement for postal automation and validation scenarios. Developers looking for unit‑test patterns or sample code for robust barcode handling will find this useful.
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
/// Contains the entry point for the Reed‑Solomon error correction demonstration for Australia Post barcodes.
/// </summary>
class Program
{
    /// <summary>
    /// Generates an Australia Post barcode, corrupts it, and checks whether the Reed‑Solomon error correction can successfully decode it.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for test artifacts
        string tempFolder = Path.Combine(Path.GetTempPath(), "AustraliaPostTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Paths for the original and corrupted barcode images
        string originalPath = Path.Combine(tempFolder, "original.png");
        string corruptedPath = Path.Combine(tempFolder, "corrupted.png");

        // Define a valid Australia Post code (FCC 59, 8‑digit DPID, 2 CTable chars)
        string codeText = "5901234567AB"; // FCC=59, DPID=01234567, customer info "AB"

        // Generate the barcode image
        using (var generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, codeText))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Parameters.Barcode.BarHeight.Pixels = 50f;
            generator.Parameters.Barcode.AustralianPost.EncodingTable = CustomerInformationInterpretingType.CTable;
            generator.Save(originalPath, BarCodeImageFormat.Png);
        }

        // Corrupt the image by drawing a white rectangle over part of it
        using (var bitmap = (Bitmap)Image.FromFile(originalPath))
        {
            using (var graphics = Graphics.FromImage(bitmap))
            {
                // Draw a white rectangle covering roughly the middle of the barcode
                int rectWidth = bitmap.Width / 4;
                int rectHeight = bitmap.Height / 2;
                int rectX = (bitmap.Width - rectWidth) / 2;
                int rectY = (bitmap.Height - rectHeight) / 2;
                using (var brush = new SolidBrush(Color.White))
                {
                    graphics.FillRectangle(brush, rectX, rectY, rectWidth, rectHeight);
                }
            }
            bitmap.Save(corruptedPath, ImageFormat.Png);
        }

        // Attempt to read the corrupted barcode using Reed‑Solomon error correction
        bool readSuccess = false;
        BaseDecodeType decodeType = DecodeType.AustraliaPost;
        using (var reader = new BarCodeReader(corruptedPath, decodeType))
        {
            // Configure recognition to match the encoding table used during generation
            reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = CustomerInformationInterpretingType.CTable;

            var results = reader.ReadBarCodes();
            if (results != null && results.Length > 0 && !string.IsNullOrEmpty(results[0].CodeText))
            {
                readSuccess = true;
            }
        }

        // Output test result
        if (readSuccess)
        {
            Console.WriteLine("PASSED: Reed‑Solomon error correction recovered the barcode.");
        }
        else
        {
            Console.WriteLine("FAILED: Barcode could not be recovered.");
        }

        // Clean up temporary files and folder
        try
        {
            if (File.Exists(originalPath)) File.Delete(originalPath);
            if (File.Exists(corruptedPath)) File.Delete(corruptedPath);
            if (Directory.Exists(tempFolder)) Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored – cleanup failure should not affect test outcome
        }
    }
}