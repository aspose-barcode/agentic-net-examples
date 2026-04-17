using System;
using System.IO;
using Aspose.Cells;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        string inputFile = Path.Combine(Environment.CurrentDirectory, "SampleData.xlsx");
        string outputFolder = Path.Combine(Environment.CurrentDirectory, "Barcodes");

        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        if (!File.Exists(inputFile))
        {
            CreateSampleExcel(inputFile);
        }

        using (Workbook workbook = new Workbook(inputFile))
        {
            var worksheet = workbook.Worksheets[0];
            var cells = worksheet.Cells;

            int startRow = 1;
            int totalRows = cells.MaxDataRow + 1;

            for (int row = startRow; row < totalRows; row++)
            {
                var cell = cells[row, 0];
                if (cell == null || cell.Value == null)
                {
                    continue;
                }

                string codeText = cell.StringValue.Trim();
                if (string.IsNullOrEmpty(codeText))
                {
                    continue;
                }

                using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.HanXin, codeText))
                {
                    generator.Parameters.Barcode.HanXin.EncodeMode = HanXinEncodeMode.Auto;

                    generator.Parameters.ImageWidth.Point = 300f;
                    generator.Parameters.ImageHeight.Point = 150f;

                    string outputPath = Path.Combine(outputFolder, $"Barcode_Row{row}.png");
                    generator.Save(outputPath);
                }
            }
        }
    }

    private static void CreateSampleExcel(string filePath)
    {
        Workbook workbook = new Workbook();
        var sheet = workbook.Worksheets[0];
        var cells = sheet.Cells;

        cells[0, 0].PutValue("CodeText");
        cells[1, 0].PutValue("Sample001");
        cells[2, 0].PutValue("测试汉信");
        cells[3, 0].PutValue("https://example.com");
        cells[4, 0].PutValue("1234567890");

        workbook.Save(filePath);
    }
}