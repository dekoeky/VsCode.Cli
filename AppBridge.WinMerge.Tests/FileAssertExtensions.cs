namespace Dekoeky.AppBridge;

public static class FileAssertExtensions
{
    extension(Assert _)
    {
        public void FileExists(string path)
        {
            if (File.Exists(path))
                return;

            Assert.Fail($"Required file not found: {path}");
        }

        public void DirectoryExists(string path)
        {
            if (Directory.Exists(path))
                return;

            Assert.Fail($"Required directory not found: {path}");
        }
    }
}
