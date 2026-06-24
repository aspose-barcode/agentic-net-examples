using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Entry point for the MaxiCode decoding utility.
/// Scans a folder for PNG images, attempts to read MaxiCode barcodes,
/// and generates a CSV report with extracted information.
/// </summary>
class Program
{
    /// <summary>
    /// Main method executed by the runtime.
    /// Accepts an optional command‑line argument specifying the input folder.
    /// </summary>
    /// <param name="args">Command‑line arguments; first argument is the input folder path.</param>
    static void Main(string[] args)
    {
        // Determine input folder: use first argument if supplied, otherwise default to "MaxiCodeImages".
        string inputFolder = args.Length > 0 ? args[0] : "MaxiCodeImages";

        // Path for the generated CSV report.
        string csvPath = "MaxiCodeReport.csv";

        // Ensure the input folder exists; create it if missing and exit.
        if (!Directory.Exists(inputFolder))
        {
            Console.WriteLine($"Input folder \"{inputFolder}\" does not exist. Creating it.");
            Directory.CreateDirectory(inputFolder);
            Console.WriteLine("Place PNG files to decode in the folder and rerun the program.");
            return;
        }

        // StringBuilder to accumulate CSV lines.
        var sb = new StringBuilder();

        // Write CSV header.
        sb.AppendLine("FileName,BarcodeType,CodeText,Mode,PostalCode,CountryCode,ServiceCategory,SecondMessage");

        // Retrieve all PNG files from the input folder.
        string[] pngFiles = Directory.GetFiles(inputFolder, "*.png");
        if (pngFiles.Length == 0)
        {
            Console.WriteLine($"No PNG files found in \"{inputFolder}\".");
        }

        // Process each PNG file individually.
        foreach (string filePath in pngFiles)
        {
            try
            {
                // Open the image file as a read‑only stream.
                using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    // Initialize the barcode reader for MaxiCode type.
                    using (var reader = new BarCodeReader(fileStream, DecodeType.MaxiCode))
                    {
                        // Attempt to read all barcodes in the image.
                        var results = reader.ReadBarCodes();

                        // If no barcodes were detected, record this in the CSV.
                        if (results.Length == 0)
                        {
                            sb.AppendLine($"{EscapeCsv(Path.GetFileName(filePath))},,,No barcode found,,,,,");
                            continue;
                        }

                        // Iterate over each detected barcode result.
                        foreach (var result in results)
                        {
                            // Basic barcode properties.
                            string barcodeType = result.CodeTypeName ?? "";
                            string codeText = result.CodeText ?? "";
                            string modeStr = "";
                            string postalCode = "";
                            string countryCode = "";
                            string serviceCategory = "";
                            string secondMessage = "";

                            // Attempt to extract MaxiCode‑specific extended data.
                            var maxiCodeExt = result.Extended?.MaxiCode;
                            if (maxiCodeExt != null)
                            {
                                // Record the MaxiCode mode (e.g., 2, 3, 4, 5, 6).
                                modeStr = maxiCodeExt.Mode.ToString();

                                // Decode the structured codetext based on the mode.
                                var decoded = ComplexCodetextReader.TryDecodeMaxiCode(maxiCodeExt.Mode, codeText);

                                // Handle Mode 2 decoding.
                                if (decoded is MaxiCodeCodetextMode2 mode2)
                                {
                                    postalCode = mode2.PostalCode ?? "";
                                    countryCode = mode2.CountryCode.ToString();
                                    serviceCategory = mode2.ServiceCategory.ToString();

                                    // Extract second message, which may be standard or structured.
                                    if (mode2.SecondMessage is MaxiCodeStandardSecondMessage stdMsg)
                                    {
                                        secondMessage = stdMsg.Message ?? "";
                                    }
                                    else if (mode2.SecondMessage is MaxiCodeStructuredSecondMessage structMsg)
                                    {
                                        var parts = new List<string>();
                                        foreach (var id in structMsg.Identifiers)
                                            parts.Add(id);
                                        parts.Add($"Year:{structMsg.Year}");
                                        secondMessage = string.Join(" | ", parts);
                                    }
                                }
                                // Handle Mode 3 decoding (similar structure to Mode 2).
                                else if (decoded is MaxiCodeCodetextMode3 mode3)
                                {
                                    postalCode = mode3.PostalCode ?? "";
                                    countryCode = mode3.CountryCode.ToString();
                                    serviceCategory = mode3.ServiceCategory.ToString();

                                    if (mode3.SecondMessage is MaxiCodeStandardSecondMessage stdMsg3)
                                    {
                                        secondMessage = stdMsg3.Message ?? "";
                                    }
                                    else if (mode3.SecondMessage is MaxiCodeStructuredSecondMessage structMsg3)
                                    {
                                        var parts = new List<string>();
                                        foreach (var id in structMsg3.Identifiers)
                                            parts.Add(id);
                                        parts.Add($"Year:{structMsg3.Year}");
                                        secondMessage = string.Join(" | ", parts);
                                    }
                                }
                            }

                            // Append a CSV line with all extracted fields, escaping as needed.
                            sb.AppendLine($"{EscapeCsv(Path.GetFileName(filePath))},{EscapeCsv(barcodeType)},{EscapeCsv(codeText)},{EscapeCsv(modeStr)},{EscapeCsv(postalCode)},{EscapeCsv(countryCode)},{EscapeCsv(serviceCategory)},{EscapeCsv(secondMessage)}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log any exception that occurs while processing a file and record it in the CSV.
                Console.WriteLine($"Error processing file \"{filePath}\": {ex.Message}");
                sb.AppendLine($"{EscapeCsv(Path.GetFileName(filePath))},Error,,{EscapeCsv(ex.Message)},,,,,");
            }
        }

        // Attempt to write the accumulated CSV content to disk.
        try
        {
            File.WriteAllText(csvPath, sb.ToString(), Encoding.UTF8);
            Console.WriteLine($"CSV report generated at \"{csvPath}\".");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to write CSV file: {ex.Message}");
        }
    }

    /// <summary>
    /// Escapes a CSV field by surrounding it with quotes if it contains
    /// commas, quotes, or line‑break characters, and doubles any internal quotes.
    /// </summary>
    /// <param name="field">The field value to escape; may be null.</param>
    /// <returns>A CSV‑safe representation of the field.</returns>
    static string EscapeCsv(string field)
    {
        if (field == null)
            return "";
        if (field.Contains("\""))
            field = field.Replace("\"", "\"\"");
        if (field.Contains(",") || field.Contains("\"") || field.Contains("\n") || field.Contains("\r"))
            return $"\"{field}\"";
        return field;
    }
}