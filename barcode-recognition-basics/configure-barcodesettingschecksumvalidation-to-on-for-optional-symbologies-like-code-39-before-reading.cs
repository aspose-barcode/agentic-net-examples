using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a Code39 barcode, saving it to a temporary file,
/// reading it back with checksum validation, and cleaning up the file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode, reads it, displays information, and deletes the temporary image.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // 1. Define the temporary file path for the generated barcode image.
        // --------------------------------------------------------------------
        string imagePath = Path.Combine(Path.GetTempPath(), "code39.png");

        // --------------------------------------------------------------------
        // 2. Set the barcode text (Code39 supports optional checksum).
        // --------------------------------------------------------------------
        string codeText = "ABC-123";

        // --------------------------------------------------------------------
        // 3. Generate a Code39 barcode and save it to the temporary file.
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39FullASCII, codeText))
        {
            // Optional: configure additional barcode appearance settings here.
            generator.Save(imagePath);
        }

        // --------------------------------------------------------------------
        // 4. Verify that the barcode image was successfully created.
        // --------------------------------------------------------------------
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Failed to create barcode image at {imagePath}");
            return;
        }

        // --------------------------------------------------------------------
        // 5. Read the barcode from the image with checksum validation enabled.
        // --------------------------------------------------------------------
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code39))
        {
            // Enable checksum validation for symbologies that support it (e.g., Code39).
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

            // Iterate through all detected barcodes in the image.
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                Console.WriteLine($"Code Text: {result.CodeText}");

                // For 1D barcodes, checksum information is available via the extended data.
                if (result.Extended?.OneD != null)
                {
                    Console.WriteLine($"Checksum Value: {result.Extended.OneD.CheckSum}");
                }
            }
        }

        // --------------------------------------------------------------------
        // 6. Clean up: delete the temporary barcode image file.
        // --------------------------------------------------------------------
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