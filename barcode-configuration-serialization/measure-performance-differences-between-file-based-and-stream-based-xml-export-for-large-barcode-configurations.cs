using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Prepare a barcode generator with a relatively complex configuration
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set various barcode parameters to increase configuration size
            generator.Parameters.Barcode.XDimension.Point = 2f;
            generator.Parameters.Barcode.BarHeight.Point = 50f;
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 150f;
            generator.Parameters.Barcode.Padding.Left.Point = 5f;
            generator.Parameters.Barcode.Padding.Top.Point = 5f;
            generator.Parameters.Barcode.Padding.Right.Point = 5f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 5f;
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Blue;
            generator.Parameters.BackColor = Aspose.Drawing.Color.LightYellow;
            generator.Parameters.Resolution = 300;
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Arial";
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 10f;
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Center;
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;

            // Measure file‑based XML export
            string xmlFilePath = Path.Combine(Path.GetTempPath(), "barcode_config_file.xml");
            var swFile = Stopwatch.StartNew();
            bool fileExportSuccess = generator.ExportToXml(xmlFilePath);
            swFile.Stop();

            // Measure stream‑based XML export
            bool streamExportSuccess;
            long streamLength;
            var swStream = Stopwatch.StartNew();
            using (var memoryStream = new MemoryStream())
            {
                streamExportSuccess = generator.ExportToXml(memoryStream);
                streamLength = memoryStream.Length;
            }
            swStream.Stop();

            // Output results
            Console.WriteLine("File export success: {0}", fileExportSuccess);
            Console.WriteLine("File export time (ms): {0}", swFile.ElapsedMilliseconds);
            Console.WriteLine("File path: {0}", xmlFilePath);
            Console.WriteLine();

            Console.WriteLine("Stream export success: {0}", streamExportSuccess);
            Console.WriteLine("Stream export time (ms): {0}", swStream.ElapsedMilliseconds);
            Console.WriteLine("Stream length (bytes): {0}", streamLength);
        }
    }
}