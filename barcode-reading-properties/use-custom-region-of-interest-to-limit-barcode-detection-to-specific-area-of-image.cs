using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Generate a barcode image and keep it in memory.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "ABC123"))
        {
            // Optional: set image size.
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 150f;

            // Save the barcode to a memory stream in PNG format.
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0;

                // Load the image into an Aspose.Drawing.Bitmap.
                using (var bitmap = new Bitmap(ms))
                {
                    // Define a region of interest (ROI) that limits detection.
                    // Here we use a small rectangle at the top‑left corner.
                    var roi = new Rectangle(0, 0, 50, 50);

                    // Initialize the barcode reader.
                    using (var reader = new BarCodeReader())
                    {
                        // Restrict recognition to the ROI.
                        reader.SetBarCodeImage(bitmap, roi);
                        // Specify which barcode type to look for.
                        reader.SetBarCodeReadType(DecodeType.Code128);

                        // Perform barcode detection.
                        foreach (BarCodeResult result in reader.ReadBarCodes())
                        {
                            Console.WriteLine("Detected CodeText: " + result.CodeText);
                            var rect = result.Region.Rectangle;
                            Console.WriteLine($"Region: X={rect.X}, Y={rect.Y}, Width={rect.Width}, Height={rect.Height}");
                        }

                        // Inform if nothing was found within the ROI.
                        if (reader.FoundCount == 0)
                        {
                            Console.WriteLine("No barcode detected within the specified region.");
                        }
                    }
                }
            }
        }
    }
}