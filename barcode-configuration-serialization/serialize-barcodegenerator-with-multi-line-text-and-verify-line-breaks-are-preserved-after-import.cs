using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Multi‑line text to encode
        string originalText = "Line1\nLine2\nLine3";

        // Create a barcode generator, set the text, save image and export settings to XML
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            generator.CodeText = originalText;
            generator.Save("barcode.png");
            generator.ExportToXml("barcode.xml");
        }

        // Import the barcode generator from the previously saved XML
        BarcodeGenerator importedGenerator = BarcodeGenerator.ImportFromXml("barcode.xml");

        // Verify that line breaks are preserved
        bool lineBreaksPreserved = originalText == importedGenerator.CodeText;

        Console.WriteLine("Original CodeText:");
        Console.WriteLine(originalText);
        Console.WriteLine();
        Console.WriteLine("Imported CodeText:");
        Console.WriteLine(importedGenerator.CodeText);
        Console.WriteLine();
        Console.WriteLine($"Line breaks preserved after import: {lineBreaksPreserved}");
    }
}