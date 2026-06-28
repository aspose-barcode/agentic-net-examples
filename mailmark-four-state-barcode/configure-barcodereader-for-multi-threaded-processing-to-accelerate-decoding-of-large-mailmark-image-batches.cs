using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates generating and reading Mailmark barcodes in parallel.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a set of Mailmark barcodes,
    /// saves them to memory streams, and reads them back using Aspose.BarCode.
    /// </summary>
    static void Main()
    {
        // Configure the number of processor cores to use for barcode reading
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = Environment.ProcessorCount;

        int sampleCount = 5; // safe sample size for the runner
        var lockObj = new object(); // synchronization object for console output

        // Process a batch of Mailmark images in parallel
        Parallel.For(0, sampleCount, i =>
        {
            // Create a Mailmark codetext with varying ItemID
            var mailmark = new MailmarkCodetext
            {
                Format = 4,               // 4‑state (unspecified/default)
                VersionID = 1,
                Class = "0",
                SupplychainID = 384224,
                ItemID = 16563760 + i,    // unique item id per sample
                DestinationPostCodePlusDPS = "EF61AH8T "
            };

            // Generate the Mailmark barcode image into a memory stream
            using (var ms = new MemoryStream())
            {
                // Generate barcode and write PNG data to the stream
                using (var generator = new ComplexBarcodeGenerator(mailmark))
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                }

                ms.Position = 0; // Reset stream position for reading

                // Read the barcode from the memory stream
                using (var reader = new BarCodeReader(ms, DecodeType.Mailmark))
                {
                    foreach (var result in reader.ReadBarCodes())
                    {
                        // Ensure console writes are not interleaved
                        lock (lockObj)
                        {
                            Console.WriteLine($"Image {i + 1}: Detected CodeText = {result.CodeText}");
                        }
                    }
                }
            }
        });
    }
}