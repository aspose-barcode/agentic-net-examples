using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Define input CSV and output folder paths
        string inputCsv = Path.Combine(Directory.GetCurrentDirectory(), "MailmarkData.csv");
        string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "MailmarkBarcodes");

        // Ensure output directory exists
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // If the CSV does not exist, create a sample file with a few rows
        if (!File.Exists(inputCsv))
        {
            var sampleLines = new[]
            {
                "Format,VersionID,Class,SupplychainID,ItemID,DestinationPostCodePlusDPS",
                "4,1,0,384224,100001,EF61AH8T ",
                "4,1,1,384224,100002,EF61AH8T ",
                "4,1,2,384224,100003,EF61AH8T ",
                "4,1,3,384224,100004,EF61AH8T ",
                "4,1,5,384224,100005,EF61AH8T "
            };
            File.WriteAllLines(inputCsv, sampleLines);
        }

        // Read all lines, skip header
        var lines = File.ReadAllLines(inputCsv);
        if (lines.Length <= 1)
        {
            Console.WriteLine("CSV file contains no data rows.");
            return;
        }

        for (int i = 1; i < lines.Length; i++)
        {
            var fields = lines[i].Split(',');

            if (fields.Length != 6)
            {
                Console.WriteLine($"Skipping line {i + 1}: incorrect number of fields.");
                continue;
            }

            // Parse fields
            // Format is expected to be integer (rule specifies setting to 4)
            if (!int.TryParse(fields[0], out int format))
            {
                Console.WriteLine($"Skipping line {i + 1}: invalid Format.");
                continue;
            }

            if (!int.TryParse(fields[1], out int versionId))
            {
                Console.WriteLine($"Skipping line {i + 1}: invalid VersionID.");
                continue;
            }

            string classValue = fields[2];

            if (!int.TryParse(fields[3], out int supplyChainId))
            {
                Console.WriteLine($"Skipping line {i + 1}: invalid SupplychainID.");
                continue;
            }

            if (!int.TryParse(fields[4], out int itemId))
            {
                Console.WriteLine($"Skipping line {i + 1}: invalid ItemID.");
                continue;
            }

            string destinationPostCodePlusDps = fields[5];

            // Create MailmarkCodetext and set required properties
            var mailmark = new MailmarkCodetext();
            mailmark.Format = format;                     // Rule 44: set Format=4 (or parsed value)
            mailmark.VersionID = versionId;               // integer
            mailmark.Class = classValue;                  // string
            mailmark.SupplychainID = supplyChainId;       // integer
            mailmark.ItemID = itemId;                     // integer
            mailmark.DestinationPostCodePlusDPS = destinationPostCodePlusDps; // string

            // Generate barcode using ComplexBarcodeGenerator
            using (var generator = new ComplexBarcodeGenerator(mailmark))
            {
                // Ensure BarHeight is positive non‑zero (rule 47)
                generator.Parameters.Barcode.BarHeight.Point = 10f;

                // Save as PNG
                string outputPath = Path.Combine(outputFolder, $"{itemId}.png");
                generator.Save(outputPath, BarCodeImageFormat.Png);
                Console.WriteLine($"Generated barcode for ItemID {itemId} at {outputPath}");
            }
        }

        Console.WriteLine("Batch generation completed.");
    }
}