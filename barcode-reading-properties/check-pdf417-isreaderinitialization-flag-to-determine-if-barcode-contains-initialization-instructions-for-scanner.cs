// Title: Check PDF417 IsReaderInitialization flag using Aspose.BarCode
// Description: This example generates a PDF417 barcode with the IsReaderInitialization flag enabled, then reads the barcode to verify the flag value.
// Category-Description: Demonstrates Aspose.BarCode barcode generation and recognition for PDF417 symbology. Shows how to set and read the IsReaderInitialization property, useful for initializing scanners. Covers BarcodeGenerator, BarCodeReader, and related parameter classes, typical for developers implementing barcode scanning workflows.
// Prompt: Check PDF417 IsReaderInitialization flag to determine if barcode contains initialization instructions for the scanner.
// Tags: pdf417, isreaderinitialization, barcode generation, barcode recognition, aspose.barcode, symbology

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a PDF417 barcode with the IsReaderInitialization flag
/// and reading it back to verify the flag value.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a temporary barcode, reads it, and displays the flag.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for the demo files
        string tempDir = Path.Combine(Path.GetTempPath(), "Pdf417ReaderInitDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string barcodePath = Path.Combine(tempDir, "Pdf417ReaderInit.png");

        // Generate a PDF417 barcode with the IsReaderInitialization flag set to true
        using (BarcodeGenerator gen = new BarcodeGenerator(EncodeTypes.Pdf417, "SampleData"))
        {
            gen.Parameters.Barcode.XDimension.Pixels = 2f;
            gen.Parameters.Barcode.Pdf417.IsReaderInitialization = true;
            gen.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was created successfully
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Read the barcode and output the IsReaderInitialization flag value
        using (BarCodeReader reader = new BarCodeReader(barcodePath, DecodeType.Pdf417))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"CodeType: {result.CodeTypeName}");
                Console.WriteLine($"CodeText: {result.CodeText}");
                Console.WriteLine($"IsReaderInitialization: {result.Extended.Pdf417.IsReaderInitialization}");
            }
        }

        // Clean up temporary files and directory
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempDir);
        }
        catch
        {
            // Ignore any errors during cleanup
        }
    }
}