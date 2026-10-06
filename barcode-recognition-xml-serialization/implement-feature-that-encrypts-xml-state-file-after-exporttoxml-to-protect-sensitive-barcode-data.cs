// Title: Encrypt exported barcode generator XML state file
// Description: Demonstrates exporting a barcode generator's state to XML and then encrypting the file to protect sensitive data.
// Category-Description: This example belongs to the Aspose.BarCode state management category, illustrating how to use BarcodeGenerator, ExportToXml, and standard .NET cryptography classes to secure barcode configuration data. Typical scenarios include persisting barcode settings in a protected format for later reuse or compliance requirements. Developers often need to serialize generator settings, store them safely, and restore them without exposing confidential information.
// Prompt: Implement a feature that encrypts the XML state file after ExportToXml to protect sensitive barcode data.
// Tags: barcode symbology, export, encryption, xml, aspose.barcode, aes, state management

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a QR barcode, exports its configuration to XML,
/// encrypts the XML file using AES, and saves the barcode image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs barcode generation, XML export, encryption,
    /// and cleanup of temporary files.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare a unique temporary output directory for the demo files.
        // --------------------------------------------------------------------
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Define file paths for the XML state, encrypted output, and barcode image.
        string xmlPath = Path.Combine(outputDir, "generator.xml");
        string encryptedPath = Path.Combine(outputDir, "generator.enc");
        string imagePath = Path.Combine(outputDir, "barcode.png");

        // --------------------------------------------------------------------
        // Create a barcode generator, configure it, export its state to XML,
        // and save the generated barcode image.
        // --------------------------------------------------------------------
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "SensitiveData"))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.ExportToXml(xmlPath);
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // --------------------------------------------------------------------
        // Derive a symmetric key and IV from a password.
        // This is for demonstration only; in production use a secure key management approach.
        // --------------------------------------------------------------------
        byte[] key;
        byte[] iv;
        using (SHA256 sha256 = SHA256.Create())
        {
            key = sha256.ComputeHash(Encoding.UTF8.GetBytes("password"));
        }
        using (MD5 md5 = MD5.Create())
        {
            iv = md5.ComputeHash(Encoding.UTF8.GetBytes("password"));
        }

        // --------------------------------------------------------------------
        // Encrypt the exported XML file using AES and write the ciphertext to a new file.
        // --------------------------------------------------------------------
        using (FileStream inputFile = new FileStream(xmlPath, FileMode.Open, FileAccess.Read))
        using (FileStream outputFile = new FileStream(encryptedPath, FileMode.Create, FileAccess.Write))
        using (Aes aes = Aes.Create())
        {
            aes.Key = key;
            aes.IV = iv;
            using (CryptoStream cryptoStream = new CryptoStream(outputFile, aes.CreateEncryptor(), CryptoStreamMode.Write))
            {
                inputFile.CopyTo(cryptoStream);
            }
        }

        // --------------------------------------------------------------------
        // Remove the unencrypted XML file to ensure only the encrypted version remains.
        // --------------------------------------------------------------------
        File.Delete(xmlPath);

        // Output the locations of the generated files for user reference.
        Console.WriteLine($"Encrypted XML saved to: {encryptedPath}");
        Console.WriteLine($"Generated barcode image saved to: {imagePath}");
    }
}