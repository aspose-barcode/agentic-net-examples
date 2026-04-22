using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Define input and output folders
        string inputFolder = Path.Combine(Directory.GetCurrentDirectory(), "InputCsv");
        string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");

        // Ensure folders exist
        if (!Directory.Exists(inputFolder))
        {
            Directory.CreateDirectory(inputFolder);
        }
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Seed a sample CSV file if none exist (safe sample of 5 rows)
        string csvPath = Path.Combine(inputFolder, "sample.csv");
        if (!File.Exists(csvPath))
        {
            string[] sampleLines = new string[]
            {
                //VersionID,Class,SupplychainID,ItemID,DestinationPostCodePlusDPS
                "1,0,384224,16563762,EF61AH8T ",
                "1,1,384224,16563763,EF61AH8T ",
                "1,2,384224,16563764,EF61AH8T ",
                "1,3,384224,16563765,EF61AH8T ",
                "1,5,384224,16563766,EF61AH8T "
            };
            File.WriteAllLines(csvPath, sampleLines);
        }

        // Read CSV rows
        string[] rows = File.ReadAllLines(csvPath);
        int index = 0;
        foreach (string row in rows)
        {
            if (string.IsNullOrWhiteSpace(row))
                continue;

            string[] parts = row.Split(',');
            if (parts.Length != 5)
            {
                Console.WriteLine($"Skipping invalid row {index + 1}: incorrect number of columns.");
                index++;
                continue;
            }

            // Parse fields
            if (!int.TryParse(parts[0].Trim(), out int versionId) ||
                !int.TryParse(parts[2].Trim(), out int supplyChainId) ||
                !int.TryParse(parts[3].Trim(), out int itemId))
            {
                Console.WriteLine($"Skipping row {index + 1}: numeric parsing failed.");
                index++;
                continue;
            }

            string classValue = parts[1].Trim();
            string destination = parts[4].Trim();

            // Validate DestinationPostCodePlusDPS length (must be 9 characters)
            if (destination.Length != 9)
            {
                Console.WriteLine($"Skipping row {index + 1}: DestinationPostCodePlusDPS must be 9 characters.");
                index++;
                continue;
            }

            // Build Mailmark codetext
            MailmarkCodetext mailmark = new MailmarkCodetext
            {
                Format = 4,                     // 4‑state format
                VersionID = versionId,
                Class = classValue,
                SupplychainID = supplyChainId,
                ItemID = itemId,
                DestinationPostCodePlusDPS = destination
            };

            // Generate barcode image
            using (var generator = new ComplexBarcodeGenerator(mailmark))
            {
                // Set a positive non‑zero bar height
                generator.Parameters.Barcode.BarHeight.Point = 0.5f;

                // Optional: set resolution if desired
                generator.Parameters.Resolution = 300;

                // Save as PNG
                string outFile = Path.Combine(outputFolder, $"Mailmark_{index + 1}.png");
                generator.Save(outFile, BarCodeImageFormat.Png);
                Console.WriteLine($"Generated barcode {outFile}");
            }

            index++;
        }

        Console.WriteLine("Processing completed.");
    }
}