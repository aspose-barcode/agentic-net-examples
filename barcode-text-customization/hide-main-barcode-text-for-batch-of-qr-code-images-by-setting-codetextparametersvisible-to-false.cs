// Title: Hide barcode text for batch QR code generation
// Description: Demonstrates generating multiple QR code images while suppressing the human‑readable text.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure barcode appearance using the BarcodeGenerator class. It shows typical use cases such as batch processing of QR codes where the visual text is not required, covering parameters like CodeTextParameters and AutoSizeMode. Developers often need to hide or customize barcode text for cleaner graphics or UI integration.
// Prompt: Hide main barcode text for a batch of QR code images by setting CodetextParameters.Visible to false.
// Tags: qr code, hide text, batch generation, aspnet, aspose.barcode, codetextparameters, autosizemode, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates a series of QR code images and hides the human‑readable text for each barcode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates QR codes from a predefined list of strings,
    /// disables the visible code text, and saves each image to the Output folder.
    /// </summary>
    static void Main()
    {
        // Define sample texts to encode into QR codes.
        string[] texts = { "Hello", "World", "Aspose", "Barcode", "QR Code" };

        // Ensure the output directory exists; create it if necessary.
        string outputDir = "Output";
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Iterate over each text value and generate a QR code without visible text.
        for (int i = 0; i < texts.Length; i++)
        {
            // Initialize the barcode generator for QR encoding with the current text.
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, texts[i]))
            {
                // Hide the main barcode text by setting its location to None.
                // (Equivalent to setting CodeTextParameters.Visible = false.)
                generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;

                // Use interpolation auto‑size mode to keep image dimensions consistent.
                generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

                // Build the full file path for the output PNG image.
                string filePath = Path.Combine(outputDir, $"qr_{i + 1}.png");

                // Save the generated QR code image to disk.
                generator.Save(filePath);

                // Inform the user that the image has been saved.
                Console.WriteLine($"Saved QR code without text: {filePath}");
            }
        }
    }
}