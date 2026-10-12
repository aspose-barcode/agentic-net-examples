// Title: Read batch of PNG barcodes with Australia Post CTable interpreting type
// Description: Demonstrates generating a set of Australia Post barcodes, saving them as PNG files, and then reading them back while applying the CTable customer‑information interpreting type.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating Australia Post barcodes, the BarCodeReader for decoding them, and the AustraliaPostSettings.CustomerInformationInterpretingType enumeration to control how customer information is interpreted. Typical scenarios include batch processing of postal barcodes, automated verification of encoded data, and integration with logistics systems. Developers often need to generate barcodes with specific encoding tables and then read them in bulk, making this pattern a common building block in shipping and mailing applications.
// Prompt: Create a sample that reads a batch of PNG files applying AustraliaPostSettings.CustomerInformationInterpretingType.CTable.
// Tags: barcode, australia post, ctable, batch, png, generation, recognition, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Sample program that generates a batch of Australia Post barcodes,
/// saves them as PNG files, and reads them back using the CTable interpreting type.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the sample. Generates barcodes, reads them, and optionally cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the sample batch
        string batchFolder = Path.Combine(Path.GetTempPath(), "Batch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(batchFolder);

        // List to hold generated file paths
        List<string> barcodeFiles = new List<string>();

        // Generate sample Australia Post barcodes with CTable encoding
        for (int i = 0; i < 3; i++)
        {
            // Customer information must be two characters for CTable
            string customerInfo = "AB" + i;
            // FCC 62 + DPID (8 digits) + customer info
            string codeText = "6201234567" + customerInfo;
            string filePath = Path.Combine(batchFolder, $"AustraliaPost_{i}.png");

            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, codeText))
            {
                // Set visual parameters
                generator.Parameters.Barcode.XDimension.Pixels = 4f;
                generator.Parameters.Barcode.BarHeight.Pixels = 50f;
                // Apply CTable encoding for customer information
                generator.Parameters.Barcode.AustralianPost.EncodingTable = CustomerInformationInterpretingType.CTable;
                // Save as PNG
                generator.Save(filePath, BarCodeImageFormat.Png);
                barcodeFiles.Add(filePath);
            }
        }

        // Read the generated barcodes applying CTable interpreting type
        foreach (string file in barcodeFiles)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            try
            {
                using (BarCodeReader reader = new BarCodeReader(file, DecodeType.AustraliaPost))
                {
                    // Configure reader to interpret customer information using CTable
                    reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = CustomerInformationInterpretingType.CTable;

                    // Output each decoded result
                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        Console.WriteLine($"File: {Path.GetFileName(file)} | Type: {result.CodeTypeName} | Text: {result.CodeText}");
                    }
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Skipping file {Path.GetFileName(file)} due to error: {ex.Message}");
            }
        }

        // Optional cleanup (comment out if you want to inspect the files)
        try
        {
            Directory.Delete(batchFolder, true);
        }
        catch
        {
            // Ignore cleanup errors
        }
    }
}