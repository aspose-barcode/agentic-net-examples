// Title: Convert QR Code Image to DataMatrix Barcode
// Description: Demonstrates generating a QR code, reading its encoded text, and creating an equivalent DataMatrix barcode image.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It shows how to use BarcodeGenerator to create QR and DataMatrix barcodes, and BarCodeReader to extract data from an existing barcode image. Developers often need to convert between symbologies while preserving the encoded information, such as migrating legacy QR codes to DataMatrix for higher data density or specific scanner requirements.
// Prompt: Create utility that converts existing QR code images to DataMatrix format while preserving encoded data.
// Tags: barcode symbology, conversion, datamatrix, qr, generation, recognition, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a QR code, reads its data, and creates a DataMatrix barcode with the same content.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Performs QR code generation, data extraction, and DataMatrix creation.
    /// </summary>
    static void Main()
    {
        // Sample data to encode in the QR code
        const string sampleText = "Hello Aspose";

        // Create a unique temporary folder to store generated images
        string tempFolder = Path.Combine(Path.GetTempPath(), "QrToDataMatrix_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define file paths for the QR code and the resulting DataMatrix images
        string qrPath = Path.Combine(tempFolder, "qr.png");
        string dmPath = Path.Combine(tempFolder, "datamatrix.png");

        // -------------------------------------------------
        // Generate QR Code image
        // -------------------------------------------------
        using (var qrGenerator = new BarcodeGenerator(EncodeTypes.QR, sampleText))
        {
            // Configure QR code to use ECI encoding with UTF-8 character set
            qrGenerator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.ECI;
            qrGenerator.Parameters.Barcode.QR.ECIEncoding = ECIEncodings.UTF8;
            // Set module size (pixel dimension) for better readability
            qrGenerator.Parameters.Barcode.XDimension.Pixels = 8f;
            // Save QR code as PNG
            qrGenerator.Save(qrPath, BarCodeImageFormat.Png);
        }

        // Verify that the QR code image was successfully created
        if (!File.Exists(qrPath))
        {
            Console.WriteLine("Failed to create QR code image.");
            return;
        }

        // -------------------------------------------------
        // Read QR Code to obtain the encoded text
        // -------------------------------------------------
        string decodedText;
        using (var reader = new BarCodeReader(qrPath, DecodeType.QR))
        {
            var results = reader.ReadBarCodes();
            if (results == null || results.Length == 0)
            {
                Console.WriteLine("No QR code detected in the image.");
                return;
            }
            // Extract the first detected barcode's text
            decodedText = results[0].CodeText;
        }

        // -------------------------------------------------
        // Generate DataMatrix barcode using the decoded data
        // -------------------------------------------------
        using (var dmGenerator = new BarcodeGenerator(EncodeTypes.DataMatrix, decodedText))
        {
            // Set module size for the DataMatrix barcode
            dmGenerator.Parameters.Barcode.XDimension.Pixels = 8f;
            // Save DataMatrix as PNG
            dmGenerator.Save(dmPath, BarCodeImageFormat.Png);
        }

        // Output the file locations of the generated barcodes
        Console.WriteLine($"QR code saved to: {qrPath}");
        Console.WriteLine($"DataMatrix saved to: {dmPath}");
    }
}