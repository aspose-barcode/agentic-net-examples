using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Create Mailmark codetext with required fields
        var mailmark = new MailmarkCodetext
        {
            Format = 4,               // 4‑state format
            VersionID = 1,
            Class = "0",
            SupplychainID = 384224,
            ItemID = 16563762,
            DestinationPostCodePlusDPS = "EF61AH8T "
        };

        // Generate the complex Mailmark barcode
        using (ComplexBarcodeGenerator generator = new ComplexBarcodeGenerator(mailmark))
        {
            // Set a positive bar height
            generator.Parameters.Barcode.BarHeight.Point = 10f;

            // Set image size (required for complex barcode generation)
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 150f;

            // Save barcode image to a memory stream as PNG
            using (MemoryStream memoryStream = new MemoryStream())
            {
                generator.Save(memoryStream, BarCodeImageFormat.Png);
                byte[] imageBytes = memoryStream.ToArray();

                // Output the size of the generated image bytes
                Console.WriteLine($"Generated Mailmark barcode image bytes: {imageBytes.Length}");
            }
        }
    }
}