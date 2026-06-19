using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a PDF417 barcode with UTF‑8 encoded text,
/// saving it to a temporary file, reading it back, and cleaning up.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode, reads it, displays the results, and deletes the temporary image.
    /// </summary>
    static void Main()
    {
        // Define a temporary file path for the barcode image.
        string imagePath = Path.Combine(Path.GetTempPath(), "pdf417_utf8.png");

        // ------------------------------------------------------------
        // Generate a PDF417 barcode containing Unicode text ("Привет мир").
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, "Привет мир"))
        {
            // Configure the generator to use ECI mode with UTF‑8 encoding.
            generator.Parameters.Barcode.Pdf417.EncodeMode = Pdf417EncodeMode.ECI;
            generator.Parameters.Barcode.Pdf417.ECIEncoding = ECIEncodings.UTF8;

            // Save the generated barcode image to the temporary file.
            generator.Save(imagePath);
        }

        // Verify that the barcode image was successfully created.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create the barcode image.");
            return;
        }

        // ------------------------------------------------------------
        // Read the barcode from the image and enable automatic encoding detection.
        // ------------------------------------------------------------
        using (var reader = new BarCodeReader(imagePath, DecodeType.Pdf417))
        {
            // Instruct the reader to detect the original encoding (UTF‑8).
            reader.BarcodeSettings.DetectEncoding = true;

            // Iterate through all detected barcodes (there should be one).
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected Symbology: {result.CodeTypeName}");
                Console.WriteLine($"Decoded Text: {result.CodeText}");
            }
        }

        // ------------------------------------------------------------
        // Clean up: delete the temporary barcode image file.
        // ------------------------------------------------------------
        try
        {
            File.Delete(imagePath);
        }
        catch
        {
            // Suppress any exceptions that occur during cleanup.
        }
    }
}