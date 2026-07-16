// Title: Generate QR Code with GS1 fields using Aspose.BarCode
// Description: Demonstrates creating a QR Code that encodes multiple GS1 Application Identifier fields separated by the group separator character.
// Category-Description: This example belongs to the Aspose.BarCode QR Code generation category, illustrating how to use the BarcodeGenerator, EncodeTypes, and QR-specific parameters to produce GS1‑compliant QR codes. Typical use cases include encoding product identifiers, batch numbers, and expiration dates for supply‑chain applications. Developers often need to set the QR encode mode to Extended and include the ASCII 29 group separator to separate AI fields.
// Prompt: Generate QR Code barcode and encode multiple GS1 fields separated by group separator.
// Tags: qr, gs1, barcode, generation, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a QR Code containing multiple GS1 fields.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates a QR Code with GS1 Application Identifiers
    /// separated by the group separator (ASCII 29) and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the current working directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "qr_gs1.png");

        // GS1 QR code data: multiple AI fields separated by the GS (group separator) character (ASCII 29).
        // Example: (01)GTIN, (10)Batch/Lot, (17)Expiration Date.
        string gs1Data = "(01)12345678901231\u001D(10)ABC123\u001D(17)210101";

        // Initialize the QR code generator with the QR encode type.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Assign the GS1 data to the barcode.
            generator.CodeText = gs1Data;

            // Enable Extended mode to activate GS1 (FNC1) handling for QR codes.
            generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.Extended;

            // Optionally set the error correction level (Level M provides a good balance).
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Save the generated QR Code image to the specified path.
            generator.Save(outputPath);
        }

        // Inform the user where the QR Code image has been saved.
        Console.WriteLine($"QR Code with GS1 fields saved to: {outputPath}");
    }
}