using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a Code39 barcode with checksum enabled,
/// then reading it back with checksum validation using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a temporary barcode image, validates it, and cleans up.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare temporary file paths
        // --------------------------------------------------------------------
        string tempDir = Path.GetTempPath();                                   // System temporary directory
        string barcodePath = Path.Combine(tempDir, "code39.png");               // Full path for the barcode image

        // --------------------------------------------------------------------
        // Generate a Code39 barcode with checksum enabled
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39FullASCII, "12345"))
        {
            // Enable checksum for optional checksum symbology
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

            // Save the generated barcode image to the temporary path
            generator.Save(barcodePath);
        }

        // --------------------------------------------------------------------
        // Verify that the barcode image was created successfully
        // --------------------------------------------------------------------
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // --------------------------------------------------------------------
        // Attempt to read the barcode with checksum validation turned on
        // --------------------------------------------------------------------
        try
        {
            using (var reader = new BarCodeReader(barcodePath, DecodeType.Code39FullASCII))
            {
                // Enable checksum validation during recognition
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

                // Read all barcodes from the image
                BarCodeResult[] results = reader.ReadBarCodes();

                if (results.Length == 0)
                {
                    // No results – treat this as a checksum validation failure
                    Console.WriteLine("Checksum validation failed: no barcode detected.");
                }
                else
                {
                    // Iterate through each detected barcode and display details
                    foreach (var result in results)
                    {
                        Console.WriteLine($"Detected barcode type: {result.CodeTypeName}");
                        Console.WriteLine($"CodeText: {result.CodeText}");

                        // For 1D barcodes, the checksum value is available in the extended data
                        if (result.Extended?.OneD != null)
                        {
                            Console.WriteLine($"Checksum: {result.Extended.OneD.CheckSum}");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Handle any unexpected errors (including checksum-related exceptions)
            Console.WriteLine($"Error during barcode recognition: {ex.Message}");
        }
        finally
        {
            // --------------------------------------------------------------------
            // Clean up the temporary barcode image
            // --------------------------------------------------------------------
            if (File.Exists(barcodePath))
            {
                try
                {
                    File.Delete(barcodePath);
                }
                catch
                {
                    // Ignore cleanup errors
                }
            }
        }
    }
}