// Title: Encrypt barcode state XML after export
// Description: Demonstrates exporting a barcode's state to XML and then encrypting the file to protect sensitive data.
// Prompt: Implement a feature that encrypts the XML state file after ExportToXml to protect sensitive barcode data.
// Tags: barcode symbology, export, xml, encryption, aes, aspose.barcode

using System;
using System.IO;
using System.Security.Cryptography;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a barcode, exports its state to an XML file,
/// and then encrypts that XML file using AES to protect sensitive barcode data.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Performs barcode generation, XML export,
    /// AES encryption of the exported file, and cleanup of the plain XML.
    /// </summary>
    static void Main()
    {
        // Define file paths for the intermediate XML state and the final encrypted output
        string xmlPath = "barcode_state.xml";
        string encryptedPath = "barcode_state.enc";

        // Create a barcode generator for Code128 with sample data and export its state to XML
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Export the barcode properties to an XML file; ExportToXml returns true on success
            bool exported = generator.ExportToXml(xmlPath);
            if (!exported)
            {
                Console.WriteLine("Failed to export barcode state to XML.");
                return;
            }
        }

        // Prepare static AES key and IV for demonstration (do NOT use static values in production)
        byte[] key = new byte[32]; // 256‑bit key
        byte[] iv = new byte[16];  // 128‑bit IV
        for (int i = 0; i < key.Length; i++) key[i] = (byte)(i + 1);
        for (int i = 0; i < iv.Length; i++) iv[i] = (byte)(i + 1);

        // Encrypt the XML file using AES and write the ciphertext to a new file
        using (Aes aes = Aes.Create())
        {
            aes.Key = key;
            aes.IV = iv;

            using (FileStream inputFile = new FileStream(xmlPath, FileMode.Open, FileAccess.Read))
            using (FileStream outputFile = new FileStream(encryptedPath, FileMode.Create, FileAccess.Write))
            using (CryptoStream cryptoStream = new CryptoStream(outputFile, aes.CreateEncryptor(), CryptoStreamMode.Write))
            {
                // Copy the plaintext XML data into the CryptoStream, which encrypts it on the fly
                inputFile.CopyTo(cryptoStream);
            }
        }

        // Attempt to delete the original plain XML file, leaving only the encrypted version
        try
        {
            File.Delete(xmlPath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Could not delete original XML file: {ex.Message}");
        }

        Console.WriteLine($"Barcode state encrypted successfully to '{encryptedPath}'.");
    }
}