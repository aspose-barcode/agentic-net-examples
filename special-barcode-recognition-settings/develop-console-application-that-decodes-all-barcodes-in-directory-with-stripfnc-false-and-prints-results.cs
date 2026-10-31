// Title: Decode multiple barcodes from generated images with StripFNC disabled
// Description: Demonstrates how to generate several barcode images, decode them using Aspose.BarCode with StripFNC set to false, and output the results to the console.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category. It shows how to use BarCodeReader, BarcodeSettings, and DecodeType to read all supported symbologies from image files. Typical use cases include batch processing of scanned documents or image folders where developers need to extract raw barcode data without stripping function characters. The snippet is useful for developers looking for a quick start on bulk decoding with customizable settings.
// Prompt: Develop a console application that decodes all barcodes in a directory with StripFNC false and prints results.
// Tags: barcode, decoding, stripfnc, batch, console, aspose.barcode, recognizer, alltypes

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates batch generation and decoding of barcodes with StripFNC disabled.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcodes, decodes them, and prints results.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder to store generated barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "BatchDecode_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // List to hold the full paths of generated barcode files
        List<string> barcodeFiles = new List<string>();

        // Generate a Code128 barcode and add its path to the list
        string code128Path = Path.Combine(tempFolder, "code128.png");
        GenerateBarcode(code128Path, EncodeTypes.Code128, "Sample123");
        barcodeFiles.Add(code128Path);

        // Generate a QR barcode and add its path to the list
        string qrPath = Path.Combine(tempFolder, "qr.png");
        GenerateBarcode(qrPath, EncodeTypes.QR, "QRText");
        barcodeFiles.Add(qrPath);

        // Generate a DataMatrix barcode and add its path to the list
        string dmPath = Path.Combine(tempFolder, "datamatrix.png");
        GenerateBarcode(dmPath, EncodeTypes.DataMatrix, "DM12345");
        barcodeFiles.Add(dmPath);

        // Decode all barcodes in the list with StripFNC set to false
        foreach (string file in barcodeFiles)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            try
            {
                // Initialize the reader for all supported barcode types
                using (BarCodeReader reader = new BarCodeReader(file, DecodeType.AllSupportedTypes))
                {
                    // Disable stripping of function characters (FNC) during decoding
                    reader.BarcodeSettings.StripFNC = false;

                    // Perform the decoding operation
                    BarCodeResult[] results = reader.ReadBarCodes();

                    // Output the number of barcodes found in the current file
                    Console.WriteLine($"File: {Path.GetFileName(file)} - Barcodes read: {results.Length}");

                    // Iterate through each result and display its type and text
                    foreach (BarCodeResult result in results)
                    {
                        Console.WriteLine($"  CodeType: {result.CodeTypeName}");
                        Console.WriteLine($"  CodeText: {result.CodeText}");
                    }
                }
            }
            catch (ArgumentException ex)
            {
                // Handle cases where the file cannot be processed as a barcode image
                Console.WriteLine($"Skipping file {Path.GetFileName(file)}: {ex.Message}");
            }
        }

        // Cleanup: delete the temporary folder and all generated files
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore any errors that occur during cleanup
        }
    }

    /// <summary>
    /// Generates a barcode image using the specified encoding type and text.
    /// </summary>
    /// <param name="filePath">Full path where the barcode image will be saved.</param>
    /// <param name="encodeType">The barcode symbology to use.</param>
    /// <param name="codeText">The text or data to encode in the barcode.</param>
    static void GenerateBarcode(string filePath, BaseEncodeType encodeType, string codeText)
    {
        // Create a barcode generator with the desired type and content
        using (BarcodeGenerator generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Optional: set XDimension for better visibility of the barcode modules
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Save the generated barcode as a PNG image
            generator.Save(filePath, BarCodeImageFormat.Png);
        }
    }
}