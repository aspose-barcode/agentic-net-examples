// Title: Extract Mailmark Barcodes from Multiple TIFF Files
// Description: Demonstrates how to generate sample Mailmark 4‑state barcodes, save them as TIFF images, read those images, decode the Mailmark data, and log the extracted fields.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of ComplexBarcodeGenerator for creating Mailmark barcodes, BarCodeReader with DecodeType.Mailmark for detection, and ComplexCodetextReader for decoding. Developers working with postal automation, logistics, or any scenario requiring bulk Mailmark processing can use these APIs to automate barcode handling in batch workflows.
// Prompt: Develop a console app that reads multiple TIFF files, extracts Mailmark barcodes, and logs decoded fields.
// Tags: mailmark, barcode, tiff, console, aspose.barcode, generation, recognition, complexbarcode

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Sample console application that generates Mailmark barcodes, saves them as TIFF files,
/// reads the files back, extracts and decodes the Mailmark data, and writes the results to the console.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // ----------------------------------------------------------------------
        // 1. Create a unique temporary folder to store generated TIFF files.
        // ----------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "MailmarkBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // ----------------------------------------------------------------------
        // 2. Prepare a list that will hold the full paths of the generated TIFF files.
        // ----------------------------------------------------------------------
        List<string> tiffFiles = new List<string>();

        // ----------------------------------------------------------------------
        // 3. Generate sample Mailmark 4‑state barcodes and save each as a separate TIFF image.
        // ----------------------------------------------------------------------
        for (int i = 0; i < 4; i++)
        {
            var mailmark = new MailmarkCodetext
            {
                Format = 4,
                VersionID = 1,
                Class = "0",
                SupplychainID = 384224,
                ItemID = 16563762 + i, // Vary ItemID to make each record distinct.
                DestinationPostCodePlusDPS = "EF61AH8T "
            };

            string filePath = Path.Combine(tempFolder, $"Mailmark_{i}.tiff");

            // Use ComplexBarcodeGenerator to create the barcode image.
            using (var generator = new ComplexBarcodeGenerator(mailmark))
            {
                // Set the X‑dimension (module size) in pixels for better readability.
                generator.Parameters.Barcode.XDimension.Pixels = 4f;
                generator.Save(filePath, BarCodeImageFormat.Tiff);
            }

            tiffFiles.Add(filePath);
        }

        // ----------------------------------------------------------------------
        // 4. Iterate over each generated TIFF file, read and decode Mailmark barcodes.
        // ----------------------------------------------------------------------
        foreach (string file in tiffFiles)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            try
            {
                // Initialize the reader for Mailmark decoding.
                using (var reader = new BarCodeReader(file, DecodeType.Mailmark))
                {
                    BarCodeResult[] results = reader.ReadBarCodes();

                    if (results.Length == 0)
                    {
                        Console.WriteLine($"No barcode detected in file: {file}");
                        continue;
                    }

                    // Process each detected barcode.
                    foreach (var result in results)
                    {
                        // Decode the raw Mailmark code text into a strongly‑typed object.
                        MailmarkCodetext decoded = ComplexCodetextReader.TryDecodeMailmark(result.CodeText);
                        if (decoded == null)
                        {
                            Console.WriteLine($"Failed to decode Mailmark data in file: {file}");
                            continue;
                        }

                        // Log the decoded fields to the console.
                        Console.WriteLine($"File: {Path.GetFileName(file)}");
                        Console.WriteLine($"  Format: {decoded.Format}");
                        Console.WriteLine($"  VersionID: {decoded.VersionID}");
                        Console.WriteLine($"  Class: {decoded.Class}");
                        Console.WriteLine($"  SupplychainID: {decoded.SupplychainID}");
                        Console.WriteLine($"  ItemID: {decoded.ItemID}");
                        Console.WriteLine($"  DestinationPostCodePlusDPS: {decoded.DestinationPostCodePlusDPS}");
                    }
                }
            }
            catch (ArgumentException ex)
            {
                // Handle cases where the image cannot be loaded or is invalid.
                Console.WriteLine($"Error loading image '{file}': {ex.Message}");
            }
        }

        // ----------------------------------------------------------------------
        // 5. Clean up temporary files and folder (optional).
        // ----------------------------------------------------------------------
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Suppress any exceptions during cleanup to avoid terminating the app.
        }
    }
}