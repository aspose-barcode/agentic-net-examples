using System;
using System.IO;
using System.Text;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Create a QR barcode with Unicode text (Cyrillic) and save to a memory stream.
        using (var ms = new MemoryStream())
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.QR))
            {
                // Encode the text using UTF8 (includes BOM).
                generator.SetCodeText("Привет", Encoding.UTF8);
                generator.Save(ms, BarCodeImageFormat.Png);
            }

            // Prepare the reader with DetectEncoding disabled.
            ms.Position = 0;
            using (var reader = new BarCodeReader(ms, DecodeType.QR))
            {
                reader.BarcodeSettings.DetectEncoding = false;

                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    // Try to interpret the raw bytes as UTF8.
                    string decodedText = TryDecodeUtf8OrFallback(result.CodeBytes);
                    Console.WriteLine("Decoded Text: " + decodedText);
                }
            }
        }
    }

    // Attempts UTF8 decoding; if the byte sequence is not valid UTF8, falls back to Windows-1251.
    private static string TryDecodeUtf8OrFallback(byte[] bytes)
    {
        if (IsValidUtf8(bytes))
        {
            return Encoding.UTF8.GetString(bytes);
        }
        // Fallback to a different encoding (e.g., Windows-1251) when UTF8 fails.
        Encoding fallbackEncoding = Encoding.GetEncoding(1251);
        return fallbackEncoding.GetString(bytes);
    }

    // Simple validation to check whether a byte array is a valid UTF8 sequence.
    private static bool IsValidUtf8(byte[] bytes)
    {
        try
        {
            // Decode and re-encode; if the round‑trip changes the data, it's not valid UTF8.
            string decoded = Encoding.UTF8.GetString(bytes);
            byte[] encoded = Encoding.UTF8.GetBytes(decoded);
            return encoded.Length == bytes.Length && CompareArrays(encoded, bytes);
        }
        catch
        {
            return false;
        }
    }

    private static bool CompareArrays(byte[] a, byte[] b)
    {
        if (a.Length != b.Length) return false;
        for (int i = 0; i < a.Length; i++)
        {
            if (a[i] != b[i]) return false;
        }
        return true;
    }
}