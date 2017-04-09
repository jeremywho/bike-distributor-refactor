using System;
using System.Linq.Expressions;

namespace BikeDistributor.Discount
{
    public class Rule
    {
        public readonly string PropertyName;
        public readonly string Operation;
        public readonly string TargetValue;

        public Rule(string propertyName, string operation, string targetValue)
        {
            PropertyName = propertyName;
            Operation = operation;
            TargetValue = targetValue;
        }

        public Func<T, bool> Compile<T>()
        {
            var parameter = Expression.Parameter(typeof(T));
            var expression = CreateComparisonExpression<T>(this, parameter);

            return Expression.Lambda<Func<T, bool>>(expression, parameter).Compile();
        }

        public static Expression CreateComparisonExpression<T>(Rule rule, Expression expression)
        {
            // get the operator
            if (!Enum.TryParse(rule.Operation, out ExpressionType tBinary))
                throw new ArithmeticException("Invalid comparison operator");

            // get the left hand side property
            (var leftExpression, var propertyType) = GetProperty(typeof(T), expression, rule.PropertyName);

            // get the right hand side propery
            var rightExpression = Expression.Constant(Convert.ChangeType(rule.TargetValue, propertyType));

            // create and return the rule expression
            return Expression.MakeBinary(tBinary, leftExpression, rightExpression);
        }

        private static (Expression, Type) GetProperty(Type type, Expression expression, string propertyName)
        {
            foreach (var p in propertyName.Split('.'))
            {
                var propertyInfo = type.GetProperty(p) ?? throw new ArgumentException($"type {type.Name} does not contain property {propertyName}");
                type = propertyInfo.PropertyType;

                expression = Expression.Property(expression, p);                
            }

            return (expression, type);
        }
    }
}