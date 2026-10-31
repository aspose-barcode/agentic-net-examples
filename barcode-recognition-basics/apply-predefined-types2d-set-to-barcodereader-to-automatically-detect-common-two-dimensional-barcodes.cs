// Title: Detect and read common 2D barcodes using BarCodeReader Types2D preset
// Description: This example generates QR, DataMatrix, and PDF417 barcodes, saves them to temporary files, and then uses BarCodeReader with the predefined Types2D set to automatically detect and decode those 2‑dimensional barcodes.
// Category-Description: Demonstrates Aspose.BarCode barcode generation and recognition within the 2‑dimensional symbology category. It showcases the use of BarcodeGenerator for creating QR, DataMatrix, and PDF417 images, and BarCodeReader with DecodeType.Types2D to recognize multiple 2D symbologies in a single pass. Developers working with scanning applications, document processing, or inventory systems often need to generate and read various 2D barcodes efficiently; this example provides a concise reference for those common tasks.
// Prompt: Apply the predefined Types2D set to BarCodeReader to automatically detect common two‑dimensional barcodes.
// Tags: barcode, 2d, qrcode, datamatrix, pdf417, generation, recognition, aspose.barcode, decode, types2d

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating QR, DataMatrix, and PDF417 barcodes and reading them using the Types2D preset.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcodes, saves them, and reads them back using BarCodeReader with DecodeType.Types2D.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the generated barcode images.
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the barcode samples: type, text, and output file name.
        var samples = new (BaseEncodeType encodeType, string text, string fileName)[]
        {
            (EncodeTypes.QR, "Hello QR", "qr.png"),
            (EncodeTypes.DataMatrix, "Hello DataMatrix", "datamatrix.png"),
            (EncodeTypes.Pdf417, "Hello PDF417", "pdf417.png")
        };

        // Generate each barcode image and save it as PNG.
        foreach (var sample in samples)
        {
            string filePath = Path.Combine(tempFolder, sample.fileName);
            using (BarcodeGenerator generator = new BarcodeGenerator(sample.encodeType, sample.text))
            {
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
        }

        Console.WriteLine("Generated barcodes in: " + tempFolder);
        Console.WriteLine();

        // Read each generated image using the Types2D preset to auto‑detect 2D barcodes.
        foreach (var sample in samples)
        {
            string filePath = Path.Combine(tempFolder, sample.fileName);
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                continue;
            }

            using (BarCodeReader reader = new BarCodeReader(filePath, DecodeType.Types2D))
            {
                Console.WriteLine($"Reading {sample.fileName}:");
                BarCodeResult[] results = reader.ReadBarCodes();

                if (results.Length == 0)
                {
                    Console.WriteLine("  No barcode detected.");
                }
                else
                {
                    // Output each detected barcode's type and decoded text.
                    foreach (BarCodeResult result in results)
                    {
                        Console.WriteLine($"  {result.CodeTypeName}: {result.CodeText}");
                    }
                }
            }
        }

        // Optional cleanup:
        // Directory.Delete(tempFolder, true);
    }
}