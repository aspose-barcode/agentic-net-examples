// Title: Convert QR Code Image to DataMatrix Barcode
// Description: Demonstrates how to read a QR code from an image, extract its encoded text, and generate an equivalent DataMatrix barcode image.
// Category-Description: This example belongs to the Aspose.BarCode barcode conversion category, showing how to use the BarCodeReader for decoding and BarcodeGenerator for encoding. Typical use cases include migrating legacy QR code assets to DataMatrix for higher data density or specific scanning requirements. Developers often need to read existing barcodes, preserve the data, and re‑encode it in a different symbology using the EncodeTypes and DecodeType APIs.
// Prompt: Create utility that converts existing QR code images to DataMatrix format while preserving encoded data.
// Tags: barcode, conversion, qrcode, datamatrix, decoding, encoding, aspnet, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates conversion of a QR code image to a DataMatrix barcode while preserving the encoded data.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a sample QR code, then converts it to DataMatrix.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary working directory for generated files
        string workDir = Path.Combine(Path.GetTempPath(), "QrToDataMatrix_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workDir);

        // Define file paths for the sample QR code and the resulting DataMatrix image
        string qrPath = Path.Combine(workDir, "sample_qr.png");
        string dmPath = Path.Combine(workDir, "sample_datamatrix.png");

        // Generate a sample QR code to demonstrate the conversion process
        string sampleText = "https://example.com/aspnet";
        GenerateQrCode(sampleText, qrPath);
        Console.WriteLine($"Sample QR code generated at: {qrPath}");

        // Convert the generated QR code image to a DataMatrix barcode
        ConvertQrToDataMatrix(qrPath, dmPath);
        Console.WriteLine($"Converted DataMatrix saved at: {dmPath}");
    }

    /// <summary>
    /// Generates a QR code image from the specified text.
    /// </summary>
    /// <param name="text">The text to encode in the QR code.</param>
    /// <param name="outputPath">The file path where the QR code image will be saved.</param>
    static void GenerateQrCode(string text, string outputPath)
    {
        // Use BarcodeGenerator with QR encode type
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, text))
        {
            // Set a reasonable module size for readability
            generator.Parameters.Barcode.XDimension.Pixels = 8f;
            // Save the QR code as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }
    }

    /// <summary>
    /// Reads a QR code image, extracts its encoded text, and creates a DataMatrix barcode with the same data.
    /// </summary>
    /// <param name="inputPath">Path to the source QR code image.</param>
    /// <param name="outputPath">Path where the generated DataMatrix image will be saved.</param>
    static void ConvertQrToDataMatrix(string inputPath, string outputPath)
    {
        // Verify that the input file exists before attempting to read it
        if (!File.Exists(inputPath))
        {
            Console.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Decode the QR code using BarCodeReader
        BaseDecodeType decodeType = DecodeType.QR;
        string codeText = null;

        using (var reader = new BarCodeReader(inputPath, decodeType))
        {
            var results = reader.ReadBarCodes();
            if (results == null || results.Length == 0)
            {
                Console.WriteLine("No barcode detected in the input image.");
                return;
            }

            // Assume the first detected barcode is the QR code we need
            codeText = results[0].CodeText;
            Console.WriteLine($"Decoded QR code text: {codeText}");
        }

        // Ensure that decoded text is not empty before proceeding
        if (string.IsNullOrEmpty(codeText))
        {
            Console.WriteLine("Decoded QR code text is empty.");
            return;
        }

        // Generate a DataMatrix barcode using the extracted text
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, codeText))
        {
            // Set module size for better readability
            generator.Parameters.Barcode.XDimension.Pixels = 8f;
            // Save the DataMatrix as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }
    }
}