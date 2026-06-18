using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a Code128 barcode with checksum, saving it to a temporary file,
/// reading it back with checksum validation, and cleaning up the temporary file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define a temporary file path for the barcode image.
        string imagePath = Path.Combine(Path.GetTempPath(), "code128.png");

        // ------------------------------------------------------------
        // Generate a Code128 barcode with checksum enabled and save it.
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Enable checksum (required for Code128).
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

            // Save the barcode image as PNG to the temporary path.
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the image file was successfully created.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // ------------------------------------------------------------
        // Read the barcode from the image and validate its checksum.
        // ------------------------------------------------------------
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            // Turn on checksum validation during recognition.
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

            // Attempt to read barcodes from the image.
            BarCodeResult[] results = reader.ReadBarCodes();

            // Check if any barcodes were detected.
            if (results == null || results.Length == 0)
            {
                Console.WriteLine("No barcode detected or checksum validation failed.");
            }
            else
            {
                // Iterate through each detected barcode and display details.
                foreach (var result in results)
                {
                    Console.WriteLine($"Detected Code128 barcode:");
                    Console.WriteLine($"  CodeText: {result.CodeText}");

                    // For 1D barcodes, checksum information is available in Extended.OneD.
                    if (result.Extended?.OneD != null)
                    {
                        Console.WriteLine($"  CheckSum: {result.Extended.OneD.CheckSum}");
                    }
                }
            }
        }

        // ------------------------------------------------------------
        // Clean up the temporary image file.
        // ------------------------------------------------------------
        try
        {
            File.Delete(imagePath);
        }
        catch
        {
            // Ignore any errors during cleanup.
        }
    }
}