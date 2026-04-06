using System;
using System.IO;
using Aspose.BarCode.Generation;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Usage: BarcodeCsvGenerator <csvFilePath>");
            return;
        }

        string csvPath = args[0];
        if (!File.Exists(csvPath))
        {
            Console.WriteLine($"File not found: {csvPath}");
            return;
        }

        foreach (var line in File.ReadLines(csvPath))
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;

            // Expected CSV format: Symbology,CodeText,Width(mm),Height(mm)
            var parts = line.Split(',');
            if (parts.Length < 4)
            {
                Console.WriteLine($"Invalid line (expected 4 columns): {line}");
                continue;
            }

            string symbology = parts[0].Trim();
            string codeText = parts[1].Trim();
            if (!float.TryParse(parts[2].Trim(), out float widthMm))
            {
                Console.WriteLine($"Invalid width value: {parts[2]}");
                continue;
            }
            if (!float.TryParse(parts[3].Trim(), out float heightMm))
            {
                Console.WriteLine($"Invalid height value: {parts[3]}");
                continue;
            }

            BaseEncodeType encodeType;
            try
            {
                encodeType = GetEncodeType(symbology);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
                continue;
            }

            string safeText = MakeFileNameSafe(codeText);
            string outputFile = $"{safeText}_{symbology}.png";

            using (var generator = new BarcodeGenerator(encodeType, codeText))
            {
                // Assign size units
                generator.Parameters.Barcode.BarHeight.Millimeters = heightMm;
                generator.Parameters.Barcode.XDimension.Millimeters = widthMm / 10f; // approximate scaling

                generator.Save(outputFile);
                Console.WriteLine($"Saved barcode to {outputFile}");
            }
        }
    }

    static BaseEncodeType GetEncodeType(string name)
    {
        return name.Trim().ToLowerInvariant() switch
        {
            "code128" => EncodeTypes.Code128,
            "code39" => EncodeTypes.Code39,
            "code39fullascii" => EncodeTypes.Code39FullASCII,
            "qr" => EncodeTypes.QR,
            "pdf417" => EncodeTypes.Pdf417,
            "itf14" => EncodeTypes.ITF14,
            "itf6" => EncodeTypes.ITF6,
            "codabar" => EncodeTypes.Codabar,
            _ => throw new ArgumentException($"Unsupported symbology: {name}")
        };
    }

    static string MakeFileNameSafe(string name)
    {
        foreach (char c in Path.GetInvalidFileNameChars())
        {
            name = name.Replace(c, '_');
        }
        return name;
    }
}