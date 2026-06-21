using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a set of barcodes, then recognizing them using different
/// quality presets provided by Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates barcode images, applies various quality settings, and reports
    /// the recognition success rate for each preset.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // 1. Prepare a small mixed‑type dataset of barcodes to be generated.
        // --------------------------------------------------------------------
        var samples = new List<(BaseEncodeType type, string text)>
        {
            (EncodeTypes.Code128, "CODE128"),
            (EncodeTypes.QR, "https://example.com"),
            (EncodeTypes.DataMatrix, "DM12345"),
            (EncodeTypes.Pdf417, "PDF417 sample text"),
            (EncodeTypes.Aztec, "Aztec"),
            (EncodeTypes.DatabarOmniDirectional, "(01)01234567890123"),
            (EncodeTypes.AustraliaPost, "5912345678ABCde")
        };

        // --------------------------------------------------------------
        // 2. Generate barcode images in memory (PNG format) for each sample.
        // --------------------------------------------------------------
        var images = new List<byte[]>();
        foreach (var sample in samples)
        {
            using (var ms = new MemoryStream())
            {
                using (var generator = new BarcodeGenerator(sample.type, sample.text))
                {
                    // Save the generated barcode to the memory stream as PNG.
                    generator.Save(ms, BarCodeImageFormat.Png);
                }

                // Store the raw image bytes for later recognition.
                images.Add(ms.ToArray());
            }
        }

        // --------------------------------------------------------------
        // 3. Define the quality presets that will be tested during recognition.
        // --------------------------------------------------------------
        var presets = new Dictionary<string, QualitySettings>
        {
            { "NormalQuality", QualitySettings.NormalQuality },
            { "HighQuality",   QualitySettings.HighQuality },
            { "MaxQuality",    QualitySettings.MaxQuality }
        };

        // --------------------------------------------------------------
        // 4. Evaluate each quality preset against all generated images.
        // --------------------------------------------------------------
        foreach (var preset in presets)
        {
            int successCount = 0; // Tracks how many images were decoded successfully.

            foreach (var imgData in images)
            {
                using (var ms = new MemoryStream(imgData))
                {
                    // Ensure the stream is positioned at the beginning before reading.
                    ms.Position = 0;

                    using (var reader = new BarCodeReader(ms, DecodeType.AllSupportedTypes))
                    {
                        // Apply the current quality preset to the reader.
                        reader.QualitySettings = preset.Value;

                        // Perform barcode recognition.
                        var results = reader.ReadBarCodes();

                        // Determine success: at least one result with non‑empty CodeText.
                        bool succeeded = false;
                        foreach (var result in results)
                        {
                            if (!string.IsNullOrEmpty(result.CodeText))
                            {
                                succeeded = true;
                                break;
                            }
                        }

                        if (succeeded)
                        {
                            successCount++;
                        }
                    }
                }
            }

            // --------------------------------------------------------------
            // 5. Report the success percentage for the current preset.
            // --------------------------------------------------------------
            double percentage = (double)successCount / images.Count * 100.0;
            Console.WriteLine($"{preset.Key}: {percentage:F2}% ({successCount}/{images.Count}) barcodes decoded successfully.");
        }
    }
}