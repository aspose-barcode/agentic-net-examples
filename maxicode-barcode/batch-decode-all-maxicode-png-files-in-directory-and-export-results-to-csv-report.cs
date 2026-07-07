// Title: Batch decode MaxiCode PNG files to CSV
// Description: Demonstrates how to read multiple MaxiCode barcodes from PNG images in a folder and export the decoded information to a CSV report.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, focusing on batch processing of MaxiCode symbology. It showcases the use of BarCodeReader, DecodeType.MaxiCode, QualitySettings, and ComplexCodetextReader to extract structured data such as postal code, country code, and service category, then writes results to a CSV file. Developers working with bulk barcode decoding, logistics, or shipping label processing can use this pattern for automated data extraction.
// Prompt: Batch decode all MaxiCode PNG files in a directory and export the results to a CSV report.
// Tags: maxicode, barcode, batch processing, csv, aspose.barcode, decoding, recognition, complexcodetext

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides a console application that batch decodes MaxiCode barcodes from PNG files
/// and writes the extracted information to a CSV report.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    /// <param name="args">
    /// Optional command‑line arguments:
    /// args[0] – input folder path (default: "Input"),
    /// args[1] – output CSV file path (default: "MaxiCodeReport.csv").
    /// </param>
    static void Main(string[] args)
    {
        // Resolve input folder and output CSV path from arguments or use defaults.
        string inputFolder = args.Length > 0 ? args[0] : "Input";
        string outputCsv = args.Length > 1 ? args[1] : "MaxiCodeReport.csv";

        // Ensure the input folder exists; create it if missing.
        if (!Directory.Exists(inputFolder))
        {
            Directory.CreateDirectory(inputFolder);
        }

        // Retrieve all PNG files from the input directory.
        string[] pngFiles = Directory.GetFiles(inputFolder, "*.png");

        // Limit processing to a maximum of 10 files as a safety guideline.
        int maxFiles = Math.Min(pngFiles.Length, 10);

        // Open a StreamWriter for the CSV report.
        using (var writer = new StreamWriter(outputCsv, false))
        {
            // Write the CSV header line.
            writer.WriteLine("FileName,CodeText,PostalCode,CountryCode,ServiceCategory,Message");

            // Process each PNG file up to the defined limit.
            for (int i = 0; i < maxFiles; i++)
            {
                string filePath = pngFiles[i];
                string fileName = Path.GetFileName(filePath);

                // Guard against missing files (should not happen after GetFiles).
                if (!File.Exists(filePath))
                {
                    Console.WriteLine($"File not found: {filePath}");
                    continue;
                }

                // Initialize a BarCodeReader for MaxiCode decoding.
                using (var reader = new BarCodeReader(filePath, DecodeType.MaxiCode))
                {
                    // Apply the highest quality settings to improve detection accuracy.
                    reader.QualitySettings = QualitySettings.MaxQuality;

                    // Read all barcodes present in the image.
                    BarCodeResult[] results = reader.ReadBarCodes();

                    // If no barcodes were found, write an empty record and continue.
                    if (results.Length == 0)
                    {
                        writer.WriteLine($"{Escape(fileName)},,,,,");

                        continue;
                    }

                    // Process each detected barcode.
                    foreach (var result in results)
                    {
                        // Retrieve raw codetext; ensure it's not null.
                        string rawCodeText = result.CodeText ?? string.Empty;

                        // Decode the structured MaxiCode codetext.
                        MaxiCodeCodetext decoded = ComplexCodetextReader.TryDecodeMaxiCode(
                            result.Extended.MaxiCode.MaxiCodeMode,
                            rawCodeText);

                        // Initialize fields with default empty values.
                        string postal = string.Empty;
                        string country = string.Empty;
                        string service = string.Empty;
                        string message = string.Empty;

                        // Extract details for Mode 2 MaxiCode.
                        if (decoded is MaxiCodeCodetextMode2 mode2)
                        {
                            postal = mode2.PostalCode ?? string.Empty;
                            country = mode2.CountryCode.ToString();
                            service = mode2.ServiceCategory.ToString();

                            if (mode2.SecondMessage is MaxiCodeStandardSecondMessage stdMsg)
                            {
                                message = stdMsg.Message ?? string.Empty;
                            }
                            else if (mode2.SecondMessage is MaxiCodeStructuredSecondMessage structMsg)
                            {
                                // Concatenate identifiers from the structured message.
                                message = string.Join(" | ", structMsg.Identifiers);
                            }
                        }
                        // Extract details for Mode 3 MaxiCode.
                        else if (decoded is MaxiCodeCodetextMode3 mode3)
                        {
                            postal = mode3.PostalCode ?? string.Empty;
                            country = mode3.CountryCode.ToString();
                            service = mode3.ServiceCategory.ToString();

                            if (mode3.SecondMessage is MaxiCodeStandardSecondMessage stdMsg)
                            {
                                message = stdMsg.Message ?? string.Empty;
                            }
                            else if (mode3.SecondMessage is MaxiCodeStructuredSecondMessage structMsg)
                            {
                                message = string.Join(" | ", structMsg.Identifiers);
                            }
                        }
                        // For other MaxiCode modes, fields remain empty.

                        // Write the CSV line, escaping commas where necessary.
                        writer.WriteLine($"{Escape(fileName)},{Escape(rawCodeText)},{Escape(postal)},{Escape(country)},{Escape(service)},{Escape(message)}");
                    }
                }
            }
        }

        Console.WriteLine($"Decoding completed. Report saved to '{outputCsv}'.");
    }

    /// <summary>
    /// Escapes a CSV field by surrounding it with double quotes if it contains a comma,
    /// and doubles any existing double quotes.
    /// </summary>
    /// <param name="s">The field value to escape.</param>
    /// <returns>The escaped field value.</returns>
    private static string Escape(string s) => s.Contains(",") ? $"\"{s.Replace("\"", "\"\"")}\"" : s;
}