using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        string xmlPath = Path.Combine(Path.GetTempPath(), "barcode_settings.xml");
        string originalImagePath = Path.Combine(Path.GetTempPath(), "original.png");
        string deserializedImagePath = Path.Combine(Path.GetTempPath(), "deserialized.png");

        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            generator.CodeText = "Test123";
            generator.Parameters.Barcode.BarColor = Color.Blue;
            generator.Parameters.BackColor = Color.Yellow;
            generator.Parameters.Barcode.BarHeight.Point = 50f;
            generator.Parameters.Barcode.XDimension.Point = 2f;

            generator.Save(originalImagePath);
            generator.ExportToXml(xmlPath);
        }

        BarcodeGenerator deserializedGenerator = BarcodeGenerator.ImportFromXml(xmlPath);
        if (deserializedGenerator == null)
        {
            Console.WriteLine("Failed to import barcode settings from XML.");
            return;
        }

        bool codeTextMatch = deserializedGenerator.CodeText == "Test123";
        bool barColorMatch = deserializedGenerator.Parameters.Barcode.BarColor.ToArgb() == Color.Blue.ToArgb();
        bool backColorMatch = deserializedGenerator.Parameters.BackColor.ToArgb() == Color.Yellow.ToArgb();
        bool barHeightMatch = Math.Abs(deserializedGenerator.Parameters.Barcode.BarHeight.Point - 50f) < 0.001f;
        bool xDimensionMatch = Math.Abs(deserializedGenerator.Parameters.Barcode.XDimension.Point - 2f) < 0.001f;

        Console.WriteLine("Verification Results:");
        Console.WriteLine($"CodeText match: {codeTextMatch}");
        Console.WriteLine($"BarColor match: {barColorMatch}");
        Console.WriteLine($"BackColor match: {backColorMatch}");
        Console.WriteLine($"BarHeight match: {barHeightMatch}");
        Console.WriteLine($"XDimension match: {xDimensionMatch}");

        using (deserializedGenerator)
        {
            deserializedGenerator.Save(deserializedImagePath);
        }

        try { File.Delete(xmlPath); } catch { }
        try { File.Delete(originalImagePath); } catch { }
        try { File.Delete(deserializedImagePath); } catch { }
    }
}