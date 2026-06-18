using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates creation and reading of a MicroPdf417 barcode with Code128 emulation enabled.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a MicroPdf417 barcode, saves it to a file, and then reads it back to verify the Code128 emulation flag.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image.
        string outputPath = "micropdf417.png";

        // Create a MicroPdf417 barcode generator with the specified data.
        // The data includes a group separator (ASCII 29) between "a" and "1234567890".
        using (var generator = new BarcodeGenerator(EncodeTypes.MicroPdf417, "a\u001d1234567890"))
        {
            // Enable Code128 emulation mode for the PDF417 barcode.
            generator.Parameters.Barcode.Pdf417.IsCode128Emulation = true;

            // Save the generated barcode image to the specified file.
            generator.Save(outputPath);
        }

        // Verify that the barcode image file was successfully created.
        if (!File.Exists(outputPath))
        {
            Console.WriteLine($"Failed to create barcode image at '{outputPath}'.");
            return;
        }

        // Load the saved barcode image and create a reader for MicroPdf417 decoding.
        using (var image = (Bitmap)Image.FromFile(outputPath))
        using (var reader = new BarCodeReader(image, DecodeType.MicroPdf417))
        {
            // Iterate through all detected barcodes in the image.
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // Output the decoded text of the barcode.
                Console.WriteLine($"CodeText: {result.CodeText}");

                // Access extended PDF417 properties to check if Code128 emulation is enabled.
                bool isEmulation = result.Extended.Pdf417.IsCode128Emulation;
                Console.WriteLine($"IsCode128Emulation: {isEmulation}");
            }
        }
    }
}