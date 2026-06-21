using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates barcode generation and recognition using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a Code128 barcode image and returns it as a <see cref="MemoryStream"/>.
    /// </summary>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <returns>A memory stream containing the PNG image of the generated barcode.</returns>
    static MemoryStream GenerateBarcode(string codeText)
    {
        // Create a memory stream to hold the barcode image.
        var stream = new MemoryStream();

        // Initialize the barcode generator with Code128 encoding and the provided text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Save the generated barcode to the stream in PNG format.
            generator.Save(stream, BarCodeImageFormat.Png);
        }

        // Reset the stream position to the beginning for subsequent reading.
        stream.Position = 0;
        return stream;
    }

    /// <summary>
    /// Entry point that generates a Code128 barcode, then reads it using default settings
    /// and with minimal X dimension configuration.
    /// </summary>
    static void Main()
    {
        const string barcodeText = "Test123";

        // Generate the barcode image and obtain a stream containing the PNG data.
        using (var barcodeStream = GenerateBarcode(barcodeText))
        {
            // ---------- Default recognition ----------
            // Create a reader that decodes Code128 barcodes from the stream.
            using (var reader = new BarCodeReader(barcodeStream, DecodeType.Code128))
            {
                bool defaultSuccess = false;

                // Iterate through all detected barcodes.
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Default read: {result.CodeText}");

                    // Verify that the decoded text matches the original.
                    if (result.CodeText == barcodeText)
                        defaultSuccess = true;
                }

                Console.WriteLine($"Default recognition success: {defaultSuccess}");
            }

            // Reset the stream position to allow a second read.
            barcodeStream.Position = 0;

            // ---------- Recognition with UseMinimalXDimension ----------
            using (var reader = new BarCodeReader(barcodeStream, DecodeType.Code128))
            {
                // Configure the reader to use a minimal X dimension for better detection of small barcodes.
                reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
                reader.QualitySettings.MinimalXDimension = 2f; // Example minimal size in points.

                bool minimalSuccess = false;

                // Iterate through all detected barcodes with the new settings.
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"UseMinimalXDimension read: {result.CodeText}");

                    // Verify that the decoded text matches the original.
                    if (result.CodeText == barcodeText)
                        minimalSuccess = true;
                }

                Console.WriteLine($"UseMinimalXDimension recognition success: {minimalSuccess}");
            }
        }
    }
}