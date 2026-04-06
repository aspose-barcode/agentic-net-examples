using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Generate a Code128 barcode and save it
        using (var code128Gen = new BarcodeGenerator(EncodeTypes.Code128, "CODE128TEXT"))
        {
            code128Gen.Save("code128.png");
        }

        // Generate a DataMatrix barcode and save it
        using (var dmGen = new BarcodeGenerator(EncodeTypes.DataMatrix, "DATAMATRIX"))
        {
            dmGen.Save("datamatrix.png");
        }

        // Load the two barcode images
        using (var bmpCode128 = new Bitmap("code128.png"))
        using (var bmpDataMatrix = new Bitmap("datamatrix.png"))
        {
            // Create a combined image large enough for both barcodes side by side
            int combinedWidth = bmpCode128.Width + bmpDataMatrix.Width;
            int combinedHeight = Math.Max(bmpCode128.Height, bmpDataMatrix.Height);
            using (var combinedBmp = new Bitmap(combinedWidth, combinedHeight))
            {
                using (var graphics = Graphics.FromImage(combinedBmp))
                {
                    // Fill background with white
                    graphics.Clear(Color.White);
                    // Draw the first barcode on the left
                    graphics.DrawImage(bmpCode128, 0, 0);
                    // Draw the second barcode on the right
                    graphics.DrawImage(bmpDataMatrix, bmpCode128.Width, 0);
                }

                // Save the combined image
                combinedBmp.Save("combined.png", ImageFormat.Png);
            }
        }

        // Recognize both Code128 and DataMatrix barcodes from the combined image
        using (var reader = new BarCodeReader("combined.png", new MultiDecodeType(DecodeType.Code128, DecodeType.DataMatrix)))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine("BarCode Type: " + result.CodeTypeName);
                Console.WriteLine("BarCode CodeText: " + result.CodeText);
            }
        }
    }
}