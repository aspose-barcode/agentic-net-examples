// Title: Convert QR Code Image to DataMatrix Barcode
// Description: Demonstrates reading an existing QR code image, extracting its encoded text, and generating an equivalent DataMatrix barcode image.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader for decoding them. Typical scenarios include migrating between symbologies, re‑encoding data, or integrating barcode workflows where different formats are required. Developers often need to decode a source barcode, manipulate the data, and generate a new barcode using the same or different symbology.
/// Prompt: Create utility that converts existing QR code images to DataMatrix format while preserving encoded data.
/// Tags: qr code, datamatrix, barcode conversion, generation, recognition, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Sample utility that converts a QR code image to a DataMatrix barcode while preserving the encoded data.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a QR code if missing, decodes it, and creates a DataMatrix barcode.
    /// </summary>
    static void Main()
    {
        // Sample data to encode into the QR code (used only if the QR image does not already exist)
        string codeText = "Hello Aspose";

        // Define temporary folder and file paths for the QR and DataMatrix images
        string tempFolder = Path.Combine(Path.GetTempPath(), "QrToDataMatrixDemo");
        Directory.CreateDirectory(tempFolder);
        string qrPath = Path.Combine(tempFolder, "qr.png");
        string dmPath = Path.Combine(tempFolder, "datamatrix.png");

        // ------------------------------------------------------------
        // Generate a QR code image if it does not already exist on disk
        // ------------------------------------------------------------
        if (!File.Exists(qrPath))
        {
            using (var qrGenerator = new BarcodeGenerator(EncodeTypes.QR, codeText))
            {
                // Set module size (pixel dimension) for better readability
                qrGenerator.Parameters.Barcode.XDimension.Pixels = 8f;
                qrGenerator.Save(qrPath, BarCodeImageFormat.Png);
                Console.WriteLine($"Generated QR code at: {qrPath}");
            }
        }
        else
        {
            Console.WriteLine($"QR code already exists at: {qrPath}");
        }

        // ------------------------------------------------------------
        // Verify that the QR image exists before attempting to decode it
        // ------------------------------------------------------------
        if (!File.Exists(qrPath))
        {
            Console.WriteLine("QR code image not found. Exiting.");
            return;
        }

        // ------------------------------------------------------------
        // Decode the QR code to retrieve the original text payload
        // ------------------------------------------------------------
        string decodedText = null;
        BaseDecodeType qrDecodeType = DecodeType.QR;
        try
        {
            using (var reader = new BarCodeReader(qrPath, qrDecodeType))
            {
                BarCodeResult[] results = reader.ReadBarCodes();
                if (results != null && results.Length > 0)
                {
                    decodedText = results[0].CodeText;
                    Console.WriteLine($"Decoded QR text: {decodedText}");
                }
                else
                {
                    Console.WriteLine("No barcode detected in the QR image.");
                }
            }
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Error reading QR image: {ex.Message}");
        }

        // If decoding failed, abort the conversion process
        if (string.IsNullOrEmpty(decodedText))
        {
            Console.WriteLine("No data to convert. Exiting.");
            return;
        }

        // ------------------------------------------------------------
        // Generate a DataMatrix barcode using the decoded text
        // ------------------------------------------------------------
        using (var dmGenerator = new BarcodeGenerator(EncodeTypes.DataMatrix, decodedText))
        {
            dmGenerator.Parameters.Barcode.XDimension.Pixels = 8f;
            dmGenerator.Save(dmPath, BarCodeImageFormat.Png);
            Console.WriteLine($"Converted DataMatrix saved at: {dmPath}");
        }
    }
}