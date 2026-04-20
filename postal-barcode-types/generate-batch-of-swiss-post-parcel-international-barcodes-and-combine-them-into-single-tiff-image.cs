using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Folder to store temporary barcode images (optional, can be in-memory)
        string outputFolder = "Barcodes";
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Sample Swiss Post Parcel International codetexts (replace with real data as needed)
        List<string> codeTexts = new List<string>
        {
            "1234567890123456",
            "2345678901234567",
            "3456789012345678",
            "4567890123456789",
            "5678901234567890"
        };

        // List to hold generated bitmap images
        List<Bitmap> barcodeImages = new List<Bitmap>();

        // Generate each barcode and keep the bitmap in memory
        foreach (string text in codeTexts)
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, text))
            {
                // Optional: set image size or resolution if needed
                generator.Parameters.AutoSizeMode = Aspose.BarCode.Generation.AutoSizeMode.None;
                generator.Parameters.ImageWidth.Point = 300f;
                generator.Parameters.ImageHeight.Point = 150f;
                generator.Parameters.Resolution = 300; // 300 DPI

                // Generate the barcode image
                Bitmap bmp = generator.GenerateBarCodeImage();
                barcodeImages.Add(bmp);
            }
        }

        // Determine combined image dimensions (stack vertically)
        int combinedWidth = 0;
        int combinedHeight = 0;
        foreach (var img in barcodeImages)
        {
            if (img.Width > combinedWidth) combinedWidth = img.Width;
            combinedHeight += img.Height;
        }

        // Create a new bitmap to hold all barcodes
        using (var combinedBitmap = new Bitmap(combinedWidth, combinedHeight))
        {
            using (var graphics = Graphics.FromImage(combinedBitmap))
            {
                graphics.Clear(Aspose.Drawing.Color.White);
                int offsetY = 0;
                foreach (var img in barcodeImages)
                {
                    graphics.DrawImage(img, 0, offsetY, img.Width, img.Height);
                    offsetY += img.Height;
                    img.Dispose(); // Dispose individual bitmap after drawing
                }
            }

            // Save the combined image as a TIFF file
            string combinedPath = Path.Combine(outputFolder, "SwissPostParcelBatch.tif");
            combinedBitmap.Save(combinedPath, ImageFormat.Tiff);
        }

        Console.WriteLine("Combined TIFF image created successfully.");
    }
}