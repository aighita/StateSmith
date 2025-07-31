using System.Text;

namespace StateSmith.Output.Gil.CodesysSt;

public class GilToCodesysSt : IGilTranspiler
{
    private readonly ICodeFileWriter codeFileWriter;
    private readonly IOutputInfo outputInfo;

    public GilToCodesysSt(IOutputInfo outputInfo, ICodeFileWriter codeFileWriter)
    {
        this.outputInfo = outputInfo;
        this.codeFileWriter = codeFileWriter;
    }

    public void TranspileAndOutputCode(string gilCode)
    {
        var fileSb = new StringBuilder();
        var visitor = new CodesysStGilVisitor(gilCode, fileSb);
        visitor.Process();

        // Write to .st file
        var outputPath = $"{outputInfo.OutputDirectory}{outputInfo.BaseFileName}.st";
        codeFileWriter.WriteFile(outputPath, fileSb.ToString());

        // Also export the GIL code to a .gil file
        var gilPath = $"{outputInfo.OutputDirectory}{outputInfo.BaseFileName}.gil";
        codeFileWriter.WriteFile(gilPath, gilCode);

        System.Diagnostics.Debug.WriteLine($"[CodesysST] Writing output to: {outputPath}");
        System.Diagnostics.Debug.WriteLine($"[CodesysST] Output content:\n{fileSb}");
        System.Diagnostics.Debug.WriteLine($"[CodesysST] Exported GIL to: {gilPath}");
        System.Console.WriteLine($"Generated Codesys ST code for {outputInfo.BaseFileName}.st");
    }
}
