// Title: Minimal XDimension vs XDimension Filtering Demo
// Description: Demonstrates how setting MinimalXDimension higher than the barcode's XDimension can unintentionally filter out valid barcodes during recognition.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator to create a barcode image and BarCodeReader with QualitySettings to adjust detection parameters such as MinimalXDimension. Developers commonly use these APIs to generate barcodes for labeling, then read them in automated scanning systems, often tweaking quality settings to improve read reliability.
// Prompt: Validate that setting MinimalXDimension higher than XDimension filters out valid barcodes unintentionally.
// Tags: barcode symbology, generation, recognition, qualitysettings, minimalxdimension, code128, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a Code128 barcode, reads it with default settings,
/// then reads it again with MinimalXDimension set higher than the generated XDimension
/// to illustrate the impact on barcode detection.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, performs two reads with different
    /// quality settings, and outputs the results to the console.
    /// </summary>
    static void Main()
    {
        // Generate a barcode image with XDimension = 2 points
        float xDim = 2f;
        MemoryStream barcodeStream = GenerateBarcode(xDim);
        barcodeStream.Position = 0;

        // --------------------------------------------------------------------
        // First read: use default quality settings (no MinimalXDimension filter)
        // --------------------------------------------------------------------
        BaseDecodeType decodeType = DecodeType.Code128;
        using (var reader = new BarCodeReader(barcodeStream, decodeType))
        {
            var results = reader.ReadBarCodes();
            Console.WriteLine("Default read count: " + results.Length);
            foreach (var result in results)
            {
                Console.WriteLine("CodeText: " + result.CodeText);
            }
        }

        // Reset the stream position for the second read operation
        barcodeStream.Position = 0;

        // --------------------------------------------------------------------
        // Second read: enable MinimalXDimension mode and set it higher than XDimension
        // --------------------------------------------------------------------
        using (var reader = new BarCodeReader(barcodeStream, decodeType))
        {
            // Activate MinimalXDimension mode
            reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
            // Set MinimalXDimension to 3 points (greater than the generated 2 points)
            reader.QualitySettings.MinimalXDimension = 3f;

            var results = reader.ReadBarCodes();
            Console.WriteLine("Read with higher MinimalXDimension count: " + results.Length);
            foreach (var result in results)
            {
                Console.WriteLine("CodeText: " + result.CodeText);
            }
        }

        // Clean up the memory stream
        barcodeStream.Dispose();
    }

    /// <summary>
    /// Generates a Code128 barcode image with the specified XDimension (module width)
    /// and returns it as a MemoryStream.
    /// </summary>
    /// <param name="xDimension">The XDimension value in points.</param>
    /// <returns>A MemoryStream containing the generated PNG barcode image.</returns>
    static MemoryStream GenerateBarcode(float xDimension)
    {
        // Create a Code128 barcode generator with sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Apply the desired XDimension (module width) to the barcode parameters
            generator.Parameters.Barcode.XDimension.Point = xDimension;

            // Save the generated barcode image to a memory stream in PNG format
            var ms = new MemoryStream();
            generator.Save(ms, BarCodeImageFormat.Png);
            ms.Position = 0;
            return ms;
        }
    }
}