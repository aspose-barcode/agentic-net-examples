using System;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Sample data for multiple Code128 barcodes
        string[] codes = { "ABC123", "987XYZ", "CODE128TEST" };

        for (int i = 0; i < codes.Length; i++)
        {
            // Create a Code128 barcode generator
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
            {
                // Set the text to encode
                generator.CodeText = codes[i];

                // Ensure the human‑readable text is visible
                // (Location set to Below makes the text appear; default is Below, but we set it explicitly)
                generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;

                // Save the barcode image
                string fileName = $"code128_{i + 1}.png";
                generator.Save(fileName);
                Console.WriteLine($"Saved {fileName}");
            }
        }
    }
}