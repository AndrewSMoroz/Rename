using System;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;

namespace Rename.Utility
{
	public static class PropertyHelper
	{
        //------------------------------------------------------------------------------------------------------------------------
        public static string GetPropertyName<TPropertySource>(Expression<Func<TPropertySource, object>> expression)
		{
			LambdaExpression lambda = expression as LambdaExpression;

			return ExtractPropertyName(lambda);
		}

        //------------------------------------------------------------------------------------------------------------------------
        public static string GetPropertyName<TPropertySource>(Expression<Func<TPropertySource>> expression)
		{
			LambdaExpression lambda = expression as LambdaExpression;

			return ExtractPropertyName(lambda);
		}

        //------------------------------------------------------------------------------------------------------------------------
        private static string ExtractPropertyName(LambdaExpression lambda)
		{
			MemberExpression memberExpression;
			if (lambda.Body is UnaryExpression)
			{
				var unaryExpression = lambda.Body as UnaryExpression;
				memberExpression = unaryExpression.Operand as MemberExpression;
			}
			else
			{
				memberExpression = lambda.Body as MemberExpression;
			}

			Debug.Assert(memberExpression != null, "Please provide a lambda expression like 'n => n.PropertyName'");

			if (memberExpression != null)
			{
				var propertyInfo = memberExpression.Member as PropertyInfo;

				return propertyInfo.Name;
			}

			return null;
		}
	}
}
