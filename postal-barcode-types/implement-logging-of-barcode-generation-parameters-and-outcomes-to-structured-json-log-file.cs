using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

namespace BarcodeLoggingDemo
{
    class Program
    {
        static void Main()
        {
            // Define output paths
            string imagePath = "barcode.png";
            string logPath = "barcode_log.json";

            // Prepare a log entry object
            var logEntry = new
            {
                Timestamp = DateTime.UtcNow,
                Success = false,
                Message = string.Empty,
                Parameters = new
                {
                    Symbology = "Code128",
                    CodeText = "123ABC",
                    XDimension = 2f,
                    BarHeight = 50f,
                    ImageWidth = 300f,
                    ImageHeight = 150f,
                    Resolution = 300,
                    BarColor = "Blue",
                    BackColor = "White"
                },
                OutputImage = imagePath
            };

            try
            {
                // Create and configure the barcode generator
                using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123ABC"))
                {
                    // Set unit‑based properties using the appropriate members
                    generator.Parameters.Barcode.XDimension.Point = 2f;
                    generator.Parameters.Barcode.BarHeight.Point = 50f;
                    generator.Parameters.ImageWidth.Point = 300f;
                    generator.Parameters.ImageHeight.Point = 150f;
                    generator.Parameters.Resolution = 300;
                    generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Blue;
                    generator.Parameters.BackColor = Aspose.Drawing.Color.White;

                    // Generate the barcode image and save it
                    using (Bitmap bitmap = generator.GenerateBarCodeImage())
                    {
                        bitmap.Save(imagePath, ImageFormat.Png);
                    }
                }

                // If we reach this point, generation succeeded
                var successLog = new
                {
                    Timestamp = DateTime.UtcNow,
                    Success = true,
                    Message = "Barcode generated and saved successfully.",
                    Parameters = logEntry.Parameters,
                    OutputImage = imagePath
                };

                string jsonSuccess = JsonSerializer.Serialize(successLog, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(logPath, jsonSuccess);
            }
            catch (Exception ex)
            {
                // On error, write failure details to the log
                var errorLog = new
                {
                    Timestamp = DateTime.UtcNow,
                    Success = false,
                    Message = ex.Message,
                    Parameters = logEntry.Parameters,
                    OutputImage = imagePath
                };

                string jsonError = JsonSerializer.Serialize(errorLog, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(logPath, jsonError);
            }
        }
    }
}