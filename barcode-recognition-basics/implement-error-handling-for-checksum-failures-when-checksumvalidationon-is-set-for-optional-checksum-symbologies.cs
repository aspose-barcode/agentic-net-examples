// Title: Code39 Barcode Generation and Checksum Validation
// Description: Generates a Code39 barcode and demonstrates error handling when checksum validation is enabled for optional checksum symbologies.
// Prompt: Implement error handling for checksum failures when ChecksumValidation.On is set for optional checksum symbologies.
// Tags: barcode symbology, generation, recognition, checksum validation, code39, aspnet

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates creating a Code39 barcode, saving it to a file, and reading it back with checksum validation enabled.
/// Includes error handling for checksum failures on optional checksum symbologies.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode image, verifies its creation, and attempts to read it with strict checksum validation.
    /// </summary>
    static void Main()
    {
        // Define file path for the generated barcode image
        string barcodePath = "code39.png";

        // Generate a Code39 barcode (checksum is optional for this symbology)
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39, "ABC"))
        {
            // Optional: set visual parameters for the barcode
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Save the generated barcode image to the specified path
            generator.Save(barcodePath);
        }

        // Verify that the barcode image was successfully created
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine($"Failed to create barcode image at '{barcodePath}'.");
            return;
        }

        // Attempt to read the barcode with checksum validation enabled
        try
        {
            using (var reader = new BarCodeReader(barcodePath, DecodeType.Code39))
            {
                // Enable strict checksum validation for optional checksum symbologies
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

                // Read all barcodes from the image
                var results = reader.ReadBarCodes();

                // If no results are returned, treat it as a checksum failure or unreadable barcode
                if (results == null || results.Length == 0)
                {
                    Console.WriteLine("No barcode detected. This may be due to checksum validation failure.");
                }
                else
                {
                    // Iterate through each detected barcode and display its details
                    foreach (var result in results)
                    {
                        Console.WriteLine($"BarCode Type: {result.CodeTypeName}");
                        Console.WriteLine($"BarCode CodeText: {result.CodeText}");

                        // For 1D barcodes, extended data may contain checksum information
                        if (result.Extended?.OneD != null)
                        {
                            Console.WriteLine($"BarCode Value: {result.Extended.OneD.Value}");
                            Console.WriteLine($"BarCode Checksum: {result.Extended.OneD.CheckSum}");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Handle any exceptions that occur during reading (e.g., checksum validation errors)
            Console.WriteLine($"Error during barcode recognition: {ex.Message}");
        }
    }
}