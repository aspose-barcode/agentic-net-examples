using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating and reading a Code39 full ASCII barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a sample barcode if missing and reads it.
    /// </summary>
    static void Main()
    {
        const string imagePath = "code39.png";

        // Verify that the sample barcode image exists; create it if it does not.
        if (!File.Exists(imagePath))
        {
            // Initialize a barcode generator for Code39 full ASCII with sample data.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code39FullASCII, "A B C 123"))
            {
                // Save the generated barcode image to the specified path.
                generator.Save(imagePath);
                Console.WriteLine($"Generated sample barcode image: {imagePath}");
            }
        }

        // Initialize a barcode reader configured for Code39 full ASCII decoding.
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code39FullASCII))
        {
            // Aspose.BarCode automatically trims quiet zones; no extra setting needed.
            // Adjust quality settings if detection issues arise.
            reader.QualitySettings.Deconvolution = DeconvolutionMode.Fast;

            // Iterate through all detected barcodes in the image.
            foreach (var result in reader.ReadBarCodes())
            {
                // Output the type and decoded text of each barcode.
                Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                Console.WriteLine($"Decoded Text : {result.CodeText}");
            }
        }
    }
}