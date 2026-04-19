using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Sample text that may cause auto encoding to fail (contains characters outside basic ASCII)
        string codeText = "Example with Unicode 🚀 and special chars ©";

        // Output file paths
        string autoPath = "qr_auto.png";
        string fallbackPath = "qr_fallback.png";

        // Try generating QR code with Auto mode
        try
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
            {
                // Ensure Auto mode (default) – explicit for clarity
                generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.Auto;
                generator.Save(autoPath);
                Console.WriteLine($"QR code saved using Auto mode: {autoPath}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Auto mode failed: {ex.Message}");
            Console.WriteLine("Attempting fallback encoding mode (Bytes)...");

            // Fallback to Bytes mode
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
            {
                generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.Bytes;
                generator.Save(fallbackPath);
                Console.WriteLine($"QR code saved using fallback mode: {fallbackPath}");
            }
        }
    }
}