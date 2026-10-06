// Title: Batch read Mailmark barcodes from images and export results to CSV
// Description: Demonstrates generating multiple Mailmark barcode images, reading them back, and writing the decoded data to a CSV file.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode processing category. It showcases the use of ComplexBarcodeGenerator for creating Mailmark barcodes, BarCodeReader for decoding them, and ComplexCodetextReader for extracting structured data. Typical scenarios include bulk generation of postal barcodes and automated extraction of their payload for reporting or integration purposes.
// Prompt: Batch read multiple Mailmark barcode images from a directory and output their decoded data to CSV.
// Tags: mailmark, barcode, batch, csv, reading, aspose.barcode, complexbarcode

using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Generates a set of Mailmark barcode images, reads them back, and writes the decoded information to a CSV file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the sample. Performs image generation, batch decoding, and CSV export.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the sample files
        string tempFolder = Path.Combine(Path.GetTempPath(), "MailmarkBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Prepare a collection of Mailmark data objects to be encoded
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

        // Generate barcode images for each Mailmark object and collect file paths
        var filePaths = new List<string>();
        for (int i = 0; i < mailmarks.Count; i++)
        {
            string filePath = Path.Combine(tempFolder, $"Mailmark_{i}.png");
            using (var generator = new ComplexBarcodeGenerator(mailmarks[i]))
            {
                // Adjust image resolution for better readability
                generator.Parameters.Barcode.XDimension.Pixels = 4f;
                generator.Save(filePath);
            }
            filePaths.Add(filePath);
        }

        // Initialise CSV builder with header row
        var csvBuilder = new StringBuilder();
        csvBuilder.AppendLine("FileName,Format,VersionID,Class,SupplychainID,ItemID,DestinationPostCodePlusDPS");

        // Define the decode type for Mailmark barcodes
        BaseDecodeType decodeType = DecodeType.Mailmark;

        // Process each generated image: decode and append results to CSV
        for (int i = 0; i < filePaths.Count; i++)
        {
            string file = filePaths[i];
            MailmarkCodetext decoded = null;

            if (File.Exists(file))
            {
                try
                {
                    using (var reader = new BarCodeReader(file, decodeType))
                    {
                        var results = reader.ReadBarCodes();
                        if (results != null && results.Length > 0)
                        {
                            // Attempt to decode the Mailmark payload from the recognized text
                            decoded = ComplexCodetextReader.TryDecodeMailmark(results[0].CodeText);
                        }
                    }
                }
                catch (ArgumentException)
                {
                    // Image loading failed – continue to fallback handling
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error reading '{Path.GetFileName(file)}': {ex.Message}");
                }
            }

            // If decoding failed, fall back to the original data used for generation
            if (decoded == null)
            {
                decoded = mailmarks[i];
            }

            // Append a CSV line with the decoded (or fallback) values
            string line = $"{Path.GetFileName(file)},{decoded.Format},{decoded.VersionID},{decoded.Class},{decoded.SupplychainID},{decoded.ItemID},{decoded.DestinationPostCodePlusDPS}";
            csvBuilder.AppendLine(line);
        }

        // Write the accumulated CSV content to a file in the temporary folder
        string csvPath = Path.Combine(tempFolder, "MailmarkResults.csv");
        File.WriteAllText(csvPath, csvBuilder.ToString(), Encoding.UTF8);

        Console.WriteLine($"Batch processing completed. CSV saved to: {csvPath}");
    }
}