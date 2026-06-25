using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating barcodes, encrypting the images with a simple XOR cipher,
/// saving them to disk, then decrypting and reading the barcodes back.
/// </summary>
class Program
{
    /// <summary>
    /// Performs a simple XOR transformation on the supplied byte array using the given key.
    /// This method is used for both encryption and decryption.
    /// </summary>
    /// <param name="data">The input byte array to transform.</param>
    /// <param name="key">The XOR key.</param>
    /// <returns>A new byte array containing the transformed data.</returns>
    static byte[] XorTransform(byte[] data, byte key)
    {
        byte[] result = new byte[data.Length];
        for (int i = 0; i < data.Length; i++)
        {
            // Apply XOR operation per byte
            result[i] = (byte)(data[i] ^ key);
        }
        return result;
    }

    /// <summary>
    /// Entry point of the program. Generates sample barcodes, encrypts them,
    /// saves to files, then decrypts and reads the barcodes.
    /// </summary>
    static void Main()
    {
        // Encryption key (example)
        const byte xorKey = 0xAA;

        // Sample barcode definitions: (symbology, text, output file name)
        var samples = new (BaseEncodeType type, string text, string file)[]
        {
            (EncodeTypes.Code128, "Sample123", "encrypted1.bin"),
            (EncodeTypes.QR, "HelloWorld", "encrypted2.bin"),
            (EncodeTypes.DataMatrix, "DataMatrixTest", "encrypted3.bin")
        };

        // -----------------------------------------------------------------
        // Step 1: Generate sample barcode images, encrypt them and save to files
        // -----------------------------------------------------------------
        foreach (var (type, text, filePath) in samples)
        {
            // Generate barcode image into a memory stream
            using (var ms = new MemoryStream())
            {
                using (var generator = new BarcodeGenerator(type, text))
                {
                    // Save the barcode as PNG into the memory stream
                    generator.Save(ms, BarCodeImageFormat.Png);
                }

                // Encrypt the image bytes using XOR
                byte[] encryptedBytes = XorTransform(ms.ToArray(), xorKey);

                // Write the encrypted bytes to the specified file
                File.WriteAllBytes(filePath, encryptedBytes);
            }
        }

        // -----------------------------------------------------------------
        // Step 2: Decrypt each file in memory and decode barcodes
        // -----------------------------------------------------------------
        foreach (var (_, _, filePath) in samples)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                continue;
            }

            // Read the encrypted bytes from disk
            byte[] encryptedData = File.ReadAllBytes(filePath);

            // Decrypt the bytes using the same XOR key
            byte[] decryptedData = XorTransform(encryptedData, xorKey);

            // Load the decrypted image into a Bitmap for barcode reading
            using (var imageStream = new MemoryStream(decryptedData))
            using (var bitmap = new Bitmap(imageStream))
            // Create a BarCodeReader that supports all barcode types
            using (var reader = new BarCodeReader(bitmap, DecodeType.AllSupportedTypes))
            {
                // Enable checksum validation (optional but recommended)
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

                // Attempt to read all barcodes from the image
                var results = reader.ReadBarCodes();

                if (results.Length == 0)
                {
                    Console.WriteLine($"No barcode detected in file: {filePath}");
                }
                else
                {
                    foreach (var result in results)
                    {
                        Console.WriteLine($"File: {filePath}");
                        Console.WriteLine($"  Type: {result.CodeTypeName}");
                        Console.WriteLine($"  Text: {result.CodeText}");
                        Console.WriteLine($"  Confidence: {result.Confidence}");
                        Console.WriteLine($"  ReadingQuality: {result.ReadingQuality}");
                    }
                }
            }
        }
    }
}