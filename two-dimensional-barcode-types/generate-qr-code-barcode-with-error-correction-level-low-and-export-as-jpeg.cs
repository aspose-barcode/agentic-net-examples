// Title: Generate QR Code with Low Error Correction and Save as JPEG
// Description: Demonstrates creating a QR Code barcode with low error correction level and exporting it as a JPEG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to configure QR Code parameters such as error correction level using the BarcodeGenerator class. Typical use cases include creating scannable QR codes for URLs, contact info, or product data with specific reliability requirements. Developers often need to adjust QR error correction to balance data density and readability across various printing and display scenarios.
// Prompt: Generate a QR Code barcode with error correction level low and export as JPEG.
// Tags: qr code, error correction, jpeg, generation, aspose.barcode, barcodegenerator

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a QR Code barcode with low error correction level and saving it as a JPEG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a QR Code with Level L error correction and writes it to a JPEG file.
    /// </summary>
    static void Main()
    {
        // Initialize a QR code generator with sample text.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "Hello World"))
        {
            // Set the QR error correction level to low (LevelL).
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelL;

            // Save the generated QR code as a JPEG image.
            generator.Save("qr_low.jpg");
        }
    }
}