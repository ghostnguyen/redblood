using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Diagnostics;
using System.Reflection;

public class MyMethodBase
{
    int _level;

    public string Name
    {
        get
        {
            StackTrace stackTrace = new StackTrace();

            try
            {
                MethodBase m = stackTrace.GetFrame(_level).GetMethod();
                string name = m.DeclaringType.Name + "." + m.Name;

                return name;
            }
            catch (Exception)
            {
                return "";
            }
        }
    }

    public MyMethodBase(int level)
    {
        _level = level;
    }

    public static MyMethodBase Current
    {
        get
        {
            return new MyMethodBase(1);
        }
    }

    public MyMethodBase Caller
    {
        get
        {
            return new MyMethodBase(_level + 1);
        }
    }
}

public class Nameof<T>
{
    public static string Property<TProp>(Expression<Func<T, TProp>> expression)
    {
        var body = expression.Body as MemberExpression;
        if (body == null)
            throw new ArgumentException("'expression' should be a member expression");
        return body.Member.Name;
    }
}

