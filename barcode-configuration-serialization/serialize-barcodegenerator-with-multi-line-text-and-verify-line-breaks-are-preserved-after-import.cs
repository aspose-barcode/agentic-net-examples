using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates exporting a barcode generator to XML and importing it back,
/// preserving multi‑line text with Windows line breaks.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define multi‑line text containing Windows style line breaks (\r\n)
        string multiLineText = "Line1\r\nLine2\r\nLine3";

        // Build a temporary file path for the XML serialization output
        string xmlPath = Path.Combine(Path.GetTempPath(), "barcode.xml");

        // Create a barcode generator for Code128, assign the multi‑line text,
        // and export its configuration to an XML file.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            generator.CodeText = multiLineText;
            generator.ExportToXml(xmlPath);
        }

        // Verify that the XML file was successfully created.
        if (!File.Exists(xmlPath))
        {
            Console.WriteLine("Failed to create XML file.");
            return;
        }

        // Import a new barcode generator instance from the previously saved XML file.
        using (var importedGenerator = BarcodeGenerator.ImportFromXml(xmlPath))
        {
            // Check whether the imported CodeText matches the original multi‑line text.
            bool lineBreaksPreserved = importedGenerator.CodeText == multiLineText;
            Console.WriteLine("Line breaks preserved: " + lineBreaksPreserved);
        }

        // Attempt to delete the temporary XML file; ignore any exceptions that may occur.
        try
        {
            File.Delete(xmlPath);
        }
        catch
        {
            // Cleanup errors are intentionally ignored.
        }
    }
}