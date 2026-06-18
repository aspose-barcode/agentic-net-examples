using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a QR code, reading it, and cleaning up the temporary file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a QR code image, reads it back, displays the decoded information,
    /// and then deletes the temporary file.
    /// </summary>
    static void Main()
    {
        // Define the path for a temporary PNG file that will hold the sample QR code.
        string tempFile = Path.Combine(Path.GetTempPath(), "sample_qr.png");

        // Generate a QR code image with the text "Hello Aspose" and save it to the temporary file.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello Aspose"))
        {
            generator.Save(tempFile);
        }

        // Verify that the file was successfully created before attempting to read it.
        if (!File.Exists(tempFile))
        {
            Console.WriteLine("Failed to create the sample barcode image.");
            return;
        }

        // Open the generated image and create a reader that only looks for 2D barcodes.
        using (var bitmap = new Bitmap(tempFile))
        using (var reader = new BarCodeReader(bitmap, DecodeType.Types2D))
        {
            // Iterate through all detected barcodes and output their type and decoded text.
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                Console.WriteLine($"Decoded Text : {result.CodeText}");
            }
        }

        // Attempt to delete the temporary file; ignore any exceptions as the OS will clean up later.
        try
        {
            File.Delete(tempFile);
        }
        catch
        {
            // No action needed – the file will be removed by the operating system eventually.
        }
    }
}