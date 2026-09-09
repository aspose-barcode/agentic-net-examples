// Title: Generate Swiss Post Parcel barcodes from an Excel file with checksum verification
// Description: Demonstrates how to read identifiers from a spreadsheet, generate Swiss Post Parcel barcodes, save them as PNG images, and verify/correct checksums using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation and validation category. It shows how to use BarcodeGenerator, BarCodeReader, and related parameter classes to create SwissPostParcel symbology, integrate with Aspose.Cells for Excel handling, and log checksum corrections—common tasks for logistics and shipping software developers.
// Prompt: Generate Swiss Post Parcel international barcodes from a spreadsheet and include checksum verification logs.
// Tags: swisspostparcel, barcode generation, checksum verification, excel, aspose.barcode, aspose.cells, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Cells;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating Swiss Post Parcel barcodes from an Excel spreadsheet,
/// saving them as PNG files, and logging checksum corrections.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates temporary data, writes sample identifiers to an Excel file,
    /// processes each row to generate a barcode image, reads it back to verify the checksum,
    /// and writes a log line to the console.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for the demo files
        string tempRoot = Path.Combine(Path.GetTempPath(), "SwissPostDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempRoot);

        // Define paths for the Excel workbook and the barcode output folder
        string excelPath = Path.Combine(tempRoot, "Identifiers.xlsx");
        string outputFolder = Path.Combine(tempRoot, "Barcodes");
        Directory.CreateDirectory(outputFolder);

        // Sample identifiers: one with correct checksum, one with wrong checksum, and one missing checksum
        string[] sampleIdentifiers = new string[]
        {
            "RM999605013CH", // correct checksum
            "RM999605017CH", // wrong checksum (will be corrected)
            "RM99960501CH"   // missing checksum (will be added)
        };

        // Create an Excel workbook and write the sample identifiers into the first column
        using (Workbook workbook = new Workbook())
        {
            var sheet = workbook.Worksheets[0];
            sheet.Cells[0, 0].PutValue("Identifier");
            for (int i = 0; i < sampleIdentifiers.Length; i++)
            {
                sheet.Cells[i + 1, 0].PutValue(sampleIdentifiers[i]);
            }
            workbook.Save(excelPath);
        }

        // Open the workbook for reading and process each identifier row
        using (Workbook workbook = new Workbook(excelPath))
        {
            var sheet = workbook.Worksheets[0];
            var cells = sheet.Cells;
            int maxRow = cells.MaxDataRow; // last row with data

            for (int row = 1; row <= maxRow; row++)
            {
                // Retrieve and trim the identifier from the current row
                string originalCode = cells[row, 0].StringValue?.Trim();
                if (string.IsNullOrEmpty(originalCode))
                    continue; // skip empty rows

                // Define the output image file name for this barcode
                string imageFile = Path.Combine(outputFolder, $"Barcode_{row}.png");

                // Generate the barcode, save it, and read it back to obtain the possibly corrected code
                string detectedCode = GenerateAndSaveBarcode(originalCode, imageFile);

                // Determine whether the checksum was corrected during generation/reading
                bool checksumChanged = !originalCode.Equals(detectedCode, StringComparison.Ordinal);

                // Log the result to the console
                Console.WriteLine($"Row {row}: Original='{originalCode}' Detected='{detectedCode}' ChecksumCorrected={checksumChanged}");
            }
        }

        // Cleanup (optional): uncomment the line below to delete temporary files after execution
        // Directory.Delete(tempRoot, true);
    }

    /// <summary>
    /// Generates a Swiss Post Parcel barcode image from the supplied text, saves it as PNG,
    /// then reads the barcode back to obtain the encoded text (which may include a corrected checksum).
    /// </summary>
    /// <param name="codeText">The identifier to encode.</param>
    /// <param name="imagePath">The full file path where the PNG image will be saved.</param>
    /// <returns>The decoded barcode text, reflecting any checksum adjustments.</returns>
    static string GenerateAndSaveBarcode(string codeText, string imagePath)
    {
        // Initialize the barcode generator with Swiss Post Parcel symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, codeText))
        {
            // Configure visual parameters
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            generator.Parameters.Barcode.BarHeight.Pixels = 40f;

            // Generate the barcode image
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Save the image as PNG
                bitmap.Save(imagePath, ImageFormat.Png);

                // Read the barcode back to verify and possibly correct the checksum
                using (var reader = new BarCodeReader(bitmap, DecodeType.SwissPostParcel))
                {
                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        return result.CodeText; // return the decoded (and corrected) text
                    }
                }
            }
        }

        // If reading fails (should not happen), return the original text
        return codeText;
    }
}