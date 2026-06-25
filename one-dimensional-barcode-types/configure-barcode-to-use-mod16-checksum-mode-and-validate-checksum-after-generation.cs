using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a Codabar barcode with Mod16 checksum,
/// saving it to a temporary file, reading it back with checksum validation,
/// and cleaning up the temporary file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode, validates it, and outputs results to the console.
    /// </summary>
    static void Main()
    {
        // Define temporary file path for the generated barcode image
        string tempPath = Path.Combine(Path.GetTempPath(), "codabar_mod16.png");

        // Generate a Codabar barcode with Mod16 checksum mode
        using (var generator = new BarcodeGenerator(EncodeTypes.Codabar, "A123456B"))
        {
            // Configure the generator to use Mod16 checksum for Codabar
            generator.Parameters.Barcode.Codabar.ChecksumMode = CodabarChecksumMode.Mod16;

            // Save the generated barcode image to the temporary file
            generator.Save(tempPath);
        }

        // Verify that the barcode image file was successfully created
        if (!File.Exists(tempPath))
        {
            Console.WriteLine("Failed to generate the barcode image.");
            return;
        }

        // Initialize a barcode reader for the saved image, specifying Codabar as the decode type
        using (var reader = new BarCodeReader(tempPath, DecodeType.Codabar))
        {
            // Enable checksum validation during reading
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

            bool anyFound = false;

            // Iterate through all detected barcodes in the image
            foreach (var result in reader.ReadBarCodes())
            {
                anyFound = true;
                // Output the decoded text of the barcode
                Console.WriteLine($"Detected CodeText: {result.CodeText}");
                // Output the Mod16 checksum value (available in OneD extended parameters)
                Console.WriteLine($"Checksum (Mod16): {result.Extended.OneD.CheckSum}");
            }

            // If no barcodes were detected, inform the user
            if (!anyFound)
            {
                Console.WriteLine("No barcode was recognized.");
            }
        }

        // Attempt to delete the temporary image file; ignore any errors that occur
        try
        {
            File.Delete(tempPath);
        }
        catch
        {
            // Suppress any exceptions during cleanup
        }
    }
}