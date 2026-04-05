using System;
using System.IO;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        const string csvPath = "barcodes.csv";

        // Ensure CSV file exists; create a sample if missing.
        if (!File.Exists(csvPath))
        {
            File.WriteAllText(csvPath,
                "123ABC,0,5\n" +
                "XYZ789,90,10\n" +
                "HELLO,45,8");
        }

        string[] lines = File.ReadAllLines(csvPath);
        int index = 1;

        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;

            string[] parts = line.Split(',');
            if (parts.Length < 3)
                throw new ArgumentException($"Invalid CSV format at line {index}: '{line}'");

            string codeText = parts[0].Trim();

            if (!float.TryParse(parts[1].Trim(), out float rotation))
                throw new ArgumentException($"Invalid rotation value at line {index}: '{parts[1]}'");

            if (!float.TryParse(parts[2].Trim(), out float padding))
                throw new ArgumentException($"Invalid padding value at line {index}: '{parts[2]}'");

            string outputFile = $"barcode_{index}.png";

            using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
            {
                // Set the text to encode.
                generator.CodeText = codeText;

                // Apply rotation.
                generator.Parameters.RotationAngle = rotation;

                // Apply uniform padding on all sides.
                generator.Parameters.Barcode.Padding.Left.Point = padding;
                generator.Parameters.Barcode.Padding.Top.Point = padding;
                generator.Parameters.Barcode.Padding.Right.Point = padding;
                generator.Parameters.Barcode.Padding.Bottom.Point = padding;

                // Save the barcode image.
                generator.Save(outputFile);
            }

            Console.WriteLine($"Generated '{outputFile}' for '{codeText}' (Rotation: {rotation}°, Padding: {padding}pt)");
            index++;
        }
    }
}