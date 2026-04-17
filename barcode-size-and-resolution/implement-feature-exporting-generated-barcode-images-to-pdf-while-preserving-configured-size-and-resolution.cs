using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Define output PDF file path
        string outputPdf = "barcode.pdf";

        // Create a barcode generator for Code128 with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Preserve size: set explicit image width and height (points)
            generator.Parameters.ImageWidth.Point = 300f;   // 300 points (~4.17 inches)
            generator.Parameters.ImageHeight.Point = 150f; // 150 points (~2.08 inches)

            // Preserve resolution: set to 300 DPI
            generator.Parameters.Resolution = 300f;

            // Disable automatic sizing to keep the dimensions we set
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;

            // Optional: set barcode colors
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Save directly as PDF; the format is inferred from the file extension
            generator.Save(outputPdf);
        }

        Console.WriteLine($"Barcode PDF generated at: {outputPdf}");
    }
}