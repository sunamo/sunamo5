﻿using sunamo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

public partial class RH
{
    static Type type = typeof(ThrowEx);

    public static string FullPathCodeEntity(Type t)
    {
        return t.Namespace + AllStrings.dot + t.Name;
    }

    public static Assembly AssemblyWithName(string name)
    {
        var ass = AppDomain.CurrentDomain.GetAssemblies();
        var result = ass.Where(d => d.GetName().Name == name);
        if (result.Count() == 0)
        {
            result = ass.Where(d => d.FullName == name);
        }
        if (result.Count() == 0)
        {
            result = ass.Where(d => d.FullName.Contains(name));
        }
        return result.FirstOrDefault();
    }

    private static List<PropertyInfo> GetProps(object carSAuto)
    {
        Type carSAutoType = GetType(carSAuto);

        var result = carSAutoType.GetProperties().ToList();
        return result;
    }

    private static Type GetType(object carSAuto)
    {
        Type carSAutoType = null;
        var t1 = carSAuto.GetType();

        if (RH.IsType(t1))
        {
            carSAutoType = carSAuto as Type;
        }
        else
        {
            carSAutoType = carSAuto.GetType();
        }

        return carSAutoType;
    }

    /// <summary>
    /// A1 can be Type of instance
    /// All fields must be public
    /// </summary>
    /// <param name="carSAutoType"></param>
    public static List<FieldInfo> GetFields(object carSAuto)
    {
        Type carSAutoType = null;
        var t1 = carSAuto.GetType();

        if (RH.IsType(t1))
        {
            carSAutoType = carSAuto as Type;
        }
        else
        {
            carSAutoType = carSAuto.GetType();
        }
        var result = carSAutoType.GetFields().ToList();
        return result;
    }

    private static bool IsType(Type t1)
    {
        var t2 = typeof(Type);
        return t1.FullName == "System.RuntimeType" || t1 == t2;
    }

    public static Dictionary<string, string> GetValuesOfConsts(Type t, params string[] onlyNames)
    {
        var props = RH.GetConsts(t);
        Dictionary<string, string> values = new Dictionary<string, string>(props.Count);

        foreach (var item in props)
        {
            if (onlyNames.Length > 0)
            {
                if (!onlyNames.Contains(item.Name))
                {
                    continue;
                }
            }

            var o = GetValueOfField(item.Name, t, null, false);
            values.Add(item.Name, o.ToString());
        }

        return values;
    }


    public static List<string> GetValuesOfConsts(Type type)
    {
        var c = GetConsts(type);
        List<string> vr = new List<string>();
        foreach (var item in c)
        {
            vr.Add(SH.NullToStringOrDefault(item.GetValue(null)));
        }
        CA.Trim(vr);
        return vr;
    }

    public static object GetValueOfProperty(string name, Type type, object instance, bool ignoreCase)
    {
        PropertyInfo[] pis = type.GetProperties();
        return GetValue(name, type, instance, pis, ignoreCase, null);
    }

    public static object GetValueOfPropertyOrField(object o, string name)
    {
        var type = o.GetType();

        var value = GetValueOfProperty(name, type, o, false);

        if (value == null)
        {
            value = GetValueOfField(name, type, o, false);
        }

        return value;
    }

    public static string DumpListAsString(DumpAsStringArgs a, bool removeNull = false)
    {
        StringBuilder sb = new StringBuilder();
        var f = CA.ToList<object>((IEnumerable)a.o);

        if (removeNull)
        {
            f.RemoveAll(d => d == null);
        }

        if (f.Count > 0)
        {
            sb.AppendLine(RH.NameOfFieldsFromDump(f.First(), a));

            foreach (var item in f)
            {
                a.o = item;
                sb.AppendLine(DumpAsString(a));
            }
        }
        return sb.ToString();
    }

    public static string DumpListAsString(string name, IEnumerable o)
    {
        StringBuilder sb = new StringBuilder();

        int i = 0;
        foreach (var item in o)
        {
            sb.AppendLine(DumpAsString2(name + "#" + i, item));
            i++;
        }

        return sb.ToString();
    }

