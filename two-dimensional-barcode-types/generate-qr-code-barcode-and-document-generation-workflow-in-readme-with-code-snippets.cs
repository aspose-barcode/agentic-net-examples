using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        string outputDir = Directory.GetCurrentDirectory();
        string qrPath = Path.Combine(outputDir, "qr.png");

        // Generate QR Code barcode
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;
            generator.Save(qrPath, BarCodeImageFormat.Png);
        }

        // Create README with code snippets
        string readmePath = Path.Combine(outputDir, "README.md");
        string readmeContent = @"# QR Code Generation Example

This example demonstrates how to generate a QR Code barcode using Aspose.BarCode and how to read it back.

## Generation

```csharp
using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Example
{
    static void Main()
    {
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, ""https://example.com""))
        {
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;
            generator.Save(""qr.png"", BarCodeImageFormat.Png);
        }
    }
}
```

## Reading

```csharp
using System;
using Aspose.BarCode.BarCodeRecognition;

class Example
{
    static void Main()
    {
        var decodeType = DecodeType.QR;
        using (var reader = new BarCodeReader(""qr.png"", decodeType))
        {
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($""Decoded text: {result.CodeText}"");
            }
        }
    }
}
```";

        File.WriteAllText(readmePath, readmeContent);

        Console.WriteLine($"QR code generated at: {qrPath}");
        Console.WriteLine($"README created at: {readmePath}");
    }
}