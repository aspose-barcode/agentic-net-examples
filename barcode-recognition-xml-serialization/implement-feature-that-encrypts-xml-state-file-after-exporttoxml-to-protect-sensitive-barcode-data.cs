using System;
using System.IO;
using System.Security.Cryptography;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates exporting a barcode generator state to XML and encrypting the XML file using AES.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a Code128 barcode, exports its state to an XML file, and encrypts that file.
    /// </summary>
    static void Main()
    {
        // Define file paths for the XML state and the encrypted output
        string xmlPath = "barcode_state.xml";
        string encryptedPath = "barcode_state.enc";

        // Create a barcode generator for Code128 with sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Export the generator's state to an XML file
            bool exported = generator.ExportToXml(xmlPath);
            Console.WriteLine(exported
                ? $"Exported XML to '{xmlPath}'."
                : $"Failed to export XML to '{xmlPath}'.");
        }

        // Ensure the XML file was created before attempting encryption
        if (!File.Exists(xmlPath))
        {
            Console.WriteLine("XML file not found. Encryption aborted.");
            return;
        }

        // Prepare a deterministic 256‑bit key and 128‑bit IV for demonstration purposes
        byte[] key = new byte[32]; // 256-bit key
        byte[] iv = new byte[16];  // 128-bit IV
        for (int i = 0; i < key.Length; i++) key[i] = (byte)(i + 1);
        for (int i = 0; i < iv.Length; i++) iv[i] = (byte)(i + 1);

        // Perform AES encryption of the XML file
        using (var aes = Aes.Create())
        {
            aes.Key = key;
            aes.IV = iv;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            // Open the source XML file for reading
            using (var inputFile = new FileStream(xmlPath, FileMode.Open, FileAccess.Read))
            // Create the destination file for the encrypted data
            using (var outputFile = new FileStream(encryptedPath, FileMode.Create, FileAccess.Write))
            // Set up a CryptoStream to handle encryption while writing
            using (var cryptoStream = new CryptoStream(outputFile, aes.CreateEncryptor(), CryptoStreamMode.Write))
            {
                // Copy the entire XML content into the CryptoStream, which encrypts it on the fly
                inputFile.CopyTo(cryptoStream);
            }
        }

        // Inform the user that encryption succeeded
        Console.WriteLine($"Encrypted XML saved to '{encryptedPath}'.");
    }
}