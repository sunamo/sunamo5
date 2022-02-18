public partial class ThrowEx
{
    public static void NotSupportedExtension(string extension)
    {
        Custom("Extensions is not supported: " + extension);
    }
}