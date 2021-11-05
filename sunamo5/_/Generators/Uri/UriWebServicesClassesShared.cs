using System;
using System.Collections.Generic;
using System.Text;

public partial class UriWebServices
{
    public partial class Facebook
    {
        public static string FacebookProfile(string nick)
        {
            return "https://www.facebook.com/" + nick;
        }
    }
}