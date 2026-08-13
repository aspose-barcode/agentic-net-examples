// Title: Decrypt Encrypted Barcode Image and Decode QR Code
// Description: This example shows how to decrypt an AES‑CBC encrypted image containing a QR code and then decode the barcode using Aspose.BarCode.
// Category-Description: Demonstrates a common Aspose.BarCode workflow—reading barcode images from encrypted sources. It covers decryption with System.Security.Cryptography, in‑memory image handling with MemoryStream, and barcode recognition via BarCodeReader. Ideal for developers needing secure storage of barcode images and runtime decoding without writing plaintext files.
// Prompt: Decode a set of barcodes from encrypted image files after decrypting them in memory.
// Tags: qr, barcode, decryption, aes, aspose.barcode, aspose.drawing, memorystream

using System;
using System.IO;
using System.Security.Cryptography;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Provides functionality to encrypt a barcode image, decrypt it in memory,
/// and decode the barcode using Aspose.BarCode.
/// </summary>
class Program
{
    // Simple AES-CBC decryption returning a MemoryStream with the plaintext
    static MemoryStream DecryptToStream(byte[] encryptedData, byte[] key, byte[] iv)
    {
        using (var aes = Aes.Create())
        {
            aes.Key = key;
            aes.IV = iv;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using (var decryptor = aes.CreateDecryptor())
            using (var msInput = new MemoryStream(encryptedData))
            using (var cs = new CryptoStream(msInput, decryptor, CryptoStreamMode.Read))
            {
                var msOutput = new MemoryStream();
                cs.CopyTo(msOutput);
                msOutput.Position = 0;
                return msOutput;
            }
        }
    }

    // Simple AES-CBC encryption used to create a sample encrypted file
    static byte[] EncryptData(byte[] plainData, byte[] key, byte[] iv)
    {
        using (var aes = Aes.Create())
        {
            aes.Key = key;
            aes.IV = iv;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using (var encryptor = aes.CreateEncryptor())
            using (var msOutput = new MemoryStream())
            using (var cs = new CryptoStream(msOutput, encryptor, CryptoStreamMode.Write))
            {
                cs.Write(plainData, 0, plainData.Length);
                cs.FlushFinalBlock();
                return msOutput.ToArray();
            }
        }
    }

    /// <summary>
    /// Entry point of the program. Generates a QR code, encrypts it, decrypts it in memory,
    /// and then decodes the barcode.
    /// </summary>
    static void Main()
    {
        // Sample AES key/IV (for demo purposes only)
        byte[] key = new byte[32]; // 256‑bit key
        byte[] iv = new byte[16];  // 128‑bit IV
        for (int i = 0; i < key.Length; i++) key[i] = (byte)(i + 1);
        for (int i = 0; i < iv.Length; i++) iv[i] = (byte)(i + 1);

        // Prepare temporary folder for the encrypted file
        string folder = Path.Combine(Directory.GetCurrentDirectory(), "temp");
        Directory.CreateDirectory(folder);
        string encryptedPath = Path.Combine(folder, "barcode_encrypted.bin");

        // -----------------------------------------------------------------
        // Step 1: Generate a barcode image and encrypt it (sample data)
        // -----------------------------------------------------------------
        if (!File.Exists(encryptedPath))
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, "HelloWorld"))
            {
                using (var plainStream = new MemoryStream())
                {
                    // Save barcode as PNG into memory
                    generator.Save(plainStream, BarCodeImageFormat.Png);
                    byte[] plainBytes = plainStream.ToArray();

                    // Encrypt the PNG bytes
                    byte[] encryptedBytes = EncryptData(plainBytes, key, iv);
                    File.WriteAllBytes(encryptedPath, encryptedBytes);
                }
            }
        }

        // -----------------------------------------------------------------
        // Step 2: Read the encrypted file, decrypt it in memory, decode barcode
        // -----------------------------------------------------------------
        if (!File.Exists(encryptedPath))
        {
            Console.WriteLine($"Encrypted file not found: {encryptedPath}");
            return;
        }

        byte[] encryptedData = File.ReadAllBytes(encryptedPath);
        using (var decryptedStream = DecryptToStream(encryptedData, key, iv))
        {
            // Use BarCodeReader on the decrypted image stream
            using (var reader = new BarCodeReader(decryptedStream, DecodeType.QR))
            {
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Detected Barcode Type: {result.CodeTypeName}");
                    Console.WriteLine($"Decoded Text: {result.CodeText}");
                }

                if (reader.FoundCount == 0)
                {
                    Console.WriteLine("No barcode detected in the decrypted image.");
                }
            }
        }

        // Cleanup (optional)
        // Directory.Delete(folder, true);
    }
}