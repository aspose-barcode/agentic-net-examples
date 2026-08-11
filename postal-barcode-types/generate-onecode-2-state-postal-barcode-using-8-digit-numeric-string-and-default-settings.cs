// Title: Generate OneCode 2‑state Postal Barcode with Default Settings
// Description: Demonstrates creating a OneCode 2‑state postal barcode from an 8‑digit numeric string using Aspose.BarCode and saving it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on postal symbologies. It showcases the use of the BarcodeGenerator class with EncodeTypes.OneCode to produce OneCode barcodes, a common requirement for postal automation. Developers often need to validate input length, ensure numeric data, and save the generated barcode in image formats for integration into mailing systems.
// Prompt: Generate a OneCode 2‑state postal barcode using an 8‑digit numeric string and default settings.
// Tags: onecode,postal,barcode,generation,aspnet,aspose.barcode,csharp,image

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a OneCode 2‑state postal barcode and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates the barcode and writes status messages to the console.
    /// </summary>
    static void Main()
    {
        // Define a sample 8‑digit numeric string to encode.
        string codeText = "12345678";

        // OneCode requires the codetext length to be exactly 20, 25, 29, or 31 digits.
        // Validate the length (and numeric content) before attempting generation.
        if (!IsValidOneCodeLength(codeText))
        {
            Console.WriteLine("Error: OneCode barcode requires a numeric codetext of length 20, 25, 29, or 31 digits.");
            Console.WriteLine($"Provided codetext length: {codeText.Length}");
            return;
        }

        // Create a BarcodeGenerator with the OneCode symbology and the provided text.
        using (var generator = new BarcodeGenerator(EncodeTypes.OneCode, codeText))
        {
            // Save the generated barcode image as a PNG file.
            generator.Save("onecode.png");
        }

        Console.WriteLine("OneCode barcode generated successfully: onecode.png");
    }

    // Helper method to verify that the input text meets OneCode length and numeric requirements.
    private static bool IsValidOneCodeLength(string text)
    {
        if (string.IsNullOrEmpty(text))
            return false;

        // Ensure every character is a digit.
        foreach (char c in text)
        {
            if (!char.IsDigit(c))
                return false;
        }

        // Allowed lengths for OneCode barcodes.
        int length = text.Length;
        return length == 20 || length == 25 || length == 29 || length == 31;
    }
}