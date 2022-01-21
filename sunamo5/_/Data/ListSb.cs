//using System;
//using System.Collections.Generic;
//using System.Text;

///// <summary>
///// Píčovina. Abych to mohl užívat v generických metodách jako je DictionaryHelper.AddOrCreate
///// musí to být list. Vytvářet něco univerzálního je píčovina. Vše co je 
///// univerzální je vždycky na hovno.
///// </summary>
//public class ListSb
//{
//    public List<string> list = null;
//    public StringBuilder sb = null;

//    public ListSb(List<string> l)
//    {
//        list = l;
//    }

//    public ListSb(String bs)
//    {
//        sb = bs;
//    }

//    internal StringBuilder Sb()
//    {
//        if (sb == null)
//        {
//            StringBuilder sb = new StringBuilder();
//            foreach (var item in list)
//            {
//                sb.AppendLine(item);
//            }
//            return sb;
//        }
//        return sb;
//    }
//}