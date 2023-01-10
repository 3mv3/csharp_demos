using AutoFixture;
using AutoFixture.Kernel;
using System.Reflection;

namespace AutoFixtureDemo
{
    public static class AutoFixtureExtensions
    {
        /// <summary>
        /// Omit all properties on a type apart from those specified in the constructor. If checkDeclaringType = false, then
        /// properties will be omitted from all types, apart from those specified in the constructor.
        /// </summary>
        public class PropertyNameIncluder<T> : ISpecimenBuilder
        {
            private readonly List<string> names = new List<string>();
            private readonly bool checkDeclaringType = true;

            internal PropertyNameIncluder(params string[] names) =>
                this.names = names.ToList();

            internal PropertyNameIncluder(bool checkDeclaringType, params string[] names)
            {
                this.names = names.ToList();
                this.checkDeclaringType = checkDeclaringType;
            }

            public object Create(object request, ISpecimenContext context)
            {
                var propInfo = request as PropertyInfo;
                if (propInfo != null &&
                    (checkDeclaringType ? propInfo.DeclaringType == typeof(T) : true) &&
                        !names.Contains(propInfo.Name))
                    return new OmitSpecimen();

                return new NoSpecimen();
            }
        }

        /// <summary>
        /// Omit all properties that are specified in the constructor for a given type. If checkDeclaringType = false, then
        /// properties specfieid in the constructor will be omitted for all types.
        /// </summary>
        public class PropertyNameOmitter<T> : ISpecimenBuilder
        {
            private readonly List<string> names = new List<string>();
            private readonly bool checkDeclaringType = true;

            internal PropertyNameOmitter(params string[] names) =>
                this.names = names.ToList();

            internal PropertyNameOmitter(bool checkDeclaringType, params string[] names)
            {
                this.names = names.ToList();
                this.checkDeclaringType = checkDeclaringType;
            }

            public object Create(object request, ISpecimenContext context)
            {
                var propInfo = request as PropertyInfo;
                if (propInfo != null &&
                    (checkDeclaringType ? propInfo.DeclaringType == typeof(T) : true) &&
                        names.Contains(propInfo.Name))
                    return new OmitSpecimen();

                return new NoSpecimen();
            }
        }

        public static void GenStringAsType<TConv>(this IFixture fixture, params string[] property)
        {
            fixture.Customizations.Add(new GenerateStringAsType<string, TConv>(fixture, property));
        }

        public static void GenPropertyAsType<TProp, TConv>(this IFixture fixture, params string[] property)
        {
            fixture.Customizations.Add(new GenerateStringAsType<TProp, TConv>(fixture, property));
        }

        public static void GenPropertyAsType<TProp, TConv>(this IFixture fixture, bool check, params string[] property)
        {
            fixture.Customizations.Add(new GenerateStringAsType<TProp, TConv>(check, fixture, property));
        }

        /// <summary>
        /// Ensure that the listed properties where type matches TProperty are always returned as a TConvertTo where applicable.
        /// If checkDeclaringType = true, when generating a DeclaringType (parent), ensure listed properties
        /// are always returned as a TConvertTo where applicable.
        /// </summary>
        public class GenerateStringAsType<TProperty, TConvertTo> : ISpecimenBuilder
        {
            private readonly IFixture fixture = null;
            private readonly List<string> properties;
            private readonly bool checkDeclaringType = false;

            public GenerateStringAsType(IFixture fixture, params string[] property)
            {
                this.fixture = fixture;
                this.properties = new List<string>(property);
            }

            public GenerateStringAsType(bool checkDeclaringType, IFixture fixture, params string[] property)
            {
                this.fixture = fixture;
                this.properties = new List<string>(property);
                this.checkDeclaringType = checkDeclaringType;
            }

            public object Create(object request, ISpecimenContext context)
            {
                var propInfo = request as PropertyInfo;

                if (propInfo == null ||
                    (checkDeclaringType ? !propInfo.DeclaringType.Equals(typeof(TProperty)) : !propInfo.PropertyType.Equals(typeof(TProperty))) ||
                        !properties.Contains(propInfo.Name))
                    return new NoSpecimen();

                return fixture.Create<TConvertTo>().ToString();
            }
        }
    }
}
