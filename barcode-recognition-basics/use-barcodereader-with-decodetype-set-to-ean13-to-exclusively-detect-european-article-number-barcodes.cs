// Title: Detect EAN13 Barcodes Using BarCodeReader
// Description: Demonstrates how to generate an EAN‑13 barcode image and then read it back using BarCodeReader with DecodeType set to EAN13, ensuring only European Article Number barcodes are detected.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the BarcodeGenerator class for creating barcode images and the BarCodeReader class for decoding them. Typical use cases include inventory management, retail point‑of‑sale systems, and any scenario where EAN‑13 symbology is required. Developers often need to restrict decoding to a specific symbology to improve performance and accuracy, which is achieved here by setting DecodeType to EAN13.
/// Prompt: Use BarCodeReader with DecodeType set to EAN13 to exclusively detect European Article Number barcodes.
/// Tags: ean13, barcode, generation, recognition, aspose.barcode, decode, symbology

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates an EAN‑13 barcode, saves it to a temporary file,
/// and then reads it back using <see cref="BarCodeReader"/> with <see cref="DecodeType.EAN13"/>.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the generate‑and‑read workflow.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder to avoid collisions.
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string barcodePath = Path.Combine(tempDir, "ean13.png");

        // Generate an EAN13 barcode image and save it as PNG.
        using (var generator = new BarcodeGenerator(EncodeTypes.EAN13, "5901234123457"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the image file was created successfully.
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Set the decode type to EAN13 so the reader only looks for this symbology.
        BaseDecodeType decodeType = DecodeType.EAN13;

        // Read the barcode from the image file.
        using (var reader = new BarCodeReader(barcodePath, decodeType))
        {
            var results = reader.ReadBarCodes();

            // Output the detection results.
            if (results.Length == 0)
            {
                Console.WriteLine("No EAN13 barcode detected.");
            }
            else
            {
                foreach (var result in results)
                {
                    Console.WriteLine($"Detected CodeText: {result.CodeText}");
                    Console.WriteLine($"Detected CodeType: {result.CodeTypeName}");
                }
            }
        }

        // Clean up temporary files and directory.
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempDir);
        }
        catch
        {
            // Suppress any exceptions during cleanup to avoid breaking the example flow.
        }
    }
}