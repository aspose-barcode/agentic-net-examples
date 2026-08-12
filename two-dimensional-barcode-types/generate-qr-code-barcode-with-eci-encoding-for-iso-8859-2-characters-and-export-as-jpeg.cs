// Title: Generate QR Code with ISO‑8859‑2 ECI Encoding and Save as JPEG
// Description: Demonstrates creating a QR Code barcode containing ISO‑8859‑2 characters using ECI encoding and exporting it to a JPEG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure QR Code parameters such as EncodeMode and ECIEncoding. Developers working with multilingual data often need to embed non‑UTF‑8 characters in barcodes; the BarcodeGenerator class together with QREncodeMode and ECIEncodings provides the required support. Typical use cases include generating QR codes for Central European languages and saving them in common image formats.
// Prompt: Generate a QR Code barcode with ECI encoding for ISO‑8859‑2 characters and export as JPEG.
// Tags: qr code, eci encoding, iso-8859-2, jpeg, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates a QR Code containing ISO‑8859‑2 characters,
/// applies ECI encoding, and saves the result as a JPEG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode and writes the output file path to the console.
    /// </summary>
    static void Main()
    {
        // Determine the full path for the output JPEG file.
        string outputPath = Path.Combine(Environment.CurrentDirectory, "qr_iso8859_2.jpg");

        // Initialize the QR Code generator with the QR symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Set the barcode text to a string that contains ISO‑8859‑2 characters.
            generator.CodeText = "ĄĆĘŁŃÓŚŹŻ";

            // Configure the QR Code to use ECI encoding and specify the ISO‑8859‑2 character set.
            generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.ECI;
            generator.Parameters.Barcode.QR.ECIEncoding = ECIEncodings.ISO_8859_2;

            // Save the generated QR Code as a JPEG image to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        // Inform the user where the QR Code image has been saved.
        Console.WriteLine($"QR code saved to: {outputPath}");
    }
}