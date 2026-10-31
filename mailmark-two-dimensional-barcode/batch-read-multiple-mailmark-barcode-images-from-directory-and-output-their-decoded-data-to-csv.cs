// Title: Batch read Mailmark barcodes from images and export results to CSV
// Description: Demonstrates generating sample Mailmark 4‑state barcodes, reading them from a folder, and writing decoded information to a CSV file.
// Category-Description: Shows how to use Aspose.BarCode to generate and recognize Mailmark barcodes, covering ComplexBarcodeGenerator, BarCodeReader, DecodeType.Mailmark, and CSV export. Typical use cases include bulk processing of Mailmark images for logistics, inventory, or postal automation. Developers working with Aspose.BarCode often need to batch‑read barcodes, extract structured data, and persist results in common formats.
// Prompt: Batch read multiple Mailmark barcode images from a directory and output their decoded data to CSV.
// Tags: mailmark, barcode, batch, csv, generation, recognition, complexbarcode, aspose.barcode

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Example program that generates Mailmark barcode images, reads them in batch,
/// and writes the decoded data to a CSV file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates sample Mailmark images, processes them,
    /// and outputs a CSV file with the decoded information.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for generated images and results
        string tempFolder = Path.Combine(Path.GetTempPath(), "MailmarkBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Generate sample Mailmark 4‑state barcode images
        var imageFiles = new System.Collections.Generic.List<string>();
        for (int i = 0; i < 5; i++)
        {
            var mailmark = new MailmarkCodetext
            {
                Format = 4,
                VersionID = 1,
                Class = "0",
                SupplychainID = 384224,
                ItemID = 16563762 + i,
                DestinationPostCodePlusDPS = "EF61AH8T "
            };

            string imagePath = Path.Combine(tempFolder, $"Mailmark_{i + 1}.png");
            using (var generator = new ComplexBarcodeGenerator(mailmark))
            {
                // Set barcode module size (X‑dimension) in pixels
                generator.Parameters.Barcode.XDimension.Pixels = 4f;
                generator.Save(imagePath, BarCodeImageFormat.Png);
            }
            imageFiles.Add(imagePath);
        }

        // Prepare CSV output with header row
        string csvPath = Path.Combine(tempFolder, "results.csv");
        var sb = new StringBuilder();
        sb.AppendLine("FileName,CodeText,CodeTypeName,VersionID,Class,SupplychainID,ItemID,DestinationPostCodePlusDPS");

        // Batch read each generated image
        foreach (string file in imageFiles)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            try
            {
                using (var reader = new BarCodeReader(file, DecodeType.Mailmark))
                {
                    BarCodeResult[] results = reader.ReadBarCodes();
                    if (results.Length == 0)
                    {
                        // No Mailmark barcode detected; record empty fields
                        sb.AppendLine($"{Path.GetFileName(file)},,,");
                        Console.WriteLine($"No barcode detected in {Path.GetFileName(file)} (Mailmark reading unsupported).");
                    }
                    else
                    {
                        foreach (var result in results)
                        {
                            string codeText = result.CodeText ?? "";
                            string codeType = result.CodeTypeName ?? "";
                            string versionId = "";
                            string classVal = "";
                            string supplyChain = "";
                            string itemId = "";
                            string destPost = "";

                            // Attempt to parse Mailmark codetext into its components
                            MailmarkCodetext decoded = ComplexCodetextReader.TryDecodeMailmark(codeText);
                            if (decoded != null)
                            {
                                versionId = decoded.VersionID.ToString();
                                classVal = decoded.Class ?? "";
                                supplyChain = decoded.SupplychainID.ToString();
                                itemId = decoded.ItemID.ToString();
                                destPost = decoded.DestinationPostCodePlusDPS ?? "";
                            }

                            // Append a CSV line with escaped values
                            sb.AppendLine($"{Path.GetFileName(file)},{EscapeCsv(codeText)},{EscapeCsv(codeType)},{EscapeCsv(versionId)},{EscapeCsv(classVal)},{EscapeCsv(supplyChain)},{EscapeCsv(itemId)},{EscapeCsv(destPost)}");
                        }
                    }
                }
            }
            catch (ArgumentException ex)
            {
                // Image loading failed or format unsupported
                Console.WriteLine($"Error processing {Path.GetFileName(file)}: {ex.Message}");
                sb.AppendLine($"{Path.GetFileName(file)},Error loading image,,,");
            }
            catch (Exception ex)
            {
                // General safety catch for unexpected errors
                Console.WriteLine($"Unexpected error for {Path.GetFileName(file)}: {ex.Message}");
                sb.AppendLine($"{Path.GetFileName(file)},Unexpected error,,,");
            }
        }

        // Write the accumulated CSV content to file
        File.WriteAllText(csvPath, sb.ToString(), Encoding.UTF8);
        Console.WriteLine($"CSV results written to: {csvPath}");
    }

    /// <summary>
    /// Escapes a string for safe inclusion in a CSV file.
    /// Wraps the value in quotes if it contains commas, quotes, or line breaks.
    /// </summary>
    /// <param name="input">The string to escape.</param>
    /// <returns>The escaped CSV field.</returns>
    static string EscapeCsv(string input)
    {
        if (input == null) return "";
        if (input.Contains(",") || input.Contains("\"") || input.Contains("\n") || input.Contains("\r"))
        {
            string escaped = input.Replace("\"", "\"\"");
            return $"\"{escaped}\"";
        }
        return input;
    }
}