// Title: Batch read Mailmark barcodes from a directory and export results to CSV
// Description: Demonstrates how to generate sample Mailmark barcode images, read them in bulk, and write the decoded data to a CSV file.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation and recognition category. It showcases the use of ComplexBarcodeGenerator to create Mailmark symbols and BarCodeReader with DecodeType.Mailmark to decode them. Typical scenarios include bulk processing of Mailmark images for logistics, inventory tracking, or data extraction, where developers need to automate image generation, batch reading, and export of results to common formats like CSV.
// Prompt: Batch read multiple Mailmark barcode images from a directory and output their decoded data to CSV.
// Tags: mailmark, barcode, batch-processing, csv, generation, recognition, aspose.barcode, complexbarcode

using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates sample Mailmark barcode images (if none exist),
/// reads all PNG images in a folder, decodes the Mailmark symbols, and writes the
/// results to a CSV file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Performs image generation, batch decoding,
    /// and CSV export.
    /// </summary>
    static void Main()
    {
        // Folder that holds Mailmark barcode images
        string folderPath = "MailmarkImages";

        // Ensure the folder exists; create it if missing
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        // Generate sample Mailmark images when the folder is empty
        string[] existingImages = Directory.GetFiles(folderPath, "*.png");
        if (existingImages.Length == 0)
        {
            for (int i = 0; i < 5; i++)
            {
                // Configure a valid MailmarkCodetext instance
                var mailmark = new MailmarkCodetext
                {
                    Format = 4,                     // 4‑state Mailmark
                    VersionID = 1,
                    Class = "0",
                    SupplychainID = 384224,
                    ItemID = 1000 + i,              // Unique ItemID per record
                    DestinationPostCodePlusDPS = "EF61AH8T " // trailing space required
                };

                // Generate the barcode image and save it as PNG
                using (var generator = new ComplexBarcodeGenerator(mailmark))
                {
                    string imagePath = Path.Combine(folderPath, $"Mailmark_{i + 1}.png");
                    generator.Save(imagePath);
                }
            }
        }

        // Path for the CSV output file
        string csvPath = "MailmarkResults.csv";

        // Write CSV header line
        File.WriteAllText(csvPath, "FileName,CodeType,CodeText" + Environment.NewLine);

        // Process each PNG image in the folder
        string[] pngFiles = Directory.GetFiles(folderPath, "*.png");
        foreach (string imageFile in pngFiles)
        {
            if (!File.Exists(imageFile))
                continue;

            // Initialize a reader for the Mailmark symbology
            using (var reader = new BarCodeReader(imageFile, DecodeType.Mailmark))
            {
                // Read all barcodes found in the image
                foreach (var result in reader.ReadBarCodes())
                {
                    // Append decoded information as a CSV line
                    string csvLine = $"{Path.GetFileName(imageFile)},{result.CodeTypeName},{result.CodeText}";
                    File.AppendAllText(csvPath, csvLine + Environment.NewLine);
                }
            }
        }

        Console.WriteLine($"Decoding completed. Results saved to {csvPath}");
    }
}