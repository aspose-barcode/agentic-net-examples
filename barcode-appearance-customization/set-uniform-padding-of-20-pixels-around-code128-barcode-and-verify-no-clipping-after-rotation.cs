using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        const string filePath = "barcode.png";

        // Create a Code128 barcode generator with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345ABC"))
        {
            // Set uniform padding of 20 pixels on all sides
            generator.Parameters.Barcode.Padding.Left.Point = 20f;
            generator.Parameters.Barcode.Padding.Top.Point = 20f;
            generator.Parameters.Barcode.Padding.Right.Point = 20f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 20f;

            // Rotate the barcode image by 90 degrees
            generator.Parameters.RotationAngle = 90f;

            // Save the barcode image
            generator.Save(filePath);
        }

        // Load the saved image to obtain its dimensions
        using (var image = Image.FromFile(filePath))
        {
            int imageWidth = image.Width;
            int imageHeight = image.Height;

            // Read the barcode from the saved image
            using (var reader = new BarCodeReader(filePath, DecodeType.Code128))
            {
                bool clippingDetected = false;

                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    // Get the bounding rectangle of the detected barcode
                    var rect = result.Region.Rectangle;

                    // Verify that the rectangle is fully inside the image bounds
                    if (rect.X < 0 || rect.Y < 0 ||
                        rect.Right > imageWidth || rect.Bottom > imageHeight)
                    {
                        clippingDetected = true;
                        Console.WriteLine("Clipping detected: barcode region exceeds image bounds.");
                    }
                    else
                    {
                        Console.WriteLine("No clipping: barcode region is within image bounds.");
                    }

                    Console.WriteLine($"Image Size: {imageWidth}x{imageHeight}");
                    Console.WriteLine($"Barcode Region: X={rect.X}, Y={rect.Y}, Width={rect.Width}, Height={rect.Height}");
                }

                if (!clippingDetected)
                {
                    Console.WriteLine("Verification passed – barcode is not clipped after rotation.");
                }
            }
        }
    }
}