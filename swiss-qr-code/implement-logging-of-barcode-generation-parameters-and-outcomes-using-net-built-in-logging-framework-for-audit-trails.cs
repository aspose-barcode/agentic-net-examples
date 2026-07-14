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
        // Prepare output directory
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "output");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        string imagePath = Path.Combine(outputDir, "barcode.png");
        string logPath = Path.Combine(outputDir, "audit.log");

        // Start log
        File.AppendAllText(logPath, $"--- Barcode generation started at {DateTime.UtcNow:u} ---{Environment.NewLine}");

        // Define barcode settings
        BaseEncodeType encodeType = EncodeTypes.Code128;
        string codeText = "123ABC";

        try
        {
            using (var generator = new BarcodeGenerator(encodeType, codeText))
            {
                // Configure generation parameters
                generator.Parameters.Barcode.XDimension.Point = 2f;
                generator.Parameters.Barcode.BarHeight.Point = 50f;
                generator.Parameters.Barcode.FilledBars = false;
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Blue;
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;
                generator.Parameters.AutoSizeMode = AutoSizeMode.None;
                generator.Parameters.Resolution = 300f;

                // Log parameters
                string paramLog = $"EncodeType: {encodeType.TypeName}, CodeText: {codeText}, XDimension: {generator.Parameters.Barcode.XDimension.Point}pt, BarHeight: {generator.Parameters.Barcode.BarHeight.Point}pt, FilledBars: {generator.Parameters.Barcode.FilledBars}, BarColor: {generator.Parameters.Barcode.BarColor}, BackColor: {generator.Parameters.BackColor}, Resolution: {generator.Parameters.Resolution}dpi";
                Console.WriteLine(paramLog);
                File.AppendAllText(logPath, paramLog + Environment.NewLine);

                // Generate and save barcode image
                using (var bitmap = generator.GenerateBarCodeImage())
                {
                    bitmap.Save(imagePath, Aspose.Drawing.Imaging.ImageFormat.Png);
                }

                // Log success
                string successLog = $"Barcode image saved to {imagePath}";
                Console.WriteLine(successLog);
                File.AppendAllText(logPath, successLog + Environment.NewLine);
            }
        }
        catch (Exception ex)
        {
            // Log any errors
            string errorLog = $"Error during barcode generation: {ex.Message}";
            Console.WriteLine(errorLog);
            File.AppendAllText(logPath, errorLog + Environment.NewLine);
        }

        // End log
        File.AppendAllText(logPath, $"--- Barcode generation finished at {DateTime.UtcNow:u} ---{Environment.NewLine}");
    }
}