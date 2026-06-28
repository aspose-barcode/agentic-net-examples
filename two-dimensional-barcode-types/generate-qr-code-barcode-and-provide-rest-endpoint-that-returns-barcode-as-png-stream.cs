using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a QR code using Aspose.BarCode and outputting it as a Base64 string.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the console application.
    /// Generates a QR code, encodes it to PNG, and writes the Base64 representation to the console.
    /// </summary>
    static void Main()
    {
        // NOTE: A full REST endpoint cannot be hosted in this console snippet.
        // The core barcode generation logic is demonstrated below.
        // The generated PNG image is output as a Base64 string, which can be
        // returned from a REST API in a real web application.

        const string qrText = "Hello, World!"; // Text to encode in the QR code

        // Create a BarcodeGenerator for QR encoding with the specified text
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, qrText))
        {
            // Optional: set error correction level to Medium (LevelM)
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Use a memory stream to hold the generated PNG image
            using (var ms = new MemoryStream())
            {
                // Save the QR code as PNG into the memory stream
                generator.Save(ms, BarCodeImageFormat.Png);

                // Convert the memory stream to a byte array
                byte[] pngBytes = ms.ToArray();

                // Encode the PNG bytes to a Base64 string for easy transport
                string base64 = Convert.ToBase64String(pngBytes);

                // Write the Base64 string to the console output
                Console.WriteLine(base64);
            }
        }
    }
}