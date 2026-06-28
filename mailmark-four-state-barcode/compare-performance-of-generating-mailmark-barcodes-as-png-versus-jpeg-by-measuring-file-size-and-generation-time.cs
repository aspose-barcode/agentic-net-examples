using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates generation of Mailmark barcodes in PNG and JPEG formats,
/// measuring generation time and optionally saving the images to disk.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Prepares a Mailmark codetext, generates barcodes in two image formats,
    /// measures performance, and writes the results to the console and files.
    /// </summary>
    static void Main()
    {
        // Prepare a valid Mailmark codetext object with required properties.
        var mailmark = new MailmarkCodetext
        {
            Format = 4,                     // 4‑state format
            VersionID = 1,
            Class = "0",                    // string property representing class
            SupplychainID = 384224,
            ItemID = 16563762,
            DestinationPostCodePlusDPS = "EF61AH8T " // known‑valid value (trailing space intentional)
        };

        // Define the image formats to compare: PNG and JPEG.
        var formats = new[] { BarCodeImageFormat.Png, BarCodeImageFormat.Jpeg };

        // Iterate over each format, generate the barcode, and record metrics.
        foreach (var fmt in formats)
        {
            // Start timing the generation process.
            var stopwatch = Stopwatch.StartNew();

            // Use a memory stream to hold the generated image data.
            using (var memory = new MemoryStream())
            {
                // Generate the Mailmark barcode and save it directly to the memory stream.
                using (var generator = new ComplexBarcodeGenerator(mailmark))
                {
                    generator.Save(memory, fmt);
                }

                // Stop the timer after generation completes.
                stopwatch.Stop();

                // Determine the appropriate file extension based on the format.
                string extension = fmt == BarCodeImageFormat.Png ? ".png" : ".jpg";
                string fileName = "Mailmark" + extension;

                // Write the image bytes to a file for optional visual verification.
                File.WriteAllBytes(fileName, memory.ToArray());

                // Output the size, generation time, and saved file name to the console.
                Console.WriteLine($"{fmt} -> Size: {memory.Length} bytes, Generation Time: {stopwatch.ElapsedMilliseconds} ms, Saved As: {fileName}");
            }
        }
    }
}