// Title: Batch conversion of CSV rows to Mailmark barcode images
// Description: Demonstrates reading a CSV file, parsing each record, and generating a separate Mailmark barcode image per row using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category, focusing on the Mailmark symbology. It showcases the use of ComplexBarcodeGenerator, MailmarkCodetext, and BarCodeImageFormat to create PNG images from structured data. Developers often need to automate barcode creation from data sources such as CSV files for mailing, logistics, and tracking applications.
// Prompt: Create a PowerShell script invoking the .NET library to batch convert CSV rows into individual Mailmark barcode images.
// Tags: mailmark, barcode, batch, csv, image, png, aspose.barcode, complexbarcode, generation

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates batch processing of CSV data to generate Mailmark barcode images using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Reads a CSV file, creates Mailmark codetext for each row,
    /// and saves the resulting barcode images as PNG files.
    /// </summary>
    static void Main()
    {
        // Create a dedicated temporary working folder for all generated files.
        string workFolder = Path.Combine(Path.GetTempPath(), "MailmarkBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workFolder);

        // Prepare a sample CSV file containing Mailmark data.
        string csvPath = Path.Combine(workFolder, "data.csv");
        string[] csvLines = new[]
        {
            "Format,VersionID,Class,SupplychainID,ItemID,DestinationPostCodePlusDPS",
            "4,1,0,384224,16563762,EF61AH8T ",
            "4,1,1,123456,98765432,EF61AH8T ",
            "4,2,0,111111,22222222,EF61AH8T "
        };
        File.WriteAllLines(csvPath, csvLines, Encoding.UTF8);

        // Output folder where generated barcode images will be stored.
        string outputFolder = Path.Combine(workFolder, "Barcodes");
        Directory.CreateDirectory(outputFolder);

        // Read all lines from the CSV file (UTF‑8) and process each data row (skip header).
        string[] allLines = File.ReadAllLines(csvPath, Encoding.UTF8);
        for (int i = 1; i < allLines.Length; i++)
        {
            string line = allLines[i];
            if (string.IsNullOrWhiteSpace(line))
                continue; // Skip empty lines.

            // Split the CSV line into its six expected columns.
            string[] parts = line.Split(',');
            if (parts.Length != 6)
                continue; // Skip malformed rows.

            // Parse individual fields.
            int format = int.Parse(parts[0]);
            int versionId = int.Parse(parts[1]);
            string classValue = parts[2];
            int supplyChainId = int.Parse(parts[3]);
            int itemId = int.Parse(parts[4]);
            string destination = parts[5];

            // Build the Mailmark codetext object using parsed values.
            var mailmark = new MailmarkCodetext
            {
                Format = format,
                VersionID = versionId,
                Class = classValue,
                SupplychainID = supplyChainId,
                ItemID = itemId,
                DestinationPostCodePlusDPS = destination
            };

            // Generate the barcode image and save it as PNG.
            string imagePath = Path.Combine(outputFolder, $"Mailmark_{i}.png");
            using (var generator = new ComplexBarcodeGenerator(mailmark))
            {
                // Set X‑dimension (module size) to 4 pixels for better readability.
                generator.Parameters.Barcode.XDimension.Pixels = 4f;
                generator.Save(imagePath, BarCodeImageFormat.Png);
            }

            Console.WriteLine($"Generated: {imagePath}");
        }

        Console.WriteLine("Batch processing completed.");
    }
}