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
        // UPC‑A with GS1 DataBar coupon codetext (example from documentation)
        const string barcodeText = "514141100906(8110)106141416543213500110000310123196000";

        // Create the barcode generator for the specific symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.UpcaGs1DatabarCoupon, barcodeText))
        {
            // Optional visual settings
            generator.Parameters.Barcode.BarColor = Color.Black;
            generator.Parameters.BackColor = Color.White;
            generator.Parameters.Barcode.XDimension.Point = 2f; // narrow bar width

            // Generate the barcode image in memory
            using (Bitmap barcodeImage = generator.GenerateBarCodeImage())
            {
                // Path to the product label image (replace with your actual file)
                const string labelPath = "product_label.png";

                // Load the label image if it exists; otherwise create a placeholder
                Image labelImage;
                if (File.Exists(labelPath))
                {
                    labelImage = Image.FromFile(labelPath);
                }
                else
                {
                    // Create a simple white placeholder (400x300 pixels)
                    labelImage = new Bitmap(400, 300);
                    using (Graphics g = Graphics.FromImage(labelImage))
                    {
                        g.Clear(Color.White);
                    }
                }

                // Ensure the label image is disposed after processing
                using (labelImage)
                {
                    // Draw the barcode onto the label at a chosen position
                    using (Graphics graphics = Graphics.FromImage(labelImage))
                    {
                        // Position (50,50) – adjust as needed
                        graphics.DrawImage(
                            barcodeImage,
                            new Rectangle(50, 50, barcodeImage.Width, barcodeImage.Height));
                    }

                    // Save the merged result
                    const string outputPath = "merged_label.png";
                    labelImage.Save(outputPath, ImageFormat.Png);
                    Console.WriteLine($"Merged image saved to: {outputPath}");
                }
            }
        }
    }
}