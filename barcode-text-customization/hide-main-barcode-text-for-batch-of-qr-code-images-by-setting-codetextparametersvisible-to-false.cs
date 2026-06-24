using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating QR code images without human‑readable text using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a set of QR code images and saves them to an output folder.
    /// </summary>
    static void Main()
    {
        // Determine the output directory path relative to the current working directory.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "output");

        // Ensure the output directory exists; create it if it does not.
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Define the sample texts to encode into QR codes.
        string[] texts = { "Sample1", "Sample2", "Sample3", "Sample4", "Sample5" };

        // Iterate over each text, generate a QR code, and save it as a PNG file.
        for (int i = 0; i < texts.Length; i++)
        {
            // Current text to encode.
            string text = texts[i];

            // Build the file name using a 1‑based index.
            string filePath = Path.Combine(outputDir, $"qr_{i + 1}.png");

            // Create a QR code generator with the specified text.
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, text))
            {
                // Suppress the display of human‑readable text beneath the barcode.
                generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;

                // Save the generated QR code image to the file system.
                generator.Save(filePath);
            }

            // Output the location of the generated file to the console.
            Console.WriteLine($"Generated QR code without text: {filePath}");
        }
    }
}