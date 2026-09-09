// Title: Read and Log Barcodes from an Image using Aspose.BarCode
// Description: This example generates a Code128 barcode image, reads it back, and logs each detected barcode's text and type to the console.
// Category-Description: Demonstrates Aspose.BarCode barcode generation and recognition. It uses BarcodeGenerator to create a barcode image, BarCodeReader to decode any supported symbologies, and BarCodeResult to access decoded data. Typical scenarios include inventory tracking, document processing, and point‑of‑sale systems where developers need to generate and subsequently read barcodes programmatically.
// Prompt: Invoke ReadBarCodes and iterate over the BarCodeResult array to log each barcode's text and type.
// Tags: barcode symbology, generation, recognition, read, console output, aspose.barcode, code128, decode, barcodereader, barcoderesult

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Sample program that creates a barcode image, reads it, and prints detected barcode information.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode, reads it back, and writes each barcode's text and type to the console.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for the sample barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "ReadDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "sample.png");

        // Generate a sample barcode image (Code128 with value "123456")
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify the file exists before attempting to read
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Barcode image file not found.");
            return;
        }

        // Read barcodes from the generated image using all supported decode types
        using (BarCodeReader reader = new BarCodeReader(barcodePath, DecodeType.AllSupportedTypes))
        {
            BarCodeResult[] results = reader.ReadBarCodes();

            // Check if any barcodes were detected
            if (results == null || results.Length == 0)
            {
                Console.WriteLine("No barcodes detected.");
            }
            else
            {
                // Iterate over each detected barcode and log its text and type
                foreach (BarCodeResult result in results)
                {
                    Console.WriteLine($"CodeText: {result.CodeText}");
                    Console.WriteLine($"CodeType: {result.CodeTypeName}");
                }
            }
        }

        // Clean up temporary files
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program outcome
        }
    }
}