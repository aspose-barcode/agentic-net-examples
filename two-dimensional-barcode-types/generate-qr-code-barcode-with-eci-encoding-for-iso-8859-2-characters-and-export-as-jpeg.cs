// Title: Generate QR Code with ISO‑8859‑2 ECI Encoding and Save as JPEG
// Description: Demonstrates creating a QR Code barcode that uses ECI encoding for ISO‑8859‑2 characters and exporting it as a JPEG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on QR Code creation with extended character set support. It showcases the use of BarcodeGenerator, EncodeTypes, QREncodeMode, and ECIEncodings classes to produce barcodes for international text. Developers often need to generate QR codes containing non‑ASCII characters for multilingual applications, and this snippet illustrates the required settings.
// Prompt: Generate a QR Code barcode with ECI encoding for ISO‑8859‑2 characters and export as JPEG.
// Tags: qr code, eci encoding, iso-8859-2, jpeg, aspose.barcode, barcode generation

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a QR Code barcode with ECI encoding for ISO‑8859‑2 characters
/// and saves the result as a JPEG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Sample text containing ISO‑8859‑2 characters
        string codeText = "ĄĆĘŁŃÓŚŹŻ";

        // Create a QR Code generator within a using block to ensure proper disposal
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Assign the text to be encoded in the QR Code
            generator.CodeText = codeText;

            // Enable ECI encoding mode for QR Code
            generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.ECIEncoding;

            // Specify the ISO‑8859‑2 character set for ECI encoding
            generator.Parameters.Barcode.QR.ECIEncoding = ECIEncodings.ISO_8859_2;

            // Define the output file path
            string outputPath = "qr_iso8859_2.jpg";

            // Save the generated QR Code as a JPEG image
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        // Inform the user that the QR code has been generated
        Console.WriteLine("QR code generated and saved to qr_iso8859_2.jpg");
    }
}