using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string outputFile = "sample_qr.png";
        const string codeText = "https://example.com";

        try
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
            {
                generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
                generator.Parameters.ImageWidth.Point = 300f;
                generator.Parameters.ImageHeight.Point = 300f;
                generator.Parameters.Resolution = 150; // DPI
                generator.Parameters.Barcode.BarColor = Color.DarkBlue;
                generator.Parameters.BackColor = Color.White;

                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;
                generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;

                Console.WriteLine("Generating QR barcode:");
                Console.WriteLine($"  CodeText: {codeText}");
                Console.WriteLine($"  ImageSize: {generator.Parameters.ImageWidth.Point}pt x {generator.Parameters.ImageHeight.Point}pt");
                Console.WriteLine($"  Resolution: {generator.Parameters.Resolution} DPI");
                Console.WriteLine($"  BarColor: {generator.Parameters.Barcode.BarColor}");
                Console.WriteLine($"  QR ErrorLevel: {generator.Parameters.Barcode.QR.ErrorLevel}");
                Console.WriteLine($"  CodeTextLocation: {generator.Parameters.Barcode.CodeTextParameters.Location}");
                Console.WriteLine($"  AutoSizeMode: {generator.Parameters.AutoSizeMode}");

                using (Bitmap bitmap = generator.GenerateBarCodeImage())
                {
                    bitmap.Save(outputFile, ImageFormat.Png);
                }

                Console.WriteLine($"Barcode image saved to '{Path.GetFullPath(outputFile)}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to generate barcode: {ex.Message}");
        }
    }
}