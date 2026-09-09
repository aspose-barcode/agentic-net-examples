// Title: Generate QR Code with Embedded Text Note
// Description: Creates a QR Code barcode containing a short plain‑text message and saves it as a PNG file.
// Category-Description: Demonstrates basic Aspose.BarCode generation for QR Code symbology. Shows how to set the message, adjust error correction level, customize code‑text appearance, and export the barcode image. This example belongs to the barcode creation category, using BarcodeGenerator, EncodeTypes, QRErrorLevel, and BarCodeImageFormat classes—common tasks for developers needing quick visual data sharing.
// Prompt: Generate QR Code barcode and embed plain text message for quick note distribution.
// Tags: qr code,barcode generation,plain text,aspose.barcode,png output

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a QR Code barcode with an embedded plain‑text note and saving it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the output folder, generates the QR Code, and writes the file path to the console.
    /// </summary>
    static void Main()
    {
        // Prepare a unique temporary output directory
        string outputDir = Path.Combine(Path.GetTempPath(), "QrDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Full path for the generated PNG file
        string filePath = Path.Combine(outputDir, "qr_note.png");

        // Message that will be encoded into the QR Code
        string message = "Quick note: Meet at 10am";

        // Initialize the barcode generator for QR Code with the specified message
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, message))
        {
            // Use the highest error correction level to improve readability after damage
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Configure the appearance of the human‑readable text below the barcode
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 12f;

            // Save the generated barcode as a PNG image
            generator.Save(filePath, BarCodeImageFormat.Png);
        }

        // Inform the user where the QR code image was saved
        Console.WriteLine($"QR code saved to: {filePath}");
    }
}