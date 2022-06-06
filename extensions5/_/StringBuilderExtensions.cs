using System;
using System.Collections.Generic;
using System.Text;


public static partial class StringBuilderExtensions
{
    public static bool EndsWith(this StringBuilder sb, string test)
    {
        if (sb.Length < test.Length)
            return false;

        string end = sb.ToString(sb.Length - test.Length, test.Length);
        return end.Equals(test);
    }
    
    public static bool StartWith(this StringBuilder sb, string test)
    {
        if (sb.Length < test.Length)
            return false;

        string start = sb.ToString(0, test.Length);
        return start.Equals(test);
    }

    public static StringBuilder TrimEnd(this StringBuilder name, string ext)
    {
        while (name.EndsWith(ext))
        {
            return name.Substring(0, name.Length - ext.Length);
        }
        return name;
    }
    
    
    
    public static StringBuilder TrimStart(this StringBuilder name, string ext)
    {
        while (name.StartWith(ext))
        {
            return name.Substring(ext.Length, name.Length - ext.Length);
        }
        return name;
    }
    
    public static StringBuilder Substring(this StringBuilder input, int indexFrom)
    {
        return input.Substring(1, input.Length - 1);
    }
    
    public static StringBuilder Substring(this StringBuilder input, int index, int length)
    {
        StringBuilder subString = new StringBuilder();
        if (index + length - 1 >= input.Length || index < 0) 
        {
            throw new ArgumentOutOfRangeException("Index out of range!"); 
        }
        int endIndex = index + length;
        for (int i = index; i < endIndex; i++)
        {
            subString.Append(input[i]);
        }
        return subString;
    }

        

        public static void TrimStart(this StringBuilder sb)
        {
            var length = sb.Length;
            for (int i = 0; i < length; i++)
            {
                if (char.IsWhiteSpace(sb[i]))
                {
                    sb.Remove(i, 1);
                }
                else
                {
                    break;
                }
            }
        }

        public static void Trim(this StringBuilder sb)
        {
            TrimEnd(sb);
            TrimStart(sb);
        }
    }
