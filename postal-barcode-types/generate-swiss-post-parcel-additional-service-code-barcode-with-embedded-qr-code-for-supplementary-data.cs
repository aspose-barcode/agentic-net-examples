// Title: Generate Swiss Post Parcel Additional Service barcode with embedded QR code
// Description: Demonstrates creating a Swiss Post Parcel additional service barcode (Code 128 based) and combining it with a QR code that holds supplementary data, then saving the result as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation and image composition category. It shows how to use the BarcodeGenerator class with EncodeTypes.SwissPostParcel and EncodeTypes.QR, configure barcode parameters, render images, and merge multiple barcode images using Aspose.Drawing. Typical use cases include creating composite shipping labels or packaging marks where a primary barcode is supplemented with a QR code for extra information. Developers often need to combine different symbologies into a single printable asset.
// Prompt: Generate a Swiss Post Parcel additional service code barcode with embedded QR code for supplementary data.
// Tags: swisspost, parcel, additionalservice, qr, barcode, generation, png, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a Swiss Post Parcel additional service barcode combined with a QR code and saving the result.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the barcodes, merges them vertically, and writes the combined image to disk.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the combined barcode image
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "SwissPostAdditionalServiceWithQR.png");

        // Create Swiss Post Parcel Additional Service barcode (Code 128 based) with the service code "0327"
        using (var swissGen = new BarcodeGenerator(EncodeTypes.SwissPostParcel, "0327"))
        {
            // Set visual parameters for the Swiss Post barcode
            swissGen.Parameters.Barcode.XDimension.Pixels = 2f;
            swissGen.Parameters.Barcode.BarHeight.Pixels = 40f;
            // Hide human‑readable text (optional)
            swissGen.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;

            // Render the Swiss Post barcode to a bitmap
            using (Bitmap swissBitmap = swissGen.GenerateBarCodeImage())
            {
                // Create QR code containing supplementary data
                using (var qrGen = new BarcodeGenerator(EncodeTypes.QR, "Supplementary data"))
                {
                    // Set visual parameters for the QR code
                    qrGen.Parameters.Barcode.XDimension.Pixels = 2f;
                    qrGen.Parameters.Barcode.BarHeight.Pixels = 40f;
                    qrGen.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;
                    qrGen.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;

                    // Render the QR code to a bitmap
                    using (Bitmap qrBitmap = qrGen.GenerateBarCodeImage())
                    {
                        // Determine combined image dimensions (vertical stacking)
                        int combinedWidth = Math.Max(swissBitmap.Width, qrBitmap.Width);
                        int combinedHeight = swissBitmap.Height + qrBitmap.Height;

                        // Create a new bitmap to hold both barcodes
                        using (Bitmap combinedBitmap = new Bitmap(combinedWidth, combinedHeight))
                        {
                            // Draw both barcodes onto the combined bitmap
                            using (Graphics g = Graphics.FromImage(combinedBitmap))
                            {
                                // Fill background with white
                                g.Clear(Aspose.Drawing.Color.White);

                                // Draw Swiss Post barcode at the top, centered horizontally
                                int swissX = (combinedWidth - swissBitmap.Width) / 2;
                                g.DrawImage(swissBitmap, swissX, 0, swissBitmap.Width, swissBitmap.Height);

                                // Draw QR code below the Swiss Post barcode, centered horizontally
                                int qrX = (combinedWidth - qrBitmap.Width) / 2;
                                g.DrawImage(qrBitmap, qrX, swissBitmap.Height, qrBitmap.Width, qrBitmap.Height);
                            }

                            // Save the combined image as a PNG file
                            combinedBitmap.Save(outputPath, ImageFormat.Png);
                        }
                    }
                }
            }
        }

        Console.WriteLine($"Combined barcode saved to: {outputPath}");
    }
}