// Title: Batch Mailmark Barcode Generation, Reading, and CSV Export
// Description: Demonstrates how to generate multiple Mailmark barcodes, read them back, and write the decoded information to a CSV file.
// Category-Description: This example belongs to the Aspose.BarCode batch processing category, showcasing the use of ComplexBarcodeGenerator for creating Mailmark symbols and BarCodeReader with DecodeType.Mailmark for decoding. It illustrates typical workflows such as bulk image generation, automated reading, and exporting results, which developers often need when integrating barcode handling into logistics or mailing systems.
// Prompt: Batch read multiple Mailmark barcode images from a directory and output their decoded data to CSV.
// Tags: mailmark, barcode, batch, csv, generation, reading, aspose.barcode, complexbarcode

using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates batch generation of Mailmark barcodes, reading them, and exporting results to CSV.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample Mailmark barcodes, reads each image, and writes decoded data to a CSV file.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the batch
        string batchFolder = Path.Combine(Path.GetTempPath(), "BatchMailmark_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(batchFolder);

        // Prepare sample Mailmark data
        var mailmarks = new List<MailmarkCodetext>();
        for (int i = 0; i < 5; i++)
        {
            var mm = new MailmarkCodetext
            {
                Format = 4,
                VersionID = 1,
                Class = "0",
                SupplychainID = 384224,
                ItemID = 16563762 + i,
                DestinationPostCodePlusDPS = "EF61AH8T "
            };
            mailmarks.Add(mm);
        }

        // Generate barcode images and keep file list
        var imageFiles = new List<string>();
        for (int i = 0; i < mailmarks.Count; i++)
        {
            string filePath = Path.Combine(batchFolder, $"mailmark_{i}.png");
            using (var generator = new ComplexBarcodeGenerator(mailmarks[i]))
            {
                // Set X-dimension for better readability
                generator.Parameters.Barcode.XDimension.Pixels = 4;
                generator.Save(filePath);
            }
            imageFiles.Add(filePath);
        }

        // Prepare CSV output header
        string csvPath = Path.Combine(batchFolder, "MailmarkBatchOutput.csv");
        var sb = new StringBuilder();
        sb.AppendLine("FileName,Format,VersionID,Class,SupplychainID,ItemID,DestinationPostCodePlusDPS,ReadSuccess");

        // Process each generated image
        foreach (string file in imageFiles)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            bool readSuccess = false;
            int format = 0, versionId = 0, supplychainId = 0, itemId = 0;
            string classStr = "", destPostcode = "";

            try
            {
                // Initialize reader for Mailmark barcode type
                using (var reader = new BarCodeReader(file, DecodeType.Mailmark))
                {
                    BarCodeResult[] results = reader.ReadBarCodes();
                    if (results.Length > 0)
                    {
                        // Assuming first result is the Mailmark barcode
                        var result = results[0];
                        var decoded = ComplexCodetextReader.TryDecodeMailmark(result.CodeText);
                        if (decoded != null)
                        {
                            format = decoded.Format;
                            versionId = decoded.VersionID;
                            classStr = decoded.Class;
                            supplychainId = decoded.SupplychainID;
                            itemId = decoded.ItemID;
                            destPostcode = decoded.DestinationPostCodePlusDPS;
                            readSuccess = true;
                        }
                    }
                }
            }
            catch (ArgumentException ex)
            {
                // Image loading failed or unsupported format
                Console.WriteLine($"Error reading file '{Path.GetFileName(file)}': {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error for file '{Path.GetFileName(file)}': {ex.Message}");
            }

            // Append result line to CSV (empty or zero values if reading failed)
            sb.AppendLine($"{Path.GetFileName(file)},{format},{versionId},{classStr},{supplychainId},{itemId},{destPostcode},{readSuccess}");
        }

        // Write CSV file to disk
        File.WriteAllText(csvPath, sb.ToString(), Encoding.UTF8);
        Console.WriteLine($"Batch processing completed. CSV saved to: {csvPath}");
    }
}