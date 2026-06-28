using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates barcode generation, saving, and validation using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a barcode image, saves it to the specified file, and validates that the saved image
    /// contains the expected encoded text.
    /// </summary>
    /// <param name="encodeType">The type of barcode to generate.</param>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <param name="fileName">The full path where the barcode image will be saved.</param>
    static void GenerateAndValidate(BaseEncodeType encodeType, string codeText, string fileName)
    {
        try
        {
            // Ensure the output directory exists before attempting to save the file.
            string directory = Path.GetDirectoryName(fileName);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            // Create a barcode generator, configure image size, and save the barcode image.
            using (var generator = new BarcodeGenerator(encodeType, codeText))
            {
                // Set a modest image size for consistency across different barcode types.
                generator.Parameters.ImageWidth.Point = 300f;
                generator.Parameters.ImageHeight.Point = 150f;
                generator.Save(fileName);
            }

            // Verify that the file was successfully created.
            if (!File.Exists(fileName))
            {
                Console.WriteLine($"[FAIL] File not created: {fileName}");
                return;
            }

            // Read the saved barcode image and attempt to decode it.
            using (var reader = new BarCodeReader(fileName, DecodeType.AllSupportedTypes))
            {
                var results = reader.ReadBarCodes();

                // If no barcodes were detected, report failure.
                if (results.Length == 0)
                {
                    Console.WriteLine($"[FAIL] No barcode detected in {Path.GetFileName(fileName)}");
                    return;
                }

                // Check whether any decoded barcode matches the original text.
                bool matchFound = false;
                foreach (var result in results)
                {
                    if (result.CodeText == codeText)
                    {
                        matchFound = true;
                        break;
                    }
                }

                // Output the validation result.
                if (matchFound)
                {
                    Console.WriteLine($"[PASS] {Path.GetFileName(fileName)} - Type: {encodeType.GetType().Name}, CodeText: \"{codeText}\"");
                }
                else
                {
                    Console.WriteLine($"[FAIL] {Path.GetFileName(fileName)} - Expected \"{codeText}\", but got different value.");
                }
            }
        }
        catch (Exception ex)
        {
            // Report any unexpected errors that occur during generation or validation.
            Console.WriteLine($"[ERROR] {Path.GetFileName(fileName)} - {ex.Message}");
        }
    }

    /// <summary>
    /// Application entry point. Executes a series of barcode generation and validation tests.
    /// </summary>
    static void Main()
    {
        // Define a small set of barcode types, associated text, and output file paths for testing.
        var tests = new (BaseEncodeType type, string text, string file)[]
        {
            (EncodeTypes.Code128, "Test123ABC", "Barcodes/Code128.png"),
            (EncodeTypes.QR, "https://example.com", "Barcodes/QR.png"),
            (EncodeTypes.DataMatrix, "DM12345", "Barcodes/DataMatrix.png"),
            (EncodeTypes.Pdf417, "PDF417 Sample Text", "Barcodes/Pdf417.png"),
            (EncodeTypes.EAN13, "1234567890128", "Barcodes/EAN13.png")
        };

        // Iterate over each test case, generating and validating the corresponding barcode.
        foreach (var (type, text, file) in tests)
        {
            GenerateAndValidate(type, text, file);
        }

        Console.WriteLine("Barcode generation and validation completed.");
    }
}