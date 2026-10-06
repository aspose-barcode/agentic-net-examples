// Title: Decode Australia Post Barcodes from Multi‑Page TIFF
// Description: Demonstrates loading a multi‑page TIFF, converting each page to PNG, and decoding Australia Post barcodes using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode image‑processing and barcode‑recognition category. It showcases the use of Aspose.Imaging to access individual frames of a TIFF file and Aspose.BarCode's BarCodeReader to decode Australia Post symbology. Developers commonly need to process scanned documents, extract barcodes from each page, and handle multi‑page image formats in automated workflows.
// Prompt: Decode an Australia Post barcode from a multi‑page TIFF and process each page sequentially.
// Tags: barcode, australia post, decode, multi-page tiff, aspose.barcode, aspose.imaging, c#

using System;
using System.IO;
using System.Collections;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that reads a multi‑page TIFF, extracts each page as a PNG image,
/// and decodes Australia Post barcodes from each page using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs the TIFF loading, page extraction,
    /// and barcode decoding workflow.
    /// </summary>
    static void Main()
    {
        // Build the full path to the sample TIFF file located in the current directory.
        string tiffPath = Path.Combine(Directory.GetCurrentDirectory(), "MultiPageAustraliaPost.tiff");
        if (!File.Exists(tiffPath))
        {
            Console.WriteLine($"File not found: {tiffPath}");
            return;
        }

        // Load Aspose.Imaging assembly via reflection to avoid a hard reference.
        Type imageType = Type.GetType("Aspose.Imaging.Image, Aspose.Imaging");
        if (imageType == null)
        {
            Console.WriteLine("Aspose.Imaging assembly is not available.");
            return;
        }

        // Use the static Image.Load method to read the TIFF file.
        var loadMethod = imageType.GetMethod("Load", new[] { typeof(string) });
        if (loadMethod == null)
        {
            Console.WriteLine("Unable to locate Image.Load method.");
            return;
        }

        object image = loadMethod.Invoke(null, new object[] { tiffPath });
        if (image == null)
        {
            Console.WriteLine("Failed to load TIFF image.");
            return;
        }

        // Verify that the loaded image is a TiffImage (supports multiple frames).
        Type tiffImageType = Type.GetType("Aspose.Imaging.FileFormats.Tiff.TiffImage, Aspose.Imaging");
        if (tiffImageType == null || !tiffImageType.IsInstanceOfType(image))
        {
            Console.WriteLine("The provided file is not a multi-page TIFF.");
            DisposeIfNeeded(image);
            return;
        }

        // Retrieve the Frames collection which holds each page of the TIFF.
        var framesProp = tiffImageType.GetProperty("Frames");
        if (framesProp == null)
        {
            Console.WriteLine("Unable to access Frames property.");
            DisposeIfNeeded(image);
            return;
        }

        var frames = framesProp.GetValue(image) as IEnumerable;
        if (frames == null)
        {
            Console.WriteLine("No frames found in TIFF.");
            DisposeIfNeeded(image);
            return;
        }

        int pageIndex = 0;
        // Iterate through each frame (page) in the TIFF.
        foreach (object frame in frames)
        {
            pageIndex++;

            // Convert the current frame to a PNG stored in a memory stream.
            using (var ms = new MemoryStream())
            {
                // Obtain the PngOptions type via reflection.
                Type pngOptionsType = Type.GetType("Aspose.Imaging.ImageOptions.PngOptions, Aspose.Imaging");
                if (pngOptionsType == null)
                {
                    Console.WriteLine("PngOptions type not found.");
                    continue;
                }
                object pngOptions = Activator.CreateInstance(pngOptionsType);

                // Call the Save method on the frame to write PNG data into the stream.
                var saveMethod = frame.GetType().GetMethod("Save", new[] { typeof(Stream), pngOptionsType });
                if (saveMethod == null)
                {
                    Console.WriteLine("Unable to locate Save method on frame.");
                    continue;
                }
                saveMethod.Invoke(frame, new object[] { ms, pngOptions });
                ms.Position = 0; // Reset stream position for reading.

                // Decode Australia Post barcodes from the PNG image.
                using (var reader = new BarCodeReader(ms, DecodeType.AustraliaPost))
                {
                    // Configure decoding settings specific to Australia Post symbology.
                    reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = CustomerInformationInterpretingType.CTable;
                    reader.BarcodeSettings.AustraliaPost.IgnoreEndingFillingPatternsForCTable = true;

                    var results = reader.ReadBarCodes();
                    if (results.Length == 0)
                    {
                        Console.WriteLine($"Page {pageIndex}: No barcode detected.");
                    }
                    else
                    {
                        foreach (var result in results)
                        {
                            Console.WriteLine($"Page {pageIndex}: Type={result.CodeTypeName}, Text={result.CodeText}");
                        }
                    }
                }
            }
        }

        // Clean up the loaded image resources.
        DisposeIfNeeded(image);
    }

    /// <summary>
    /// Disposes the provided object if it implements <see cref="IDisposable"/> or
    /// exposes a parameterless Dispose method via reflection.
    /// </summary>
    /// <param name="obj">The object to dispose.</param>
    static void DisposeIfNeeded(object obj)
    {
        if (obj is IDisposable disposable)
        {
            disposable.Dispose();
        }
        else
        {
            // Attempt to invoke Dispose via reflection for explicit interface implementations.
            var disposeMethod = obj.GetType().GetMethod("Dispose", Type.EmptyTypes);
            if (disposeMethod != null)
            {
                disposeMethod.Invoke(obj, null);
            }
        }
    }
}