// Title: Generate GS1 QR Code with multiple fields
// Description: Demonstrates creating a QR Code barcode that encodes multiple GS1 Application Identifiers separated by the group separator.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes.GS1QR to produce GS1-compliant QR codes. Typical use cases include encoding product identifiers, serial numbers, and quantities for supply‑chain applications. Developers often need to set barcode dimensions, error correction levels, and save the image in a desired format.
/// Prompt: Generate QR Code barcode and encode multiple GS1 fields separated by group separator.
/// Tags: qr code, gs1, barcode generation, aspose.barcode, encode types, png output

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a GS1 QR Code barcode with multiple Application Identifier fields.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates a temporary directory, generates the barcode, and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define output directory inside the system temporary folder
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        // Ensure the directory exists
        Directory.CreateDirectory(outputDir);
        // Combine directory and file name to get the full output path
        string outputPath = Path.Combine(outputDir, "gs1qr.png");

        // GS1 fields: (01) GTIN (14 digits), (21) Serial, (30) Quantity
        string codeText = "(01)00123456789012(21)ABC123(30)9876";

        // Initialize the barcode generator for GS1 QR code with the specified data
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1QR, codeText))
        {
            // Set the module size (X dimension) in pixels
            generator.Parameters.Barcode.XDimension.Pixels = 8f;
            // Set QR code error correction level to Medium
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;
            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Output the location of the saved barcode image
        Console.WriteLine($"GS1 QR code saved to: {outputPath}");
    }
}