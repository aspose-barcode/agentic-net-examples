using System;
using System.IO;
using System.Security.Cryptography;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    // Sample AES key and IV (for demonstration only)
    private static readonly byte[] AesKey = new byte[32] {
        0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,
        0x08,0x09,0x0A,0x0B,0x0C,0x0D,0x0E,0x0F,
        0x10,0x11,0x12,0x13,0x14,0x15,0x16,0x17,
        0x18,0x19,0x1A,0x1B,0x1C,0x1D,0x1E,0x1F };
    private static readonly byte[] AesIv = new byte[16] {
        0xA0,0xA1,0xA2,0xA3,0xA4,0xA5,0xA6,0xA7,
        0xA8,0xA9,0xAA,0xAB,0xAC,0xAD,0xAE,0xAF };

    static void Main()
    {
        // List of encrypted image files to process
        string[] encryptedFiles = new string[]
        {
            "encrypted1.bin",
            "encrypted2.bin"
        };

        foreach (string encFile in encryptedFiles)
        {
            if (!File.Exists(encFile))
            {
                Console.WriteLine($"File not found: {encFile}");
                continue;
            }

            // Decrypt the file into a memory stream
            using (MemoryStream decryptedStream = DecryptFileToMemory(encFile))
            {
                // Reset position just in case
                decryptedStream.Position = 0;

                // Initialize BarCodeReader with the decrypted image stream
                using (BarCodeReader reader = new BarCodeReader(decryptedStream, DecodeType.AllSupportedTypes))
                {
                    // Read all barcodes from the image
                    BarCodeResult[] results = reader.ReadBarCodes();

                    if (results.Length == 0)
                    {
                        Console.WriteLine($"No barcodes found in {encFile}");
                    }
                    else
                    {
                        Console.WriteLine($"Barcodes in {encFile}:");
                        foreach (BarCodeResult result in results)
                        {
                            Console.WriteLine($"  Type: {result.CodeTypeName}, Text: {result.CodeText}");
                        }
                    }
                }
            }
        }
    }

    // Decrypts an encrypted file using AES and returns a MemoryStream containing the plaintext image data
    private static MemoryStream DecryptFileToMemory(string encryptedFilePath)
    {
        // Read all encrypted bytes
        byte[] encryptedData = File.ReadAllBytes(encryptedFilePath);

        // Prepare streams
        MemoryStream output = new MemoryStream();

        using (Aes aes = Aes.Create())
        {
            aes.Key = AesKey;
            aes.IV = AesIv;
            aes.Padding = PaddingMode.PKCS7;
            aes.Mode = CipherMode.CBC;

            using (MemoryStream input = new MemoryStream(encryptedData))
            using (CryptoStream cryptoStream = new CryptoStream(input, aes.CreateDecryptor(), CryptoStreamMode.Read))
            {
                // Copy decrypted bytes to output stream
                cryptoStream.CopyTo(output);
            }
        }

        // Set position to beginning for subsequent reading
        output.Position = 0;
        return output;
    }
}