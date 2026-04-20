using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Cells;

class Program
{
    static void Main()
    {
        const string excelPath = "ParcelData.xlsx";

        // Ensure the spreadsheet exists; if not, create a small sample.
        if (!File.Exists(excelPath))
        {
            using (var wb = new Workbook())
            {
                var sheet = wb.Worksheets[0];
                sheet.Cells[0, 0].PutValue("ParcelCode");
                sheet.Cells[1, 0].PutValue("12345678901234567890"); // sample 20‑digit code
                sheet.Cells[2, 0].PutValue("98765432109876543210"); // sample 20‑digit code
                sheet.Cells[3, 0].PutValue("11111111111111111111"); // sample 20‑digit code
                sheet.Cells[4, 0].PutValue("22222222222222222222"); // sample 20‑digit code
                wb.Save(excelPath);
            }
        }

        // Load the spreadsheet.
        using (var workbook = new Workbook(excelPath))
        {
            var sheet = workbook.Worksheets[0];
            int lastRow = sheet.Cells.MaxDataRow;

            for (int row = 1; row <= lastRow; row++) // start at 1 to skip header
            {
                string codeText = sheet.Cells[row, 0].StringValue?.Trim();
                if (string.IsNullOrEmpty(codeText))
                {
                    Console.WriteLine($"Row {row}: Empty code text, skipping.");
                    continue;
                }

                // Generate Swiss Post Parcel barcode.
                using (var generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, codeText))
                {
                    // Enable checksum generation explicitly.
                    generator.Parameters.Barcode.IsChecksumEnabled = Aspose.BarCode.Generation.EnableChecksum.Yes;

                    // Set image size (optional, using points).
                    generator.Parameters.ImageWidth.Point = 300f;
                    generator.Parameters.ImageHeight.Point = 150f;

                    string imageFile = $"SwissPost_{row}.png";
                    generator.Save(imageFile);

                    // Recognize and verify checksum.
                    using (var reader = new BarCodeReader(imageFile, DecodeType.SwissPostParcel))
                    {
                        // Force checksum validation.
                        reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

                        foreach (BarCodeResult result in reader.ReadBarCodes())
                        {
                            Console.WriteLine($"Row {row}:");
                            Console.WriteLine($"  CodeText : {result.CodeText}");
                            Console.WriteLine($"  Value    : {result.Extended.OneD.Value}");
                            Console.WriteLine($"  Checksum : {result.Extended.OneD.CheckSum}");
                        }
                    }
                }
            }
        }
    }
}