    public static string DumpListAsStringOneLine(string operation, IEnumerable o, DumpAsStringHeaderArgs a)
    {
        if (o.Count() > 0)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(Consts._3Asterisks);
            sb.AppendLine(operation + AllStrings.space + AllStrings.lb + o.Count() + AllStrings.rb + AllStrings.colon);

            sb.AppendLine(NameOfFieldsFromDump(o.FirstOrNull(), a));

            int i = 0;
            foreach (var item in o)
            {
                sb.AppendLine(DumpAsString(new DumpAsStringArgs { d = DumpProvider.Reflection, deli = AllStrings.swd, o = item, onlyValues = true, onlyNames = a.onlyNames }));
                i++;
            }

            sb.AppendLine(Consts._3Asterisks);
            return sb.ToString();
        }
        return string.Empty;
    }

    /// <summary>
    /// Delimited by NL
    /// </summary>
    /// <param name="v"></param>
    /// <param name="device"></param>
    /// <returns></returns>
    public static string DumpAsString2(string v, object device)
    {
        return DumpAsString(new DumpAsStringArgs { name = v, o = device, d = DumpProvider.Yaml });
    }

    /// <summary>
    /// swda Delimiter
    /// Mainly for fast comparing objects
    /// 
    /// Zde můžu zadat jen onlyNames kvůli DumpAsStringHeaderArgs
    /// Pokud chci více customizovat výstup, musím užít DumpAsString - DumpAsStringArgs
    /// </summary>
    /// <param name="v"></param>
    /// <param name="tableRowPageNew"></param>
    /// <returns></returns>
    public static string DumpAsString3(object tableRowPageNew, DumpAsStringHeaderArgs a = null)
    {
        if (a == null)
        {
            a = DumpAsStringHeaderArgs.Default;
        }
        var dasa = new DumpAsStringArgs { o = tableRowPageNew, deli = AllStrings.swd, onlyValues = true, onlyNames = a.onlyNames };
        return DumpAsString(dasa);
    }

    public static List<string> GetValuesOfField(object o, params string[] onlyNames)
    {
        return GetValuesOfField(o, onlyNames);
    }

    public static List<string> GetValuesOfField(object o, IList<string> onlyNames, bool onlyValues)
    {
        var t = o.GetType();
        var props = t.GetFields();
        List<string> values = new List<string>(props.Length);

        foreach (var item in props)
        {
            if (onlyNames.Count > 0)
            {
                if (!onlyNames.Contains(item.Name))
                {
                    continue;
                }
            }

            //values.Add(item.Name + AllStrings.cs2 + SH.ListToString(GetValueOfField(item.Name, t, o, false)));

            AddValue(values, item.Name, SH.ListToString(GetValueOfField(item.Name, t, o, false)), onlyValues);
        }

        return values;
    }

    public static List<string> GetValuesOfProperty2(object obj, List<string> onlyNames, bool onlyValues, bool takeVariablesIfThereIsNoProps = true)
    {
        var onlyNames2 = onlyNames.ToList();
        List<string> values = new List<string>();

        string name = null;
        var props = GetProps(obj); //TypeDescriptor.GetProperties(obj);

        bool isAllNeg = true;
        foreach (var item in onlyNames)
        {
            if (!item.StartsWith(AllStrings.excl))
            {
                isAllNeg = false;
            }
        }

        if (props.Count == 0)
        {
            var d = GetFields(obj);
            foreach (var descriptor in d)
            {
                GetValue(descriptor, isAllNeg, onlyNames, onlyNames2, obj, values, onlyValues);
            }
        }
        else
        {
            foreach (var descriptor in props)
            {
                GetValue(descriptor, isAllNeg, onlyNames, onlyNames2, obj, values, onlyValues);
            }
        }

        return values;
    }

    public static void GetValue(MemberInfo descriptor, bool isAllNeg, List<string> onlyNames, List<string> onlyNames2, object obj, List<string> values, bool onlyValues)
    {
        bool add = true;
        var name = descriptor.Name;

        if (onlyNames.Contains(AllStrings.excl + name))
        {
            return;
        }

        if (onlyNames2.Count > 0)
        {
            if (isAllNeg)
            {
                if (onlyNames2.Contains(AllStrings.excl + name))
                {
                    add = false;
                }
            }
            else
            {
                if (!onlyNames2.Contains(name))
                {
                    add = false;
                }
            }
        }

        if (add)
        {
            var value = GetValue(obj, descriptor);
            AddValue(values, name, value, onlyValues);
        }
    }

    #region For easy copy from RHShared64.cs
    #region Get types of class
    /// <summary>
    /// Return FieldInfo, so will be useful extract Name etc. 
    /// </summary>
    /// <param name="type"></param>
    public static List<FieldInfo> GetConsts(Type type, GetMemberArgs a = null)
    {
        if (a == null)
        {
            a = new GetMemberArgs();
        }
        IEnumerable<FieldInfo> fieldInfos = null;
        if (a.onlyPublic)
        {
            fieldInfos = type.GetFields(BindingFlags.Public | BindingFlags.Static |
            // return protected/public but not private
            BindingFlags.FlattenHierarchy).ToList();
        }
        else
        {
            ///fieldInfos = type.GetFields(BindingFlags.Static);//.Where(f => f.IsLiteral);
            fieldInfos = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic |
              BindingFlags.FlattenHierarchy).ToList();

        }


        var withType = fieldInfos.Where(fi => fi.IsLiteral && !fi.IsInitOnly).ToList();
        return withType;
    }
    #endregion 
    #endregion

    public static object GetValueOfField(string name, Type type, object instance, bool ignoreCase)
    {
        FieldInfo[] pis = type.GetFields();

        return GetValue(name, type, instance, pis, ignoreCase, null);
    }

    private static object GetValue(object instance, MemberInfo[] property, object v)
    {
        return GetValue(instance, property);
    }

    private static object GetValue(object instance, params MemberInfo[] property)
    {
        var val = property[0];

        if (val is PropertyInfo)
        {
            var pi = (PropertyInfo)val;
            return pi.GetValue(instance);
        }
        else if (val is FieldInfo)
        {
            var pi = (FieldInfo)val;
            return pi.GetValue(instance);
        }

        return null;
    }


    public static object GetValue(string name, Type type, object instance, IEnumerable pis, bool ignoreCase, object v)
    {
        return GetOrSetValue(name, type, instance, pis, ignoreCase, GetValue, v);
    }



    public static object GetOrSetValue(string name, Type type, object instance, IEnumerable pis, bool ignoreCase, Func<object, MemberInfo[], object, object> getOrSet, object v)
    {
        if (ignoreCase)
        {
            name = name.ToLower();
            foreach (MemberInfo item in pis)
            {
                if (item.Name.ToLower() == name)
                {
                    var property = type.GetMember(name);
                    if (property != null)
                    {
                        return getOrSet(instance, property, v);
                        //return GetValue(instance, property);
                    }
                }
            }
        }
        else
        {
            foreach (MemberInfo item in pis)
            {
                if (item.Name == name)
                {
                    var property = type.GetMember(name);
                    if (property != null)
                    {
                        return getOrSet(instance, property, v);
                        //return GetValue(instance, property);
                    }
                }
            }
        }
        return null;
    }




    private static void AddValue(List<string> values, string name, object value, bool onlyValue)
    {
        var v = SH.ListToString(value);
        if (onlyValue)
        {
            values.Add(v);
        }
        else
        {
            values.Add($"{name}: {v}");
        }

    }

    /// <summary>
    /// Check whether A1 is or is derived from A2
    /// </summary>
    /// <param name="type1"></param>
    /// <param name="type2"></param>
    public static bool IsOrIsDeriveFromBaseClass(Type children, Type parent, bool a1CanBeString = true)
    {
        if (children == Types.tString && !a1CanBeString)
        {
            return false;
        }

        if (children == null)
        {
            ThrowEx.IsNull(Exc.GetStackTrace(), type, "IsOrIsDeriveFromBaseClass", "children", children);
        }
        while (true)
        {
            if (children == null)
            {
                return false;
            }
            if (children == parent)
            {
                return true;
            }
            foreach (var inter in children.GetInterfaces())
            {
                if (inter == parent)
                {
                    return true;
                }
            }

            children = children.BaseType;
        }
    }



    /// <summary>
    /// A1 have to be selected
    /// </summary>
    /// <param name="name"></param>
    /// <param name="o"></param>
    public static string DumpAsString(DumpAsStringArgs a)
    {
        // When I was serializing ISymbol, execution takes unlimited time here
        //return o.DumpToString(name);
        string dump = null;
        if (a.o.GetType() == Types.tString)
        {
            dump = a.o.ToString();
        }
        else
        {
        switch (a.d)
        {
            case DumpProvider.Yaml:
            case DumpProvider.Json:
            case DumpProvider.ObjectDumper:
            case DumpProvider.Reflection:
                dump = SH.Join(a.onlyValues ? a.deli : Environment.NewLine, RH.GetValuesOfProperty2(a.o, a.onlyNames, a.onlyValues));
                break;
            default:
                ThrowEx.NotImplementedCase(Exc.GetStackTrace(), type, "DumpAsString", a.d);
                break;
        }
        }
        if (string.IsNullOrWhiteSpace(a.name))
        {
            return dump;
        }
        return a.name + Environment.NewLine + dump;

    }



    private static string NameOfFieldsFromDump(object obj, DumpAsStringHeaderArgs a)
    {
        var properties = TypeDescriptor.GetProperties(obj);
        List<string> ls = new List<string>();

        string name = null;

        foreach (PropertyDescriptor descriptor in properties)
        {
            name = descriptor.Name;
            if (a.onlyNames.Contains(AllStrings.excl + name))
            {
                continue;
            }
            ls.Add(name);
        }

        return SH.Join(AllStrings.swd, ls);
    }

    public static string DumpAsString3Dictionary3<T, T2, U>(string operation, Dictionary<T, Dictionary<T2, List<U>>> grouped)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine(operation);

        foreach (var item in grouped)
        {
            sb.AppendLine("1) " + item.Key.ToString());

            foreach (var item3 in item.Value)
            {
                sb.AppendLine("2) " + item3.Key.ToString());

                foreach (var v in item3.Value)
                {
                    sb.AppendLine(v.ToString());
                }
                sb.AppendLine();
            }
        }

        var vr = sb.ToString();
        return vr;
    }

    public static string DumpAsString3Dictionary2<T, T1>(string operation, Dictionary<T, List<T1>> grouped)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine(operation);

        foreach (var item in grouped)
        {
            sb.AppendLine("1) " + item.Key.ToString());

            foreach (var v in item.Value)
            {
                sb.AppendLine(v.ToString());
            }
            sb.AppendLine();
        }

        var vr = sb.ToString();
        return vr;
    }
}
