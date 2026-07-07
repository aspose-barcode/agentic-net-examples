// Title: Configure Code 16K barcode quiet zones and generate PDF
// Description: Demonstrates setting custom quiet zone coefficients for a Code 16K barcode and saving it as a PDF, useful for matching printer margin requirements.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on symbology-specific parameter configuration. It showcases the BarcodeGenerator class, EncodeTypes enumeration, and barcode parameter objects to adjust quiet zones and module size. Developers often need to tailor barcode dimensions for printing workflows, ensuring proper margins and readability.
// Prompt: Configure Code 16K quiet zones to match printer margin requirements, generate PDF output.
// Tags: code16k, quiet zones, pdf output, barcode generation, aspose.barcode, symbology

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates a Code 16K barcode with custom quiet zones and saves it as a PDF.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Configures quiet zones, module size, and creates the PDF file.
    /// </summary>
    static void Main()
    {
        // Sample Code16K barcode text (must meet Code16K requirements)
        const string codeText = "12345678901234567890";

        // Initialize the barcode generator for Code16K symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.Code16K, codeText))
        {
            // Configure quiet zones (coefficients are multiples of XDimension)
            // Adjust these values to match the printer's required margins.
            generator.Parameters.Barcode.Code16K.QuietZoneLeftCoef = 20;   // e.g., 20 * XDimension
            generator.Parameters.Barcode.Code16K.QuietZoneRightCoef = 5;   // e.g., 5 * XDimension

            // Optional: define module size (XDimension) for better control of overall size
            generator.Parameters.Barcode.XDimension.Point = 2f; // 2 points per module

            // Save the barcode directly as a PDF file
            generator.Save("code16k.pdf");
        }

        Console.WriteLine("Code16K barcode with custom quiet zones saved to code16k.pdf");
    }
}