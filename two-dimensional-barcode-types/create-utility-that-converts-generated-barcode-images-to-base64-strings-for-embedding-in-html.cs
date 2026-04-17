using System;
using System.IO;
using System.Text;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

namespace BarcodeToBase64Demo
{
    class Program
    {
        static void Main()
        {
            // Sample barcode text and type
            const string barcodeText = "123ABC";
            const string outputFileName = "barcode.png";

            // Generate barcode image
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, barcodeText))
            {
                // Optionally customize parameters here
                // e.g., generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Blue;

                // Create bitmap
                using (Bitmap bitmap = generator.GenerateBarCodeImage())
                {
                    // Save to a memory stream in PNG format
                    using (var ms = new MemoryStream())
                    {
                        bitmap.Save(ms, ImageFormat.Png);
                        byte[] imageBytes = ms.ToArray();

                        // Convert to Base64 string
                        string base64 = Convert.ToBase64String(imageBytes);

                        // Output HTML img tag with embedded Base64 data
                        Console.WriteLine("<img src=\"data:image/png;base64,{0}\" alt=\"Barcode\" />", base64);

                        // Also save the image to a file for verification (optional)
                        File.WriteAllBytes(outputFileName, imageBytes);
                    }
                }
            }
        }
    }
}