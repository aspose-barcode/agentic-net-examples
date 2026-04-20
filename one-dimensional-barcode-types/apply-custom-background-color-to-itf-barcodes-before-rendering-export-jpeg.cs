using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Output file path
        string outputPath = "itf_barcode.jpg";

        // Create a barcode generator for ITF14
        using (var generator = new BarcodeGenerator(EncodeTypes.ITF14))
        {
            // Set the code text (numeric, even length)
            generator.CodeText = "123456789012";

            // Apply a custom background color
            generator.Parameters.BackColor = Color.LightGray;

            // Optional: set bar (foreground) color
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Save the barcode as JPEG
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        Console.WriteLine($"Barcode saved to {Path.GetFullPath(outputPath)}");
    }
}