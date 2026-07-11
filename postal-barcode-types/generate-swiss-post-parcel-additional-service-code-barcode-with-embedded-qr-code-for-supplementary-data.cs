// Title: Generate Swiss Post Parcel barcode with embedded QR code
// Description: Demonstrates creating a Swiss Post Parcel Additional Service Code barcode and a QR code for supplementary data, then combining them into a single image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use BarcodeGenerator with EncodeTypes.SwissPostParcel and EncodeTypes.QR. It illustrates typical use cases such as combining multiple symbologies, adjusting dimensions, and saving the result as an image—common tasks for developers integrating postal and QR barcodes.
// Prompt: Generate a Swiss Post Parcel additional service code barcode with embedded QR code for supplementary data.
// Tags: swisspostparcel, qr, barcode generation, image composition, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a Swiss Post Parcel Additional Service Code barcode with an embedded QR code and saving the combined image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates the Swiss Post barcode, QR code, merges them side‑by‑side, and writes the result to a PNG file.
    /// </summary>
    static void Main()
    {
        // Primary Swiss Post Parcel barcode (Additional Service Code)
        const string swissPostCodeText = "1234567890"; // example code text
        using (var swissGenerator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, swissPostCodeText))
        {
            // Optional: adjust size or colors if needed
            swissGenerator.Parameters.Barcode.XDimension.Point = 2f;
            swissGenerator.Parameters.ImageWidth.Point = 300f;
            swissGenerator.Parameters.ImageHeight.Point = 150f;

            using (var swissImage = swissGenerator.GenerateBarCodeImage())
            {
                // QR code for supplementary data
                const string qrSupplementText = "Supplementary Info";
                using (var qrGenerator = new BarcodeGenerator(EncodeTypes.QR, qrSupplementText))
                {
                    // Set QR error correction level
                    qrGenerator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;
                    qrGenerator.Parameters.ImageWidth.Point = 150f;
                    qrGenerator.Parameters.ImageHeight.Point = 150f;

                    using (var qrImage = qrGenerator.GenerateBarCodeImage())
                    {
                        // Combine both images side by side
                        int combinedWidth = swissImage.Width + qrImage.Width;
                        int combinedHeight = Math.Max(swissImage.Height, qrImage.Height);
                        using (var combinedBitmap = new Bitmap(combinedWidth, combinedHeight))
                        {
                            using (var graphics = Graphics.FromImage(combinedBitmap))
                            {
                                // Fill background with white
                                graphics.Clear(Aspose.Drawing.Color.White);
                                // Draw Swiss Post barcode on the left
                                graphics.DrawImage(swissImage, 0, 0, swissImage.Width, swissImage.Height);
                                // Draw QR code on the right
                                graphics.DrawImage(qrImage, swissImage.Width, 0, qrImage.Width, qrImage.Height);
                            }

                            // Save the final combined barcode image
                            const string outputPath = "SwissPostParcel_With_QR.png";
                            combinedBitmap.Save(outputPath, ImageFormat.Png);
                            Console.WriteLine($"Combined barcode saved to: {outputPath}");
                        }
                    }
                }
            }
        }
    }
}