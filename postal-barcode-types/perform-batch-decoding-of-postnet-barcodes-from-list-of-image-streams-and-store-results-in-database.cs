using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Sample zip codes to encode as Postnet barcodes
        string[] zipCodes = { "12345", "67890", "24680", "13579", "11223" };

        // Prepare a list that holds image name and its corresponding stream
        var barcodeStreams = new List<(string Name, Stream ImageStream)>();

        // Generate Postnet barcode images and store them in memory streams
        foreach (string zip in zipCodes)
        {
            // Create a barcode generator for Postnet symbology
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Postnet, zip))
            {
                // Generate the barcode image as a Bitmap
                using (Bitmap bitmap = generator.GenerateBarCodeImage())
                {
                    // Save bitmap to a memory stream in PNG format
                    var ms = new MemoryStream();
                    bitmap.Save(ms, ImageFormat.Png);
                    ms.Position = 0; // Reset stream position for later reading

                    // Store the stream together with a friendly name
                    barcodeStreams.Add(( $"Postnet_{zip}.png", ms));
                }
            }
        }

        // Ensure output directory exists
        string outputDir = Path.Combine(Environment.CurrentDirectory, "Output");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Path to the simple CSV "database"
        string csvPath = Path.Combine(outputDir, "DecodedResults.csv");

        // Write CSV header
        using (var writer = new StreamWriter(csvPath, false))
        {
            writer.WriteLine("ImageName,DecodedText");
        }

        // Batch decode each barcode stream and store results
        foreach (var (name, stream) in barcodeStreams)
        {
            // Ensure the stream is at the beginning
            stream.Position = 0;

            // Initialize BarCodeReader for Postnet decoding
            using (BarCodeReader reader = new BarCodeReader(stream, DecodeType.Postnet))
            {
                // Perform decoding
                BarCodeResult[] results = reader.ReadBarCodes();

                // If a barcode was found, write its text to the CSV file
                if (results.Length > 0)
                {
                    string decodedText = results[0].CodeText ?? string.Empty;
                    using (var writer = new StreamWriter(csvPath, true))
                    {
                        writer.WriteLine($"{name},{decodedText}");
                    }
                }
                else
                {
                    // No barcode detected – still record the entry with empty result
                    using (var writer = new StreamWriter(csvPath, true))
                    {
                        writer.WriteLine($"{name},");
                    }
                }
            }

            // Dispose the memory stream now that it is no longer needed
            stream.Dispose();
        }

        Console.WriteLine($"Decoding completed. Results saved to: {csvPath}");
    }
}