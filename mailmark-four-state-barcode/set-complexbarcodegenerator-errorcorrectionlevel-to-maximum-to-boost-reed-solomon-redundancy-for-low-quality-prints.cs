// Title: Generate QR Code with Maximum Error Correction and Mailmark 4‑State Barcode
// Description: Demonstrates creating a QR code with the highest Reed‑Solomon error correction level and generating a Mailmark 4‑State complex barcode, saving both as image files.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to configure barcode parameters such as error correction level, module size, and X‑dimension using the BarcodeGenerator and ComplexBarcodeGenerator classes. Typical use cases include producing robust QR codes for low‑quality printing and creating postal Mailmark barcodes. Developers often need to adjust these settings to meet printing constraints and compliance requirements.
// Prompt: Set ComplexBarcodeGenerator ErrorCorrectionLevel to maximum to boost Reed‑Solomon redundancy for low‑quality prints.
// Tags: qr code,error correction,reed-solomon,mailmark,complex barcode,barcode generation,aspose.barcode,output png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a QR code with maximum error correction
/// and a Mailmark 4‑State complex barcode, then saves them to temporary files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for output files
        string outputFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // ------------------------------------------------------------
        // Example 1: QR Code with maximum error correction (Level H)
        // ------------------------------------------------------------
        string qrText = "https://example.com/low-quality-print";
        string qrPath = Path.Combine(outputFolder, "qr_max_error.png");
        using (var qrGenerator = new BarcodeGenerator(EncodeTypes.QR, qrText))
        {
            // Set maximum Reed‑Solomon error correction (Level H)
            qrGenerator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Optional: set module size (X dimension) to 2 points
            qrGenerator.Parameters.Barcode.XDimension.Point = 2f;

            // Save the QR code as a PNG image
            qrGenerator.Save(qrPath, BarCodeImageFormat.Png);
        }
        Console.WriteLine($"QR code with max error correction saved to: {qrPath}");

        // ------------------------------------------------------------
        // Example 2: Complex barcode (Mailmark 4‑State) – fixed error correction
        // ------------------------------------------------------------
        string mailmarkPath = Path.Combine(outputFolder, "mailmark.png");
        var mailmark = new MailmarkCodetext
        {
            Format = 4,
            VersionID = 1,
            Class = "0",
            SupplychainID = 384224,
            ItemID = 16563762,
            DestinationPostCodePlusDPS = "EF61AH8T "
        };
        using (var complexGenerator = new ComplexBarcodeGenerator(mailmark))
        {
            // ComplexBarcodeGenerator does not expose an ErrorCorrectionLevel property.
            // Mailmark 4‑State uses a fixed Reed‑Solomon scheme internally.

            // Set X dimension in pixels for the barcode rendering
            complexGenerator.Parameters.Barcode.XDimension.Pixels = 4;

            // Save the Mailmark barcode (format inferred from generator)
            complexGenerator.Save(mailmarkPath);
        }
        Console.WriteLine($"Mailmark 4‑State barcode saved to: {mailmarkPath}");
    }
}