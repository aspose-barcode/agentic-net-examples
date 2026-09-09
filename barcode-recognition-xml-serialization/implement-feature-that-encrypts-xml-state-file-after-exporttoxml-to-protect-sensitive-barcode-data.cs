// Title: Encrypt exported barcode generation state XML using AES
// Description: Demonstrates generating a QR barcode with Aspose.BarCode, exporting its generation state to XML, and then encrypting that XML file to protect sensitive data.
// Category-Description: This example belongs to the Aspose.BarCode generation and data protection category. It shows how to use BarcodeGenerator to create barcodes, export the generation state via ExportToXml, and apply standard .NET cryptography (Aes) to secure the XML. Developers often need to store barcode configuration securely for later reuse or compliance, and this pattern illustrates typical API usage for such scenarios.
// Prompt: Implement a feature that encrypts the XML state file after ExportToXml to protect sensitive barcode data.
// Tags: qr, barcode, generation, xml, encryption, aesprefix

using System;
using System.IO;
using System.Security.Cryptography;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates a QR barcode, exports its generation state to XML, and encrypts the XML file using AES.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs barcode generation, XML export, encryption, and cleanup.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary output directory for all generated files.
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(outputDir);

        // Define file paths for the barcode image, the plain XML state, and the encrypted output.
        string imagePath = Path.Combine(outputDir, "barcode.png");
        string xmlPath = Path.Combine(outputDir, "barcode_state.xml");
        string encryptedPath = Path.Combine(outputDir, "barcode_state.enc");

        // Generate a QR barcode, save the image, and export the generation state to an XML file.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "SampleText"))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 4;
            generator.Save(imagePath, BarCodeImageFormat.Png);
            generator.ExportToXml(xmlPath);
        }

        // Read the exported XML file into a byte array for encryption.
        byte[] plainBytes = File.ReadAllBytes(xmlPath);

        // Example key and IV (for demonstration only; do not use hard‑coded keys in production).
        byte[] key = new byte[32]; // 256‑bit key
        byte[] iv = new byte[16];  // 128‑bit IV
        for (int i = 0; i < key.Length; i++) key[i] = (byte)(i + 1);
        for (int i = 0; i < iv.Length; i++) iv[i] = (byte)(i + 1);

        // Encrypt the XML data using AES and write the encrypted bytes to a new file.
        using (Aes aes = Aes.Create())
        {
            aes.Key = key;
            aes.IV = iv;

            using (ICryptoTransform encryptor = aes.CreateEncryptor())
            using (var memoryStream = new MemoryStream())
            using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
            {
                cryptoStream.Write(plainBytes, 0, plainBytes.Length);
                cryptoStream.FlushFinalBlock();

                byte[] encryptedBytes = memoryStream.ToArray();
                File.WriteAllBytes(encryptedPath, encryptedBytes);
            }
        }

        // Optionally delete the plain XML file to ensure only the encrypted version remains.
        try
        {
            File.Delete(xmlPath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Could not delete XML file: {ex.Message}");
        }

        // Inform the user where the output files are located.
        Console.WriteLine($"Barcode image saved to: {imagePath}");
        Console.WriteLine($"Encrypted XML saved to: {encryptedPath}");
    }
}