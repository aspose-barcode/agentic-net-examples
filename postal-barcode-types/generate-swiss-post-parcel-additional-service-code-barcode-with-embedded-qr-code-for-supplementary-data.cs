// Title: Generate Swiss Post Parcel Additional Service barcode with embedded QR code
// Description: Demonstrates creating a Swiss Post Parcel Additional Service barcode (code 0327) and combining it with a QR code that carries supplementary data, then saving the composite image as PNG.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use BarcodeGenerator with EncodeTypes.SwissPostParcel and EncodeTypes.QR. It illustrates typical use cases such as combining multiple symbologies into a single image for packaging labels, where developers need to embed extra information alongside standard barcodes. The example highlights key API classes like BarcodeGenerator, Parameters, and image handling via Aspose.Drawing.
// Prompt: Generate a Swiss Post Parcel additional service code barcode with embedded QR code for supplementary data.
// Tags: swisspost, qr, barcode generation, png, aspose.barcode, image composition

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a Swiss Post Parcel Additional Service barcode combined with a QR code and saving the result.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the barcodes, merges them vertically, and writes the PNG file.
    /// </summary>
    static void Main()
    {
        // Define and create the output directory
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "output");
        Directory.CreateDirectory(outputDir);
        string outputPath = Path.Combine(outputDir, "SwissPostAdditionalServiceWithQR.png");

        // Generate Swiss Post Parcel Additional Service barcode (code 0327)
        using (var swissGen = new BarcodeGenerator(EncodeTypes.SwissPostParcel, "0327"))
        {
            // Configure visual appearance of the Swiss Post barcode
            swissGen.Parameters.Barcode.XDimension.Pixels = 2;
            swissGen.Parameters.Barcode.BarHeight.Pixels = 40;
            swissGen.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;
            swissGen.Parameters.CaptionAbove.Visible = true;
            swissGen.Parameters.CaptionAbove.Alignment = TextAlignment.Left;
            swissGen.Parameters.CaptionAbove.Text = "AR";
            swissGen.Parameters.CaptionAbove.Font.Size.Pixels = 24;
            swissGen.Parameters.CaptionAbove.Font.Style = FontStyle.Bold;

            // Render the Swiss Post barcode to a bitmap
            using (Bitmap swissBmp = swissGen.GenerateBarCodeImage())
            {
                // Generate QR code containing supplementary data
                using (var qrGen = new BarcodeGenerator(EncodeTypes.QR, "Supplementary data"))
                {
                    // Configure QR code appearance and error correction level
                    qrGen.Parameters.Barcode.XDimension.Pixels = 2;
                    qrGen.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

                    // Render the QR code to a bitmap
                    using (Bitmap qrBmp = qrGen.GenerateBarCodeImage())
                    {
                        // Determine dimensions for the combined image (vertical stacking)
                        int finalWidth = Math.Max(swissBmp.Width, qrBmp.Width);
                        int finalHeight = swissBmp.Height + qrBmp.Height;

                        // Create a new bitmap to hold both barcodes
                        using (Bitmap finalBmp = new Bitmap(finalWidth, finalHeight))
                        {
                            using (Graphics g = Graphics.FromImage(finalBmp))
                            {
                                // Fill background with white
                                g.Clear(Color.White);
                                // Draw Swiss Post barcode centered at the top
                                g.DrawImage(swissBmp, (finalWidth - swissBmp.Width) / 2, 0);
                                // Draw QR code centered below the Swiss Post barcode
                                g.DrawImage(qrBmp, (finalWidth - qrBmp.Width) / 2, swissBmp.Height);
                            }

                            // Save the combined image as PNG
                            finalBmp.Save(outputPath, ImageFormat.Png);
                        }
                    }
                }
            }
        }

        // Inform the user where the file was saved
        Console.WriteLine($"Combined barcode saved to: {outputPath}");
    }
}