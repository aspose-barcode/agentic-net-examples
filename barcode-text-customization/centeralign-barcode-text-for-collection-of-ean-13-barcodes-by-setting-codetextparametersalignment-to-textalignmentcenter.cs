using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating EAN‑13 barcode images using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates barcode images for a set of sample EAN‑13 codes.
    /// </summary>
    static void Main()
    {
        // Define an array of sample EAN‑13 codes (including checksum digit)
        string[] ean13Codes = new string[]
        {
            "5901234123457",
            "4006381333931",
            "73513537" + "12345", // placeholder, ensure 13 digits
            "9780306406157",
            "1234567890128"
        };

        // Iterate over each code to generate and save its barcode image
        for (int i = 0; i < ean13Codes.Length; i++)
        {
            // Current EAN‑13 code
            string code = ean13Codes[i];

            // Construct a file name for the output image (e.g., ean13_1.png)
            string fileName = $"ean13_{i + 1}.png";

            // Create and configure the barcode generator within a using block to ensure disposal
            using (var generator = new BarcodeGenerator(EncodeTypes.EAN13, code))
            {
                // Center‑align the human‑readable text beneath the barcode
                generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Center;

                // Save the generated barcode image to the specified file
                generator.Save(fileName);
            }

            // Output a confirmation message to the console
            Console.WriteLine($"Saved {fileName}");
        }
    }
}