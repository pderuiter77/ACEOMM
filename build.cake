var target = Argument("target", "Release");

var configuration = Argument("configuration", "Release");
var solution = "./ACEOMM.sln";

var buildDir = Directory("./bin") + Directory(configuration);

Task("Clean")
    .Does(() =>
{
    CleanDirectory(buildDir);
});

Task("Restore-NuGet-Packages")
    .IsDependentOn("Clean")
    .Does(() =>
{
    NuGetRestore(solution);
});

Task("Build")
    .IsDependentOn("Restore-NuGet-Packages")
    .Does(() =>
{
      // Use MSBuild
      MSBuild(solution, settings =>
        settings.SetConfiguration(configuration));
});

Task("Rebuild")
  .IsDependentOn("Clean")
  .IsDependentOn("Build");

Task("Clean-Release-Folder")
	.Does(() =>
{

});

Task("Release")
  .IsDependentOn("Rebuild")
  .IsDependentOn("Clean-Release-Folder");

RunTarget(target);