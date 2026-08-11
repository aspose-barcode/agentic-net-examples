// Title: Read batch of PNG barcodes with Australia Post CTable interpretation
// Description: Demonstrates generating Australia Post barcodes, saving them as PNG files, and reading them back using CustomerInformationInterpretingType.CTable.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the BarcodeGenerator for creating barcodes and BarCodeReader for decoding them, focusing on the AustraliaPost symbology. Developers often need to batch‑process image files, apply specific encoding tables (CTable), and extract barcode data for logistics or mailing applications. The code illustrates typical use cases such as bulk image creation, file system handling, and customized decoding settings.
// Prompt: Create a sample that reads a batch of PNG files applying AustraliaPostSettings.CustomerInformationInterpretingType.CTable.
// Tags: barcode, australia post, ctable, generation, recognition, png, batch, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Sample program that generates Australia Post barcodes, saves them as PNG files,
/// and reads them back using CTable customer information interpreting type.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcodes, saves them, and decodes them.
    /// </summary>
    static void Main()
    {
        // Define folder for sample barcode images
        string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        // Sample Australia Post codetexts (FCC=59, DPID=8 digits, optional CTable info)
        string[] sampleCodes = new[]
        {
            "5912345678AB",   // 2 CTable chars
            "6212345678ABCDE",// 5 CTable chars (max)
            "5912345678"      // No customer info
        };

        // -------------------------------------------------
        // Generate barcode images and apply CTable interpreting type
        // -------------------------------------------------
        for (int i = 0; i < sampleCodes.Length; i++)
        {
            string codeText = sampleCodes[i];
            string filePath = Path.Combine(folderPath, $"barcode{i + 1}.png");

            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, codeText))
            {
                // Apply CTable interpreting type for encoding
                generator.Parameters.Barcode.AustralianPost.EncodingTable = CustomerInformationInterpretingType.CTable;
                generator.Save(filePath); // format inferred from extension
            }
        }

        // -------------------------------------------------
        // Read and decode the generated PNG files using CTable interpreting type
        // -------------------------------------------------
        string[] pngFiles = Directory.GetFiles(folderPath, "barcode*.png");
        foreach (string pngFile in pngFiles)
        {
            try
            {
                using (BarCodeReader reader = new BarCodeReader(pngFile, DecodeType.AustraliaPost))
                {
                    // Set decoding to use CTable interpreting type
                    reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = CustomerInformationInterpretingType.CTable;

                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        Console.WriteLine($"File: {Path.GetFileName(pngFile)}");
                        Console.WriteLine($"  BarCode Type: {result.CodeType}");
                        Console.WriteLine($"  BarCode CodeText: {result.CodeText}");
                    }
                }
            }
            catch (ArgumentException)
            {
                // Skip files that cannot be loaded as images
                continue;
            }
        }
    }
}