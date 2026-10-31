// Title: Binary Encoding Mode with QR and DataMatrix Barcodes
// Description: Demonstrates generating QR and DataMatrix barcodes using Aspose.BarCode in binary mode and handling errors when non‑ASCII text is not supported.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use EncodeTypes, BarcodeGenerator, and specific encode mode settings (QREncodeMode, DataMatrixEncodeMode) to create barcodes. Typical use cases include embedding binary data in QR or DataMatrix symbols and handling unsupported character sets. Developers often need to detect and manage exceptions when binary mode receives characters outside the allowed ASCII range.
// Prompt: Implement error handling for unsupported encoding mode when Binary mode receives non‑ASCII text.
// Tags: barcode, qrcode, datamatrix, binary encoding, error handling, aspose.barcode, generation, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates binary encoding for QR and DataMatrix barcodes and error handling for unsupported characters.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcodes and showcases error handling.
    /// </summary>
    static void Main()
    {
        // Example with QR code and non-ASCII text (should trigger exception in Binary mode)
        GenerateBarcode("QR", "Hello世界");

        // Example with DataMatrix and ASCII text (should succeed)
        GenerateBarcode("DataMatrix", "HelloWorld");
    }

    /// <summary>
    /// Generates a barcode of the specified symbology using binary encoding mode.
    /// Handles unsupported encoding scenarios and reports errors.
    /// </summary>
    /// <param name="symbologyName">Name of the barcode symbology (e.g., "QR", "DataMatrix").</param>
    /// <param name="codeText">Text to encode in the barcode.</param>
    static void GenerateBarcode(string symbologyName, string codeText)
    {
        // Resolve EncodeTypes member via reflection
        var field = typeof(EncodeTypes).GetField(symbologyName);
        if (field == null)
        {
            Console.WriteLine($"Unknown symbology: {symbologyName}");
            return;
        }

        // Get the corresponding BaseEncodeType value
        BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);
        string outputPath = Path.Combine(Path.GetTempPath(), $"{symbologyName}_Binary_{Guid.NewGuid():N}.png");

        try
        {
            // Initialize the barcode generator with the selected symbology and text
            using (var generator = new BarcodeGenerator(encodeType, codeText))
            {
                // Set Binary encoding mode based on the symbology
                if (symbologyName.Equals("QR", StringComparison.OrdinalIgnoreCase))
                {
                    generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.Binary;
                }
                else if (symbologyName.Equals("DataMatrix", StringComparison.OrdinalIgnoreCase))
                {
                    generator.Parameters.Barcode.DataMatrix.EncodeMode = DataMatrixEncodeMode.Binary;
                }
                else
                {
                    Console.WriteLine($"Binary mode not demonstrated for symbology: {symbologyName}");
                    return;
                }

                // Attempt to save the barcode image as PNG
                generator.Save(outputPath, BarCodeImageFormat.Png);
                Console.WriteLine($"Barcode saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Capture and display any errors (e.g., unsupported characters in binary mode)
            Console.WriteLine($"Error generating {symbologyName} barcode in Binary mode: {ex.Message}");
        }
    }
}