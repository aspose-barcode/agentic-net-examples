// Title: Encrypt exported barcode XML state file
// Description: Demonstrates exporting a barcode configuration to XML and then encrypting the file to protect sensitive data.
// Category-Description: This example belongs to the Aspose.BarCode configuration management category, showing how to use BarcodeGenerator, ExportToXml, and standard .NET cryptography classes to secure barcode state files. Developers often need to store barcode settings securely for later reuse, requiring encryption of the XML representation. The snippet illustrates typical use cases such as persisting and protecting barcode configurations in enterprise applications.
// Prompt: Implement a feature that encrypts the XML state file after ExportToXml to protect sensitive barcode data.
// Tags: barcode symbology, export, xml, encryption, aes, aspnet, aspose.barcode

using System;
using System.IO;
using System.Security.Cryptography;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates exporting a barcode generator's configuration to XML and encrypting the resulting file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Code128 barcode, exports its state to XML, encrypts the XML, and cleans up the plaintext file.
    /// </summary>
    static void Main()
    {
        // Paths for the intermediate XML and the final encrypted file
        string xmlPath = "barcode_state.xml";
        string encryptedPath = "barcode_state.enc";

        // Create a barcode generator, configure it, and export its state to an XML file
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            bool exported = generator.ExportToXml(xmlPath);
            Console.WriteLine($"Exported to XML: {exported}");
        }

        // Prepare a demo AES key and IV (replace with secure values in production)
        byte[] key = new byte[32]; // 256‑bit key
        byte[] iv = new byte[16];  // 128‑bit IV
        for (int i = 0; i < key.Length; i++) key[i] = (byte)(i + 1);
        for (int i = 0; i < iv.Length; i++) iv[i] = (byte)(i + 1);

        // Encrypt the XML file using AES and write the ciphertext to the encrypted file path
        using (Aes aes = Aes.Create())
        {
            aes.Key = key;
            aes.IV = iv;

            using (FileStream inputFile = new FileStream(xmlPath, FileMode.Open, FileAccess.Read))
            using (FileStream encryptedFile = new FileStream(encryptedPath, FileMode.Create, FileAccess.Write))
            using (CryptoStream cryptoStream = new CryptoStream(encryptedFile, aes.CreateEncryptor(), CryptoStreamMode.Write))
            {
                inputFile.CopyTo(cryptoStream);
            }
        }

        Console.WriteLine($"Encrypted XML saved to: {encryptedPath}");

        // Attempt to delete the plain XML file to ensure only the encrypted version remains
        try
        {
            File.Delete(xmlPath);
            Console.WriteLine("Plain XML file deleted.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to delete plain XML: {ex.Message}");
        }
    }
}