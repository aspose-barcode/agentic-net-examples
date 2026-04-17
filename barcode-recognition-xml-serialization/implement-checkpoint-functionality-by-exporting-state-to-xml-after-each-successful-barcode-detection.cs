using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Define sample barcodes to generate
        var samples = new (BaseEncodeType type, string text, string file)[]
        {
            (EncodeTypes.Code128, "ABC123", "code128.png"),
            (EncodeTypes.QR, "https://example.com", "qr.png")
        };

        // Generate barcode images
        foreach (var sample in samples)
        {
            using (var generator = new BarcodeGenerator(sample.type, sample.text))
            {
                // Save image to file
                generator.Save(sample.file);
            }
        }

        // Process each generated image and export checkpoint after each detection
        int checkpointIndex = 0;
        foreach (var sample in samples)
        {
            if (!File.Exists(sample.file))
            {
                Console.WriteLine($"File not found: {sample.file}");
                continue;
            }

            using (var reader = new BarCodeReader(sample.file))
            {
                // Read all barcodes in the image
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Detected Barcode Type: {result.CodeTypeName}");
                    Console.WriteLine($"Detected CodeText: {result.CodeText}");

                    // Export reader state to XML as a checkpoint
                    string checkpointFile = $"checkpoint_{checkpointIndex}.xml";
                    bool exported = reader.ExportToXml(checkpointFile);
                    Console.WriteLine(exported
                        ? $"Checkpoint exported to {checkpointFile}"
                        : $"Failed to export checkpoint to {checkpointFile}");

                    checkpointIndex++;
                }
            }
        }
    }
}