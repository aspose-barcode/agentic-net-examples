// Title: Decode encrypted barcode images in memory
// Description: Demonstrates generating QR and Code128 barcodes, encrypting the image files with a simple XOR cipher, then decrypting them in memory and decoding the barcodes.
// Category-Description: This example belongs to the Aspose.BarCode image processing and recognition category, showcasing how to use BarcodeGenerator, BarCodeReader, and related classes for encryption‑aware workflows. Typical use cases include secure storage of barcode images and on‑the‑fly decoding without persisting plaintext files. Developers often need to combine file I/O, cryptographic transformations, and barcode APIs to meet security requirements.
// Prompt: Decode a set of barcodes from encrypted image files after decrypting them in memory.
// Tags: qr, code128, barcode, decode, encryption, memory, aspose.barcode, aspose.drawing

using System;
using System.IO;
using System.Text;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates barcodes, encrypts the image files,
/// decrypts them in memory, and decodes the barcodes using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Executes the generate‑encrypt‑decrypt‑decode workflow.
    /// </summary>
    static void Main()
    {
        // Create a temporary working folder for generated and encrypted files
        string workFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workFolder);

        // Simple XOR key used for both encryption and decryption
        byte xorKey = 0xAA;

        // Define the barcodes to generate: QR and Code128
        var barcodes = new (BaseEncodeType Encode, string Text, string FileName)[]
        {
            (EncodeTypes.QR, "Hello Aspose", "qr.png"),
            (EncodeTypes.Code128, "1234567890", "code128.png")
        };

        // -----------------------------------------------------------------
        // Generate barcode images and encrypt them on disk
        // -----------------------------------------------------------------
        foreach (var (encode, text, fileName) in barcodes)
        {
            string originalPath = Path.Combine(workFolder, fileName);
            string encryptedPath = originalPath + ".enc";

            // Generate barcode and save as PNG
            using (var generator = new BarcodeGenerator(encode, text))
            {
                generator.Save(originalPath, BarCodeImageFormat.Png);
            }

            // Encrypt the PNG file using XOR and write the encrypted version
            if (File.Exists(originalPath))
            {
                byte[] data = File.ReadAllBytes(originalPath);
                for (int i = 0; i < data.Length; i++)
                {
                    data[i] ^= xorKey;
                }
                File.WriteAllBytes(encryptedPath, data);

                // Remove the unencrypted file to keep only the encrypted copy
                File.Delete(originalPath);
            }
        }

        // Prepare the list of barcode types that the reader should attempt to decode
        BaseDecodeType[] decodeTypes = new BaseDecodeType[] { DecodeType.QR, DecodeType.Code128 };

        // -----------------------------------------------------------------
        // Decrypt each encrypted file in memory and decode the barcode
        // -----------------------------------------------------------------
        foreach (var (encode, text, fileName) in barcodes)
        {
            string encryptedPath = Path.Combine(workFolder, fileName + ".enc");
            if (!File.Exists(encryptedPath))
            {
                Console.WriteLine($"Encrypted file not found: {encryptedPath}");
                continue;
            }

            // Read encrypted bytes and decrypt them using the same XOR key
            byte[] encryptedData = File.ReadAllBytes(encryptedPath);
            for (int i = 0; i < encryptedData.Length; i++)
            {
                encryptedData[i] ^= xorKey;
            }

            // Decode the barcode from the decrypted memory stream
            using (var ms = new MemoryStream(encryptedData))
            using (var reader = new BarCodeReader(ms, decodeTypes))
            {
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Decoded Type: {result.CodeTypeName}, Text: {result.CodeText}");
                }
            }
        }

        // -----------------------------------------------------------------
        // Cleanup temporary files and folder
        // -----------------------------------------------------------------
        try
        {
            Directory.Delete(workFolder, true);
        }
        catch
        {
            // Suppress any cleanup errors (e.g., files still in use)
        }
    }
}