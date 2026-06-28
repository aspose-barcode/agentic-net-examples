using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a barcode image, verifying its creation,
/// configuring single‑core processing, and reading the barcode back.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // 1. Generate a sample barcode image and save it to a temp file.
        // ------------------------------------------------------------
        string imagePath = Path.Combine(Path.GetTempPath(), "sample.png");
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Save the generated barcode as a PNG file.
            generator.Save(imagePath);
        }

        // ------------------------------------------------------------
        // 2. Verify that the barcode image was successfully created.
        // ------------------------------------------------------------
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // ------------------------------------------------------------
        // 3. Configure the barcode reader to use a single CPU core.
        // ------------------------------------------------------------
        BarCodeReader.ProcessorSettings.UseAllCores = false;          // Disable automatic multi‑core usage.
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = 1;    // Restrict processing to one core.

        // ------------------------------------------------------------
        // 4. Read and display barcode information from the generated image.
        // ------------------------------------------------------------
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            foreach (var result in reader.ReadBarCodes())
            {
                // Output the type and decoded text of each detected barcode.
                Console.WriteLine($"Barcode Type: {result.CodeTypeName}");
                Console.WriteLine($"Barcode Text: {result.CodeText}");
            }
        }
    }
}