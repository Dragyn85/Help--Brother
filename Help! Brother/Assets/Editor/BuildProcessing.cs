using System;
using System.IO;
using Microsoft.Win32;
using UnityEngine;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

public class BuildProcessing : IPreprocessBuildWithReport, IPostprocessBuildWithReport
{
    [MenuItem("My Menu/Quick Build")]
    static void QuickBuild()
    {
        string buildpath = Path.Combine(Application.dataPath, "../Builds/");
        if (Directory.Exists(buildpath))
            Directory.CreateDirectory(buildpath);

        string exePath = Path.Combine(buildpath, Application.productName + ".exe");
        BuildPipeline.BuildPlayer(EditorBuildSettings.scenes, exePath, EditorUserBuildSettings.activeBuildTarget,
            BuildOptions.None);
    }
    
    public int callbackOrder 
    {
        get {return 0; }
    }
    public void OnPostprocessBuild(BuildReport report)
    {
        if(report.summary.result == BuildResult.Failed)
            return;
        
        ExportVersionFile(report.summary.outputPath);

        if (report.summary.platformGroup == BuildTargetGroup.Standalone)
        {
            if(EditorUtility.DisplayDialog("Create Installer", "Would you like to create an installer?","Yes","no"))
                RunInstallerCompiler();
        }
    }

    public void OnPreprocessBuild(BuildReport report)
    {
        IncrementVersion();
    }

    void IncrementVersion()
    {
        Version version;
        if (Version.TryParse(PlayerSettings.bundleVersion, out version))
        {
            version = new Version(version.Major, version.Minor, version.Build + 1);
            PlayerSettings.bundleVersion = version.ToString();
        }
    }

    void ExportVersionFile(string outputPath)
    {
        string buildPath = new FileInfo(outputPath).Directory.FullName;
        string versionPath = Path.Combine(buildPath, "Version.txt");
        File.WriteAllText(versionPath,PlayerSettings.bundleVersion);
    }

    void RunInstallerCompiler()
    {
        string innoCopilerPath;
        if (TryGetInnoSetupCompilerPath(out innoCopilerPath))
        {

            string installationPath = Path.Combine(Application.dataPath, "../Installers/");

            string installerPath = Path.Combine(installationPath, "installer.iss");

            if (File.Exists(innoCopilerPath) && File.Exists(installerPath))
            {
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                process.StartInfo.FileName = innoCopilerPath;
                process.StartInfo.Arguments = @"/cc " + installerPath;
                process.Start();
            }
        }
    }

    bool TryGetInnoSetupCompilerPath(out string path)
    {
        RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Classes\InnoSetupScriptFile\shell\open\command");
        if (key != null)
        {
            string value = key.GetValue("") as string;
            foreach (var entry in value.Split('\"'))
            {
                if (entry.EndsWith(".exe"))
                {
                    path = entry;
                    return true;
                }
            }
        }
        Debug.LogError("Inno Setup Compiler not found!");
        path = null;
        return false;


    }
}
