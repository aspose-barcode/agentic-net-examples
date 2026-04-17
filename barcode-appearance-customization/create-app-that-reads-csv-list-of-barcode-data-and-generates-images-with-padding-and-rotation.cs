using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main(string[] args)
    {
        // Determine CSV file path: first argument or default sample file.
        string csvPath = args.Length > 0 ? args[0] : "barcodes.csv";

        if (!File.Exists(csvPath))
        {
            Console.WriteLine($"CSV file not found: {csvPath}");
            // Create a small sample CSV so the program can still run.
            File.WriteAllText(csvPath, "1234567890,code1.png,0\n9876543210,code2.png,90");
            Console.WriteLine("Sample CSV created. Re-run the program to generate images.");
            return;
        }

        // Read all non‑empty lines.
        string[] lines = File.ReadAllLines(csvPath);
        foreach (string rawLine in lines)
        {
            if (string.IsNullOrWhiteSpace(rawLine))
                continue;

            // Expected format: CodeText,OutputFileName,RotationDegrees
            string[] parts = rawLine.Split(',');
            if (parts.Length < 2)
            {
                Console.WriteLine($"Invalid line (needs at least CodeText and OutputFileName): {rawLine}");
                continue;
            }

            string codeText = parts[0].Trim();
            string outputFile = parts[1].Trim();
            int rotation = 0;
            if (parts.Length >= 3 && int.TryParse(parts[2].Trim(), out int rot))
                rotation = rot % 360; // normalize

            // Create barcode generator (using Code128 as a generic symbology)
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
            {
                generator.CodeText = codeText;

                // Set uniform padding of 10 points on each side
                generator.Parameters.Barcode.Padding.Left.Point = 10f;
                generator.Parameters.Barcode.Padding.Top.Point = 10f;
                generator.Parameters.Barcode.Padding.Right.Point = 10f;
                generator.Parameters.Barcode.Padding.Bottom.Point = 10f;

                // Generate the barcode image
                using (Bitmap bmp = generator.GenerateBarCodeImage())
                {
                    Bitmap finalBmp = bmp;

                    // Apply rotation if needed (supports only multiples of 90 degrees)
                    if (rotation != 0)
                    {
                        RotateFlipType rotateType = RotateFlipType.RotateNoneFlipNone;
                        switch (rotation)
                        {
                            case 90:
                                rotateType = RotateFlipType.Rotate90FlipNone;
                                break;
                            case 180:
                                rotateType = RotateFlipType.Rotate180FlipNone;
                                break;
                            case 270:
                                rotateType = RotateFlipType.Rotate270FlipNone;
                                break;
                            default:
                                Console.WriteLine($"Unsupported rotation {rotation}° for {outputFile}. Skipping rotation.");
                                break;
                        }

                        if (rotateType != RotateFlipType.RotateNoneFlipNone)
                        {
                            // Clone to avoid modifying the original bitmap used by the generator
                            finalBmp = (Bitmap)bmp.Clone();
                            finalBmp.RotateFlip(rotateType);
                        }
                    }

                    // Save the final image as PNG
                    finalBmp.Save(outputFile, ImageFormat.Png);

                    if (!ReferenceEquals(finalBmp, bmp))
                        finalBmp.Dispose();

                    Console.WriteLine($"Generated: {outputFile}");
                }
            }
        }
    }
}