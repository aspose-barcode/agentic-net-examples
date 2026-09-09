// Title: Decode Barcodes in a Directory with StripFNC Disabled
// Description: Generates sample barcodes, saves them to a temporary folder, then decodes each image with StripFNC set to false and prints the results.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It demonstrates how to use the BarcodeGenerator to create barcodes and the BarCodeReader to batch‑process images, configuring BarcodeSettings (e.g., StripFNC) for custom decoding behavior. Typical use cases include automated scanning of image folders, validation of barcode data, and handling of Function Code (FNC) characters in industrial applications.
// Prompt: Develop a console application that decodes all barcodes in a directory with StripFNC false and prints results.
// Tags: barcode, decoding, stripfnc, batch, console, aspose.barcode, generation, recognition

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating sample barcodes, decoding them with StripFNC disabled, and outputting results.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcodes, decodes them with StripFNC false, and prints the decoded information.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for generated barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "Batch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // List to hold the full paths of generated barcode files
        List<string> barcodeFiles = new List<string>();

        // -------------------- Generate sample Code128 barcode --------------------
        string code128Path = Path.Combine(tempFolder, "code128.png");
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            generator.Save(code128Path, BarCodeImageFormat.Png);
        }
        barcodeFiles.Add(code128Path);

        // -------------------- Generate sample QR barcode --------------------
        string qrPath = Path.Combine(tempFolder, "qr.png");
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            generator.Save(qrPath, BarCodeImageFormat.Png);
        }
        barcodeFiles.Add(qrPath);

        // -------------------- Generate sample DataMatrix barcode --------------------
        string dmPath = Path.Combine(tempFolder, "datamatrix.png");
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "DM12345"))
        {
            generator.Save(dmPath, BarCodeImageFormat.Png);
        }
        barcodeFiles.Add(dmPath);

        // -------------------- Decode each barcode with StripFNC set to false --------------------
        foreach (string filePath in barcodeFiles)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                continue;
            }

            Console.WriteLine($"Decoding file: {Path.GetFileName(filePath)}");
            try
            {
                using (var reader = new BarCodeReader(filePath))
                {
                    // Disable stripping of Function Code (FNC) characters during recognition
                    reader.BarcodeSettings.StripFNC = false;

                    // Read all barcodes present in the image
                    BarCodeResult[] results = reader.ReadBarCodes();

                    // Output each detected barcode's type and text
                    foreach (BarCodeResult result in results)
                    {
                        Console.WriteLine($"CodeType: {result.CodeTypeName}");
                        Console.WriteLine($"CodeText: {result.CodeText}");
                    }
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Failed to load image '{filePath}': {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing '{filePath}': {ex.Message}");
            }
        }

        // -------------------- Clean up temporary folder --------------------
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore any errors that occur during cleanup
        }
    }
}