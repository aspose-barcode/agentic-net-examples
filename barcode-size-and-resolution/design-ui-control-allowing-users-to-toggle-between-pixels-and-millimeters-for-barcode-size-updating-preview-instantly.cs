using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Generate barcode using pixel units
        GenerateAndSaveBarcode(
            unitName: "Pixels",
            setSize: (gen) =>
            {
                gen.Parameters.ImageWidth.Pixels = 300f;
                gen.Parameters.ImageHeight.Pixels = 150f;
                gen.Parameters.Barcode.BarHeight.Pixels = 50f;
            },
            fileName: "barcode_pixels.png");

        // Generate barcode using millimeter units
        GenerateAndSaveBarcode(
            unitName: "Millimeters",
            setSize: (gen) =>
            {
                gen.Parameters.ImageWidth.Millimeters = 80f;
                gen.Parameters.ImageHeight.Millimeters = 40f;
                gen.Parameters.Barcode.BarHeight.Millimeters = 12f;
            },
            fileName: "barcode_mm.png");
    }

    static void GenerateAndSaveBarcode(string unitName, Action<BarcodeGenerator> setSize, string fileName)
    {
        // Create a barcode generator for Code128 with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Apply unit-specific size settings
            setSize(generator);

            // Optional: set background and bar colors
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;

            // Generate the barcode image
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Save the image to file
                bitmap.Save(fileName, ImageFormat.Png);
            }
        }

        Console.WriteLine($"{unitName} barcode saved as {fileName}");
    }
}