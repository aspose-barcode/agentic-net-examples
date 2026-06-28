using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a QR code from binary data and reading it back using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a QR code from a binary file, saves it, and reads it back.
    /// </summary>
    static void Main()
    {
        // Define file paths for the binary source and the generated QR image.
        string binaryFilePath = "sample.bin";
        string qrImagePath = "qr_binary.png";

        // Ensure a small binary file exists; create one with sample data if missing.
        if (!File.Exists(binaryFilePath))
        {
            byte[] sampleData = new byte[] { 0x01, 0x02, 0x03, 0x04, 0x05 };
            File.WriteAllBytes(binaryFilePath, sampleData);
        }

        // Read the entire binary content into a byte array.
        byte[] fileBytes = File.ReadAllBytes(binaryFilePath);

        // Generate a QR code that embeds the binary payload.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Set the binary data as the code text for the QR code.
            generator.SetCodeText(fileBytes);

            // Use a high error correction level to improve robustness of the QR code.
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Save the generated QR code as a PNG image.
            generator.Save(qrImagePath, BarCodeImageFormat.Png);
        }

        // Read and decode the QR code image to verify the embedded data.
        using (var reader = new BarCodeReader(qrImagePath, DecodeType.QR))
        {
            var results = reader.ReadBarCodes();

            // Iterate through all decoded results (typically one for this example).
            foreach (var result in results)
            {
                // Output the length of the decoded text.
                Console.WriteLine($"Decoded CodeText Length: {result.CodeText.Length}");

                // Convert the decoded string back to bytes (UTF-8) and display as hexadecimal.
                byte[] decodedBytes = System.Text.Encoding.UTF8.GetBytes(result.CodeText);
                Console.WriteLine("Decoded Bytes (hex): " + BitConverter.ToString(decodedBytes));
            }
        }

        // Inform the user where the QR code image has been saved.
        Console.WriteLine($"QR code image saved to: {qrImagePath}");
    }
}