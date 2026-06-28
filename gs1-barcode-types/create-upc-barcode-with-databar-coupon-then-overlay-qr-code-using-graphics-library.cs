using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a UPC‑A barcode with a DataBar coupon and overlaying a QR code.
/// </summary>
class Program
{
    /// <summary>
    /// Application entry point.
    /// </summary>
    static void Main()
    {
        // Define the UPC‑A barcode text, including the DataBar coupon segment.
        const string upcCodeText = "012345678905(8110)106141416543213500110000310123196000";

        // Create a barcode generator for UPC‑A with GS1 DataBar coupon encoding.
        using (var upcGenerator = new BarcodeGenerator(EncodeTypes.UpcaGs1DatabarCoupon, upcCodeText))
        {
            // Set a high resolution to improve image quality.
            upcGenerator.Parameters.Resolution = 300f;

            // Render the UPC‑A barcode to a memory stream.
            using (var upcStream = new MemoryStream())
            {
                upcGenerator.Save(upcStream, BarCodeImageFormat.Png);
                upcStream.Position = 0; // Reset stream position for reading.

                // Load the generated barcode into a bitmap.
                using (var upcBitmap = new Bitmap(upcStream))
                {
                    // Define the QR code text (e.g., a URL).
                    const string qrCodeText = "https://example.com";

                    // Create a barcode generator for a QR code.
                    using (var qrGenerator = new BarcodeGenerator(EncodeTypes.QR, qrCodeText))
                    {
                        // Use the same high resolution for consistency.
                        qrGenerator.Parameters.Resolution = 300f;

                        // Render the QR code to a memory stream.
                        using (var qrStream = new MemoryStream())
                        {
                            qrGenerator.Save(qrStream, BarCodeImageFormat.Png);
                            qrStream.Position = 0; // Reset stream position for reading.

                            // Load the QR code into a bitmap.
                            using (var qrBitmap = new Bitmap(qrStream))
                            {
                                // Prepare to draw the QR code onto the UPC‑A bitmap.
                                using (var graphics = Graphics.FromImage(upcBitmap))
                                {
                                    // Calculate position: bottom‑right corner with a small margin.
                                    int margin = 10;
                                    int x = upcBitmap.Width - qrBitmap.Width - margin;
                                    int y = upcBitmap.Height - qrBitmap.Height - margin;

                                    // Draw the QR code onto the UPC‑A image.
                                    graphics.DrawImage(qrBitmap, x, y, qrBitmap.Width, qrBitmap.Height);
                                }

                                // Save the combined image to disk.
                                upcBitmap.Save("CombinedBarcode.png", ImageFormat.Png);
                                Console.WriteLine("Combined barcode saved as CombinedBarcode.png");
                            }
                        }
                    }
                }
            }
        }
    }
}