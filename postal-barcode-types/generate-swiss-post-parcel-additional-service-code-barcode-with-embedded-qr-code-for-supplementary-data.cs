// Title: Swiss Post Parcel Additional Service Barcode with Embedded QR Code
// Description: Generates a Swiss Post Parcel additional service barcode (Code 128) and combines it with a QR code containing supplementary data, then saves the combined image.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It demonstrates using BarcodeGenerator to create Code 128 (SwissPostParcel) and QR barcodes, customizing visual parameters, merging images with Aspose.Drawing, and reading the barcodes back with BarCodeReader. Developers often need to produce composite barcodes for logistics and embed extra information, making this pattern common in shipping and parcel tracking solutions.
// Prompt: Generate a Swiss Post Parcel additional service code barcode with embedded QR code for supplementary data.
// Tags: swisspost, parcel, additionalservice, barcode, qr, generation, recognition, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a Swiss Post Parcel additional service barcode combined with a QR code
/// and reading them back using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates, combines, saves, and reads barcodes.
    /// </summary>
    static void Main()
    {
        // Prepare output directory
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "output");
        Directory.CreateDirectory(outputDir);

        // Create Swiss Post Parcel Additional Service barcode (Code 128)
        using (var spGenerator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, "0327"))
        {
            // Configure visual appearance
            spGenerator.Parameters.Barcode.XDimension.Pixels = 2f;
            spGenerator.Parameters.Barcode.BarHeight.Pixels = 40f;
            spGenerator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;
            spGenerator.Parameters.CaptionAbove.Visible = true;
            spGenerator.Parameters.CaptionAbove.Alignment = TextAlignment.Left;
            spGenerator.Parameters.CaptionAbove.Text = "AR";
            spGenerator.Parameters.CaptionAbove.Font.Size.Pixels = 24f;
            spGenerator.Parameters.CaptionAbove.Font.Style = Aspose.Drawing.FontStyle.Bold;

            using (Bitmap spBitmap = spGenerator.GenerateBarCodeImage())
            {
                // Create QR code for supplementary data
                using (var qrGenerator = new BarcodeGenerator(EncodeTypes.QR, "Supplementary data"))
                {
                    // Configure QR code appearance and error correction
                    qrGenerator.Parameters.Barcode.XDimension.Pixels = 2f;
                    qrGenerator.Parameters.Barcode.BarHeight.Pixels = 40f;
                    qrGenerator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;
                    qrGenerator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.Auto;

                    using (Bitmap qrBitmap = qrGenerator.GenerateBarCodeImage())
                    {
                        // Combine both barcodes side by side into a single image
                        int combinedWidth = spBitmap.Width + qrBitmap.Width;
                        int combinedHeight = Math.Max(spBitmap.Height, qrBitmap.Height);

                        using (Bitmap combinedBitmap = new Bitmap(combinedWidth, combinedHeight))
                        {
                            using (Graphics g = Graphics.FromImage(combinedBitmap))
                            {
                                g.Clear(Aspose.Drawing.Color.White);
                                g.DrawImage(spBitmap, 0, 0, spBitmap.Width, spBitmap.Height);
                                g.DrawImage(qrBitmap, spBitmap.Width, 0, qrBitmap.Width, qrBitmap.Height);
                            }

                            // Save the combined barcode image
                            string outputPath = Path.Combine(outputDir, "SwissPostAdditionalServiceWithQR.png");
                            combinedBitmap.Save(outputPath, ImageFormat.Png);
                            Console.WriteLine($"Combined barcode saved to: {outputPath}");
                        }
                    }
                }
            }
        }

        // Demonstrate reading the Swiss Post barcode
        using (var spReader = new BarCodeReader(
            new BarcodeGenerator(EncodeTypes.SwissPostParcel, "0327").GenerateBarCodeImage(),
            DecodeType.SwissPostParcel))
        {
            foreach (BarCodeResult result in spReader.ReadBarCodes())
            {
                Console.WriteLine($"Read Swiss Post barcode: Type={result.CodeTypeName}, Text={result.CodeText}");
            }
        }

        // Demonstrate reading the QR code
        using (var qrReader = new BarCodeReader(
            new BarcodeGenerator(EncodeTypes.QR, "Supplementary data").GenerateBarCodeImage(),
            DecodeType.QR))
        {
            foreach (BarCodeResult result in qrReader.ReadBarCodes())
            {
                Console.WriteLine($"Read QR code: Type={result.CodeTypeName}, Text={result.CodeText}");
            }
        }
    }
}