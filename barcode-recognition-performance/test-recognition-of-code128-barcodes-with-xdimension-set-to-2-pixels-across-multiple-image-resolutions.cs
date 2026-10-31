// Title: Code128 Barcode Generation and Recognition with XDimension = 2 Pixels at Multiple Resolutions
// Description: Demonstrates generating Code128 barcodes with a fixed X‑dimension of 2 pixels at various DPI settings, saving them as PNG, and then recognizing them using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes, configuring XDimension and image resolution, and BarCodeReader for decoding. Typical use cases include testing barcode readability across different image qualities and preparing assets for scanning devices. Developers often need to adjust XDimension, resolution, and decoding settings to ensure reliable scanning in real‑world applications.
// Prompt: Test recognition of Code128 barcodes with XDimension set to 2 pixels across multiple image resolutions.
// Tags: code128, barcode generation, barcode recognition, xdimension, resolution, png, aspose.barcode, c#

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Generates Code128 barcodes with a specific XDimension at several DPI settings,
/// saves them as PNG files, and then reads them back to verify recognition.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Handles barcode creation, recognition, and cleanup.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for generated barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "Code128Test_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the DPI values to test
        float[] resolutions = new float[] { 72f, 150f, 300f };
        // Collect file paths of successfully generated barcodes
        List<string> barcodeFiles = new List<string>();

        // --------------------------------------------------------------------
        // Generate Code128 barcodes with XDimension = 2 pixels for each DPI
        // --------------------------------------------------------------------
        for (int i = 0; i < resolutions.Length; i++)
        {
            float res = resolutions[i];
            string codeText = $"Test{res}";
            string filePath = Path.Combine(tempFolder, $"code128_{res}.png");

            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Set the XDimension (module width) to 2 pixels
                generator.Parameters.Barcode.XDimension.Pixels = 2f;
                // Apply the desired image resolution (dots per inch)
                generator.Parameters.Resolution = res;
                // Save the generated barcode as a PNG image
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            // Verify that the file was created and store its path
            if (File.Exists(filePath))
            {
                barcodeFiles.Add(filePath);
                Console.WriteLine($"Generated barcode at {res} DPI: {filePath}");
            }
            else
            {
                Console.WriteLine($"Failed to generate barcode at {res} DPI.");
            }
        }

        // ---------------------------------------------------------------
        // Recognize each generated barcode and output the decoded values
        // ---------------------------------------------------------------
        foreach (string file in barcodeFiles)
        {
            Console.WriteLine($"\nReading barcode from file: {Path.GetFileName(file)}");
            using (BarCodeReader reader = new BarCodeReader(file, DecodeType.Code128))
            {
                // Read all barcodes found in the image (default quality settings)
                BarCodeResult[] results = reader.ReadBarCodes();
                Console.WriteLine($"Barcodes read: {results.Length}");
                foreach (BarCodeResult result in results)
                {
                    Console.WriteLine($"Detected: {result.CodeTypeName} - {result.CodeText}");
                }
            }
        }

        // ---------------------------------------------------------------
        // Cleanup: delete the temporary folder and all generated files
        // ---------------------------------------------------------------
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Cleanup failed: {ex.Message}");
        }
    }
}