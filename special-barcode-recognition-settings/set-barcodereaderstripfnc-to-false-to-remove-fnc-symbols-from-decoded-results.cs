using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode;

/// <summary>
/// Demonstrates generating a GS1 Code128 barcode with FNC1 characters,
/// saving it to a temporary PNG file, reading it back while preserving
/// FNC characters, and cleaning up the temporary file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode, reads it, displays results, and deletes the temporary image.
    /// </summary>
    static void Main()
    {
        // Define path for a temporary PNG file to store the generated barcode image
        string tempImagePath = Path.Combine(Path.GetTempPath(), "sample_barcode.png");

        // Generate a GS1 Code128 barcode containing FNC1 characters
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1Code128, "(01)12345678901231(10)ABC123"))
        {
            // Save the generated barcode image to the temporary file
            generator.Save(tempImagePath);
        }

        // Verify that the barcode image file was successfully created
        if (!File.Exists(tempImagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Initialize a barcode reader for Code128 and configure it to retain FNC characters
        using (var reader = new BarCodeReader(tempImagePath, DecodeType.Code128))
        {
            // Ensure FNC characters are not stripped during decoding
            reader.BarcodeSettings.StripFNC = false;

            // Read all barcodes found in the image
            foreach (var result in reader.ReadBarCodes())
            {
                // Output the detected barcode type
                Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                // Output the decoded text with FNC characters retained
                Console.WriteLine($"CodeText (FNC retained): {result.CodeText}");
            }
        }

        // Attempt to delete the temporary image file; ignore any exceptions
        try
        {
            File.Delete(tempImagePath);
        }
        catch
        {
            // Suppress any errors that occur during cleanup
        }
    }
}