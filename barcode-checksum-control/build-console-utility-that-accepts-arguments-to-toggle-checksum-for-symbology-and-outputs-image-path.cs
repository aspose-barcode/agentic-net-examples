using System;
using System.IO;
using Aspose.BarCode.Generation;

namespace BarcodeChecksumToggle
{
    class Program
    {
        static void Main(string[] args)
        {
            // Expect three arguments: symbology, code text, checksum flag (yes/no)
            if (args.Length != 3)
            {
                Console.WriteLine("Usage: <symbology> <codeText> <checksum:yes|no>");
                return;
            }

            string symbology = args[0];
            string codeText = args[1];
            string checksumArg = args[2].ToLowerInvariant();

            // Resolve symbology to EncodeTypes member
            BaseEncodeType encodeType = GetEncodeType(symbology);

            // Determine checksum setting
            EnableChecksum checksumSetting = checksumArg switch
            {
                "yes" => EnableChecksum.Yes,
                "no" => EnableChecksum.No,
                _ => throw new ArgumentException("Checksum argument must be 'yes' or 'no'.")
            };

            // Generate barcode and save image
            using (var generator = new BarcodeGenerator(encodeType))
            {
                generator.CodeText = codeText;
                generator.Parameters.Barcode.IsChecksumEnabled = checksumSetting;

                // Optional: always show checksum text for applicable symbologies
                // generator.Parameters.Barcode.ChecksumAlwaysShow = true;

                string fileName = $"{symbology}_{codeText}.png";
                string fullPath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
                generator.Save(fullPath);
                Console.WriteLine(fullPath);
            }
        }

        // Maps a string name to a supported EncodeTypes member
        private static BaseEncodeType GetEncodeType(string name)
        {
            return name.ToLowerInvariant() switch
            {
                "code128" => EncodeTypes.Code128,
                "code39" => EncodeTypes.Code39,
                "code39fullascii" => EncodeTypes.Code39FullASCII,
                "ean13" => EncodeTypes.EAN13,
                "ean8" => EncodeTypes.EAN8,
                "upca" => EncodeTypes.UPCA,
                "upce" => EncodeTypes.UPCE,
                "itf14" => EncodeTypes.ITF14,
                "codabar" => EncodeTypes.Codabar,
                // Add other symbologies as needed
                _ => throw new ArgumentException($"Unsupported symbology: {name}")
            };
        }
    }
}