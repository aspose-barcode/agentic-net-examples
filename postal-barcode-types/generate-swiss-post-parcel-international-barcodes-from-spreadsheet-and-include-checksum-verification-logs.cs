// Title: Generate Swiss Post Parcel Barcodes from Excel and Verify Checksums
// Description: This example reads parcel identifiers from an Excel file, creates Swiss Post Parcel barcodes, decodes them to verify the checksum, and logs the results.
// Category-Description: Demonstrates batch barcode generation and verification using Aspose.BarCode and Aspose.Cells. The example showcases the BarcodeGenerator for SwissPostParcel symbology, BarCodeReader for checksum validation, and Workbook handling for spreadsheet data. Ideal for developers automating barcode creation from data sources such as spreadsheets, needing reliable checksum checks and logging.
// Prompt: Generate Swiss Post Parcel international barcodes from a spreadsheet and include checksum verification logs.
// Tags: barcode generation, barcode recognition, swisspostparcel, excel, aspose.barcode, aspose.cells, checksum verification, png, console

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Cells;

/// <summary>
/// Reads parcel codes from an Excel file, generates Swiss Post Parcel barcodes,
/// verifies them by decoding, and writes a verification log.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the full barcode generation and verification workflow.
    /// </summary>
    static void Main()
    {
        // ----------------------------------------------------------------------
        // Define file system paths for the temporary Excel source, output directory,
        // and verification log file.
        // ----------------------------------------------------------------------
        string excelPath = Path.Combine(Path.GetTempPath(), "SwissPostCodes.xlsx");
        string outputDir = Path.Combine(Path.GetTempPath(), "SwissPostBarcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);
        string logPath = Path.Combine(outputDir, "log.txt");

        // ----------------------------------------------------------------------
        // Create a sample Excel workbook if it does not already exist.
        // The workbook contains three rows with different checksum scenarios.
        // ----------------------------------------------------------------------
        if (!File.Exists(excelPath))
        {
            using (var wb = new Workbook())
            {
                var ws = wb.Worksheets[0];
                ws.Cells[0, 0].PutValue("RM999605013CH"); // correct checksum
                ws.Cells[1, 0].PutValue("RM999605017CH"); // wrong checksum
                ws.Cells[2, 0].PutValue("RM99960501CH");  // missing checksum
                wb.Save(excelPath);
            }
        }

        // ----------------------------------------------------------------------
        // Load the Excel file and iterate over each populated row.
        // ----------------------------------------------------------------------
        using (var workbook = new Workbook(excelPath))
        {
            var worksheet = workbook.Worksheets[0];
            var cells = worksheet.Cells;
            int maxRow = cells.MaxDataRow;

            for (int row = 0; row <= maxRow; row++)
            {
                // Retrieve and trim the parcel code from the first column.
                string originalCode = cells[row, 0].StringValue?.Trim();
                if (string.IsNullOrEmpty(originalCode))
                    continue; // Skip empty rows.

                // --------------------------------------------------------------
                // Generate a Swiss Post Parcel barcode image for the current code.
                // --------------------------------------------------------------
                string imagePath = Path.Combine(outputDir, $"barcode_{row + 1}.png");
                using (var generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, originalCode))
                {
                    generator.Parameters.Barcode.XDimension.Pixels = 2f;
                    generator.Parameters.Barcode.BarHeight.Pixels = 40f;
                    generator.Save(imagePath, BarCodeImageFormat.Png);
                }

                // --------------------------------------------------------------
                // Decode the generated barcode to verify the checksum.
                // --------------------------------------------------------------
                string decodedCode = "";
                using (var reader = new BarCodeReader(imagePath, DecodeType.SwissPostParcel))
                {
                    foreach (var result in reader.ReadBarCodes())
                    {
                        decodedCode = result.CodeText;
                        break; // Only one barcode expected per image.
                    }
                }

                // --------------------------------------------------------------
                // Log the verification result: OK if codes match, otherwise Corrected.
                // --------------------------------------------------------------
                string checksumStatus = decodedCode.Equals(originalCode, StringComparison.Ordinal) ? "OK" : "Corrected";
                string logEntry = $"Row {row + 1}: Original=\"{originalCode}\", Decoded=\"{decodedCode}\", Status={checksumStatus}{Environment.NewLine}";
                File.AppendAllText(logPath, logEntry);
                Console.WriteLine(logEntry.TrimEnd());
            }
        }

        // ----------------------------------------------------------------------
        // Output final locations of generated barcodes and the log file.
        // ----------------------------------------------------------------------
        Console.WriteLine($"Barcodes saved to: {outputDir}");
        Console.WriteLine($"Log file: {logPath}");
    }
}