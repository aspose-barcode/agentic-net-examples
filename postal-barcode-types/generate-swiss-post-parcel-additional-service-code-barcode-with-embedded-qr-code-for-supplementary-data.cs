using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a Swiss Post Parcel barcode and a QR code,
/// then combining them into a single image file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates the barcodes, merges them, and saves the result to disk.
    /// </summary>
    static void Main()
    {
        // Sample Swiss Post Parcel code text (replace with a valid value as per specification)
        const string swissPostCodeText = "1234567890123456789012345";

        // Sample supplementary data to be encoded in QR code
        const string qrSupplementaryData = "Additional service information";

        // Generate Swiss Post Parcel barcode image
        using (var swissGenerator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, swissPostCodeText))
        {
            // Optional: configure appearance of the Swiss barcode
            swissGenerator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            swissGenerator.Parameters.BackColor = Aspose.Drawing.Color.White;

            using (var swissStream = new MemoryStream())
            {
                // Save Swiss barcode to memory stream as PNG
                swissGenerator.Save(swissStream, BarCodeImageFormat.Png);
                swissStream.Position = 0; // Reset stream position for reading

                using (var swissBitmap = new Bitmap(swissStream))
                {
                    // Generate QR code image
                    using (var qrGenerator = new BarcodeGenerator(EncodeTypes.QR, qrSupplementaryData))
                    {
                        // Set high error correction level for robustness
                        qrGenerator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;
                        qrGenerator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.Auto;

                        using (var qrStream = new MemoryStream())
                        {
                            // Save QR code to memory stream as PNG
                            qrGenerator.Save(qrStream, BarCodeImageFormat.Png);
                            qrStream.Position = 0; // Reset stream position for reading

                            using (var qrBitmap = new Bitmap(qrStream))
                            {
                                // Determine combined image size (Swiss barcode on top, QR below with padding)
                                const int padding = 20;
                                int combinedWidth = Math.Max(swissBitmap.Width, qrBitmap.Width);
                                int combinedHeight = swissBitmap.Height + qrBitmap.Height + padding;

                                using (var combinedBitmap = new Bitmap(combinedWidth, combinedHeight))
                                {
                                    using (var graphics = Graphics.FromImage(combinedBitmap))
                                    {
                                        // Fill background with white
                                        graphics.Clear(Aspose.Drawing.Color.White);

                                        // Draw Swiss Post barcode centered horizontally at the top
                                        int swissX = (combinedWidth - swissBitmap.Width) / 2;
                                        graphics.DrawImage(swissBitmap, swissX, 0, swissBitmap.Width, swissBitmap.Height);

                                        // Draw QR code centered horizontally below the Swiss barcode
                                        int qrX = (combinedWidth - qrBitmap.Width) / 2;
                                        int qrY = swissBitmap.Height + padding;
                                        graphics.DrawImage(qrBitmap, qrX, qrY, qrBitmap.Width, qrBitmap.Height);
                                    }

                                    // Save the combined image to a file
                                    const string outputPath = "SwissPostParcel_WithQR.png";
                                    combinedBitmap.Save(outputPath, ImageFormat.Png);
                                    Console.WriteLine($"Combined barcode saved to: {Path.GetFullPath(outputPath)}");
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}