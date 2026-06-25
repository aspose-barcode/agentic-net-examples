using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generation of Mailmark barcodes (filled and unfilled) and compares the resulting images pixel by pixel.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates two Mailmark barcode images (filled and unfilled), saves them to disk,
    /// and reports the number of differing pixels between the two images.
    /// </summary>
    static void Main()
    {
        // Define the Mailmark code text with required parameters.
        var mailmark = new MailmarkCodetext
        {
            Format = 4,
            VersionID = 1,
            Class = "0",
            SupplychainID = 384224,
            ItemID = 16563762,
            DestinationPostCodePlusDPS = "EF61AH8T "
        };

        // Output file names for the generated images.
        string filledPath = "mailmark_filled.png";
        string unfilledPath = "mailmark_unfilled.png";

        // Generate and save the barcode with filled bars.
        using (var generatorFilled = new ComplexBarcodeGenerator(mailmark))
        {
            generatorFilled.Save(filledPath);
        }

        // Generate and save the barcode with unfilled bars.
        using (var generatorUnfilled = new ComplexBarcodeGenerator(mailmark))
        {
            // Disable bar filling for the unfilled version.
            generatorUnfilled.Parameters.Barcode.FilledBars = false;
            generatorUnfilled.Save(unfilledPath);
        }

        // Counter for differing pixels between the two images.
        int diffCount = 0;

        // Load both images for pixel-by-pixel comparison.
        using (var bmpFilled = new Bitmap(filledPath))
        using (var bmpUnfilled = new Bitmap(unfilledPath))
        {
            // Verify that both images share the same dimensions.
            if (bmpFilled.Width != bmpUnfilled.Width || bmpFilled.Height != bmpUnfilled.Height)
            {
                Console.WriteLine("Images have different dimensions; cannot compare.");
                return;
            }

            // Iterate over each pixel coordinate.
            for (int y = 0; y < bmpFilled.Height; y++)
            {
                for (int x = 0; x < bmpFilled.Width; x++)
                {
                    // Compare ARGB values of corresponding pixels.
                    if (bmpFilled.GetPixel(x, y).ToArgb() != bmpUnfilled.GetPixel(x, y).ToArgb())
                    {
                        diffCount++;
                    }
                }
            }
        }

        // Output the results to the console.
        Console.WriteLine($"Filled bars image saved to: {Path.GetFullPath(filledPath)}");
        Console.WriteLine($"Unfilled bars image saved to: {Path.GetFullPath(unfilledPath)}");
        Console.WriteLine($"Number of differing pixels: {diffCount}");
        Console.WriteLine(diffCount == 0
            ? "Images are identical (no visual difference)."
            : "Images differ; bar filling effect is visible.");
    }
}