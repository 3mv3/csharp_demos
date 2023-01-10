using System.Linq.Expressions;
using System.Reflection;

namespace ExtensionMethodsDemo
{
    public class ExpressionExtensionTests
    {
        [Test]
        public void GetMemberNamesFromCollection()
        {
            // Arrange
            Expression<Func<Class1, object?[]>> expression = x => new[]
            {
                x.Name,
                x.Description
            };

            // Act
            var names = expression.GetMemberNamesFromCollection();

            // Assert
            Assert.That(names.Length, Is.EqualTo(2));
            Assert.That(names.Any(x => x == "Name"));
            Assert.That(names.Any(x => x == "Description"));
        }

        [Test]
        public void GetMemberNames()
        {
            // Arrange
            Expression<Func<Class1, object>> expression = x => x.Name;

            // Act
            var name = expression.GetMemberName();

            // Assert
            Assert.That(name, Is.EqualTo("Name"));
        }
    }

    public static class ExpressionExtensionTests
    {
        public static T GetMemberInfo<T>(this Expression expression)
            where T : MemberInfo
        {
            MemberExpression memberExpression = null;

            if (expression is LambdaExpression l)
            {
                if (l.Body is UnaryExpression)
                {
                    var unaryExpression = (UnaryExpression)l.Body;
                    memberExpression = (MemberExpression)unaryExpression.Operand;
                }
                else
                {
                    memberExpression = (MemberExpression)l.Body;
                }
            }
            else if (expression is UnaryExpression u)
            {
                memberExpression = u.Operand as MemberExpression;
            }
            else if (expression is MemberExpression m)
            {
                memberExpression = m;
            }

            return memberExpression.Member as T;
        }

        public static string GetMemberName(this Expression expresion)
        {
            return GetMemberInfo<MemberInfo>(expresion).Name;
        }

        /// <summary>
        /// Get member info for new array or list init expressions.
        /// </summary>
        public static T[] GetMemberInfoFromCollection<T>(this Expression expression)
            where T : MemberInfo
        {
            List<MemberInfo> memberInfo = new List<MemberInfo>();
            var t = ((LambdaExpression)expression).Body;

            if (t is NewArrayExpression n)
            {
                n.Expressions.ToList().ForEach(x =>
                {
                    var mem = x.GetMemberInfo<MemberInfo>();
                    memberInfo.Add(mem);
                });
            }
            else if (t is ListInitExpression l)
            {
                l.Initializers.ToList().ForEach(x =>
                {
                    var mem = x.Arguments[0].GetMemberInfo<MemberInfo>();
                    memberInfo.Add(mem);
                });
            }

            return memberInfo.Select(x => x as T).ToArray();
        }

        /// <summary>
        /// Get the member names for new array or list init expressions.
        /// </summary>
        public static string[] GetMemberNamesFromCollection(this Expression expression)
        {
            return GetMemberInfoFromCollection<MemberInfo>(expression)
                .Select(x => x.Name)
                .ToArray();
        }
    }
}
