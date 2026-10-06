// Title: Generate QR Code with ECI ISO‑8859‑2 Encoding and Save as JPEG
// Description: Demonstrates creating a QR Code barcode using ECI encoding for ISO‑8859‑2 characters and exporting it as a JPEG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing how to configure QR Code parameters such as EncodeMode and ECIEncoding. It uses the BarcodeGenerator class along with EncodeTypes, QREncodeMode, and ECIEncodings to produce barcodes for international character sets. Developers often need these settings when generating QR codes that must represent non‑ASCII text for multilingual applications.
// Prompt: Generate a QR Code barcode with ECI encoding for ISO‑8859‑2 characters and export as JPEG.
// Tags: qr code, eci encoding, iso-8859-2, jpeg, aspose.barcode, barcode generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a QR Code with ECI encoding for ISO‑8859‑2 characters
/// and saves the result as a JPEG image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Create a temporary directory to store the generated barcode image.
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(tempDir);

        // Define the full path for the output JPEG file.
        string outputPath = Path.Combine(tempDir, "qr_eci.jpg");

        // Text containing ISO‑8859‑2 characters to encode in the QR Code.
        string codeText = "ĄĆĘŁŃÓŚŹŻ";

        // Initialize the barcode generator for QR Code with the specified text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            // Set QR Code to use ECI (Extended Channel Interpretation) mode.
            generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.ECI;

            // Specify the ECI encoding as ISO‑8859‑2.
            generator.Parameters.Barcode.QR.ECIEncoding = ECIEncodings.ISO_8859_2;

            // Save the generated QR Code as a JPEG image.
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        // Inform the user where the QR code image has been saved.
        Console.WriteLine($"QR code saved to: {outputPath}");
    }
}