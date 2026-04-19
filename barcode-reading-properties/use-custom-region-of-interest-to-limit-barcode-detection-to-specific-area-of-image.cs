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
        // Generate a sample barcode image in memory
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Optional: set image size
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 150f;

            using (Bitmap barcodeImage = generator.GenerateBarCodeImage())
            {
                // Define a custom region of interest (ROI) – a rectangle slightly inset from the image borders
                Rectangle roi = new Rectangle(10, 10, barcodeImage.Width - 20, barcodeImage.Height - 20);

                // Initialize the reader and assign the image with the ROI
                using (var reader = new BarCodeReader())
                {
                    reader.SetBarCodeImage(barcodeImage, roi);
                    // Optionally limit decode types; here we allow all supported types
                    reader.BarCodeReadType = DecodeType.AllSupportedTypes;

                    // Perform recognition
                    BarCodeResult[] results = reader.ReadBarCodes();

                    if (results.Length == 0)
                    {
                        Console.WriteLine("No barcodes detected within the specified region.");
                    }
                    else
                    {
                        foreach (BarCodeResult result in results)
                        {
                            Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                            Console.WriteLine($"Code Text: {result.CodeText}");
                            // Output the bounding rectangle of the detected barcode
                            var rect = result.Region.Rectangle;
                            Console.WriteLine($"Region - X:{rect.X}, Y:{rect.Y}, Width:{rect.Width}, Height:{rect.Height}");
                        }
                    }
                }
            }
        }
    }
}