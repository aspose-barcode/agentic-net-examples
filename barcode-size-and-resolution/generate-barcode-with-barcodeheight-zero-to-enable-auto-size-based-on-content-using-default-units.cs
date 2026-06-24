using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode;

/// <summary>
/// Demo program that generates a barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a barcode with auto‑size mode and saves it to a PNG file.
    /// </summary>
    static void Main()
    {
        // The requested feature (BarCodeHeight = 0) is not supported and throws an exception.
        // Instead, enable auto‑size by using AutoSizeMode.Interpolation, which lets the
        // barcode size itself based on the content using default units.

        // Choose a sample symbology (Code 128) and the text to encode.
        BaseEncodeType encodeType = EncodeTypes.Code128;
        string codeText = "Sample123";

        // Create the barcode generator within a using block to ensure proper disposal.
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Enable auto‑size mode so the barcode height is calculated automatically.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // No need to set BarHeight; it will be determined automatically.

            // Define the output file path.
            string outputPath = "barcode.png";

            // Save the generated barcode image to the specified file.
            generator.Save(outputPath);

            // Inform the user that the barcode has been generated.
            Console.WriteLine($"Barcode generated and saved to '{outputPath}'.");
        }
    }
}