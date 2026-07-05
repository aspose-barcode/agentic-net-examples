// Title: Barcode XML Import Integrity Diagnostic
// Description: Demonstrates generating a barcode, exporting its settings to XML, re‑importing them, and comparing the original and imported results to verify data integrity.
// Prompt: Develop a diagnostic tool that compares original results with those obtained after XML import to ensure data integrity.
// Tags: barcode, xml, import, export, integrity, diagnostics, aspose.barcode, code128, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Entry point for the barcode XML import integrity diagnostic example.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a barcode, exports its configuration to XML, re‑imports it, and compares the original and imported results.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Define file names for the original image, imported image, and XML file.
        // --------------------------------------------------------------------
        const string originalImagePath = "original.png";
        const string importedImagePath = "imported.png";
        const string xmlPath = "barcode.xml";

        // --------------------------------------------------------------------
        // Sample barcode data: code text and symbology type.
        // --------------------------------------------------------------------
        const string codeText = "1234567890";
        BaseEncodeType encodeType = EncodeTypes.Code128;

        // --------------------------------------------------------------------
        // Create the original barcode generator, apply custom visual settings,
        // save the image, and export the generator configuration to XML.
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Example customizations
            generator.Parameters.Barcode.BarColor = Color.Blue;
            generator.Parameters.Barcode.XDimension.Point = 2f;
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 150f;

            // Save the generated barcode image.
            generator.Save(originalImagePath, BarCodeImageFormat.Png);

            // Export the generator's settings to an XML file.
            generator.ExportToXml(xmlPath);
        }

        // --------------------------------------------------------------------
        // Verify that the XML file was created before attempting import.
        // --------------------------------------------------------------------
        if (!File.Exists(xmlPath))
        {
            Console.WriteLine("XML file was not created. Exiting.");
            return;
        }

        // --------------------------------------------------------------------
        // Import a new barcode generator from the previously saved XML.
        // --------------------------------------------------------------------
        BarcodeGenerator importedGenerator = BarcodeGenerator.ImportFromXml(xmlPath);
        if (importedGenerator == null)
        {
            Console.WriteLine("Failed to import generator from XML. Exiting.");
            return;
        }

        // --------------------------------------------------------------------
        // Save an image using the imported generator settings.
        // --------------------------------------------------------------------
        importedGenerator.Save(importedImagePath, BarCodeImageFormat.Png);
        importedGenerator.Dispose();

        // --------------------------------------------------------------------
        // Compare the original and imported images byte‑by‑byte.
        // --------------------------------------------------------------------
        bool imagesEqual = CompareFiles(originalImagePath, importedImagePath);
        Console.WriteLine($"Image comparison result: {(imagesEqual ? "Identical" : "Different")}");

        // --------------------------------------------------------------------
        // Compare core properties (symbology type and code text) of the original
        // and imported generators to ensure configuration integrity.
        // --------------------------------------------------------------------
        using (var originalGen = new BarcodeGenerator(encodeType, codeText))
        using (var importedGen = BarcodeGenerator.ImportFromXml(xmlPath))
        {
            bool typeEqual = originalGen.BarcodeType.TypeName == importedGen.BarcodeType.TypeName;
            bool textEqual = originalGen.CodeText == importedGen.CodeText;
            Console.WriteLine($"Symbology comparison: {(typeEqual ? "Match" : "Mismatch")}");
            Console.WriteLine($"CodeText comparison: {(textEqual ? "Match" : "Mismatch")}");
        }

        // --------------------------------------------------------------------
        // Clean up temporary files (optional). Uncomment to delete files.
        // --------------------------------------------------------------------
        // File.Delete(originalImagePath);
        // File.Delete(importedImagePath);
        // File.Delete(xmlPath);
    }

    // ------------------------------------------------------------------------
    // Helper method to compare two files byte by byte.
    // Returns true if files are identical; otherwise false.
    // ------------------------------------------------------------------------
    static bool CompareFiles(string path1, string path2)
    {
        if (!File.Exists(path1) || !File.Exists(path2))
            return false;

        byte[] file1 = File.ReadAllBytes(path1);
        byte[] file2 = File.ReadAllBytes(path2);

        if (file1.Length != file2.Length)
            return false;

        for (int i = 0; i < file1.Length; i++)
        {
            if (file1[i] != file2[i])
                return false;
        }
        return true;
    }
}