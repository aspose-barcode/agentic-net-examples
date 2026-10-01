// Title: Restartable Barcode Scanning Service with XML State Persistence
// Description: Demonstrates a barcode scanning workflow that can be paused, saved to an XML file, and resumed after a crash.
// Category-Description: This example belongs to the Aspose.BarCode scanning and state management category. It shows how to generate barcode images, read them using BarCodeReader, and persist the list of pending files with XDocument. Developers often need to build resilient scanning services that survive interruptions, and this pattern illustrates typical use of BarcodeGenerator, BarCodeReader, and XML state handling.
// Prompt: Implement a restartable barcode scanning service that saves its state to XML and restores it after a crash.
// Tags: barcode, scanning, xml, state, persistence, code128, aspose.barcode, generation, recognition

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates a restartable barcode scanning service that persists its state to XML
/// and can resume processing after an unexpected termination.
/// </summary>
class Program
{
    // Path to temporary folder that holds generated barcode images
    private static readonly string ImagesFolder = Path.Combine(Path.GetTempPath(), "BarcodeScanDemo_" + Guid.NewGuid().ToString("N"));
    // Path to XML file that stores pending files between runs
    private static readonly string StateFilePath = Path.Combine(ImagesFolder, "scan_state.xml");
    // Maximum number of files to process in a single execution (simulates a crash after this many)
    private const int MaxFilesPerRun = 2;

    /// <summary>
    /// Application entry point. Generates sample barcodes, loads or creates the processing state,
    /// scans a limited number of images, updates the state, and cleans up when finished.
    /// </summary>
    static void Main()
    {
        // Ensure the working folder exists
        Directory.CreateDirectory(ImagesFolder);

        // Step 1: Generate sample barcode images if they do not already exist
        GenerateSampleBarcodes();

        // Step 2: Load pending file list from XML or initialize with all images
        List<string> pendingFiles = LoadPendingFiles();

        if (pendingFiles.Count == 0)
        {
            Console.WriteLine("No barcode images to scan. Exiting.");
            CleanupIfDone();
            return;
        }

        Console.WriteLine($"Pending files to scan: {pendingFiles.Count}");

        // Step 3: Process up to MaxFilesPerRun images
        int filesToProcess = Math.Min(MaxFilesPerRun, pendingFiles.Count);
        for (int i = 0; i < filesToProcess; i++)
        {
            string filePath = pendingFiles[i];
            Console.WriteLine($"Scanning file: {Path.GetFileName(filePath)}");
            ScanBarcode(filePath);
        }

        // Step 4: Remove processed files from the pending list
        pendingFiles.RemoveRange(0, filesToProcess);

        // Step 5: Save remaining pending files back to XML (or delete state if done)
        if (pendingFiles.Count > 0)
        {
            SavePendingFiles(pendingFiles);
            Console.WriteLine($"State saved. {pendingFiles.Count} file(s) remain for next run.");
        }
        else
        {
            // All files processed – clean up state file
            if (File.Exists(StateFilePath))
                File.Delete(StateFilePath);
            Console.WriteLine("All files processed. State cleared.");
        }

        // Optional cleanup of generated images (comment out if you want to inspect them)
        CleanupIfDone();
    }

    // Generates a small set of barcode images (Code128) for demonstration
    private static void GenerateSampleBarcodes()
    {
        // Create 5 sample images if they are not already present
        for (int i = 1; i <= 5; i++)
        {
            string filePath = Path.Combine(ImagesFolder, $"barcode_{i}.png");
            if (File.Exists(filePath))
                continue;

            string codeText = $"CODE{i:D3}";
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Simple configuration – default settings are sufficient
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
        }
    }

    // Loads pending file list from the XML state file; if missing, returns all image files
    private static List<string> LoadPendingFiles()
    {
        if (File.Exists(StateFilePath))
        {
            try
            {
                XDocument doc = XDocument.Load(StateFilePath);
                var files = doc.Root?
                    .Element("PendingFiles")?
                    .Elements("File")
                    .Select(e => e.Value)
                    .Where(p => File.Exists(p))
                    .ToList() ?? new List<string>();
                return files;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load state file: {ex.Message}");
                // Fallback to full list if state is corrupted
            }
        }

        // No valid state – start with all generated images
        return Directory.GetFiles(ImagesFolder, "*.png").ToList();
    }

    // Saves the list of pending files to the XML state file
    private static void SavePendingFiles(List<string> pendingFiles)
    {
        var doc = new XDocument(
            new XElement("ScanState",
                new XElement("PendingFiles",
                    pendingFiles.Select(p => new XElement("File", p))
                )
            )
        );
        doc.Save(StateFilePath);
    }

    // Scans a single barcode image and writes the result to console
    private static void ScanBarcode(string imagePath)
    {
        // Use BarCodeReader with default decode types (auto-detect)
        using (var reader = new BarCodeReader(imagePath))
        {
            try
            {
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"  Detected: {result.CodeText} (Symbology: {result.CodeTypeName})");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  Error reading barcode: {ex.Message}");
            }
        }
    }

    // Deletes generated images and folder when processing is complete
    private static void CleanupIfDone()
    {
        if (Directory.Exists(ImagesFolder) && !Directory.EnumerateFileSystemEntries(ImagesFolder).Any())
        {
            try
            {
                Directory.Delete(ImagesFolder, true);
            }
            catch
            {
                // Ignored – cleanup is best-effort
            }
        }
    }
}