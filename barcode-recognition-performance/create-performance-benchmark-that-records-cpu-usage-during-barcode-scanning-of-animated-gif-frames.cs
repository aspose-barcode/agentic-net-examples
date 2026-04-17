using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Path to the animated GIF containing barcodes.
        const string gifPath = "barcode_anim.gif";

        // Verify that the file exists.
        if (!File.Exists(gifPath))
        {
            Console.WriteLine($"File not found: {gifPath}");
            return;
        }

        // Load the animated GIF.
        using (Image gifImage = Image.FromFile(gifPath))
        {
            // The first entry in FrameDimensionsList is usually the time dimension for animated GIFs.
            var timeDimension = new FrameDimension(gifImage.FrameDimensionsList[0]);
            int frameCount = gifImage.GetFrameCount(timeDimension);
            Console.WriteLine($"Animated GIF contains {frameCount} frame(s).");

            // Process each frame and measure CPU time.
            Process currentProcess = Process.GetCurrentProcess();

            for (int i = 0; i < frameCount; i++)
            {
                // Select the current frame.
                gifImage.SelectActiveFrame(timeDimension, i);

                // Clone the active frame into a Bitmap for recognition.
                using (Bitmap frameBitmap = new Bitmap(gifImage))
                {
                    // Record CPU time before recognition.
                    TimeSpan cpuStart = currentProcess.TotalProcessorTime;
                    Stopwatch sw = Stopwatch.StartNew();

                    // Initialize the barcode reader for common symbologies.
                    using (BarCodeReader reader = new BarCodeReader(frameBitmap, DecodeType.Code128, DecodeType.QR, DecodeType.DataMatrix))
                    {
                        // Use high‑performance settings to focus on CPU usage.
                        reader.QualitySettings = QualitySettings.HighPerformance;

                        // Perform recognition (results are ignored; we only measure time).
                        foreach (var result in reader.ReadBarCodes())
                        {
                            // No action needed; just iterate to ensure full processing.
                        }
                    }

                    sw.Stop();
                    TimeSpan cpuEnd = currentProcess.TotalProcessorTime;
                    double cpuMs = (cpuEnd - cpuStart).TotalMilliseconds;

                    Console.WriteLine($"Frame {i + 1}/{frameCount}: Elapsed = {sw.ElapsedMilliseconds} ms, CPU = {cpuMs:F2} ms");
                }
            }
        }
    }
}