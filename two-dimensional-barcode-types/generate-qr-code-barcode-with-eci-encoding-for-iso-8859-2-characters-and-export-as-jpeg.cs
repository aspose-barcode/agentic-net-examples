// Title: Generate QR Code with ISO‑8859‑2 ECI Encoding and Save as JPEG
// Description: Demonstrates creating a QR Code barcode that uses ECI encoding for ISO‑8859‑2 characters and exporting the image as a JPEG file.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on QR Code creation with extended character set support via ECI. It showcases the use of BarcodeGenerator, EncodeTypes, QREncodeMode, and ECIEncodings classes to produce QR codes for non‑Unicode text, a common requirement when integrating with legacy systems or printing devices that expect specific code pages.
// Prompt: Generate a QR Code barcode with ECI encoding for ISO‑8859‑2 characters and export as JPEG.
// Tags: qr code, eci encoding, iso-8859-2, jpeg, aspose.barcode, barcode generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a QR Code with ISO‑8859‑2 ECI encoding and saving it as a JPEG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the QR Code and writes the output file path to the console.
    /// </summary>
    static void Main()
    {
        // Sample text containing ISO‑8859‑2 characters (Polish letters)
        string codeText = "ĄĆĘŁŃÓŚŹŻ";

        // Determine the output file path in the current working directory
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "qr_iso_8859_2.jpg");

        // Initialize the QR Code generator with the specified text
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            // Configure the QR Code to use ECI (Extended Channel Interpretation) mode
            generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.ECI;

            // Set the specific ECI encoding to ISO‑8859‑2
            generator.Parameters.Barcode.QR.ECIEncoding = ECIEncodings.ISO_8859_2;

            // Optional: lower resolution to reduce JPEG file size
            generator.Parameters.Resolution = 72f;
            generator.Parameters.UseAntiAlias = false;

            // Save the generated barcode as a JPEG image
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        // Inform the user where the QR Code image was saved
        Console.WriteLine($"QR Code saved to: {outputPath}");
    }
}