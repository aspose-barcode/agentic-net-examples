using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Multi‑line text to be encoded
        string originalText = "First line\r\nSecond line\r\nThird line";

        // Path for the temporary XML file
        string xmlPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode.xml");

        // Create a barcode generator, set the code text and export to XML
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            generator.CodeText = originalText;
            generator.ExportToXml(xmlPath);
        }

        // Import the barcode generator from the XML file
        using (var importedGenerator = BarcodeGenerator.ImportFromXml(xmlPath))
        {
            string importedText = importedGenerator.CodeText;
            bool lineBreaksPreserved = originalText == importedText;

            Console.WriteLine("Line breaks preserved: " + lineBreaksPreserved);
            Console.WriteLine("Original text:");
            Console.WriteLine(originalText);
            Console.WriteLine("Imported text:");
            Console.WriteLine(importedText);
        }
    }
}