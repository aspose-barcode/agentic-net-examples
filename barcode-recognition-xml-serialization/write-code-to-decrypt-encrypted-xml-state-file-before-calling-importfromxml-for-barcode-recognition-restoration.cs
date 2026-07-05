// Title: Decrypt Encrypted XML State for Barcode Recognition
// Description: Demonstrates decrypting an AES‑encrypted XML state file and importing it into Aspose.BarCode to restore barcode recognition settings.
// Prompt: Write code to decrypt an encrypted XML state file before calling ImportFromXml for barcode recognition restoration.
// Tags: barcode, decryption, xml, import, aspose.barcode, aes

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that decrypts an encrypted XML state file and restores barcode recognition settings using Aspose.BarCode.
/// </summary>
class Program
{
    // Sample AES key (32 bytes) and IV (16 bytes) for encryption/decryption.
    private static readonly byte[] AesKey = Encoding.UTF8.GetBytes("0123456789ABCDEF0123456789ABCDEF");
    private static readonly byte[] AesIv = Encoding.UTF8.GetBytes("ABCDEF0123456789");

    /// <summary>
    /// Entry point. Ensures an encrypted state file exists, decrypts it, imports settings, generates a matching barcode, and reads it.
    /// </summary>
    static void Main()
    {
        const string encryptedFilePath = "encrypted_state.bin";

        // Ensure an encrypted state file exists. If not, create one from a sample barcode.
        if (!File.Exists(encryptedFilePath))
        {
            CreateEncryptedStateFile(encryptedFilePath);
        }

        // Decrypt the XML state.
        byte[] decryptedXml = DecryptFile(encryptedFilePath, AesKey, AesIv);

        // Import the settings into a BarCodeReader instance.
        using (var xmlStream = new MemoryStream(decryptedXml))
        using (var reader = BarCodeReader.ImportFromXml(xmlStream))
        {
            if (reader == null)
            {
                Console.WriteLine("Failed to import settings from XML.");
                return;
            }

            // Generate a barcode image that matches the imported settings.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
            using (var barcodeImage = generator.GenerateBarCodeImage())
            {
                // Assign the image to the reader.
                reader.SetBarCodeImage(barcodeImage);

                // Perform recognition and output results.
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"BarCode Type: {result.CodeTypeName}");
                    Console.WriteLine($"BarCode CodeText: {result.CodeText}");
                }
            }
        }
    }

    // Creates an encrypted XML state file from a sample barcode's settings.
    private static void CreateEncryptedStateFile(string filePath)
    {
        // Generate a sample barcode and export its settings to XML.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        using (var xmlMemory = new MemoryStream())
        {
            generator.ExportToXml(xmlMemory);
            byte[] xmlBytes = xmlMemory.ToArray();

            // Encrypt the XML bytes.
            byte[] encryptedBytes = Encrypt(xmlBytes, AesKey, AesIv);

            // Write encrypted data to file.
            File.WriteAllBytes(filePath, encryptedBytes);
        }
    }

    // Encrypts plain data using AES-CBC with PKCS7 padding.
    private static byte[] Encrypt(byte[] plainData, byte[] key, byte[] iv)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = key;
            aes.IV = iv;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using (ICryptoTransform encryptor = aes.CreateEncryptor())
            using (var ms = new MemoryStream())
            using (var cryptoStream = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
            {
                cryptoStream.Write(plainData, 0, plainData.Length);
                cryptoStream.FlushFinalBlock();
                return ms.ToArray();
            }
        }
    }

    // Decrypts the entire file content using AES-CBC with PKCS7 padding.
    private static byte[] DecryptFile(string filePath, byte[] key, byte[] iv)
    {
        byte[] cipherData = File.ReadAllBytes(filePath);
        using (Aes aes = Aes.Create())
        {
            aes.Key = key;
            aes.IV = iv;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using (ICryptoTransform decryptor = aes.CreateDecryptor())
            using (var ms = new MemoryStream(cipherData))
            using (var cryptoStream = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
            using (var resultStream = new MemoryStream())
            {
                cryptoStream.CopyTo(resultStream);
                return resultStream.ToArray();
            }
        }
    }
}