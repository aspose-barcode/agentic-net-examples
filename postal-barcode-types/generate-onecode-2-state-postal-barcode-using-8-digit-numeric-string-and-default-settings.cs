// Title: Generate OneCode 2‑state postal barcode with 8‑digit numeric string
// Description: Demonstrates creating a OneCode 2‑state postal barcode using Aspose.BarCode with default settings.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on postal symbologies. It showcases the use of EncodeTypes, BaseEncodeType, and BarcodeGenerator classes to produce OneCode barcodes, a common requirement for mailing applications where developers need to generate compliant postal barcodes quickly.
// Prompt: Generate a OneCode 2‑state postal barcode using an 8‑digit numeric string and default settings.
// Tags: onecode, postal barcode, generation, png, aspose.barcode, csharp

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a OneCode 2‑state postal barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // 8‑digit numeric string for the OneCode 2‑state postal barcode
        string codeText = "12345678";

        // OneCode requires the codetext length to be exactly 20, 25, 29, or 31 digits.
        int length = codeText.Length;
        if (length != 20 && length != 25 && length != 29 && length != 31)
        {
            Console.WriteLine($"Invalid OneCode codetext length: {length}. Allowed lengths are 20, 25, 29, or 31 digits.");
            return;
        }

        // Resolve the OneCode symbology using reflection (EncodeTypes.OneCode)
        const string symbologyName = "OneCode";
        var field = typeof(EncodeTypes).GetField(symbologyName);
        if (field == null)
        {
            Console.WriteLine($"Symbology '{symbologyName}' not found in EncodeTypes.");
            return;
        }

        // Cast the reflected value to BaseEncodeType
        BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

        // Generate the barcode with default settings
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Save the barcode image (default format is PNG)
            string outputPath = "onecode.png";
            generator.Save(outputPath);
            Console.WriteLine($"OneCode barcode saved to '{outputPath}'.");
        }
    }
}