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

/// <summary>
/// Gets property name using lambda expressions.
/// </summary>
public class PropertyName
{
    public static string For<T>(
        Expression<Func<T, object>> expression)
    {
        Expression body = expression.Body;
        return GetMemberName(body);
    }

    public static string For(
        Expression<Func<object>> expression)
    {
        Expression body = expression.Body;
        return GetMemberName(body);
    }

    public static string GetMemberName(
        Expression expression)
    {
        if (expression is MemberExpression)
        {
            var memberExpression = (MemberExpression)expression;

            if (memberExpression.Expression.NodeType ==
                ExpressionType.MemberAccess)
            {
                return GetMemberName(memberExpression.Expression)
                    + "."
                    + memberExpression.Member.Name;
            }
            return memberExpression.Member.Name;
        }

        if (expression is UnaryExpression)
        {
            var unaryExpression = (UnaryExpression)expression;

            if (unaryExpression.NodeType != ExpressionType.Convert)
                throw new Exception(string.Format(
                    "Cannot interpret member from {0}",
                    expression));

            return GetMemberName(unaryExpression.Operand);
        }

        throw new Exception(string.Format(
            "Could not determine member from {0}",
            expression));
    }
}


