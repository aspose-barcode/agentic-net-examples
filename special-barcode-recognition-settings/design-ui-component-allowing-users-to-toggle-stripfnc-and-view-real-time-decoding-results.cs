using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code128 barcode, toggling the StripFNC setting,
/// and decoding the barcode with and without stripping FNC characters.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the console application.
    /// Generates a barcode, decodes it twice (once with StripFNC disabled,
    /// once with it enabled), and writes the results to the console.
    /// </summary>
    static void Main()
    {
        // Create a barcode generator for Code128 with the data "123456".
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Store the generated barcode image in a memory stream.
            using (var ms = new MemoryStream())
            {
                // Save the barcode as a PNG image into the memory stream.
                generator.Save(ms, BarCodeImageFormat.Png);
                // Reset stream position to the beginning for reading.
                ms.Position = 0;

                // ------------------------------------------------------------
                // First decoding pass: StripFNC disabled (default behavior)
                // ------------------------------------------------------------
                using (var bitmap = new Bitmap(ms))
                using (var reader = new BarCodeReader(bitmap, DecodeType.AllSupportedTypes))
                {
                    // Ensure FNC characters are not stripped during decoding.
                    reader.BarcodeSettings.StripFNC = false;

                    // Iterate through all detected barcodes and output their details.
                    foreach (var result in reader.ReadBarCodes())
                    {
                        Console.WriteLine($"StripFNC=False -> Type: {result.CodeTypeName}, Text: {result.CodeText}");
                    }
                }

                // Reset the stream position again for the second decoding pass.
                ms.Position = 0;

                // ------------------------------------------------------------
                // Second decoding pass: StripFNC enabled
                // ------------------------------------------------------------
                using (var bitmap = new Bitmap(ms))
                using (var reader = new BarCodeReader(bitmap, DecodeType.AllSupportedTypes))
                {
                    // Enable stripping of FNC characters during decoding.
                    reader.BarcodeSettings.StripFNC = true;

                    // Iterate through all detected barcodes and output their details.
                    foreach (var result in reader.ReadBarCodes())
                    {
                        Console.WriteLine($"StripFNC=True -> Type: {result.CodeTypeName}, Text: {result.CodeText}");
                    }
                }
            }
        }
    }
}