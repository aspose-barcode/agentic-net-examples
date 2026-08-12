// Title: Convert QR Code Image to DataMatrix Barcode
// Description: Demonstrates generating a QR code, decoding it, and recreating the same data as a DataMatrix barcode.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, showcasing how to use BarcodeGenerator for creating QR and DataMatrix symbols and BarCodeReader for decoding. Typical use cases include migrating between symbologies while preserving encoded information, useful for developers needing to reformat barcodes without losing data.
// Prompt: Create utility that converts existing QR code images to DataMatrix format while preserving encoded data.
// Tags: barcode, qrcode, datamatrix, conversion, generation, recognition, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates converting a QR code image to a DataMatrix barcode while preserving the encoded data.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR code, decodes it, and creates a DataMatrix barcode with the same content.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for all generated files
        string tempFolder = Path.Combine(Path.GetTempPath(), "QrToDataMatrix_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Sample text to encode
        string sampleText = "Hello Aspose";

        // Paths for QR and DataMatrix images
        string qrPath = Path.Combine(tempFolder, "qr.png");
        string dmPath = Path.Combine(tempFolder, "datamatrix.png");

        // -------------------------------------------------
        // Step 1: Generate a QR code image with the sample text
        // -------------------------------------------------
        using (var qrGenerator = new BarcodeGenerator(EncodeTypes.QR, sampleText))
        {
            // Optional: set error correction level or other QR parameters here
            qrGenerator.Save(qrPath);
        }

        Console.WriteLine($"QR code saved to: {qrPath}");

        // -------------------------------------------------
        // Step 2: Decode the QR code to retrieve the encoded data
        // -------------------------------------------------
        string decodedText = null;
        if (File.Exists(qrPath))
        {
            using (var reader = new BarCodeReader(qrPath, DecodeType.QR))
            {
                foreach (var result in reader.ReadBarCodes())
                {
                    decodedText = result.CodeText;
                    break; // We expect only one barcode in the image
                }
            }
        }

        if (string.IsNullOrEmpty(decodedText))
        {
            Console.WriteLine("Failed to decode QR code. Exiting.");
            return;
        }

        Console.WriteLine($"Decoded text from QR: {decodedText}");

        // -------------------------------------------------
        // Step 3: Generate a DataMatrix barcode using the same text
        // -------------------------------------------------
        using (var dmGenerator = new BarcodeGenerator(EncodeTypes.DataMatrix, decodedText))
        {
            // Ensure Unicode text is correctly encoded
            dmGenerator.Parameters.Barcode.DataMatrix.EncodeMode = DataMatrixEncodeMode.ECI;
            dmGenerator.Parameters.Barcode.DataMatrix.ECIEncoding = ECIEncodings.UTF8;

            // Optional: set ECC type or version if needed
            dmGenerator.Parameters.Barcode.DataMatrix.EccType = DataMatrixEccType.Ecc200;

            dmGenerator.Save(dmPath);
        }

        Console.WriteLine($"DataMatrix code saved to: {dmPath}");
        Console.WriteLine("Conversion completed successfully.");
    }
}