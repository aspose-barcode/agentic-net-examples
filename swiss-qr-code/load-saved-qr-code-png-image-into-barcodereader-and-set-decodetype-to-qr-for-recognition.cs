// Title: QR Code generation, saving, and recognition using Aspose.BarCode
// Description: Demonstrates creating a QR code image, saving it as PNG, then loading it with BarCodeReader to decode the QR content.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator for QR code creation and BarCodeReader with DecodeType.QR for reading saved images. Developers commonly use these APIs to embed QR codes in documents or applications and later extract the encoded data from stored image files.
// Prompt: Load a saved QR Code PNG image into BarCodeReader and set DecodeType to QR for recognition.
// Tags: qr code, barcode generation, barcode recognition, png, aspose.barcode, decode type, qrcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a QR code, saving it as PNG, and reading it back using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the demo. Generates a QR code, saves it, reads it, and outputs the decoded information.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for the demo
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeQrDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string imagePath = Path.Combine(tempFolder, "qr.png");

        // Generate a QR code image and save it as PNG
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "Hello World"))
        {
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify the file exists before attempting to read
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("QR code image file was not found.");
            return;
        }

        // Read the QR code using BarCodeReader with DecodeType set to QR
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.QR))
        {
            BarCodeResult[] results = reader.ReadBarCodes();

            // Check if any QR codes were detected
            if (results.Length == 0)
            {
                Console.WriteLine("No QR code detected.");
            }
            else
            {
                // Output each detected QR code's type and text
                foreach (BarCodeResult result in results)
                {
                    Console.WriteLine($"Detected Code Type: {result.CodeTypeName}");
                    Console.WriteLine($"Detected Code Text: {result.CodeText}");
                }
            }
        }

        // Clean up temporary files
        try
        {
            File.Delete(imagePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored - cleanup failure should not affect demo outcome
        }
    }
}