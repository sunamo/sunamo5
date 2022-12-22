public static class SpecialFolders
{
    public static string MyDocuments(string path)
    {
        return @"D:\Documents\" + path.TrimStart(AllChars.bs);
    }

    public static string eMyDocuments(string path)
    {
        return @"E:\Documents\" + path.TrimStart(AllChars.bs);
    }
}