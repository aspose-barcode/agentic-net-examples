// Title: Generate Swiss Post Parcel Barcodes from Excel and Log Checksums
// Description: This example reads parcel codes from an Excel spreadsheet, creates Swiss Post Parcel barcodes as PNG images, and logs checksum verification results.
// Category-Description: Demonstrates Aspose.BarCode barcode generation and recognition combined with Aspose.Cells for spreadsheet handling. Shows how to enable checksum generation, save barcodes, read them back for validation, and log results—common tasks for logistics and shipping software developers.
// Prompt: Generate Swiss Post Parcel international barcodes from a spreadsheet and include checksum verification logs.
// Tags: swisspostparcel, barcode generation, barcode recognition, checksum, excel, png, aspose.cells, aspose.barcode

using System;
using System.IO;
using Aspose.Cells;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Reads parcel identifiers from an Excel file, generates Swiss Post Parcel barcodes,
/// validates the checksums by re‑reading the images, and writes a verification log.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs file preparation, barcode generation,
    /// checksum verification, and logging.
    /// </summary>
    static void Main()
    {
        // Define file and folder paths
        string excelPath = "ParcelData.xlsx";
        string outputDir = "Barcodes";
        string logPath = "checksum_log.txt";

        // Ensure the output directory exists
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Create a sample Excel file if it does not already exist
        if (!File.Exists(excelPath))
        {
            CreateSampleExcel(excelPath);
        }

        // Load the workbook and get the first worksheet
        Workbook workbook = new Workbook(excelPath);
        Worksheet sheet = workbook.Worksheets[0];
        Cells cells = sheet.Cells;

        // Clear any previous log content
        File.WriteAllText(logPath, string.Empty);

        // Iterate through data rows (skip the header row)
        int startRow = 1;
        int totalRows = cells.MaxDataRow + 1; // inclusive upper bound
        for (int row = startRow; row < totalRows; row++)
        {
            // Column A (index 0) holds the parcel code text
            string codeText = cells[row, 0]?.StringValue?.Trim();
            if (string.IsNullOrEmpty(codeText))
            {
                continue; // Skip rows without a code
            }

            // Build the output image path for the current barcode
            string imagePath = Path.Combine(outputDir, $"barcode_{row}.png");

            // Generate the barcode image with checksum enabled
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, codeText))
            {
                generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
                generator.Parameters.Barcode.ChecksumAlwaysShow = true; // Show checksum in human‑readable text
                generator.Save(imagePath, BarCodeImageFormat.Png);
            }

            // Verify the checksum by reading the generated barcode image
            using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.SwissPostParcel))
            {
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On; // Enable checksum validation

                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    string logEntry = $"Row {row}: CodeText=\"{result.CodeText}\"";

                    // Attempt to retrieve the checksum value if the symbology provides it
                    try
                    {
                        string checksum = result.Extended?.OneD?.CheckSum;
                        if (!string.IsNullOrEmpty(checksum))
                        {
                            logEntry += $", CheckSum=\"{checksum}\"";
                        }
                    }
                    catch
                    {
                        // Ignore exceptions when checksum information is unavailable
                    }

                    Console.WriteLine(logEntry);
                    File.AppendAllText(logPath, logEntry + Environment.NewLine);
                }
            }
        }

        Console.WriteLine("Barcode generation and checksum verification completed.");
    }

    // Helper method to create a sample Excel file with dummy parcel data
    private static void CreateSampleExcel(string path)
    {
        using (Workbook wb = new Workbook())
        {
            Worksheet ws = wb.Worksheets[0];
            Cells cells = ws.Cells;

            // Header row
            cells[0, 0].PutValue("SwissPostParcelCode");

            // Sample parcel codes (must be valid for Swiss Post Parcel)
            cells[1, 0].PutValue("123456789012");
            cells[2, 0].PutValue("987654321098");
            cells[3, 0].PutValue("555555555555");

            wb.Save(path);
        }
    }
}