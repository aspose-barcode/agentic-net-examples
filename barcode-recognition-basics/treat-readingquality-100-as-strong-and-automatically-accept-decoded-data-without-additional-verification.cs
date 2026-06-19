using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a QR code barcode, saving it to a file,
/// reading it back, and displaying the detection results.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a QR code, reads it, outputs confidence information,
    /// and cleans up the temporary image file.
    /// </summary>
    static void Main()
    {
        // Define a temporary file path for the generated barcode image
        string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "sample_barcode.png");

        // Generate a QR code barcode with the text "Hello World" and save it to the file
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello World"))
        {
            generator.Save(imagePath);
        }

        // Verify that the image file was successfully created
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Read the barcode from the image using all supported decode types
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Iterate through all detected barcodes in the image
            foreach (var result in reader.ReadBarCodes())
            {
                // Retrieve the reading quality (confidence) of the detection
                double quality = result.ReadingQuality; // ReadingQuality is a double

                // If quality is 100, treat it as strong confidence and accept automatically
                if (quality == 100.0)
                {
                    Console.WriteLine($"Accepted (Strong confidence): {result.CodeText}");
                }
                else
                {
                    Console.WriteLine($"Detected (Quality {quality}): {result.CodeText}");
                }
            }
        }

        // Attempt to delete the temporary image file; ignore any errors during cleanup
        try
        {
            File.Delete(imagePath);
        }
        catch
        {
            // No action needed if deletion fails
        }
    }
}