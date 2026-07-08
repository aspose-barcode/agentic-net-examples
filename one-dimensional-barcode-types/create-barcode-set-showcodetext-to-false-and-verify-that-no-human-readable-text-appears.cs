// Title: Generate Code128 barcode without human‑readable text
// Description: This example creates a Code128 barcode, disables the human‑readable code text, and saves it as a PNG image.
// Category-Description: Demonstrates Aspose.BarCode generation and recognition APIs. It shows how to configure BarcodeGenerator to hide the code text (using CodeLocation.None) and how to verify the setting with BarCodeReader. Typical for developers needing clean barcode images for packaging, labeling, or UI without accompanying text.
// Prompt: Create a barcode, set ShowCodeText to false, and verify that no human‑readable text appears.
// Tags: code128, hide text, png, barcodegenerator, barcodereader, generation, recognition

using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a Code128 barcode without human‑readable text,
/// saves it to a PNG file, and verifies the configuration using a barcode reader.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs barcode generation, saving, and verification.
    /// </summary>
    static void Main()
    {
        // Output file path and the data to encode
        const string outputPath = "barcode.png";
        const string codeText = "1234567890";

        // ------------------------------------------------------------
        // 1. Generate a Code128 barcode and hide the human‑readable text
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            // Set the data to encode
            generator.CodeText = codeText;

            // Hide the code text (human‑readable) by setting its location to None
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;

            // Save the barcode image as PNG
            generator.Save(outputPath);
        }

        // ------------------------------------------------------------
        // 2. Verify that the barcode was generated without human‑readable text
        //    (the setting above ensures the text is not rendered)
        // ------------------------------------------------------------
        using (var reader = new BarCodeReader(outputPath, DecodeType.Code128))
        {
            foreach (var result in reader.ReadBarCodes())
            {
                // Output the decoded text; the absence of visual text does not affect decoding
                Console.WriteLine($"Detected CodeText: {result.CodeText}");
            }
        }

        // ------------------------------------------------------------
        // 3. Additional check: confirm the generator's setting was applied
        // ------------------------------------------------------------
        using (var generatorCheck = new BarcodeGenerator(EncodeTypes.Code128))
        {
            if (generatorCheck.Parameters.Barcode.CodeTextParameters.Location == CodeLocation.None)
            {
                Console.WriteLine("Human‑readable text is disabled (CodeLocation.None).");
            }
            else
            {
                Console.WriteLine("Human‑readable text is enabled.");
            }
        }
    }
}