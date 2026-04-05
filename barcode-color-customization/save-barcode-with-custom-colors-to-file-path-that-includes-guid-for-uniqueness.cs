using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Generate a unique file name using a GUID
        string fileName = Path.Combine(Directory.GetCurrentDirectory(), Guid.NewGuid().ToString() + ".png");

        // Create a barcode generator for Code128 with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            generator.CodeText = "1234567890";

            // Set custom foreground (bars) and background colors
            generator.Parameters.Barcode.BarColor = Color.Blue;
            generator.Parameters.BackColor = Color.Yellow;

            // Save the barcode image to the unique file path
            generator.Save(fileName);
        }

        Console.WriteLine("Barcode saved to: " + fileName);
    }
}