// Title: Generate and Read a Code128 Barcode with Aspose.BarCode
// Description: This example creates a Code128 barcode image, saves it as a PNG file, and then reads the barcode back using BarCodeReader.
// Category-Description: Demonstrates Aspose.BarCode generation and recognition workflows. It showcases the use of BarcodeGenerator to encode data into a barcode image and BarCodeReader to decode it. Developers commonly use these APIs for creating barcodes for packaging, inventory, and scanning applications, as well as for validating printed codes.
// Prompt: Dispose BarCodeReader instance properly within a using block to release unmanaged resources.
// Tags: code128, barcode generation, barcode reading, png, barcodereader, barcodegenerator

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates how to generate a Code128 barcode, save it as PNG, and read it back using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the sample. Generates a barcode, reads it, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the sample files
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeSample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the full path for the generated barcode image
        string barcodePath = Path.Combine(tempFolder, "sample.png");

        // -------------------- Barcode Generation --------------------
        // Text to encode in the barcode
        string codeText = "1234567890";

        // Use BarcodeGenerator to create a Code128 barcode and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image file was successfully created
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // -------------------- Barcode Reading --------------------
        // Use BarCodeReader inside a using block to ensure proper disposal of unmanaged resources
        using (BarCodeReader reader = new BarCodeReader(barcodePath, DecodeType.AllSupportedTypes))
        {
            // Optional: configure reader settings here (e.g., checksum validation)
            // reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

            // Read all barcodes found in the image
            BarCodeResult[] results = reader.ReadBarCodes();

            if (results.Length == 0)
            {
                Console.WriteLine("No barcode detected.");
            }
            else
            {
                // Output details of each detected barcode
                foreach (BarCodeResult result in results)
                {
                    Console.WriteLine($"CodeText: {result.CodeText}");
                    Console.WriteLine($"Symbology: {result.CodeTypeName}");
                    Console.WriteLine($"Reading Quality: {result.ReadingQuality}");
                }
            }
        }

        // -------------------- Cleanup --------------------
        // Attempt to delete the temporary files and folder; ignore any errors
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Cleanup failures are non‑critical; they do not affect program execution
        }
    }
}