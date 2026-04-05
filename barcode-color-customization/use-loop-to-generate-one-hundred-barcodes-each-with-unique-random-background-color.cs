using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Random number generator for colors and code texts
        Random rnd = new Random();

        // Generate 100 barcodes
        for (int i = 1; i <= 100; i++)
        {
            // Create a unique code text for each barcode
            string codeText = $"CODE{i:D3}";

            // Generate a random background color
            int r = rnd.Next(0, 256);
            int g = rnd.Next(0, 256);
            int b = rnd.Next(0, 256);
            Color bgColor = Color.FromArgb(r, g, b);

            // Create the barcode generator with Code128 symbology
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Set the random background color
                generator.Parameters.BackColor = bgColor;

                // Optional: set a fixed foreground (bar) color for visibility
                generator.Parameters.Barcode.BarColor = Color.Black;

                // Save the barcode image to a PNG file
                string fileName = $"barcode_{i:D3}.png";
                generator.Save(fileName);
            }
        }
    }
}