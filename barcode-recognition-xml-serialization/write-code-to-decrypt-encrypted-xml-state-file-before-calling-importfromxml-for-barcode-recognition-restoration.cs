// Title: Decrypt encrypted XML state file for barcode reader restoration
// Description: Demonstrates decrypting an AES‑encrypted XML state file and importing it to restore barcode recognition settings.
// Category-Description: This example belongs to the Aspose.BarCode state management category, showing how to export, encrypt, decrypt, and import barcode reader settings using ExportToXml, ImportFromXml, and AES encryption. Developers often need to persist and protect reader configurations for later reuse, especially in secure or distributed environments.
// Prompt: Write code to decrypt an encrypted XML state file before calling ImportFromXml for barcode recognition restoration.
// Tags: barcode symbology, encryption, importfromxml, aesencryption, barcoderecognition

using System;
using System.IO;
using System.Security.Cryptography;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates exporting a barcode reader state to XML, encrypting the XML,
/// decrypting it back into memory, and restoring the reader settings using
/// <c>BarCodeReader.ImportFromXml</c>. The example uses AES‑128 for encryption
/// and shows how to reuse the same barcode image with the restored settings.
/// </summary>
class Program
{
    /// <summary>
    /// Encrypts the specified input file using AES and writes the ciphertext to the output file.
    /// This method is for demonstration purposes only; a fixed key/IV is used.
    /// </summary>
    /// <param name="inputPath">Path to the plaintext file.</param>
    /// <param name="outputPath">Path where the encrypted file will be created.</param>
    /// <param name="key">AES key (16 bytes for AES‑128).</param>
    /// <param name="iv">AES initialization vector (16 bytes).</param>
    private static void EncryptFile(string inputPath, string outputPath, byte[] key, byte[] iv)
    {
        using (var aes = Aes.Create())
        {
            aes.Key = key;
            aes.IV = iv;

            using (var inputFile = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
            using (var outputFile = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            using (var cryptoStream = new CryptoStream(outputFile, aes.CreateEncryptor(), CryptoStreamMode.Write))
            {
                // Copy plaintext into the crypto stream to produce encrypted output.
                inputFile.CopyTo(cryptoStream);
            }
        }
    }

    /// <summary>
    /// Decrypts an AES‑encrypted file and returns its contents in a <see cref="MemoryStream"/>.
    /// </summary>
    /// <param name="encryptedPath">Path to the encrypted file.</param>
    /// <param name="key">AES key used for decryption.</param>
    /// <param name="iv">AES initialization vector used for decryption.</param>
    /// <returns>A memory stream containing the decrypted XML data.</returns>
    private static MemoryStream DecryptToMemoryStream(string encryptedPath, byte[] key, byte[] iv)
    {
        var memoryStream = new MemoryStream();

        using (var aes = Aes.Create())
        {
            aes.Key = key;
            aes.IV = iv;

            using (var encryptedFile = new FileStream(encryptedPath, FileMode.Open, FileAccess.Read))
            using (var cryptoStream = new CryptoStream(encryptedFile, aes.CreateDecryptor(), CryptoStreamMode.Read))
            {
                // Copy decrypted bytes into the memory stream.
                cryptoStream.CopyTo(memoryStream);
            }
        }

        // Reset the stream position so it can be read from the beginning.
        memoryStream.Position = 0;
        return memoryStream;
    }

    /// <summary>
    /// Entry point of the example. Generates a barcode, exports its reader state,
    /// encrypts the state XML, decrypts it, and restores the reader settings for
    /// barcode recognition.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Define file paths used throughout the example.
        // --------------------------------------------------------------------
        const string barcodePath = "barcode.png";
        const string xmlPath = "state.xml";
        const string encryptedPath = "state.enc";

        // --------------------------------------------------------------------
        // Prepare a fixed AES‑128 key and IV (for demo only; use secure keys in production).
        // --------------------------------------------------------------------
        byte[] key = new byte[16];
        byte[] iv = new byte[16];
        for (int i = 0; i < 16; i++)
        {
            key[i] = (byte)i;
            iv[i] = (byte)(16 - i);
        }

        // --------------------------------------------------------------------
        // 1. Generate a sample barcode image (Code128 with value "123456").
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            generator.Save(barcodePath);
        }

        // --------------------------------------------------------------------
        // 2. Create a reader, assign the image, and perform an initial read.
        // --------------------------------------------------------------------
        using (var reader = new BarCodeReader(barcodePath, DecodeType.AllSupportedTypes))
        {
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"[Initial] Type: {result.CodeTypeName}, Text: {result.CodeText}");
            }

            // 3. Export the reader's configuration and state to an XML file.
            reader.ExportToXml(xmlPath);
        }

        // --------------------------------------------------------------------
        // 4. Encrypt the exported XML state file using the fixed key/IV.
        // --------------------------------------------------------------------
        if (File.Exists(xmlPath))
        {
            EncryptFile(xmlPath, encryptedPath, key, iv);
        }
        else
        {
            Console.WriteLine("Exported XML file not found.");
            return;
        }

        // --------------------------------------------------------------------
        // 5. Decrypt the encrypted XML back into a memory stream.
        // --------------------------------------------------------------------
        using (var decryptedStream = DecryptToMemoryStream(encryptedPath, key, iv))
        {
            // 6. Import the reader settings from the decrypted XML stream.
            using (var importedReader = BarCodeReader.ImportFromXml(decryptedStream))
            {
                // 7. Assign the same barcode image to the imported reader.
                importedReader.SetBarCodeImage(barcodePath);

                // 8. Perform barcode recognition using the restored settings.
                foreach (var result in importedReader.ReadBarCodes())
                {
                    Console.WriteLine($"[Restored] Type: {result.CodeTypeName}, Text: {result.CodeText}");
                }
            }
        }
    }
}