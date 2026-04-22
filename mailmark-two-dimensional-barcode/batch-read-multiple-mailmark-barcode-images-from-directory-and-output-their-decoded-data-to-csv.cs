using System;
using System.IO;
using System.Linq;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

class Program
{
    static void Main()
    {
        // Input folder containing Mailmark barcode images
        string inputFolder = "MailmarkImages";
        // Output CSV file path
        string outputCsv = "MailmarkDecoded.csv";

        // Ensure the input folder exists
        if (!Directory.Exists(inputFolder))
        {
            Directory.CreateDirectory(inputFolder);
            Console.WriteLine($"Input folder '{inputFolder}' was created. Add barcode images and rerun the program.");
            return;
        }

        // Get image files (common image extensions)
        var imageFiles = Directory.GetFiles(inputFolder)
            .Where(f => f.EndsWith(".png", StringComparison.OrdinalIgnoreCase) ||
                        f.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
                        f.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase) ||
                        f.EndsWith(".bmp", StringComparison.OrdinalIgnoreCase) ||
                        f.EndsWith(".tif", StringComparison.OrdinalIgnoreCase) ||
                        f.EndsWith(".tiff", StringComparison.OrdinalIgnoreCase))
            .ToList();

        if (imageFiles.Count == 0)
        {
            Console.WriteLine($"No image files found in '{inputFolder}'.");
            return;
        }

        // Write CSV header
        using (var writer = new StreamWriter(outputCsv, false))
        {
            writer.WriteLine("FileName,Class,DestinationPostCodePlusDPS,Format,ItemID,SupplychainID,VersionID");

            foreach (var filePath in imageFiles)
            {
                if (!File.Exists(filePath))
                {
                    Console.WriteLine($"File not found: {filePath}");
                    continue;
                }

                // Read Mailmark barcodes from the image
                using (var reader = new BarCodeReader(filePath, DecodeType.Mailmark))
                {
                    var results = reader.ReadBarCodes();

                    if (results == null || results.Length == 0)
                    {
                        Console.WriteLine($"No Mailmark barcode detected in '{Path.GetFileName(filePath)}'.");
                        continue;
                    }

                    foreach (var result in results)
                    {
                        // Decode the complex Mailmark codetext
                        MailmarkCodetext mailmark = ComplexCodetextReader.TryDecodeMailmark(result.CodeText);

                        if (mailmark != null)
                        {
                            // Prepare CSV line with decoded fields
                            string line = string.Join(",",
                                EscapeCsv(Path.GetFileName(filePath)),
                                EscapeCsv(mailmark.Class),
                                EscapeCsv(mailmark.DestinationPostCodePlusDPS),
                                EscapeCsv(mailmark.Format.ToString()),
                                mailmark.ItemID,
                                mailmark.SupplychainID,
                                mailmark.VersionID);

                            writer.WriteLine(line);
                        }
                        else
                        {
                            // If decoding fails, write raw CodeText
                            string line = string.Join(",",
                                EscapeCsv(Path.GetFileName(filePath)),
                                "N/A", "N/A", "N/A", "0", "0", "0");
                            writer.WriteLine(line);
                        }
                    }
                }
            }
        }

        Console.WriteLine($"Decoding completed. Results saved to '{outputCsv}'.");
    }

    // Helper to escape CSV fields containing commas or quotes
    private static string EscapeCsv(string field)
    {
        if (field == null)
            return "";
        if (field.Contains(",") || field.Contains("\"") || field.Contains("\n"))
        {
            string escaped = field.Replace("\"", "\"\"");
            return $"\"{escaped}\"";
        }
        return field;
    }
}