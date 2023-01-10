using AutoFixture;
using static AutoFixtureDemo.AutoFixtureExtensions;

namespace AutoFixtureDemos
{
    public class DemoObject1
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
    }

    public class DemoObject2
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
    }

    public class Tests
    {
        public Fixture fixture;

        [SetUp]
        public void SetUp()
        {
            fixture = new Fixture();
        }

        #region Omitter

        /// <summary>
        /// This test will ensure that when creating the specified object the property
        /// provided will be ommited.
        /// </summary>
        [Test]
        public void PropertyNameOmitter_CheckDeclaringType_True()
        {
            var c = new PropertyNameOmitter<DemoObject1>(new[]
            {
                nameof(DemoObject1.Description)
            });

            fixture.Customizations.Add(c);

            // Generate another object to make sure declaring type was checked
            // and specified properties weren't omitted from everything
            var demo1 = fixture.Create<DemoObject1>();
            var demo2 = fixture.Create<DemoObject2>();

            Assert.IsNotNull(demo1);

            // Assert random field was generated
            Assert.IsNotNull(demo1.Name);

            // Assert specified fields were omitted
            Assert.IsNull(demo1.Description);

            // Assert specified fields were not omitted from everything
            Assert.IsNotNull(demo2.Description);
        }

        /// <summary>
        /// This test will ensure that when creating any object that has a property
        /// matching the one outlined in the omitter, it won't be populated
        /// </summary>
        [Test]
        public void PropertyNameOmitter_CheckDeclaringType_False()
        {
            var c = new PropertyNameOmitter<DemoObject1>(false, new[]
            {
                nameof(DemoObject1.Description)
            });

            fixture.Customizations.Add(c);

            // Generate another object to make sure declaring type was checked
            // and specified properties weren't omitted from everything
            var demo1 = fixture.Create<DemoObject1>();
            var demo2 = fixture.Create<DemoObject2>();

            Assert.IsNotNull(demo1);

            // Assert random field was generated
            Assert.IsNotNull(demo1.Name);

            // Assert specified fields were omitted from everything
            Assert.IsNull(demo1.Description);
            Assert.IsNull(demo2.Description);
        }

        #endregion

        #region Includer

        [Test]
        public void PropertyNameIncluder_CheckDeclaringType_True()
        {
            var c = new PropertyNameIncluder<DemoObject1>(new[]
            {
                nameof(DemoObject1.Description)
            });

            fixture.Customizations.Add(c);

            // Generate another object to make sure declaring type was checked
            // and specified properties weren't omitted from everything
            var demo1 = fixture.Create<DemoObject1>();
            var demo2 = fixture.Create<DemoObject2>();

            Assert.IsNotNull(demo1);

            // Assert random field is null
            Assert.IsNull(demo1.Name);

            // Assert only specified fields were included
            Assert.IsNotNull(demo1.Description);
            Assert.IsNull(demo1.Name);

            // Assert specified fields were not omitted from everything
            Assert.IsNotNull(demo2.Description);
            Assert.IsNotNull(demo2.Name);
        }

        [Test]
        public void PropertyNameIncluder_CheckDeclaringType_False()
        {
            var c = new PropertyNameIncluder<DemoObject1>(false, new[]
            {
                nameof(DemoObject1.Description)
            });

            fixture.Customizations.Add(c);

            // Generate another object to make sure specified properties were
            // omitted from everything
            var demo1 = fixture.Create<DemoObject1>();
            var demo2 = fixture.Create<DemoObject2>();

            Assert.IsNotNull(demo1);

            // Assert expect fields are null
            Assert.IsNull(demo1.Name);
            Assert.IsNull(demo2.Name);

            // Assert only specified fields were included
            Assert.IsNotNull(demo1.Description);
            Assert.IsNotNull(demo2.Description);
        }

        #endregion

        #region GenerateStringAsType

        [Test]
        public void GenerateStringAsType_PropertyType_CheckDeclaringType_False()
        {
            // Passing in string as first type should result in any new object made
            // with property "string Name" should be returned as a long
            fixture.GenStringAsType<long>("Name");

            var demo1 = fixture.Create<DemoObject1>();
            var demo2 = fixture.Create<DemoObject2>();

            Assert.IsTrue(long.TryParse(demo1.Name, out long l));
            Assert.IsTrue(long.TryParse(demo2.Name, out long s));
        }

        [Test]
        public void GenerateStringAsType_DeclaringType_CheckDeclaringType_False()
        {
            // Passing in complex type as first type should result in only complex type
            // with property string Name should be returned as a long
            fixture.GenPropertyAsType<DemoObject1, long>("Name");

            var demo1 = fixture.Create<DemoObject1>();
            var demo2 = fixture.Create<DemoObject2>();

            Assert.IsFalse(long.TryParse(demo1.Name, out long l));
            Assert.IsFalse(long.TryParse(demo2.Name, out long s));
        }

        [Test]
        public void GenerateStringAsType_DeclaringType_CheckDeclaringType_True()
        {
            // Passing in complex type as first type should result in only complex type
            // with property string Subinventory should be returned as a long
            fixture.GenPropertyAsType<DemoObject1, long>(true, "Name");

            var demo1 = fixture.Create<DemoObject1>();
            var demo2 = fixture.Create<DemoObject2>();

            Assert.IsTrue(long.TryParse(demo1.Name, out long l));
            Assert.IsFalse(long.TryParse(demo2.Name, out long s));
        }
        #endregion
    }
}