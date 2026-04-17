using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string codeText = "ABC1234567890";

        // Generate Code128 barcode with uniform 20‑pixel padding
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Set padding on all sides (20 points = 20 pixels)
            generator.Parameters.Barcode.Padding.Left.Point = 20f;
            generator.Parameters.Barcode.Padding.Top.Point = 20f;
            generator.Parameters.Barcode.Padding.Right.Point = 20f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 20f;

            // Create the barcode image
            using (Bitmap barcodeImage = generator.GenerateBarCodeImage())
            {
                // Rotate the image 90 degrees clockwise
                barcodeImage.RotateFlip(RotateFlipType.Rotate90FlipNone);

                // Verify that the rotated barcode can still be read (no clipping)
                using (var reader = new BarCodeReader(barcodeImage, DecodeType.Code128))
                {
                    bool found = false;
                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        Console.WriteLine($"Detected CodeText: {result.CodeText}");
                        if (result.CodeText == codeText)
                        {
                            found = true;
                        }
                    }

                    if (found)
                    {
                        Console.WriteLine("Verification succeeded: barcode is readable after rotation.");
                    }
                    else
                    {
                        Console.WriteLine("Verification failed: barcode could not be read after rotation.");
                    }
                }

                // Optionally save the rotated image for visual inspection
                barcodeImage.Save("rotated_code128.png", ImageFormat.Png);
            }
        }
    }
}