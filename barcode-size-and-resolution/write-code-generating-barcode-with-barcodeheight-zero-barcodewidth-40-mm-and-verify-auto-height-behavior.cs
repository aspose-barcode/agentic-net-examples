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
        const string outputPath = "barcode.png";

        // Create a barcode generator for Code128 with sample text
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Enable auto‑size mode that uses ImageWidth/ImageHeight
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Set desired barcode image width to 40 mm
            generator.Parameters.ImageWidth.Millimeters = 40f;

            // Optionally set a high resolution for better accuracy
            generator.Parameters.Resolution = 300f; // dpi

            // Generate the barcode image
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Save the image to file
                bitmap.Save(outputPath, ImageFormat.Png);
            }
        }

        // Verify the resulting image height (auto‑height behavior)
        if (File.Exists(outputPath))
        {
            using (Image img = Image.FromFile(outputPath))
            {
                int heightPixels = img.Height;
                float dpi = img.HorizontalResolution; // assume square pixels
                float heightMillimeters = heightPixels * 25.4f / dpi;

                Console.WriteLine($"Generated barcode saved to '{Path.GetFullPath(outputPath)}'.");
                Console.WriteLine($"Image size: {img.Width} px × {heightPixels} px");
                Console.WriteLine($"Image height: {heightMillimeters:F2} mm (auto‑height)");
            }
        }
        else
        {
            Console.WriteLine("Failed to generate the barcode image.");
        }
    }
}