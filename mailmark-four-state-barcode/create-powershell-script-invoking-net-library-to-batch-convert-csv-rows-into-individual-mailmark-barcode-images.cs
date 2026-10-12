// Title: Generate Mailmark barcodes from CSV via PowerShell script
// Description: Demonstrates creating a temporary workspace, writing sample CSV data, generating a PowerShell script that uses Aspose.BarCode to convert each CSV row into a Mailmark barcode image, and indicating where the output is stored.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcode types such as Mailmark. It shows how to prepare data, invoke the Aspose.BarCode .NET library from PowerShell, and batch‑process rows to produce PNG images. Developers working with postal barcodes, bulk image creation, or automation scripts will find this pattern useful for integrating Aspose.BarCode into CI pipelines or custom tooling.
// Prompt: Create a PowerShell script invoking the .NET library to batch convert CSV rows into individual Mailmark barcode images.
// Tags: mailmark, barcode, generation, powershell, csv, batch, png, aspose.barcode

using System;
using System.IO;
using System.Text;

/// <summary>
/// Demonstrates generating a PowerShell script that reads a CSV file and creates Mailmark barcode images using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Sets up temporary files, writes sample CSV data, builds the PowerShell script, and outputs paths.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary working directory
        string workDir = Path.Combine(Path.GetTempPath(), "MailmarkBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workDir);

        // Define paths for the CSV file, PowerShell script, and barcode output folder
        string csvPath = Path.Combine(workDir, "data.csv");
        string scriptPath = Path.Combine(workDir, "GenerateMailmark.ps1");
        string outputDir = Path.Combine(workDir, "Barcodes");
        Directory.CreateDirectory(outputDir);

        // Write sample CSV data that matches the Mailmark codetext fields
        using (var writer = new StreamWriter(csvPath, false, Encoding.UTF8))
        {
            writer.WriteLine("Format,VersionID,Class,SupplychainID,ItemID,DestinationPostCodePlusDPS");
            writer.WriteLine("4,1,0,384224,16563762,EF61AH8T ");
            writer.WriteLine("4,1,1,123456,100001,EF61AH8T ");
            writer.WriteLine("4,1,2,654321,200002,EF61AH8T ");
        }

        // Build the PowerShell script that will read the CSV and generate barcodes
        var sb = new StringBuilder();
        sb.AppendLine("# PowerShell script to generate Mailmark barcodes from CSV");
        sb.AppendLine($"$csvPath = \"{csvPath}\"");
        sb.AppendLine($"$outputDir = \"{outputDir}\"");
        sb.AppendLine("if (-not (Test-Path $outputDir)) { New-Item -ItemType Directory -Path $outputDir | Out-Null }");
        sb.AppendLine();
        sb.AppendLine("Add-Type -Path \"Aspose.BarCode.dll\"");
        sb.AppendLine();
        sb.AppendLine("$rows = Import-Csv -Path $csvPath");
        sb.AppendLine("foreach ($row in $rows) {");
        sb.AppendLine("    $mailmark = New-Object Aspose.BarCode.ComplexBarcode.MailmarkCodetext");
        sb.AppendLine("    $mailmark.Format = [int]$row.Format");
        sb.AppendLine("    $mailmark.VersionID = [int]$row.VersionID");
        sb.AppendLine("    $mailmark.Class = $row.Class");
        sb.AppendLine("    $mailmark.SupplychainID = [int]$row.SupplychainID");
        sb.AppendLine("    $mailmark.ItemID = [int]$row.ItemID");
        sb.AppendLine("    $mailmark.DestinationPostCodePlusDPS = $row.DestinationPostCodePlusDPS");
        sb.AppendLine();
        sb.AppendLine("    $generator = New-Object Aspose.BarCode.ComplexBarcode.ComplexBarcodeGenerator $mailmark");
        sb.AppendLine("    $generator.Parameters.Barcode.XDimension.Pixels = 4");
        sb.AppendLine();
        sb.AppendLine("    $fileName = \"Mailmark_{0}_{1}.png\" -f $row.ItemID, ($row.DestinationPostCodePlusDPS -replace \"\\s+$\", \"\")");
        sb.AppendLine("    $outputPath = Join-Path $outputDir $fileName");
        sb.AppendLine("    $generator.Save($outputPath, [Aspose.BarCode.Generation.BarCodeImageFormat]::Png)");
        sb.AppendLine("    $generator.Dispose()");
        sb.AppendLine("}");
        string scriptContent = sb.ToString();

        // Write the generated PowerShell script to a file
        using (var writer = new StreamWriter(scriptPath, false, Encoding.UTF8))
        {
            writer.Write(scriptContent);
        }

        // Inform the user where the temporary files are located
        Console.WriteLine("Work directory: " + workDir);
        Console.WriteLine("CSV file: " + csvPath);
        Console.WriteLine("PowerShell script: " + scriptPath);
        Console.WriteLine("Generated script will create barcode images in: " + outputDir);
    }
}