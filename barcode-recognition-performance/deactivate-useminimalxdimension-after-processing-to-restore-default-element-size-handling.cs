using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates barcode generation, saving to memory, and recognition with different X-dimension settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a Code128 barcode, reads it with minimal X-dimension enabled, then reads it again with default settings.
    /// </summary>
    static void Main()
    {
        // Create a barcode generator for Code128 with the text "Sample123"
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Store the generated barcode image in a memory stream
            using (var ms = new MemoryStream())
            {
                // Save the barcode as PNG into the memory stream
                generator.Save(ms, BarCodeImageFormat.Png);
                // Reset stream position to the beginning for reading
                ms.Position = 0;

                // Load the PNG image from the memory stream into a Bitmap for recognition
                using (var bitmap = new Bitmap(ms))
                {
                    // Initialize a barcode reader that supports all barcode types
                    using (var reader = new BarCodeReader(bitmap, DecodeType.AllSupportedTypes))
                    {
                        // Enable minimal X-dimension mode and set an example minimal size
                        reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
                        reader.QualitySettings.MinimalXDimension = 5f; // example minimal size

                        Console.WriteLine("Recognition with UseMinimalXDimension:");
                        // Iterate over all detected barcodes and output their type and text
                        foreach (var result in reader.ReadBarCodes())
                        {
                            Console.WriteLine($"  Type: {result.CodeTypeName}, Text: {result.CodeText}");
                        }

                        // Deactivate minimal X-dimension mode to revert to normal handling
                        reader.QualitySettings.XDimension = XDimensionMode.Normal;
                        reader.QualitySettings.MinimalXDimension = 0f;

                        Console.WriteLine("Recognition after deactivating UseMinimalXDimension:");
                        // Perform recognition again with default X-dimension settings
                        foreach (var result in reader.ReadBarCodes())
                        {
                            Console.WriteLine($"  Type: {result.CodeTypeName}, Text: {result.CodeText}");
                        }
                    }
                }
            }
        }
    }
}