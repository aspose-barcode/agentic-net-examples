using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Prepare file paths
        string xmlPath = Path.Combine(Path.GetTempPath(), "barcode_settings.xml");
        string imgPath = Path.Combine(Path.GetTempPath(), "barcode.png");

        // Create original barcode generator and set visual properties
        using (var original = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Image size
            original.Parameters.ImageWidth.Point = 300f;
            original.Parameters.ImageHeight.Point = 150f;

            // Colors
            original.Parameters.Barcode.BarColor = Color.Blue;
            original.Parameters.BackColor = Color.Yellow;

            // Bar dimensions
            original.Parameters.Barcode.BarHeight.Point = 50f;
            original.Parameters.Barcode.XDimension.Point = 2f;

            // Padding
            original.Parameters.Barcode.Padding.Left.Point = 5f;
            original.Parameters.Barcode.Padding.Top.Point = 5f;
            original.Parameters.Barcode.Padding.Right.Point = 5f;
            original.Parameters.Barcode.Padding.Bottom.Point = 5f;

            // Caption above
            original.Parameters.CaptionAbove.Text = "Above Caption";
            original.Parameters.CaptionAbove.Font.FamilyName = "Arial";
            original.Parameters.CaptionAbove.Font.Size.Point = 12f;
            original.Parameters.CaptionAbove.Alignment = TextAlignment.Center;

            // Human‑readable text (CodeTextParameters)
            original.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Times New Roman";
            original.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 10f;
            original.Parameters.Barcode.CodeTextParameters.Color = Color.Green;
            original.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Right;
            original.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Above;

            // Save image (optional, just to have a file)
            original.Save(imgPath);

            // Export settings to XML
            bool exported = original.ExportToXml(xmlPath);
            Console.WriteLine($"Exported to XML: {exported}");
        }

        // Verify XML file exists
        if (!File.Exists(xmlPath))
        {
            Console.WriteLine("XML file was not created.");
            return;
        }

        // Import settings from XML into a new generator
        using (var imported = BarcodeGenerator.ImportFromXml(xmlPath))
        {
            // Compare properties
            Compare("ImageWidth", imported.Parameters.ImageWidth.Point, 300f);
            Compare("ImageHeight", imported.Parameters.ImageHeight.Point, 150f);
            CompareColor("BarColor", imported.Parameters.Barcode.BarColor, Color.Blue);
            CompareColor("BackColor", imported.Parameters.BackColor, Color.Yellow);
            Compare("BarHeight", imported.Parameters.Barcode.BarHeight.Point, 50f);
            Compare("XDimension", imported.Parameters.Barcode.XDimension.Point, 2f);
            Compare("PaddingLeft", imported.Parameters.Barcode.Padding.Left.Point, 5f);
            Compare("PaddingTop", imported.Parameters.Barcode.Padding.Top.Point, 5f);
            Compare("PaddingRight", imported.Parameters.Barcode.Padding.Right.Point, 5f);
            Compare("PaddingBottom", imported.Parameters.Barcode.Padding.Bottom.Point, 5f);
            CompareString("CaptionAbove.Text", imported.Parameters.CaptionAbove.Text, "Above Caption");
            CompareString("CaptionAbove.FontFamily", imported.Parameters.CaptionAbove.Font.FamilyName, "Arial");
            Compare("CaptionAbove.FontSize", imported.Parameters.CaptionAbove.Font.Size.Point, 12f);
            CompareEnum("CaptionAbove.Alignment", imported.Parameters.CaptionAbove.Alignment, TextAlignment.Center);
            CompareString("CodeTextParameters.FontFamily", imported.Parameters.Barcode.CodeTextParameters.Font.FamilyName, "Times New Roman");
            Compare("CodeTextParameters.FontSize", imported.Parameters.Barcode.CodeTextParameters.Font.Size.Point, 10f);
            CompareColor("CodeTextParameters.Color", imported.Parameters.Barcode.CodeTextParameters.Color, Color.Green);
            CompareEnum("CodeTextParameters.Alignment", imported.Parameters.Barcode.CodeTextParameters.Alignment, TextAlignment.Right);
            CompareEnum("CodeTextParameters.Location", imported.Parameters.Barcode.CodeTextParameters.Location, CodeLocation.Above);
        }

        // Clean up temporary files
        try { File.Delete(xmlPath); } catch { }
        try { File.Delete(imgPath); } catch { }
    }

    static void Compare(string name, float actual, float expected)
    {
        const float tolerance = 0.001f;
        bool match = Math.Abs(actual - expected) <= tolerance;
        Console.WriteLine($"{name}: {(match ? "Match" : $"Mismatch (actual={actual}, expected={expected})")}");
    }

    static void CompareString(string name, string actual, string expected)
    {
        bool match = string.Equals(actual, expected, StringComparison.Ordinal);
        Console.WriteLine($"{name}: {(match ? "Match" : $"Mismatch (actual=\"{actual}\", expected=\"{expected}\")")}");
    }

    static void CompareEnum<T>(string name, T actual, T expected) where T : Enum
    {
        bool match = actual.Equals(expected);
        Console.WriteLine($"{name}: {(match ? "Match" : $"Mismatch (actual={actual}, expected={expected})")}");
    }

    static void CompareColor(string name, Color actual, Color expected)
    {
        bool match = actual.ToArgb() == expected.ToArgb();
        Console.WriteLine($"{name}: {(match ? "Match" : $"Mismatch (actual={actual}, expected={expected})")}");
    }
}