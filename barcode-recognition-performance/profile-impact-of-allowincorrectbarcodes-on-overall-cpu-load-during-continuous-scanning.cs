// Title: Barcode scanning performance impact of AllowIncorrectBarcodes
// Description: Demonstrates measuring CPU load when toggling AllowIncorrectBarcodes during continuous scanning of a Code128 barcode.
// Prompt: Profile the impact of AllowIncorrectBarcodes on overall CPU load during continuous scanning.
// Tags: barcode symbology, scanning, performance, allowincorrectbarcodes, aspose.barcode

using System;
using System.Diagnostics;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that profiles the CPU impact of the <c>AllowIncorrectBarcodes</c> setting
/// while repeatedly scanning a generated Code128 barcode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode, performs warm‑up reads, then measures
    /// scanning time with <c>AllowIncorrectBarcodes</c> set to <c>false</c> and <c>true</c>.
    /// </summary>
    static void Main()
    {
        // Define the barcode content
        const string codeText = "1234567890";

        // Create a barcode generator for Code128
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Set a consistent image size for the generated barcode
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 150f;

            // Generate the barcode image in memory
            using (Bitmap barcodeImage = generator.GenerateBarCodeImage())
            {
                // Warm‑up read to eliminate one‑time JIT overhead from measurements
                using (var warmReader = new BarCodeReader(barcodeImage, DecodeType.Code128))
                {
                    warmReader.ReadBarCodes();
                }

                // ------------------------------------------------------------
                // Measure scanning time with AllowIncorrectBarcodes = false
                // ------------------------------------------------------------
                var swFalse = Stopwatch.StartNew();
                for (int i = 0; i < 5; i++)
                {
                    using (var reader = new BarCodeReader(barcodeImage, DecodeType.Code128))
                    {
                        // Explicitly set the default (false) to ensure consistency
                        reader.QualitySettings.AllowIncorrectBarcodes = false;

                        // Iterate through all detected barcodes (none are output)
                        foreach (var result in reader.ReadBarCodes())
                        {
                            // Access CodeText to force full processing
                            var _ = result.CodeText;
                        }
                    }
                }
                swFalse.Stop();

                // ------------------------------------------------------------
                // Measure scanning time with AllowIncorrectBarcodes = true
                // ------------------------------------------------------------
                var swTrue = Stopwatch.StartNew();
                for (int i = 0; i < 5; i++)
                {
                    using (var reader = new BarCodeReader(barcodeImage, DecodeType.Code128))
                    {
                        // Enable tolerance for incorrectly formatted barcodes
                        reader.QualitySettings.AllowIncorrectBarcodes = true;

                        // Iterate through all detected barcodes (none are output)
                        foreach (var result in reader.ReadBarCodes())
                        {
                            var _ = result.CodeText;
                        }
                    }
                }
                swTrue.Stop();

                // Output the measured elapsed times
                Console.WriteLine($"Scanning 5 times with AllowIncorrectBarcodes = false: {swFalse.ElapsedMilliseconds} ms");
                Console.WriteLine($"Scanning 5 times with AllowIncorrectBarcodes = true : {swTrue.ElapsedMilliseconds} ms");
            }
        }
    }
}