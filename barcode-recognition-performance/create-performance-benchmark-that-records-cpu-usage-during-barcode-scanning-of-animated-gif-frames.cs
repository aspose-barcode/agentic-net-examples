using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates loading an animated GIF, extracting frames, and scanning each frame for barcodes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Base64-encoded simple animated GIF (2 frames, 2x2 pixels)
        const string gifBase64 =
            "R0lGODdhAgACAIAAAAAAAP///ywAAAAAAgACAAACCoSPqcvtD6OctNqLs968+w+G4kiW5oqnm" +
            "rRvYbG2b7rv9gAAOw==";

        // Write the decoded GIF bytes to a temporary file on disk
        string tempPath = Path.Combine(Path.GetTempPath(), "sample.gif");
        File.WriteAllBytes(tempPath, Convert.FromBase64String(gifBase64));

        // Verify that the temporary file was created successfully
        if (!File.Exists(tempPath))
        {
            Console.WriteLine("Failed to create sample GIF.");
            return;
        }

        // Load the animated GIF using Aspose.Drawing.Image
        using (Image gifImage = Image.FromFile(tempPath))
        {
            // Determine the number of frames in the time dimension (animation frames)
            var timeDimension = FrameDimension.Time;
            int frameCount = gifImage.GetFrameCount(timeDimension);

            // Process up to 5 frames as a safety cap (in case of many frames)
            int framesToProcess = Math.Min(frameCount, 5);
            Console.WriteLine($"Total frames: {frameCount}, processing: {framesToProcess}");

            // Iterate over each frame to be processed
            for (int i = 0; i < framesToProcess; i++)
            {
                // Activate the current frame in the GIF image
                gifImage.SelectActiveFrame(timeDimension, i);

                // Save the active frame to a memory stream in PNG format
                using (var ms = new MemoryStream())
                {
                    gifImage.Save(ms, ImageFormat.Png);
                    ms.Position = 0; // Reset stream position for reading

                    // Load the PNG data as a bitmap for barcode recognition
                    using (var bitmap = new Bitmap(ms))
                    {
                        // Capture CPU time before barcode scanning
                        Process proc = Process.GetCurrentProcess();
                        TimeSpan cpuBefore = proc.TotalProcessorTime;

                        // Perform barcode recognition on the bitmap
                        using (var reader = new BarCodeReader(bitmap, DecodeType.AllSupportedTypes))
                        {
                            foreach (var result in reader.ReadBarCodes())
                            {
                                Console.WriteLine($"Frame {i + 1}: Type={result.CodeTypeName}, Text={result.CodeText}");
                            }
                        }

                        // Capture CPU time after barcode scanning and calculate elapsed time
                        TimeSpan cpuAfter = proc.TotalProcessorTime;
                        double cpuMs = (cpuAfter - cpuBefore).TotalMilliseconds;
                        Console.WriteLine($"Frame {i + 1}: CPU time for scanning = {cpuMs:F2} ms");
                    }
                }
            }
        }

        // Attempt to delete the temporary GIF file; ignore any errors
        try
        {
            File.Delete(tempPath);
        }
        catch
        {
            // Ignored
        }
    }
}