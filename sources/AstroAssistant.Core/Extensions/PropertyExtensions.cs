using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace AstroAssistant
{
    /// <summary>
    /// Extensions for properties management
    /// </summary>
    public static class PropertyExtensions
    {
        /// <summary>
        /// Helper to find a member expression
        /// </summary>
        private static MemberExpression FindMemberExpression<T>(Expression<Func<T>> expression)
        {
            if (expression.Body is UnaryExpression)
            {
                var unary = (UnaryExpression)expression.Body;
                var member = unary.Operand as MemberExpression;
                if (member == null)
                    throw new ArgumentException(global::AstroAssistant.Resources.Locales.WrongUnaryExpressionMessage, "expression");
                return member;
            }
            return expression.Body as MemberExpression;
        }

        /// <summary>
        /// Get the property name from an expression
        /// </summary>
        public static String GetPropertyName<T>(this object target, Expression<Func<T>> expression)
        {
            if (expression == null)
            {
                throw new ArgumentNullException("expression");
            }

            // Find the expression
            var memberExpression = FindMemberExpression(expression);
            if (memberExpression == null)
            {
                throw new ArgumentException(global::AstroAssistant.Resources.Locales.WrongExpressionMessage, "expression");
            }

            // Find the member
            var member = memberExpression.Member as PropertyInfo;
            if (member == null)
            {
                throw new ArgumentException(global::AstroAssistant.Resources.Locales.WrongExpressionMessage, "expression");
            }

            // Reject the member from another class
            if (target != null && member.DeclaringType != null)
            {
                if (!member.DeclaringType.IsAssignableFrom(target.GetType()))
                {
                    throw new ArgumentException(global::AstroAssistant.Resources.Locales.WrongExpressionMessage, "expression");
                }
            }

            // Reject static member
            if (member.GetGetMethod(true).IsStatic)
            {
                throw new ArgumentException(global::AstroAssistant.Resources.Locales.WrongExpressionMessage, "expression");
            }

            // Returns the name
            return member.Name;
        }

    }
}
