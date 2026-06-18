using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a PDF417 barcode with the IsReaderInitialization flag,
/// reading it back, and cleaning up the generated image file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a PDF417 barcode, saves it as an image, reads the barcode,
    /// and then deletes the image file.
    /// </summary>
    static void Main()
    {
        // Define the text to encode in the PDF417 barcode.
        const string codeText = "PDF417 Initialization Test";

        // Specify the output image file name.
        string imagePath = "pdf417.png";

        // ------------------------------------------------------------
        // Generate PDF417 barcode with IsReaderInitialization flag set.
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, codeText))
        {
            // Instruct the scanner that this barcode contains initialization instructions.
            generator.Parameters.Barcode.Pdf417.IsReaderInitialization = true;

            // Save the generated barcode image to the specified path.
            generator.Save(imagePath);
        }

        // Verify that the image file was successfully created.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Failed to create barcode image at '{imagePath}'.");
            return;
        }

        // ------------------------------------------------------------
        // Read the barcode from the saved image.
        // ------------------------------------------------------------
        using (var reader = new BarCodeReader(imagePath, DecodeType.Pdf417))
        {
            // Iterate through all detected barcodes (should be only one in this case).
            foreach (var result in reader.ReadBarCodes())
            {
                // Output basic barcode information.
                Console.WriteLine($"Detected Barcode Type: {result.CodeTypeName}");
                Console.WriteLine($"Decoded Text: {result.CodeText}");

                // The recognition API does not expose a direct IsReaderInitialization property.
                // Therefore we cannot programmatically confirm the flag via the reader.
                // The presence of initialization instructions is inferred from the generation setting.
                Console.WriteLine("IsReaderInitialization flag is not available via the recognition API.");
                Console.WriteLine("Assuming the barcode was generated with IsReaderInitialization = true.");
                Console.WriteLine();
            }
        }

        // ------------------------------------------------------------
        // Clean up the generated image file (optional).
        // ------------------------------------------------------------
        try
        {
            File.Delete(imagePath);
        }
        catch
        {
            // Ignore any errors that occur during cleanup.
        }
    }
}