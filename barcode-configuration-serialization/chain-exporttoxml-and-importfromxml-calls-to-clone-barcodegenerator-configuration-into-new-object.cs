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
        // Create the original barcode generator with some custom settings
        using (var original = new BarcodeGenerator(EncodeTypes.Code128, "12345"))
        {
            original.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Blue;
            original.Parameters.BackColor = Aspose.Drawing.Color.Yellow;
            original.Parameters.Barcode.XDimension.Point = 2f;
            original.Parameters.ImageWidth.Point = 300f;
            original.Parameters.ImageHeight.Point = 150f;

            // Export the configuration to an in‑memory XML stream
            using (var xmlStream = new MemoryStream())
            {
                bool exported = original.ExportToXml(xmlStream);
                if (!exported)
                {
                    Console.WriteLine("Export to XML failed.");
                    return;
                }

                // Reset the stream position before reading
                xmlStream.Position = 0;

                // Import the configuration into a new generator (clone)
                using (var cloned = BarcodeGenerator.ImportFromXml(xmlStream))
                {
                    // Generate barcode images from both generators
                    using (var originalImage = original.GenerateBarCodeImage())
                    using (var clonedImage = cloned.GenerateBarCodeImage())
                    {
                        // Save the images to files for verification
                        originalImage.Save("original.png", ImageFormat.Png);
                        clonedImage.Save("cloned.png", ImageFormat.Png);
                    }
                }
            }
        }

        Console.WriteLine("Barcode images generated.");
    }
}