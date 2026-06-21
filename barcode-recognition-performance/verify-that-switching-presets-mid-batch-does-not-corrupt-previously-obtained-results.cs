using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating barcodes, storing them in memory, and recognizing them
/// while switching between different quality settings of the <see cref="BarCodeReader"/>.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates sample barcodes, reads them with varying quality presets,
    /// and verifies the recognition results.
    /// </summary>
    static void Main()
    {
        // Define sample barcodes to generate (type and text)
        var samples = new List<(BaseEncodeType Encode, string Text)>
        {
            (EncodeTypes.Code128, "ABC123"),
            (EncodeTypes.QR, "https://example.com")
        };

        // Generate barcode images and keep them as byte arrays in memory
        var images = new List<byte[]>();
        foreach (var (encode, text) in samples)
        {
            using (var ms = new MemoryStream())
            {
                // Create a generator for the current barcode type and text
                using (var generator = new BarcodeGenerator(encode, text))
                {
                    // Save the generated barcode as PNG into the memory stream
                    generator.Save(ms, BarCodeImageFormat.Png);
                }

                // Store the image bytes for later processing
                images.Add(ms.ToArray());
            }
        }

        // Use a single BarCodeReader instance and switch its QualitySettings between reads
        using (var reader = new BarCodeReader())
        {
            for (int i = 0; i < images.Count; i++)
            {
                // Load the current image from memory into a stream
                using (var imgStream = new MemoryStream(images[i]))
                {
                    // Assign the image stream to the reader
                    reader.SetBarCodeImage(imgStream);
                }

                // Apply quality preset: first image uses NormalQuality, others use MaxQuality
                if (i == 0)
                {
                    reader.QualitySettings = QualitySettings.NormalQuality;
                }
                else
                {
                    reader.QualitySettings = QualitySettings.MaxQuality;
                }

                // Perform barcode recognition on the current image
                var results = reader.ReadBarCodes();

                // If no barcodes were detected, report and continue to next image
                if (results.Length == 0)
                {
                    Console.WriteLine($"Image {i}: No barcode detected.");
                    continue;
                }

                // Take the first detection result (there should be only one per sample)
                var result = results[0];
                Console.WriteLine($"Image {i}: Detected Type = {result.CodeTypeName}, CodeText = {result.CodeText}");

                // Verify that the recognized text matches the original sample text
                if (result.CodeText == samples[i].Text)
                {
                    Console.WriteLine("  Verification passed.");
                }
                else
                {
                    Console.WriteLine("  Verification failed.");
                }
            }
        }

        // If execution reaches this point without exceptions, all preset switches succeeded
        Console.WriteLine("Batch processing completed successfully.");
    }
}