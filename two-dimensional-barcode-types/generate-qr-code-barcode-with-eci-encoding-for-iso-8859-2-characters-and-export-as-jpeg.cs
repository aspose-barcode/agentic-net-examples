using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a QR Code with ISO‑8859‑2 ECI encoding using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a QR code containing characters from the ISO‑8859‑2 character set,
    /// configures the generator for ECI encoding, and saves the result as a JPEG file.
    /// </summary>
    static void Main()
    {
        // Define sample text that includes ISO‑8859‑2 specific characters.
        string codeText = "ČŽŠ";

        // Initialize a QR Code generator within a using block to ensure proper disposal.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Assign the text to be encoded in the QR code.
            generator.CodeText = codeText;

            // Activate ECI (Extended Channel Interpretation) encoding mode for QR codes.
            generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.ECIEncoding;

            // Set the specific ECI encoding to ISO‑8859‑2.
            generator.Parameters.Barcode.QR.ECIEncoding = ECIEncodings.ISO_8859_2;

            // Persist the generated QR code as a JPEG image file.
            generator.Save("qr_iso8859_2.jpg");
        }

        // Inform the user that the QR code has been saved.
        Console.WriteLine("QR Code with ISO‑8859‑2 ECI encoding saved as 'qr_iso8859_2.jpg'.");
    }
}