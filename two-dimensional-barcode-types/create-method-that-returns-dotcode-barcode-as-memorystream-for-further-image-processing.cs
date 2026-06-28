using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generation of a DotCode barcode using Aspose.BarCode and returns it as a MemoryStream.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a sample DotCode barcode and displays the stream length.
    /// </summary>
    static void Main()
    {
        // Sample usage of the GetDotCodeBarcode method
        string sampleText = "Sample DotCode Text";

        // Generate the barcode and obtain it as a MemoryStream
        using (MemoryStream barcodeStream = GetDotCodeBarcode(sampleText))
        {
            // Output the length of the generated barcode image stream
            Console.WriteLine($"Generated DotCode barcode stream length: {barcodeStream.Length} bytes");

            // Further image processing can be performed here using the returned MemoryStream
        }
    }

    /// <summary>
    /// Generates a DotCode barcode image and returns it as a MemoryStream.
    /// </summary>
    /// <param name="codeText">The text to encode in the DotCode barcode.</param>
    /// <returns>A MemoryStream containing the PNG image of the generated barcode.</returns>
    static MemoryStream GetDotCodeBarcode(string codeText)
    {
        // Validate input to ensure a non-empty string is provided
        if (string.IsNullOrEmpty(codeText))
            throw new ArgumentException("Code text must be a non-empty string.", nameof(codeText));

        // Create a memory stream that will hold the barcode image.
        MemoryStream ms = new MemoryStream();

        // Use a using block for the BarcodeGenerator (IDisposable) to ensure resources are released.
        using (var generator = new BarcodeGenerator(EncodeTypes.DotCode, codeText))
        {
            // Optional: set ECI encoding for Unicode support.
            generator.Parameters.Barcode.DotCode.ECIEncoding = ECIEncodings.UTF8;

            // Save the barcode image to the memory stream in PNG format.
            generator.Save(ms, BarCodeImageFormat.Png);
        }

        // Reset the stream position so it can be read from the beginning.
        ms.Position = 0;
        return ms;
    }
}