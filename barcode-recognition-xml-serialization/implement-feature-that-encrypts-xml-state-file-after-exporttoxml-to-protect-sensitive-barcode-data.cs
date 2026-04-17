using System;
using System.IO;
using System.Security.Cryptography;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Paths for generated files
        string imagePath = "barcode.png";
        string xmlPath = "barcode_state.xml";
        string encryptedPath = "barcode_state.enc";

        // Create a barcode, save image and export its state to XML
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Example of setting a property
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Save the barcode image
            generator.Save(imagePath);

            // Export barcode settings to XML file
            bool exported = generator.ExportToXml(xmlPath);
            if (!exported)
            {
                Console.WriteLine("Failed to export barcode settings to XML.");
                return;
            }
        }

        // Simple AES key and IV (for demonstration only; use secure key management in production)
        byte[] key = new byte[32];
        byte[] iv = new byte[16];
        for (int i = 0; i < key.Length; i++) key[i] = (byte)(i + 1);
        for (int i = 0; i < iv.Length; i++) iv[i] = (byte)(i + 1);

        // Encrypt the XML file
        using (var aes = Aes.Create())
        {
            aes.Key = key;
            aes.IV = iv;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using (var inputStream = new FileStream(xmlPath, FileMode.Open, FileAccess.Read))
            using (var outputStream = new FileStream(encryptedPath, FileMode.Create, FileAccess.Write))
            using (var encryptor = aes.CreateEncryptor())
            using (var cryptoStream = new CryptoStream(outputStream, encryptor, CryptoStreamMode.Write))
            {
                inputStream.CopyTo(cryptoStream);
            }
        }

        // Remove the plain XML file to keep only the encrypted version
        if (File.Exists(xmlPath))
        {
            File.Delete(xmlPath);
        }

        Console.WriteLine("Barcode generated and XML state encrypted successfully.");
    }
}