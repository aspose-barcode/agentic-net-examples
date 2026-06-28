using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates reading Mailmark barcodes from multi‑frame TIFF files using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Determines which TIFF files to process and iterates over them.
    /// </summary>
    /// <param name="args">Command‑line arguments specifying TIFF file paths.</param>
    static void Main(string[] args)
    {
        // Determine TIFF files to process: use command‑line arguments or fallback sample list
        string[] tiffFiles;
        if (args != null && args.Length > 0)
        {
            tiffFiles = args;
        }
        else
        {
            // Sample fallback – adjust paths as needed for the runner environment
            tiffFiles = new string[]
            {
                "sample1.tif",
                "sample2.tif",
                "sample3.tif"
            };
        }

        // Process each file path supplied
        foreach (var filePath in tiffFiles)
        {
            // Verify the file exists before attempting to read it
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                continue;
            }

            Console.WriteLine($"Processing TIFF file: {filePath}");
            ProcessTiffFile(filePath);
        }
    }

    /// <summary>
    /// Loads a multi‑frame TIFF, extracts each frame (up to a safe limit), and reads Mailmark barcodes.
    /// </summary>
    /// <param name="tiffPath">Path to the TIFF file to process.</param>
    static void ProcessTiffFile(string tiffPath)
    {
        // Load the multi‑frame TIFF image
        using (Image tiffImage = Image.FromFile(tiffPath))
        {
            // Use the built‑in FrameDimension for time (pages/frames)
            var frameDimension = FrameDimension.Time;
            int frameCount = tiffImage.GetFrameCount(frameDimension);

            // Limit processing to a safe number of frames for the runner environment
            int maxFrames = Math.Min(frameCount, 5);
            for (int i = 0; i < maxFrames; i++)
            {
                // Select the current frame for processing
                tiffImage.SelectActiveFrame(frameDimension, i);
                using (var ms = new MemoryStream())
                {
                    // Save the current frame to a memory stream as PNG (required by BarCodeReader)
                    tiffImage.Save(ms, ImageFormat.Png);
                    ms.Position = 0; // Reset stream position for reading

                    // Read Mailmark barcodes from the frame
                    using (var reader = new BarCodeReader(ms, DecodeType.Mailmark))
                    {
                        var results = reader.ReadBarCodes();
                        if (results.Length == 0)
                        {
                            Console.WriteLine($"  Frame {i + 1}: No Mailmark barcode detected.");
                            continue;
                        }

                        // Iterate over all detected barcodes in the current frame
                        for (int r = 0; r < results.Length; r++)
                        {
                            var result = results[r];
                            Console.WriteLine($"  Frame {i + 1}, Barcode {r + 1}: CodeText = {result.CodeText}");

                            // Decode the complex Mailmark codetext into its structured fields
                            MailmarkCodetext mailmark = ComplexCodetextReader.TryDecodeMailmark(result.CodeText);
                            if (mailmark != null)
                            {
                                Console.WriteLine("    Decoded Mailmark fields:");
                                Console.WriteLine($"      Format: {mailmark.Format}");
                                Console.WriteLine($"      VersionID: {mailmark.VersionID}");
                                Console.WriteLine($"      Class: {mailmark.Class}");
                                Console.WriteLine($"      SupplychainID: {mailmark.SupplychainID}");
                                Console.WriteLine($"      ItemID: {mailmark.ItemID}");
                                Console.WriteLine($"      DestinationPostCodePlusDPS: {mailmark.DestinationPostCodePlusDPS}");
                            }
                            else
                            {
                                Console.WriteLine("    Failed to decode Mailmark codetext.");
                            }
                        }
                    }
                }
            }
        }
    }
}