using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Paths for the XML state file and the encrypted output
        string xmlPath = "barcode_state.xml";
        string encryptedPath = "barcode_state.enc";

        // Create a barcode generator, set the code text and save an image
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            generator.CodeText = "123ABC";
            generator.Save("barcode.png");

            // Export the barcode settings to an XML file
            bool exported = generator.ExportToXml(xmlPath);
            Console.WriteLine($"Exported to XML: {exported}");
        }

        // Simple 256‑bit key and 128‑bit IV for demonstration purposes
        // In a real application store keys securely (e.g., Azure Key Vault)
        byte[] key = Encoding.UTF8.GetBytes("0123456789ABCDEF0123456789ABCDEF"); // 32 bytes
        byte[] iv  = Encoding.UTF8.GetBytes("0123456789ABCDEF");               // 16 bytes

        // Encrypt the XML file using AES and write the encrypted data to a new file
        using (Aes aes = Aes.Create())
        {
            aes.Key = key;
            aes.IV = iv;

            using (FileStream inputFile = new FileStream(xmlPath, FileMode.Open, FileAccess.Read))
            using (FileStream outputFile = new FileStream(encryptedPath, FileMode.Create, FileAccess.Write))
            using (CryptoStream cryptoStream = new CryptoStream(outputFile, aes.CreateEncryptor(), CryptoStreamMode.Write))
            {
                inputFile.CopyTo(cryptoStream);
            }
        }

        Console.WriteLine($"Encrypted XML saved to: {encryptedPath}");
    }
}