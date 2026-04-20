using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Swiss Post Parcel barcode (additional service code)
        using (var parcelGenerator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, "123456789012"))
        {
            // Generate the barcode image
            using (Bitmap parcelImage = parcelGenerator.GenerateBarCodeImage())
            {
                // QR code containing supplementary data
                using (var qrGenerator = new BarcodeGenerator(EncodeTypes.QR, "Supplementary Info"))
                {
                    // High error correction level for robustness
                    qrGenerator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

                    using (Bitmap qrImage = qrGenerator.GenerateBarCodeImage())
                    {
                        // Combine both images side by side
                        int spacing = 20; // pixels between images
                        int combinedWidth = parcelImage.Width + spacing + qrImage.Width;
                        int combinedHeight = Math.Max(parcelImage.Height, qrImage.Height);

                        using (var combinedBitmap = new Bitmap(combinedWidth, combinedHeight))
                        {
                            using (var graphics = Graphics.FromImage(combinedBitmap))
                            {
                                // Fill background with white
                                graphics.Clear(Aspose.Drawing.Color.White);

                                // Draw the parcel barcode on the left
                                graphics.DrawImage(parcelImage, 0, (combinedHeight - parcelImage.Height) / 2);

                                // Draw the QR code on the right
                                graphics.DrawImage(qrImage, parcelImage.Width + spacing, (combinedHeight - qrImage.Height) / 2);
                            }

                            // Save the combined image
                            combinedBitmap.Save("SwissPostParcelWithQR.png", ImageFormat.Png);
                        }
                    }
                }
            }
        }
    }
}