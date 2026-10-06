// Title: QR Code Generation and Embedding into Excel with README Documentation
// Description: Demonstrates generating a QR Code barcode using Aspose.BarCode, saving it as PNG, embedding it into an Excel workbook via Aspose.Cells, and creating a README with code snippets.
// Category-Description: This example belongs to the Aspose.BarCode and Aspose.Cells integration category, showcasing how to create QR Code barcodes, manipulate image streams, and embed graphics into spreadsheet documents. It highlights key API classes such as BarcodeGenerator, BarcodeParameters, Workbook, and Picture, which developers commonly use for automated document generation and reporting workflows.
// Prompt: Generate QR Code barcode and document generation workflow in README with code snippets.
// Tags: qr code, barcode generation, image output, excel embedding, aspose.barcode, aspose.cells, readme creation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Cells;
using Aspose.Cells.Drawing;
using Aspose.Drawing;

/// <summary>
/// Demonstrates QR Code generation, embedding into Excel, and README creation.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates QR code image, embeds it into an Excel file, and writes a README with code snippets.
    /// </summary>
    static void Main()
    {
        // Prepare output directory
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir);

        // Define file paths for generated artifacts
        string qrImagePath = Path.Combine(outputDir, "qr_code.png");
        string excelPath = Path.Combine(outputDir, "QrInExcel.xlsx");
        string readmePath = Path.Combine(outputDir, "README.md");

        // Generate QR Code barcode
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Configure QR-specific parameters
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;
            generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.Auto;
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Save QR code as a PNG image file
            generator.Save(qrImagePath, BarCodeImageFormat.Png);

            // Embed QR code image into an Excel workbook
            using (var qrStream = new MemoryStream())
            {
                // Write QR code to memory stream
                generator.Save(qrStream, BarCodeImageFormat.Png);
                qrStream.Position = 0;

                // Create a new workbook and access the first worksheet
                var workbook = new Workbook();
                var sheet = workbook.Worksheets[0];

                // Add the QR code picture to the worksheet
                int pictureIndex = sheet.Pictures.Add(0, 0, qrStream);
                var picture = sheet.Pictures[pictureIndex];
                picture.Placement = PlacementType.FreeFloating;

                // Save the workbook as an XLSX file
                workbook.Save(excelPath, SaveFormat.Xlsx);
            }
        }

        // Create README file with embedded code snippets
        string readmeContent = @"# QR Code Generation and Document Workflow

This example demonstrates how to generate a QR Code barcode using **Aspose.BarCode**, save it as an image, and embed it into an Excel workbook using **Aspose.Cells**.

## QR Code Generation (C#)

```csharp
using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class QrGenerator
{
    static void Main()
    {
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, ""https://example.com""))
        {
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;
            generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.Auto;
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            generator.Save(""qr_code.png"", BarCodeImageFormat.Png);
        }
    }
}
```

## Embedding QR Code into Excel (C#)

```csharp
using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Cells;
using Aspose.Cells.Drawing;
using Aspose.Drawing;

class ExcelEmbedding
{
    static void Main()
    {
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, ""https://example.com""))
        {
            using (var qrStream = new MemoryStream())
            {
                generator.Save(qrStream, BarCodeImageFormat.Png);
                qrStream.Position = 0;

                var workbook = new Workbook();
                var sheet = workbook.Worksheets[0];

                int pictureIdx = sheet.Pictures.Add(0, 0, qrStream);
                var picture = sheet.Pictures[pictureIdx];
                picture.Placement = PlacementType.FreeFloating;

                workbook.Save(""QrInExcel.xlsx"", SaveFormat.Xlsx);
            }
        }
    }
}
```

The generated files (`qr_code.png`, `QrInExcel.xlsx`) are placed in the **Output** folder alongside this README.

";

        // Write README content to file
        File.WriteAllText(readmePath, readmeContent);

        // Inform the user about the output location
        Console.WriteLine("QR code image, Excel workbook, and README have been created in:");
        Console.WriteLine(outputDir);
    }
}