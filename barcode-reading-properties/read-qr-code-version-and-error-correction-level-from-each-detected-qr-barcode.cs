using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates creating a QR barcode, saving it to a temporary file,
/// reading it back, and cleaning up the temporary file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a QR barcode image, reads it, displays information,
    /// and then deletes the temporary image file.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // 1. Generate a QR barcode image and save it to a temporary location.
        // --------------------------------------------------------------------
        string imagePath = Path.Combine(Path.GetTempPath(), "sample_qr.png");
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Aspose.BarCode"))
        {
            // Set a known error correction level (example: LevelM).
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Save the generated barcode image to the temporary file.
            generator.Save(imagePath);
        }

        // ---------------------------------------------------------------
        // 2. Verify that the barcode image was successfully created.
        // ---------------------------------------------------------------
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create the QR barcode image.");
            return;
        }

        // ---------------------------------------------------------------
        // 3. Read the QR barcode from the saved image and display details.
        // ---------------------------------------------------------------
        using (var reader = new BarCodeReader(imagePath, DecodeType.QR))
        {
            foreach (var result in reader.ReadBarCodes())
            {
                // Output the detected barcode type and its encoded text.
                Console.WriteLine($"Detected barcode type: {result.CodeTypeName}");
                Console.WriteLine($"Code text: {result.CodeText}");

                // Note: Aspose.BarCode does not expose QR version or error correction
                // level in the recognition API, so we inform the user accordingly.
                Console.WriteLine("QR version: not available via Aspose.BarCode recognition API.");
                Console.WriteLine("QR error correction level: not available via Aspose.BarCode recognition API.");
                Console.WriteLine();
            }
        }

        // ---------------------------------------------------------------
        // 4. Clean up: delete the temporary image file.
        // ---------------------------------------------------------------
        try
        {
            File.Delete(imagePath);
        }
        catch
        {
            // Suppress any exceptions that occur during cleanup.
        }
    }
}