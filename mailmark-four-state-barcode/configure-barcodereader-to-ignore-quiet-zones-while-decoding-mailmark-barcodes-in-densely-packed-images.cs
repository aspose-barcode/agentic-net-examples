using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generation and recognition of a Mailmark barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a Mailmark barcode, saves it to a memory stream,
    /// and then reads it back using <see cref="BarCodeReader"/>.
    /// </summary>
    static void Main()
    {
        // Create a sample Mailmark codetext object with valid data
        var mailmark = new MailmarkCodetext
        {
            Format = 4,                 // 4-state (unspecified/default)
            VersionID = 1,
            Class = "0",                // Test class
            SupplychainID = 384224,
            ItemID = 16563762,
            DestinationPostCodePlusDPS = "EF61AH8T " // Valid postcode+DP
        };

        // Generate a Mailmark barcode image using ComplexBarcodeGenerator
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        using (var ms = new MemoryStream())
        {
            // Save the generated barcode to the memory stream in PNG format
            generator.Save(ms, BarCodeImageFormat.Png);
            // Reset stream position to the beginning for reading
            ms.Position = 0;

            // Initialize BarCodeReader for Mailmark symbology
            using (var reader = new BarCodeReader(ms, DecodeType.Mailmark))
            {
                // Configure quality settings to improve detection
                // Allow recognition of potentially damaged barcodes
                reader.QualitySettings.AllowIncorrectBarcodes = true;
                // Use fast deconvolution to enhance detection in dense images
                reader.QualitySettings.Deconvolution = DeconvolutionMode.Fast;

                // Read barcodes from the image
                var results = reader.ReadBarCodes();

                // Output detection results
                if (results.Length == 0)
                {
                    Console.WriteLine("No Mailmark barcode detected.");
                }
                else
                {
                    foreach (var result in results)
                    {
                        Console.WriteLine($"Detected Mailmark barcode: {result.CodeText}");
                    }
                }
            }
        }
    }
}