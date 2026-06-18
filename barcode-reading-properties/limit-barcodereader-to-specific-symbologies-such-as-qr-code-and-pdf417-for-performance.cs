using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating QR and PDF417 barcodes and reading them using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates sample barcode images if they are missing and then reads them.
    /// </summary>
    static void Main()
    {
        // Define file paths for the sample barcode images
        string qrPath = "qr.png";
        string pdfPath = "pdf417.png";

        // ------------------------------------------------------------
        // Generate a QR Code image if it does not already exist
        // ------------------------------------------------------------
        if (!File.Exists(qrPath))
        {
            // Create a QR code generator with the desired content
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
            {
                // Save the generated QR code to the specified file
                generator.Save(qrPath);
            }
        }

        // ------------------------------------------------------------
        // Generate a PDF417 image if it does not already exist
        // ------------------------------------------------------------
        if (!File.Exists(pdfPath))
        {
            // Create a PDF417 generator with the desired content
            using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, "Sample PDF417 Text"))
            {
                // Save the generated PDF417 barcode to the specified file
                generator.Save(pdfPath);
            }
        }

        // List of barcode image files to be processed
        string[] files = new[] { qrPath, pdfPath };

        // ------------------------------------------------------------
        // Iterate over each file and attempt to read barcodes
        // ------------------------------------------------------------
        foreach (string file in files)
        {
            // Verify that the file exists before attempting to read it
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            // Use BarCodeReader limited to QR and PDF417 symbologies for better performance
            using (var reader = new BarCodeReader(file, DecodeType.QR, DecodeType.Pdf417))
            {
                // Read all barcodes found in the image
                foreach (var result in reader.ReadBarCodes())
                {
                    // Output the file name, detected barcode type, and decoded text
                    Console.WriteLine($"File: {Path.GetFileName(file)} | Detected Type: {result.CodeTypeName} | CodeText: {result.CodeText}");
                }
            }
        }
    }
}