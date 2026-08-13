// Title: Generate Swiss Post Parcel barcode with embedded QR code
// Description: Demonstrates creating a Swiss Post Parcel barcode and a QR code with supplementary data, then combining them into a single image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use BarcodeGenerator with different symbologies (SwissPostParcel and QR) and combine multiple barcode images. Typical use cases include packaging labels that require a primary barcode plus an auxiliary QR code for tracking URLs or additional information. Developers often need to generate, customize, and merge barcode graphics for printing or digital distribution.
// Prompt: Generate a Swiss Post Parcel additional service code barcode with embedded QR code for supplementary data.
// Tags: swisspostparcel, qr, barcode generation, image composition, aspose.barcode, csharp

using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates a Swiss Post Parcel barcode, generates a QR code with
/// supplementary tracking data, and merges both images side‑by‑side into a single PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcodes, composes them, and saves the result.
    /// </summary>
    static void Main()
    {
        // Sample parcel identifier (Swiss Post Parcel service code) and supplementary tracking URL
        string parcelCode = "1234567890123456";
        string supplementaryData = "https://example.com/track/123456";

        // Create a Swiss Post Parcel barcode generator with the parcel identifier
        using (var parcelGenerator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, parcelCode))
        {
            // Render the Swiss Post Parcel barcode to a bitmap
            using (Bitmap parcelImage = parcelGenerator.GenerateBarCodeImage())
            {
                // Create a QR code generator containing the supplementary tracking URL
                using (var qrGenerator = new BarcodeGenerator(EncodeTypes.QR, supplementaryData))
                {
                    // Use high error correction level for better robustness of the QR code
                    qrGenerator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

                    // Render the QR code to a bitmap
                    using (Bitmap qrImage = qrGenerator.GenerateBarCodeImage())
                    {
                        // Define spacing between the two barcodes
                        int margin = 10;

                        // Calculate dimensions for the combined image
                        int combinedWidth = parcelImage.Width + qrImage.Width + margin;
                        int combinedHeight = Math.Max(parcelImage.Height, qrImage.Height);

                        // Create a new bitmap to hold the combined image
                        using (Bitmap combined = new Bitmap(combinedWidth, combinedHeight))
                        {
                            // Draw both barcode images onto the combined bitmap
                            using (Graphics g = Graphics.FromImage(combined))
                            {
                                // Fill background with white
                                g.Clear(Color.White);

                                // Center the parcel barcode vertically
                                g.DrawImage(parcelImage, 0, (combinedHeight - parcelImage.Height) / 2);

                                // Center the QR code vertically, positioned after the parcel barcode plus margin
                                g.DrawImage(qrImage, parcelImage.Width + margin, (combinedHeight - qrImage.Height) / 2);
                            }

                            // Save the combined image as PNG
                            string outputPath = "SwissPostParcelWithQR.png";
                            combined.Save(outputPath, ImageFormat.Png);
                            Console.WriteLine($"Combined barcode saved to {outputPath}");
                        }
                    }
                }
            }
        }
    }
}