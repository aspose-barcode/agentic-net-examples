// Title: Check PDF417 IsReaderInitialization Flag in Generated Barcode
// Description: Demonstrates generating a PDF417 barcode with the IsReaderInitialization flag enabled and reading it back to verify the flag.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, focusing on PDF417 symbology. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader for decoding them, highlighting how to work with extended PDF417 properties such as IsReaderInitialization. Developers often need to embed scanner initialization instructions in barcodes; this snippet illustrates the typical workflow for that scenario.
// Prompt: Check PDF417 IsReaderInitialization flag to determine if barcode contains initialization instructions for the scanner.
// Tags: pdf417, barcode, generation, recognition, isreaderinitialization, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a PDF417 barcode with the IsReaderInitialization flag set,
/// saves it to a temporary file, and then reads the barcode to verify the flag value.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs barcode generation, file verification, and barcode reading.
    /// </summary>
    static void Main()
    {
        // Define a temporary directory to store the generated barcode image
        string tempDir = Path.Combine(Path.GetTempPath(), "Pdf417Demo");
        if (!Directory.Exists(tempDir))
        {
            Directory.CreateDirectory(tempDir);
        }

        // Full path for the barcode image file
        string barcodePath = Path.Combine(tempDir, "Pdf417ReaderInit.png");

        // Generate a PDF417 barcode with the IsReaderInitialization flag enabled
        using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, "Aspose.BarCode"))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 2f;          // Set module width
            generator.Parameters.Barcode.Pdf417.Columns = 3;            // Define number of columns
            generator.Parameters.Barcode.Pdf417.IsReaderInitialization = true; // Enable scanner initialization flag
            generator.Save(barcodePath, BarCodeImageFormat.Png);        // Save as PNG image
        }

        // Verify that the barcode file was created successfully before attempting to read it
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine($"Barcode file not found: {barcodePath}");
            return;
        }

        // Read the barcode from the saved image and output the extended PDF417 properties
        using (var reader = new BarCodeReader(barcodePath, DecodeType.Pdf417))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"CodeType: {result.CodeTypeName}");
                Console.WriteLine($"CodeText: {result.CodeText}");
                Console.WriteLine($"IsReaderInitialization: {result.Extended.Pdf417.IsReaderInitialization}");
            }
        }
    }
}