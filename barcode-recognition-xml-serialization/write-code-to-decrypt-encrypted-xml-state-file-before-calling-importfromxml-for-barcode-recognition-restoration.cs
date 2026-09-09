// Title: Decrypt Encrypted XML State and Restore BarCodeReader Settings
// Description: This example shows how to encrypt a BarCodeReader's exported XML state, decrypt it, and import the settings to continue barcode recognition.
// Category-Description: Demonstrates Aspose.BarCode state management using ExportToXml and ImportFromXml. It covers encryption with Aes, handling of temporary files, and restoring reader configuration such as DecodeType and quality settings. Ideal for developers needing to persist and securely store barcode reader settings across sessions or machines.
// Prompt: Write code to decrypt an encrypted XML state file before calling ImportFromXml for barcode recognition restoration.
// Tags: pdf417, encryption, xml, import, export, barcodereader, barcodegenerator, aesencryption, state-management

using System;
using System.IO;
using System.Security.Cryptography;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates decrypting an encrypted XML state file and restoring a <see cref="BarCodeReader"/> instance.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a barcode, exports its state, encrypts/decrypts the XML, and restores the reader.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // 1. Prepare a temporary working directory for all demo files.
        // ------------------------------------------------------------
        string workDir = Path.Combine(Path.GetTempPath(), "BarcodeStateDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workDir);

        // ------------------------------------------------------------
        // 2. Define file paths for the sample image and the encrypted XML.
        // ------------------------------------------------------------
        string imagePath = Path.Combine(workDir, "sample.png");
        string encryptedXmlPath = Path.Combine(workDir, "state_encrypted.bin");

        // ------------------------------------------------------------
        // 3. Create a deterministic AES key and IV for demonstration purposes.
        // ------------------------------------------------------------
        byte[] aesKey = new byte[32]; // 256‑bit key
        byte[] aesIv = new byte[16];  // 128‑bit IV
        for (int i = 0; i < aesKey.Length; i++) aesKey[i] = (byte)(i + 1);
        for (int i = 0; i < aesIv.Length; i++) aesIv[i] = (byte)(i + 1);

        // ------------------------------------------------------------
        // 4. Generate a sample PDF417 barcode image.
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, "DEMO12345"))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 2;
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // ------------------------------------------------------------
        // 5. Export the BarCodeReader's configuration to an in‑memory XML stream.
        // ------------------------------------------------------------
        MemoryStream xmlStream = new MemoryStream();
        using (var reader = new BarCodeReader())
        {
            reader.SetBarCodeReadType(DecodeType.Pdf417);
            reader.BarcodeSettings.StripFNC = true;
            reader.QualitySettings.XDimension = XDimensionMode.Small;
            reader.ExportToXml(xmlStream);
        }

        // ------------------------------------------------------------
        // 6. Encrypt the XML and write it to a file.
        // ------------------------------------------------------------
        xmlStream.Position = 0;
        using (var fileStream = new FileStream(encryptedXmlPath, FileMode.Create, FileAccess.Write))
        using (var aes = Aes.Create())
        {
            aes.Key = aesKey;
            aes.IV = aesIv;
            using (var cryptoStream = new CryptoStream(fileStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
            {
                xmlStream.CopyTo(cryptoStream);
            }
        }

        // ------------------------------------------------------------
        // 7. Decrypt the XML back into a new memory stream.
        // ------------------------------------------------------------
        MemoryStream decryptedXmlStream = new MemoryStream();
        using (var fileStream = new FileStream(encryptedXmlPath, FileMode.Open, FileAccess.Read))
        using (var aes = Aes.Create())
        {
            aes.Key = aesKey;
            aes.IV = aesIv;
            using (var cryptoStream = new CryptoStream(fileStream, aes.CreateDecryptor(), CryptoStreamMode.Read))
            {
                cryptoStream.CopyTo(decryptedXmlStream);
            }
        }
        decryptedXmlStream.Position = 0;

        // ------------------------------------------------------------
        // 8. Import the BarCodeReader state from the decrypted XML.
        // ------------------------------------------------------------
        using (var importedReader = BarCodeReader.ImportFromXml(decryptedXmlStream))
        {
            // The image path and read type are not stored in the XML, so set them manually.
            importedReader.SetBarCodeImage(imagePath);
            importedReader.SetBarCodeReadType(DecodeType.Pdf417);

            // Output restored settings to verify successful import.
            Console.WriteLine($"StripFNC: {importedReader.BarcodeSettings.StripFNC}");
            Console.WriteLine($"XDimension mode: {importedReader.QualitySettings.XDimension}");

            // Perform barcode recognition using the restored configuration.
            BarCodeResult[] results = importedReader.ReadBarCodes();
            Console.WriteLine($"Barcodes read: {results.Length}");
            foreach (BarCodeResult result in results)
            {
                Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
            }
        }

        // ------------------------------------------------------------
        // 9. Clean up temporary files and directories (optional).
        // ------------------------------------------------------------
        try { Directory.Delete(workDir, true); } catch { }
    }
}