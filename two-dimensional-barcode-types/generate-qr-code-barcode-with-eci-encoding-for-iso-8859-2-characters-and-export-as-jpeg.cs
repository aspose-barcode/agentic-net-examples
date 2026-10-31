// Title: Generate QR Code with ISO‑8859‑2 ECI and save as JPEG
// Description: This example creates a QR Code containing characters from the ISO‑8859‑2 (Central European) character set using ECI encoding, then saves the barcode as a JPEG image.
// Category-Description: Demonstrates how to generate barcodes with Aspose.BarCode, specifically using the BarcodeGenerator class to configure QR Code parameters such as EncodeMode and ECIEncoding. Typical use cases include creating machine‑readable codes for multilingual data and exporting them to common image formats. Developers often need to set ECI to support non‑UTF‑8 character sets and then save the result as PNG, JPEG, or other image types.
// Prompt: Generate a QR Code barcode with ECI encoding for ISO‑8859‑2 characters and export as JPEG.
// Tags: qr code, eci encoding, iso-8859-2, jpeg, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a QR Code with ISO‑8859‑2 ECI encoding and saving it as a JPEG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates output directory, generates the QR Code, and saves it.
    /// </summary>
    static void Main()
    {
        // Determine the output directory path relative to the current working directory
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");

        // Ensure the output directory exists; create it if it does not
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Define the full path for the resulting JPEG file and the text to encode
        string outputPath = Path.Combine(outputDir, "QrCode_ECI_ISO8859_2.jpg");
        string codeText = "ĄĆĘŁŃÓŚŹŻ";

        // Initialize the barcode generator for a QR Code with the specified text
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            // Configure the QR Code to use ECI (Extended Channel Interpretation) encoding mode
            generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.ECI;

            // Set the specific ECI encoding to ISO‑8859‑2 (Central European)
            generator.Parameters.Barcode.QR.ECIEncoding = ECIEncodings.ISO_8859_2;

            // Save the generated barcode as a JPEG image to the defined path
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        // Output the location of the saved QR Code image
        Console.WriteLine($"QR Code saved to: {outputPath}");
    }
}