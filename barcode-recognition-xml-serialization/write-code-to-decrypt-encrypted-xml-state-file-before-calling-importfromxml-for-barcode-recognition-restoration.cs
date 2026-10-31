// Title: Decrypt Encrypted XML State and Restore BarCodeReader
// Description: Demonstrates how to encrypt a BarCodeReader state to XML, decrypt it, and import the state to continue barcode recognition.
// Category-Description: This example belongs to the Aspose.BarCode state management category, showing how to export a BarCodeReader configuration to XML, secure it with AES encryption, and later restore it using ImportFromXml. It highlights key API classes such as BarcodeGenerator, BarCodeReader, QualitySettings, and cryptographic streams, useful for developers needing persistent, protected barcode processing settings.
// Prompt: Write code to decrypt an encrypted XML state file before calling ImportFromXml for barcode recognition restoration.
// Tags: qr, encryption, xml, import, export, aspose.barcode, barcodegeneration, barcoderecognition

using System;
using System.IO;
using System.Security.Cryptography;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Sample program that generates a QR barcode, exports the BarCodeReader state to XML,
/// encrypts the XML, decrypts it back into memory, and restores the reader state for
/// barcode recognition.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the demo. Executes the full workflow from barcode generation to
    /// encrypted state restoration and barcode reading.
    /// </summary>
    static void Main()
    {
        // Create a temporary working directory
        string tempDir = Path.Combine(Path.GetTempPath(), "BarCodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Define file paths for the barcode image, plain XML state, and encrypted XML
        string barcodeImagePath = Path.Combine(tempDir, "sample.png");
        string xmlPath = Path.Combine(tempDir, "state.xml");
        string encryptedXmlPath = Path.Combine(tempDir, "state.enc");

        // AES key/IV for demo encryption (256‑bit key, 128‑bit IV)
        byte[] key = new byte[32];
        byte[] iv = new byte[16];
        for (int i = 0; i < key.Length; i++) key[i] = (byte)(i + 1);
        for (int i = 0; i < iv.Length; i++) iv[i] = (byte)(i + 1);

        // 1. Generate a sample QR barcode image
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Demo"))
        {
            generator.Save(barcodeImagePath, BarCodeImageFormat.Png);
        }

        // 2. Configure a BarCodeReader, export its state to XML
        using (var reader = new BarCodeReader())
        {
            reader.SetBarCodeImage(barcodeImagePath);
            reader.SetBarCodeReadType(DecodeType.QR);
            reader.BarcodeSettings.StripFNC = true;
            reader.QualitySettings = QualitySettings.HighPerformance;

            // Export the configured reader state to an XML file
            reader.ExportToXml(xmlPath);
        }

        // 3. Encrypt the XML state file and delete the plain version
        EncryptFile(xmlPath, encryptedXmlPath, key, iv);
        File.Delete(xmlPath);

        // 4. Decrypt the XML back into a memory stream and import the reader state
        using (MemoryStream decryptedStream = DecryptToStream(encryptedXmlPath, key, iv))
        {
            using (var importedReader = BarCodeReader.ImportFromXml(decryptedStream))
            {
                // Re‑apply image and read type (these are not stored in the XML)
                importedReader.SetBarCodeImage(barcodeImagePath);
                importedReader.SetBarCodeReadType(DecodeType.QR);

                // Perform barcode reading
                BarCodeResult[] results = importedReader.ReadBarCodes();
                Console.WriteLine($"Barcodes read: {results.Length}");
                foreach (BarCodeResult result in results)
                {
                    Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
                }
            }
        }

        // Cleanup temporary files and directory
        try { Directory.Delete(tempDir, true); } catch { /* ignore cleanup errors */ }
    }

    /// <summary>
    /// Encrypts the input file using AES and writes the ciphertext to the output path.
    /// </summary>
    static void EncryptFile(string inputPath, string outputPath, byte[] key, byte[] iv)
    {
        using (FileStream inputFs = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
        using (FileStream outputFs = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
        using (Aes aes = Aes.Create())
        {
            aes.Key = key;
            aes.IV = iv;
            using (CryptoStream cryptoStream = new CryptoStream(outputFs, aes.CreateEncryptor(), CryptoStreamMode.Write))
            {
                inputFs.CopyTo(cryptoStream);
            }
        }
    }

    /// <summary>
    /// Decrypts the encrypted file into a MemoryStream positioned at the beginning.
    /// </summary>
    static MemoryStream DecryptToStream(string encryptedPath, byte[] key, byte[] iv)
    {
        MemoryStream output = new MemoryStream();
        using (FileStream encryptedFs = new FileStream(encryptedPath, FileMode.Open, FileAccess.Read))
        using (Aes aes = Aes.Create())
        {
            aes.Key = key;
            aes.IV = iv;
            using (CryptoStream cryptoStream = new CryptoStream(encryptedFs, aes.CreateDecryptor(), CryptoStreamMode.Read))
            {
                cryptoStream.CopyTo(output);
            }
        }
        output.Position = 0;
        return output;
    }
}