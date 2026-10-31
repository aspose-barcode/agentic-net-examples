// Title: Generate Mailmark barcode PowerShell script from CSV data
// Description: Demonstrates creating a temporary CSV file and a PowerShell script that uses Aspose.BarCode to generate Mailmark 4‑state barcode images for each row.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcode types such as Mailmark. It shows how to prepare input data, build a PowerShell script, and invoke the Aspose.BarCode .NET library to produce PNG images. Developers working with batch barcode creation, data‑driven barcode generation, or integration of .NET libraries into PowerShell will find this pattern useful.
// Prompt: Create a PowerShell script invoking the .NET library to batch convert CSV rows into individual Mailmark barcode images.
// Tags: mailmark, barcode, generation, csv, powershell, aspose.barcode, png

using System;
using System.IO;
using System.Text;

/// <summary>
/// Demonstrates how to generate a PowerShell script that reads a CSV file
/// and creates Mailmark 4‑state barcode images using the Aspose.BarCode library.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the demo. Creates a temporary folder, writes a sample CSV,
    /// builds a PowerShell script that generates barcode images, and writes the script to disk.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the demo
        string baseFolder = Path.Combine(Path.GetTempPath(), "MailmarkBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(baseFolder);

        // Define paths for the CSV file and the PowerShell script within the temporary folder
        string csvPath = Path.Combine(baseFolder, "data.csv");
        string psScriptPath = Path.Combine(baseFolder, "GenerateMailmarkBarcodes.ps1");

        // Write a small sample CSV (VersionID, Class, SupplychainID, ItemID, DestinationPostCodePlusDPS)
        using (var writer = new StreamWriter(csvPath, false, Encoding.UTF8))
        {
            writer.WriteLine("VersionID,Class,SupplychainID,ItemID,DestinationPostCodePlusDPS");
            writer.WriteLine("1,0,384224,16563762,EF61AH8T ");
            writer.WriteLine("1,1,384225,16563763,EF61AH8T ");
            writer.WriteLine("1,0,384226,16563764,EF61AH8T ");
        }

        // Build the PowerShell script content that will generate the barcodes
        var sb = new StringBuilder();
        sb.AppendLine("# PowerShell script to generate Mailmark 4-state barcodes from CSV");
        sb.AppendLine("$scriptDir = Split-Path -Parent $MyInvocation.MyCommand.Definition");
        sb.AppendLine("$csvPath = Join-Path $scriptDir \"data.csv\"");
        sb.AppendLine("$outputDir = Join-Path $scriptDir \"Barcodes\"");
        sb.AppendLine("if (-not (Test-Path $outputDir)) { New-Item -ItemType Directory -Path $outputDir | Out-Null }");
        sb.AppendLine("");
        sb.AppendLine("# Load Aspose.BarCode assembly (assumes Aspose.BarCode.dll is in the same folder)");
        sb.AppendLine("Add-Type -Path (Join-Path $scriptDir \"Aspose.BarCode.dll\")");
        sb.AppendLine("");
        sb.AppendLine("# Import CSV records");
        sb.AppendLine("$records = Import-Csv -Path $csvPath");
        sb.AppendLine("");
        sb.AppendLine("foreach ($rec in $records) {");
        sb.AppendLine("    $mailmark = New-Object Aspose.BarCode.ComplexBarcode.MailmarkCodetext");
        sb.AppendLine("    $mailmark.Format = 4");
        sb.AppendLine("    $mailmark.VersionID = [int]$rec.VersionID");
        sb.AppendLine("    $mailmark.Class = $rec.Class");
        sb.AppendLine("    $mailmark.SupplychainID = [int]$rec.SupplychainID");
        sb.AppendLine("    $mailmark.ItemID = [int]$rec.ItemID");
        sb.AppendLine("    $mailmark.DestinationPostCodePlusDPS = $rec.DestinationPostCodePlusDPS");
        sb.AppendLine("");
        sb.AppendLine("    $generator = New-Object Aspose.BarCode.ComplexBarcode.ComplexBarcodeGenerator($mailmark)");
        sb.AppendLine("    $generator.Parameters.Barcode.XDimension.Pixels = 4");
        sb.AppendLine("");
        sb.AppendLine("    $fileName = \"Mailmark_{0}.png\" -f $rec.ItemID");
        sb.AppendLine("    $outputPath = Join-Path $outputDir $fileName");
        sb.AppendLine("    $generator.Save($outputPath, [Aspose.BarCode.Generation.BarCodeImageFormat]::Png)");
        sb.AppendLine("}");
        string psScriptContent = sb.ToString();

        // Write the PowerShell script file to the temporary folder
        using (var writer = new StreamWriter(psScriptPath, false, Encoding.UTF8))
        {
            writer.Write(psScriptContent);
        }

        // Output locations for verification
        Console.WriteLine("Demo folder: " + baseFolder);
        Console.WriteLine("CSV file: " + csvPath);
        Console.WriteLine("PowerShell script: " + psScriptPath);
        Console.WriteLine("Run the PowerShell script to generate barcode images.");
    }
}