using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    // Simple AES encryption
    private static byte[] EncryptString(string plainText, byte[] key, byte[] iv)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = key;
            aes.IV = iv;
            using (var encryptor = aes.CreateEncryptor())
            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                using (var sw = new StreamWriter(cs, Encoding.UTF8))
                {
                    sw.Write(plainText);
                }
                return ms.ToArray();
            }
        }
    }

    // Simple AES decryption
    private static string DecryptBytes(byte[] cipherText, byte[] key, byte[] iv)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = key;
            aes.IV = iv;
            using (var decryptor = aes.CreateDecryptor())
            using (var ms = new MemoryStream(cipherText))
            using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
            using (var sr = new StreamReader(cs, Encoding.UTF8))
            {
                return sr.ReadToEnd();
            }
        }
    }

    static void Main()
    {
        // Demo key/IV (must be 16 bytes for AES-128)
        byte[] key = Encoding.UTF8.GetBytes("1234567890ABCDEF");
        byte[] iv = Encoding.UTF8.GetBytes("ABCDEF1234567890");

        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(tempDir);

        string xmlFilePath = Path.Combine(tempDir, "barcode_state.xml");
        string encryptedFilePath = Path.Combine(tempDir, "barcode_state.enc");
        string restoredImagePath = Path.Combine(tempDir, "restored.png");

        // 1. Create a barcode generator and export its settings to XML (in memory)
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Demo123"))
        {
            using (var xmlStream = new MemoryStream())
            {
                bool exported = generator.ExportToXml(xmlStream);
                if (!exported)
                {
                    Console.WriteLine("Failed to export barcode settings to XML.");
                    return;
                }

                // Get XML string
                string xmlContent = Encoding.UTF8.GetString(xmlStream.ToArray());

                // 2. Encrypt the XML and save to a file
                byte[] encryptedData = EncryptString(xmlContent, key, iv);
                File.WriteAllBytes(encryptedFilePath, encryptedData);
                Console.WriteLine($"Encrypted XML saved to: {encryptedFilePath}");
            }
        }

        // 3. Read the encrypted file, decrypt it back to XML
        byte[] encryptedBytes = File.ReadAllBytes(encryptedFilePath);
        string decryptedXml = DecryptBytes(encryptedBytes, key, iv);
        Console.WriteLine("XML decrypted successfully.");

        // 4. Import the barcode settings from the decrypted XML
        using (var xmlMemory = new MemoryStream(Encoding.UTF8.GetBytes(decryptedXml)))
        {
            BarcodeGenerator restoredGenerator = BarcodeGenerator.ImportFromXml(xmlMemory);
            // 5. Save the restored barcode image
            restoredGenerator.Save(restoredImagePath);
            Console.WriteLine($"Restored barcode image saved to: {restoredImagePath}");
        }

        // Optional: demonstrate recognition of the restored image
        using (var reader = new BarCodeReader(restoredImagePath, DecodeType.Code128))
        {
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Recognized CodeText: {result.CodeText}");
            }
        }
    }
}