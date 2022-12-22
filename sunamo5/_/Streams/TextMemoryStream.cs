using System.Text;

public class TextMemoryStream
{
    public StringBuilder line = new StringBuilder();
    string fn = null;

    public TextMemoryStream(string t)
    {
        fn = t;

        string line2 = string.Empty;
        if (FS.ExistsFile(fn))
        {
            line2 = TF.ReadAllText(t, Encoding.UTF8);
        }

        line.Append(line2);
    }

    public void Save()
    {
        TF.SaveFile(line.ToString(), fn);
    }

    //public string LineStartingWith(string date)
    //{
    //    foreach (var item in lines)
    //    {
    //        if (item.StartsWith(date))
    //        {
    //            return item;
    //        }
    //    }
    //    return null;
    //}
}
