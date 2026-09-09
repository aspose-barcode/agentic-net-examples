// Title: Batch generate Mailmark 2D barcodes from CSV data
// Description: Demonstrates how to read multiple rows from a CSV file and generate Mailmark 2D barcodes, saving each as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcode types such as Mailmark. It showcases the use of ComplexBarcodeGenerator, Mailmark2DCodetext, and related parameters to create high‑volume barcode images from structured data. Developers often need to automate barcode creation from databases or CSV files for mailing and logistics workflows.
// Prompt: Batch generate Mailmark barcodes from a CSV file containing multiple rows of field data.
// Tags: mailmark, barcode, batch, csv, generation, png, aspose.barcode, complexbarcode, mailmark2d

using System;
using System.IO;
using System.Text;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates batch generation of Mailmark 2D barcodes from a CSV file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Reads CSV rows, creates Mailmark2DCodetext objects, and saves PNG images.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary folder for output
        string outputFolder = Path.Combine(Path.GetTempPath(), "MailmarkBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);
        Console.WriteLine("Output folder: " + outputFolder);

        // Path to the CSV file (in the same folder as the executable)
        string csvPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "mailmark_data.csv");

        // If the CSV does not exist, create a sample one
        if (!File.Exists(csvPath))
        {
            var sample = new StringBuilder();
            sample.AppendLine("UPUCountryID,InformationTypeID,VersionID,Class,SupplyChainID,ItemID,DestinationPostCodeAndDPS,RTSFlag,ReturnToSenderPostCode,CustomerContent,DataMatrixType");
            sample.AppendLine("JGB ,0,1,1,123,1234,EF61AH8T ,0, ,CUSTOM,7");
            sample.AppendLine("JGB ,0,1,1,124,1235,EF61AH8T ,0, ,CUSTOM DATA,9");
            sample.AppendLine("JGB ,0,1,1,125,1236,EF61AH8T ,0, ,CUSTOM DATA,29");
            File.WriteAllText(csvPath, sample.ToString(), Encoding.UTF8);
            Console.WriteLine("Sample CSV created at: " + csvPath);
        }

        // Read all lines from the CSV (skip header)
        string[] lines = File.ReadAllLines(csvPath, Encoding.UTF8);
        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i];
            if (string.IsNullOrWhiteSpace(line))
                continue; // Skip empty lines

            // Split the line into columns
            string[] parts = line.Split(',');
            if (parts.Length < 11)
            {
                Console.WriteLine($"Line {i + 1}: insufficient columns, skipping.");
                continue;
            }

            try
            {
                // Populate Mailmark2DCodetext with CSV values
                var mailmark2D = new Mailmark2DCodetext
                {
                    UPUCountryID = parts[0].Trim(),
                    InformationTypeID = parts[1].Trim(),
                    VersionID = parts[2].Trim(),
                    Class = parts[3].Trim(),
                    SupplyChainID = int.Parse(parts[4].Trim()),
                    ItemID = int.Parse(parts[5].Trim()),
                    DestinationPostCodeAndDPS = parts[6].Trim(),
                    RTSFlag = parts[7].Trim(),
                    ReturnToSenderPostCode = parts[8].Trim(),
                    CustomerContent = parts[9].Trim()
                };

                // Map DataMatrixType string to enum
                string typeStr = parts[10].Trim();
                switch (typeStr)
                {
                    case "7":
                        mailmark2D.DataMatrixType = Mailmark2DType.Type_7;
                        break;
                    case "9":
                        mailmark2D.DataMatrixType = Mailmark2DType.Type_9;
                        break;
                    case "29":
                        mailmark2D.DataMatrixType = Mailmark2DType.Type_29;
                        break;
                    default:
                        Console.WriteLine($"Line {i + 1}: unknown DataMatrixType '{typeStr}', skipping.");
                        continue;
                }

                // Define output file path for the generated barcode image
                string outputPath = Path.Combine(outputFolder, $"Mailmark_{i}.png");

                // Generate and save the barcode
                using (var generator = new ComplexBarcodeGenerator(mailmark2D))
                {
                    generator.Parameters.Barcode.XDimension.Pixels = 4f;
                    generator.Save(outputPath, BarCodeImageFormat.Png);
                }

                Console.WriteLine($"Generated barcode {i}: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Line {i + 1}: error - {ex.Message}");
            }
        }

        Console.WriteLine("Batch generation completed.");
    }
}