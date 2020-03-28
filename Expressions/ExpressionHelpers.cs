using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Dasetto.Word
{
    public static class ExpressionHelpers
    {

        // Compiles an expression and gets the function to return value
        public static T GetPropertyValue <T> (this Expression<Func<T>> lamba)
        {
            return lamba.Compile().Invoke();
        }


        //sets the underlying properties value to the given value, fro, an expression that contains the property
        public static void SetPropertyValue<T>(this Expression<Func<T>> lamba, T value)
        {
            //Converts  a lamba () => some.Property to some.Property
            var expression = (lamba as LambdaExpression).Body as MemberExpression;

            // Get the property expression so we can see it
            var propertyInfo = (PropertyInfo)expression.Member;
            var target = Expression.Lambda(expression.Expression).Compile().DynamicInvoke();

            //Set the value
            propertyInfo.SetValue(target, value);
        }
    }
}
