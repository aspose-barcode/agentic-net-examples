using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a barcode image, reading it back, and cleaning up the temporary file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a Code128 barcode, saves it to a temporary file, reads it back,
    /// displays detection results, and finally deletes the temporary file.
    /// </summary>
    static void Main()
    {
        // Define the path for the temporary barcode image.
        string barcodePath = Path.Combine(Path.GetTempPath(), "sample_barcode.png");

        // ------------------------------------------------------------
        // Generate a sample barcode image and save it to the temporary path.
        // ------------------------------------------------------------
        try
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456789012"))
            {
                // Optional: configure generator settings such as resolution here.
                generator.Save(barcodePath);
                Console.WriteLine($"Barcode image saved to: {barcodePath}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to generate barcode: {ex.Message}");
            return;
        }

        // Verify that the image file exists before attempting to read it.
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Barcode image file does not exist.");
            return;
        }

        // ------------------------------------------------------------
        // Read the barcode from the saved image with error handling.
        // ------------------------------------------------------------
        try
        {
            using (var reader = new BarCodeReader(barcodePath, DecodeType.AllSupportedTypes))
            {
                // Enable checksum validation (optional).
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;
                // Allow recognition of barcodes with incorrect checksum or damaged data.
                reader.QualitySettings.AllowIncorrectBarcodes = true;

                // Perform the barcode detection.
                BarCodeResult[] results = reader.ReadBarCodes();

                // Check if any barcodes were detected.
                if (results == null || results.Length == 0)
                {
                    Console.WriteLine("No barcodes were detected in the image.");
                }
                else
                {
                    // Iterate through each detected barcode and display its details.
                    foreach (var result in results)
                    {
                        // Handle cases where the decoded text is missing or incomplete.
                        if (string.IsNullOrEmpty(result.CodeText))
                        {
                            Console.WriteLine($"Detected barcode of type '{result.CodeTypeName}' but CodeText is missing or incomplete.");
                        }
                        else
                        {
                            Console.WriteLine($"Detected barcode type: {result.CodeTypeName}");
                            Console.WriteLine($"CodeText: {result.CodeText}");
                            Console.WriteLine($"Confidence: {result.Confidence}");
                            Console.WriteLine($"ReadingQuality: {result.ReadingQuality}");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during barcode decoding: {ex.Message}");
        }
        finally
        {
            // ------------------------------------------------------------
            // Clean up the temporary file regardless of success or failure.
            // ------------------------------------------------------------
            try
            {
                if (File.Exists(barcodePath))
                {
                    File.Delete(barcodePath);
                }
            }
            catch
            {
                // Ignored - cleanup failure should not affect program flow.
            }
        }
    }
}