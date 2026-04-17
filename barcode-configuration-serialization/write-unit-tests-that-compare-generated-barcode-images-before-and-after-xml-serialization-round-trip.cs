using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Define test cases with symbology and sample text
        var testCases = new (BaseEncodeType type, string text)[]
        {
            (EncodeTypes.Code128, "ABC123456"),
            (EncodeTypes.QR, "https://example.com"),
            (EncodeTypes.EAN13, "1234567890128")
        };

        foreach (var (type, text) in testCases)
        {
            Console.WriteLine($"Testing {type}...");

            // Create original generator and set some parameters
            using (var generator = new BarcodeGenerator(type, text))
            {
                // Example parameter settings
                generator.Parameters.Barcode.XDimension.Point = 2f;
                generator.Parameters.Barcode.BarHeight.Point = 50f;
                generator.Parameters.ImageWidth.Point = 300f;
                generator.Parameters.ImageHeight.Point = 150f;
                generator.Parameters.Resolution = 96;
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Blue;
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;

                // Generate original image and capture bytes
                using (Bitmap originalBitmap = generator.GenerateBarCodeImage())
                using (var originalStream = new MemoryStream())
                {
                    originalBitmap.Save(originalStream, ImageFormat.Png);
                    byte[] originalBytes = originalStream.ToArray();

                    // Export settings to XML
                    string xmlPath = Path.Combine(Path.GetTempPath(), $"barcode_{Guid.NewGuid()}.xml");
                    generator.ExportToXml(xmlPath);

                    // Import settings from XML
                    using (BarcodeGenerator importedGenerator = BarcodeGenerator.ImportFromXml(xmlPath))
                    {
                        // Generate image after round‑trip
                        using (Bitmap roundTripBitmap = importedGenerator.GenerateBarCodeImage())
                        using (var roundTripStream = new MemoryStream())
                        {
                            roundTripBitmap.Save(roundTripStream, ImageFormat.Png);
                            byte[] roundTripBytes = roundTripStream.ToArray();

                            // Compare byte arrays
                            bool areEqual = AreByteArraysEqual(originalBytes, roundTripBytes);
                            Console.WriteLine(areEqual
                                ? "PASS: Images are identical after XML round‑trip."
                                : "FAIL: Images differ after XML round‑trip.");
                        }
                    }

                    // Clean up temporary XML file
                    if (File.Exists(xmlPath))
                    {
                        try { File.Delete(xmlPath); } catch { /* ignore */ }
                    }
                }
            }
        }
    }

    // Simple byte‑wise comparison
    private static bool AreByteArraysEqual(byte[] a, byte[] b)
    {
        if (a == null || b == null) return false;
        if (a.Length != b.Length) return false;
        for (int i = 0; i < a.Length; i++)
        {
            if (a[i] != b[i]) return false;
        }
        return true;
    }
}