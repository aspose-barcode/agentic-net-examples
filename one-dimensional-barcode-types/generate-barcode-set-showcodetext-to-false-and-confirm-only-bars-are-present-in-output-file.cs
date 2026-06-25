using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code128 barcode with hidden human‑readable text,
/// saving it to a file, and then verifying the barcode by reading it back.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode, saves it, and validates the saved image.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image.
        string outputPath = "barcode.png";

        // Set barcode encoding type and the text to encode.
        BaseEncodeType encodeType = EncodeTypes.Code128;
        string codeText = "123456";

        // Create a BarcodeGenerator instance with the specified type and text.
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Configure the generator to hide the human‑readable code text.
            // Only the barcode bars will be rendered in the image.
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;

            // Save the generated barcode image to the specified file.
            generator.Save(outputPath);
        }

        // Verify that the barcode image file was successfully created.
        if (!File.Exists(outputPath))
        {
            Console.WriteLine("Failed to create the barcode image.");
            return;
        }

        // Initialize a BarCodeReader to read and decode the saved barcode image.
        using (var reader = new BarCodeReader(outputPath, DecodeType.Code128))
        {
            // Read all barcodes found in the image.
            var results = reader.ReadBarCodes();

            // Check if at least one barcode was read and if its text matches the original.
            if (results.Length > 0 && results[0].CodeText == codeText)
            {
                Console.WriteLine("Barcode generated successfully. Code text is hidden (only bars present).");
            }
            else
            {
                Console.WriteLine("Barcode verification failed.");
            }
        }
    }
}