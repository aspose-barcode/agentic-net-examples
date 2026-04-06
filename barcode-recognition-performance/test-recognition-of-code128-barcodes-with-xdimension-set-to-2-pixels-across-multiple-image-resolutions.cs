using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Text to encode
        string codeText = "1234567890";

        // Resolutions (DPI) to test
        float[] resolutions = new float[] { 96f, 150f, 300f };
        string[] fileNames = new string[resolutions.Length];

        // Generate Code128 barcodes with XDimension = 2 pixels at each resolution
        for (int i = 0; i < resolutions.Length; i++)
        {
            string fileName = $"code128_{(int)resolutions[i]}dpi.png";
            fileNames[i] = fileName;

            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Set XDimension to 2 pixels
                generator.Parameters.Barcode.XDimension.Point = 2f;

                // Set image resolution
                generator.Parameters.Resolution = resolutions[i];

                // Save the barcode image
                generator.Save(fileName);
            }
        }

        // Recognize each generated barcode using XDimension mode Normal (2 pixels or more)
        for (int i = 0; i < fileNames.Length; i++)
        {
            Console.WriteLine($"Reading {fileNames[i]} (Resolution {resolutions[i]} DPI):");

            using (var reader = new BarCodeReader(fileNames[i], DecodeType.Code128))
            {
                // Use normal quality preset and set XDimension mode to Normal
                reader.QualitySettings = QualitySettings.NormalQuality;
                reader.QualitySettings.XDimension = XDimensionMode.Normal;

                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"  Detected CodeText: {result.CodeText}");
                }
            }
        }
    }
}