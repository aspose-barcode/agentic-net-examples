// Title: Generate QR Code with Maximum Error Correction (Level H)
// Description: Demonstrates how to create a QR code using Aspose.BarCode with the highest Reed‑Solomon error‑correction level to improve readability on low‑quality prints.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on QR code creation and configuration. It showcases the use of BarcodeGenerator, EncodeTypes, and QRErrorLevel classes to adjust error correction. Developers often need to increase redundancy for QR codes used in printing, packaging, or scanning environments where print quality may be poor.
// Prompt: Set ComplexBarcodeGenerator ErrorCorrectionLevel to maximum to boost Reed‑Solomon redundancy for low‑quality prints.
// Tags: qr code, error correction, reed-solomon, barcode generation, aspose.barcode, png output

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a QR code with the maximum error‑correction level (Level H) using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates a temporary folder, generates a QR code with high redundancy, and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare output directory
        // --------------------------------------------------------------------
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeDemo");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Define full path for the resulting image file
        string outputPath = Path.Combine(outputDir, "QrCode_MaxError.png");

        // --------------------------------------------------------------------
        // Create and configure the QR code generator
        // --------------------------------------------------------------------
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "Sample QR Code"))
        {
            // Set the highest Reed‑Solomon error correction level (Level H)
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Optionally increase the size of each QR module (pixel dimension)
            generator.Parameters.Barcode.XDimension.Pixels = 8f;

            // Save the generated QR code as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the image was saved
        Console.WriteLine($"QR code with maximum error correction saved to: {outputPath}");
    }
}