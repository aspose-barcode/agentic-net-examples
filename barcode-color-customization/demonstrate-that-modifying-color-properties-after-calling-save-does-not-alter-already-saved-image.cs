using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Create a barcode, set initial colors, and save it.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            generator.CodeText = "123ABC";
            generator.Parameters.Barcode.BarColor = Color.Red;          // Bars in red
            generator.Parameters.BackColor = Color.Yellow;            // Background in yellow

            string firstFile = "barcode1.png";
            generator.Save(firstFile);                                 // Save first image

            // Modify colors after the first save.
            generator.Parameters.Barcode.BarColor = Color.Blue;       // Bars now blue
            generator.Parameters.BackColor = Color.LightGray;         // Background now light gray

            string secondFile = "barcode2.png";
            generator.Save(secondFile);                                // Save second image
        }

        // Load both images to demonstrate that the first one kept its original colors.
        using (var img1 = new Bitmap("barcode1.png"))
        using (var img2 = new Bitmap("barcode2.png"))
        {
            // Sample pixel check (top‑left corner) – just for illustration.
            var pixel1 = img1.GetPixel(0, 0);
            var pixel2 = img2.GetPixel(0, 0);

            Console.WriteLine($"First image top‑left pixel ARGB: {pixel1.ToArgb():X8}");
            Console.WriteLine($"Second image top‑left pixel ARGB: {pixel2.ToArgb():X8}");
            Console.WriteLine("The first image retains its original colors despite later changes.");
        }
    }
}