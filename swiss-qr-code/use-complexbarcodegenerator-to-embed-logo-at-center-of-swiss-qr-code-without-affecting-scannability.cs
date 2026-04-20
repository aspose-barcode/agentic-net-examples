using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Prepare Swiss QR bill data (using a known‑valid IBAN and amount)
        var swissQr = new SwissQRCodetext();
        swissQr.Bill.Account = "CH9300762011623852957";
        swissQr.Bill.Amount = 199.95m;
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;
        swissQr.Bill.Creditor.CountryCode = "CH";

        // Required creditor fields
        swissQr.Bill.Creditor.Name = "John Doe";
        swissQr.Bill.Creditor.AddressLine1 = "Main Street 1";
        swissQr.Bill.Creditor.PostalCode = "8000";
        swissQr.Bill.Creditor.Town = "Zurich";

        // Create the complex barcode generator and set high error correction (Level H)
        using (var generator = new ComplexBarcodeGenerator(swissQr))
        {
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Generate the QR code image
            using (Bitmap qrBitmap = generator.GenerateBarCodeImage())
            {
                // Determine logo size (e.g., 20 % of the QR code dimensions)
                int logoSize = (int)(Math.Min(qrBitmap.Width, qrBitmap.Height) * 0.20f);
                int logoX = (qrBitmap.Width - logoSize) / 2;
                int logoY = (qrBitmap.Height - logoSize) / 2;
                var logoRect = new Rectangle(logoX, logoY, logoSize, logoSize);

                // Draw a simple logo (white circle with a black border) onto the QR code
                using (Graphics graphics = Graphics.FromImage(qrBitmap))
                {
                    // Clear the logo area with white to improve contrast
                    using (var whiteBrush = new SolidBrush(Color.White))
                    {
                        graphics.FillEllipse(whiteBrush, logoRect);
                    }

                    // Draw the logo border
                    using (var blackPen = new Pen(Color.Black, 2f))
                    {
                        graphics.DrawEllipse(blackPen, logoRect);
                    }

                    // Draw initials inside the logo
                    using (var font = new Font("Arial", logoSize * 0.4f, FontStyle.Bold))
                    using (var textBrush = new SolidBrush(Color.Black))
                    {
                        var format = new StringFormat
                        {
                            Alignment = StringAlignment.Center,
                            LineAlignment = StringAlignment.Center
                        };
                        graphics.DrawString("AB", font, textBrush, logoRect, format);
                    }
                }

                // Save the final image
                const string outputPath = "SwissQRWithLogo.png";
                qrBitmap.Save(outputPath, ImageFormat.Png);
                Console.WriteLine($"Swiss QR Code with embedded logo saved to: {Path.GetFullPath(outputPath)}");
            }
        }
    }
}