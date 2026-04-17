using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Aspose.BarCode.Generation;
using Aspose.BarCode;

class Program
{
    static void Main()
    {
        const string encryptedFilePath = "barcode_state.enc";
        const string outputImagePath = "restored.png";

        // Simple AES key and IV (for demonstration only)
        byte[] key = Encoding.UTF8.GetBytes("1234567890ABCDEF"); // 16 bytes = 128‑bit key
        byte[] iv = Encoding.UTF8.GetBytes("ABCDEF1234567890"); // 16 bytes IV

        if (!File.Exists(encryptedFilePath))
        {
            Console.WriteLine($"Encrypted file not found: {encryptedFilePath}");
            return;
        }

        byte[] encryptedData;
        try
        {
            encryptedData = File.ReadAllBytes(encryptedFilePath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to read encrypted file: {ex.Message}");
            return;
        }

        byte[] decryptedXml;
        try
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;
                aes.Padding = PaddingMode.PKCS7;
                aes.Mode = CipherMode.CBC;

                using (ICryptoTransform decryptor = aes.CreateDecryptor())
                using (MemoryStream msInput = new MemoryStream(encryptedData))
                using (CryptoStream cs = new CryptoStream(msInput, decryptor, CryptoStreamMode.Read))
                using (MemoryStream msOutput = new MemoryStream())
                {
                    cs.CopyTo(msOutput);
                    decryptedXml = msOutput.ToArray();
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Decryption failed: {ex.Message}");
            return;
        }

        // Import barcode settings from the decrypted XML
        try
        {
            using (MemoryStream xmlStream = new MemoryStream(decryptedXml))
            using (BarcodeGenerator generator = BarcodeGenerator.ImportFromXml(xmlStream))
            {
                // Save the restored barcode image to verify successful import
                generator.Save(outputImagePath);
                Console.WriteLine($"Barcode restored and saved to: {outputImagePath}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ImportFromXml failed: {ex.Message}");
        }
    }
}