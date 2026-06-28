using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a UPC‑A GS1 DataBar coupon barcode,
/// combining it with a simple logo, and saving both images to disk.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates the barcode, creates a combined image with a logo,
    /// and writes the results to PNG files.
    /// </summary>
    static void Main()
    {
        // Sample UPC‑A with DataBar coupon code text (UPCA part + GS1 DataBar part)
        const string upcCouponCode = "514141100906(8110)106141416543213500110000310123196000";

        // Output file paths
        const string barcodePath = "barcode.png";
        const string combinedPath = "combined.png";

        // Create a barcode generator for the specified type and data
        using (var generator = new BarcodeGenerator(EncodeTypes.UpcaGs1DatabarCoupon, upcCouponCode))
        {
            // Optional: increase resolution for higher quality output
            generator.Parameters.Resolution = 300f;

            // Save the generated barcode to a memory stream in PNG format
            using (var barcodeStream = new MemoryStream())
            {
                generator.Save(barcodeStream, BarCodeImageFormat.Png);
                barcodeStream.Position = 0; // Reset stream position for reading

                // Load the barcode image from the memory stream
                using (var barcodeBitmap = new Bitmap(barcodeStream))
                {
                    // Create a simple 100x100 logo bitmap with the text "Logo"
                    using (var logoBitmap = new Bitmap(100, 100))
                    {
                        using (var gLogo = Graphics.FromImage(logoBitmap))
                        {
                            // Fill background with light gray
                            gLogo.Clear(Color.LightGray);

                            // Draw the word "Logo" centered vertically
                            using (var font = new Font("Arial", 20f))
                            using (var brush = new SolidBrush(Color.Black))
                            {
                                gLogo.DrawString("Logo", font, brush, new PointF(10f, 40f));
                            }
                        }

                        // Determine dimensions for the combined image (barcode left, logo right)
                        int combinedWidth = barcodeBitmap.Width + logoBitmap.Width;
                        int combinedHeight = Math.Max(barcodeBitmap.Height, logoBitmap.Height);

                        // Create the combined bitmap
                        using (var combinedBitmap = new Bitmap(combinedWidth, combinedHeight))
                        {
                            using (var g = Graphics.FromImage(combinedBitmap))
                            {
                                // Fill the background with white
                                g.Clear(Color.White);

                                // Center the barcode vertically and draw it on the left side
                                int barcodeY = (combinedHeight - barcodeBitmap.Height) / 2;
                                g.DrawImage(
                                    barcodeBitmap,
                                    new Rectangle(0, barcodeY, barcodeBitmap.Width, barcodeBitmap.Height));

                                // Center the logo vertically and draw it on the right side
                                int logoY = (combinedHeight - logoBitmap.Height) / 2;
                                g.DrawImage(
                                    logoBitmap,
                                    new Rectangle(barcodeBitmap.Width, logoY, logoBitmap.Width, logoBitmap.Height));
                            }

                            // Save the combined image to file
                            combinedBitmap.Save(combinedPath, ImageFormat.Png);
                        }
                    }

                    // Save the pure barcode image for reference
                    barcodeBitmap.Save(barcodePath, ImageFormat.Png);
                }
            }
        }

        // Output the full paths of the saved files
        Console.WriteLine($"Barcode saved to: {Path.GetFullPath(barcodePath)}");
        Console.WriteLine($"Combined image saved to: {Path.GetFullPath(combinedPath)}");
    }
}