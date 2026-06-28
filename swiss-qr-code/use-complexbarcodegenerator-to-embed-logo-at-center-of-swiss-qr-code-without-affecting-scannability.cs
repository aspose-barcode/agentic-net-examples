using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generation of a Swiss QR code with an embedded logo using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a Swiss QR barcode, embeds a logo at its centre, and saves the result as a PNG file.
    /// </summary>
    static void Main()
    {
        // Output file for the final QR code image and optional logo source file
        string outputPath = "SwissQR_with_logo.png";
        string logoPath = "logo.png";

        // --------------------------------------------------------------------
        // Ensure a placeholder logo exists if the specified logo file is missing
        // --------------------------------------------------------------------
        if (!File.Exists(logoPath))
        {
            // Create a 100x100 white bitmap and draw the word "Logo" on it
            using (var placeholder = new Bitmap(100, 100))
            {
                using (var g = Graphics.FromImage(placeholder))
                {
                    g.Clear(Color.White);
                    using (var font = new Font("Arial", 12f))
                    {
                        g.DrawString("Logo", font, Brushes.Black, new PointF(10f, 40f));
                    }
                }
                // Save the placeholder as a PNG file
                placeholder.Save(logoPath, ImageFormat.Png);
            }
        }

        // --------------------------------------------------------------
        // Build the Swiss QR code text with required billing information
        // --------------------------------------------------------------
        var swissQr = new SwissQRCodetext();
        swissQr.Bill.Creditor.Name = "John Doe";
        swissQr.Bill.Creditor.CountryCode = "CH";
        swissQr.Bill.Account = "CH9300762011623852957";
        swissQr.Bill.Amount = 199.95m;
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;

        // --------------------------------------------------------------
        // Generate the QR barcode with high error correction (Level H)
        // --------------------------------------------------------------
        using (var generator = new ComplexBarcodeGenerator(swissQr))
        {
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Create the barcode image
            using (var barcodeImage = generator.GenerateBarCodeImage())
            {
                // Load the logo image from file
                using (var logoImage = new Bitmap(logoPath))
                {
                    int barcodeWidth = barcodeImage.Width;
                    int barcodeHeight = barcodeImage.Height;

                    // ------------------------------------------------------
                    // Create a new bitmap to hold the combined barcode + logo
                    // ------------------------------------------------------
                    using (var finalImage = new Bitmap(barcodeWidth, barcodeHeight))
                    {
                        using (var graphics = Graphics.FromImage(finalImage))
                        {
                            // Draw the QR barcode onto the final image
                            graphics.DrawImage(barcodeImage, 0, 0, barcodeWidth, barcodeHeight);

                            // Calculate logo size (20% of barcode dimensions) and centre position
                            int logoWidth = (int)(barcodeWidth * 0.2f);
                            int logoHeight = (int)(barcodeHeight * 0.2f);
                            int logoX = (barcodeWidth - logoWidth) / 2;
                            int logoY = (barcodeHeight - logoHeight) / 2;

                            // Draw the logo at the calculated position
                            graphics.DrawImage(logoImage, new Rectangle(logoX, logoY, logoWidth, logoHeight));
                        }

                        // Save the combined image as a PNG file
                        finalImage.Save(outputPath, ImageFormat.Png);
                    }
                }
            }
        }

        // Output the full path of the saved image to the console
        Console.WriteLine($"Swiss QR code with embedded logo saved to: {Path.GetFullPath(outputPath)}");
    }
}