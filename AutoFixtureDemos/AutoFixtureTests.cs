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
        [Test]
        public void PropertyNameOmitter_ArrayExpInput_CheckDeclaringType_True()
        {
            var c = new PropertyNameOmitter<DemoObject1>(x => new object[]
            {
                x.Description
            });

            fixture.Customizations.Add(c);

            // Generate another object to make sure declaring type was checked
            // and specified properties weren't omitted from everything
            var pickDetails = fixture.Create<DemoObject1>();
            var sub = fixture.Create<DemoObject2>();

            Assert.IsNotNull(pickDetails);

            // Assert random field was generated
            Assert.IsNotNull(pickDetails.ShippingPriority);

            // Assert specified fields were omitted
            Assert.IsNull(pickDetails.Subinventory);
            Assert.IsNull(pickDetails.Locator);

            // Assert specified fields were not omitted from everything
            Assert.IsNotNull(sub.Subinventory);
            Assert.IsNotNull(loc.Locator);
        }

        [Test]
        public void PropertyNameOmitter_ArrayExpInput_CheckDeclaringType_False()
        {
            var c = new PropertyNameOmitter<DemoObject1>(false, x => new object[]
            {
                x.Description

            });

            fixture.Customizations.Add(c);

            var pickDetails = fixture.Create<DemoObject1>();

            // Generate another object to compare against
            // As declaring type wasn't checked, any property name that matches
            // should be omitted
            var sub = fixture.Create<DemoObject2>();
            var loc = fixture.Create<RfsLocator>();

            Assert.IsNotNull(pickDetails);

            // Assert random field was generated
            Assert.IsNotNull(pickDetails.ShippingPriority);

            // Assert specified fields were omitted
            Assert.IsNull(pickDetails.Subinventory);
            Assert.IsNull(pickDetails.Locator);

            // Assert specified fields were omitted from everything
            Assert.IsNull(sub.Subinventory);
            Assert.IsNull(loc.Locator);
        }

        [Test]
        public void PropertyNameOmitter_SingleExpInput_CheckDeclaringType_True()
        {
            var c = new PropertyNameOmitter<DemoObject1>(x => x.Subinventory);

            fixture.Customizations.Add(c);

            var pickDetails = fixture.Create<DemoObject1>();

            // Generate another object to make sure declaring type was checked
            // and specified properties weren't omitted from everything
            var sub = fixture.Create<DemoObject2>();

            Assert.IsNotNull(pickDetails);

            // Assert random field was generated
            Assert.IsNotNull(pickDetails.ShippingPriority);

            // Assert specified fields were omitted
            Assert.IsNull(pickDetails.Subinventory);

            // Assert specified fields were not omitted from everything
            Assert.IsNotNull(sub.Subinventory);
        }

        [Test]
        public void PropertyNameOmitter_SingleExpInput_CheckDeclaringType_False()
        {
            var c = new PropertyNameOmitter<DemoObject1>(false, x => x.Subinventory);

            fixture.Customizations.Add(c);

            var pickDetails = fixture.Create<DemoObject1>();

            // Generate another object to compare against
            // As declaring type wasn't checked, any property name that matches
            // should be omitted
            var sub = fixture.Create<DemoObject2>();

            Assert.IsNotNull(pickDetails);

            // Assert random field was generated
            Assert.IsNotNull(pickDetails.ShippingPriority);

            // Assert specified fields were omitted
            Assert.IsNull(pickDetails.Subinventory);

            // Assert specified fields were omitted from everything
            Assert.IsNull(sub.Subinventory);
        }

        [Test]
        public void PropertyNameOmitter_SingleStringInput_CheckDeclaringType_True()
        {
            var c = new PropertyNameOmitter<DemoObject1>("Subinventory");

            fixture.Customizations.Add(c);

            var pickDetails = fixture.Create<DemoObject1>();

            // Generate another object to make sure declaring type was checked
            // and specified properties weren't omitted from everything
            var sub = fixture.Create<DemoObject2>();

            Assert.IsNotNull(pickDetails);

            // Assert random field was generated
            Assert.IsNotNull(pickDetails.ShippingPriority);

            // Assert specified fields were omitted
            Assert.IsNull(pickDetails.Subinventory);

            // Assert specified fields were not omitted from everything
            Assert.IsNotNull(sub.Subinventory);
        }

        [Test]
        public void PropertyNameOmitter_SingleStringInput_CheckDeclaringType_False()
        {
            var c = new PropertyNameOmitter<DemoObject1>(false, "Subinventory");

            fixture.Customizations.Add(c);

            var pickDetails = fixture.Create<DemoObject1>();

            // Generate another object to compare against
            // As declaring type wasn't checked, any property name that matches
            // should be omitted
            var sub = fixture.Create<DemoObject2>();

            Assert.IsNotNull(pickDetails);

            // Assert random field was generated
            Assert.IsNotNull(pickDetails.ShippingPriority);

            // Assert specified fields were omitted
            Assert.IsNull(pickDetails.Subinventory);

            // Assert specified fields were omitted from everything
            Assert.IsNull(sub.Subinventory);
        }

        [Test]
        public void PropertyNameOmitter_MultiStringInput_CheckDeclaringType_True()
        {
            var c = new PropertyNameOmitter<DemoObject1>("Subinventory", "Locator");

            fixture.Customizations.Add(c);

            var pickDetails = fixture.Create<DemoObject1>();

            // Generate another object to make sure declaring type was checked
            // and specified properties weren't omitted from everything
            var sub = fixture.Create<DemoObject2>();
            var loc = fixture.Create<RfsLocator>();

            Assert.IsNotNull(pickDetails);

            // Assert random field was generated
            Assert.IsNotNull(pickDetails.ShippingPriority);

            // Assert specified fields were omitted
            Assert.IsNull(pickDetails.Subinventory);
            Assert.IsNull(pickDetails.Locator);

            // Assert specified fields were not omitted from everything
            Assert.IsNotNull(sub.Subinventory);
            Assert.IsNotNull(loc.Locator);
        }

        [Test]
        public void PropertyNameOmitter_MultiStringInput_CheckDeclaringType_False()
        {
            var c = new PropertyNameOmitter<DemoObject1>(false, "Subinventory", "Locator");

            fixture.Customizations.Add(c);

            var pickDetails = fixture.Create<DemoObject1>();

            // Generate another object to compare against
            // As declaring type wasn't checked, any property name that matches
            // should be omitted
            var sub = fixture.Create<DemoObject2>();
            var loc = fixture.Create<RfsLocator>();

            Assert.IsNotNull(pickDetails);

            // Assert random field was generated
            Assert.IsNotNull(pickDetails.ShippingPriority);

            // Assert specified fields were omitted
            Assert.IsNull(pickDetails.Subinventory);
            Assert.IsNull(pickDetails.Locator);

            // Assert specified fields were omitted from everything
            Assert.IsNull(sub.Subinventory);
            Assert.IsNull(loc.Locator);
        }
        #endregion

        #region Includer
        [Test]
        public void PropertyNameIncluder_ArrayExpInput_CheckDeclaringType_True()
        {
            var c = new PropertyNameIncluder<DemoObject1>(x => new object[]
            {
                x.Description

            });

            fixture.Customizations.Add(c);

            var pickDetails = fixture.Create<DemoObject1>();

            // Generate another object to make sure declaring type was checked
            // and specified properties weren't omitted from everything
            var sub = fixture.Create<DemoObject2>();
            var loc = fixture.Create<RfsLocator>();

            Assert.IsNotNull(pickDetails);

            // Assert random field is null
            Assert.IsNull(pickDetails.ShippingPriority);

            // Assert specified fields were included
            Assert.IsNotNull(pickDetails.Subinventory);
            Assert.IsNotNull(pickDetails.Locator);

            // Assert specified fields were not omitted from everything
            Assert.IsNotNull(sub.Subinventory);
            Assert.IsNotNull(sub.CountMethod);
            Assert.IsNotNull(loc.Locator);
            Assert.IsNotNull(loc.LocatorDescription);
        }

        [Test]
        public void PropertyNameIncluder_ArrayExpInput_CheckDeclaringType_False()
        {
            var c = new PropertyNameIncluder<DemoObject1>(false, x => new object[]
            {
                x.Description

            });

            fixture.Customizations.Add(c);

            var pickDetails = fixture.Create<DemoObject1>();

            // Generate another object to compare against
            // As declaring type wasn't checked, any property name that matches
            // should be omitted
            var sub = fixture.Create<DemoObject2>();
            var loc = fixture.Create<RfsLocator>();

            Assert.IsNotNull(pickDetails);

            // Assert random field is null
            Assert.IsNull(pickDetails.ShippingPriority);

            // Assert specified fields were included
            Assert.IsNotNull(pickDetails.Subinventory);
            Assert.IsNotNull(pickDetails.Locator);

            // Assert ONLY specified fields were included in all fixtures
            Assert.IsNotNull(sub.Subinventory);
            Assert.IsNull(sub.CountMethod);
            Assert.IsNotNull(loc.Locator);
            Assert.IsNull(loc.LocatorDescription);
        }

        [Test]
        public void PropertyNameIncluder_SingleExpInput_CheckDeclaringType_True()
        {
            var c = new PropertyNameIncluder<DemoObject1>(x => x.Subinventory);

            fixture.Customizations.Add(c);

            var pickDetails = fixture.Create<DemoObject1>();

            // Generate another object to make sure declaring type was checked
            // and specified properties weren't omitted from everything
            var sub = fixture.Create<DemoObject2>();

            Assert.IsNotNull(pickDetails);

            // Assert random field is null
            Assert.IsNull(pickDetails.ShippingPriority);

            // Assert specified fields were included
            Assert.IsNotNull(pickDetails.Subinventory);

            // Assert specified fields were not omitted from everything
            Assert.IsNotNull(sub.Subinventory);
            Assert.IsNotNull(sub.CountMethod);
        }

        [Test]
        public void PropertyNameIncluder_SingleExpInput_CheckDeclaringType_False()
        {
            var c = new PropertyNameIncluder<DemoObject1>(false, x => x.Subinventory);

            fixture.Customizations.Add(c);

            var pickDetails = fixture.Create<DemoObject1>();

            // Generate another object to compare against
            // As declaring type wasn't checked, any property name that matches
            // should be omitted
            var sub = fixture.Create<DemoObject2>();

            Assert.IsNotNull(pickDetails);

            // Assert random field is null
            Assert.IsNull(pickDetails.ShippingPriority);

            // Assert specified fields were included
            Assert.IsNotNull(pickDetails.Subinventory);

            // Assert ONLY specified fields were included in all fixtures
            Assert.IsNotNull(sub.Subinventory);
            Assert.IsNull(sub.CountMethod);
        }

        [Test]
        public void PropertyNameIncluder_SingleStringInput_CheckDeclaringType_True()
        {
            var c = new PropertyNameIncluder<DemoObject1>("Subinventory");

            fixture.Customizations.Add(c);

            var pickDetails = fixture.Create<DemoObject1>();

            // Generate another object to make sure declaring type was checked
            // and specified properties weren't omitted from everything
            var sub = fixture.Create<DemoObject2>();

            Assert.IsNotNull(pickDetails);

            // Assert random field is null
            Assert.IsNull(pickDetails.ShippingPriority);

            // Assert specified fields were included
            Assert.IsNotNull(pickDetails.Subinventory);

            // Assert specified fields were not omitted from everything
            Assert.IsNotNull(sub.Subinventory);
            Assert.IsNotNull(sub.CountMethod);
        }

        [Test]
        public void PropertyNameIncluder_SingleStringInput_CheckDeclaringType_False()
        {
            var c = new PropertyNameIncluder<DemoObject1>(false, "Subinventory");

            fixture.Customizations.Add(c);

            var pickDetails = fixture.Create<DemoObject1>();

            // Generate another object to compare against
            // As declaring type wasn't checked, any property name that matches
            // should be omitted
            var sub = fixture.Create<DemoObject2>();

            Assert.IsNotNull(pickDetails);

            // Assert random field is null
            Assert.IsNull(pickDetails.ShippingPriority);

            // Assert specified fields were included
            Assert.IsNotNull(pickDetails.Subinventory);

            // Assert ONLY specified fields were included in all fixtures
            Assert.IsNotNull(sub.Subinventory);
            Assert.IsNull(sub.CountMethod);
        }

        [Test]
        public void PropertyNameIncluder_MultiStringInput_CheckDeclaringType_True()
        {
            var c = new PropertyNameIncluder<DemoObject1>("Subinventory", "Locator");

            fixture.Customizations.Add(c);

            var pickDetails = fixture.Create<DemoObject1>();

            // Generate another object to make sure declaring type was checked
            // and specified properties weren't omitted from everything
            var sub = fixture.Create<DemoObject2>();
            var loc = fixture.Create<RfsLocator>();

            Assert.IsNotNull(pickDetails);

            // Assert random field is null
            Assert.IsNull(pickDetails.ShippingPriority);

            // Assert specified fields were included
            Assert.IsNotNull(pickDetails.Subinventory);
            Assert.IsNotNull(pickDetails.Locator);

            // Assert specified fields were not omitted from everything
            Assert.IsNotNull(sub.Subinventory);
            Assert.IsNotNull(sub.CountMethod);
            Assert.IsNotNull(loc.Locator);
            Assert.IsNotNull(loc.LocatorDescription);
        }

        [Test]
        public void PropertyNameIncluder_MultiStringInput_CheckDeclaringType_False()
        {
            var c = new PropertyNameIncluder<DemoObject1>(false, "Subinventory", "Locator");

            fixture.Customizations.Add(c);

            var pickDetails = fixture.Create<DemoObject1>();

            // Generate another object to compare against
            // As declaring type wasn't checked, any property name that matches
            // should be omitted
            var sub = fixture.Create<DemoObject2>();
            var loc = fixture.Create<RfsLocator>();

            Assert.IsNotNull(pickDetails);

            // Assert random field is null
            Assert.IsNull(pickDetails.ShippingPriority);

            // Assert specified fields were included
            Assert.IsNotNull(pickDetails.Subinventory);
            Assert.IsNotNull(pickDetails.Locator);

            // Assert ONLY specified fields were included in all fixtures
            Assert.IsNotNull(sub.Subinventory);
            Assert.IsNull(sub.CountMethod);
            Assert.IsNotNull(loc.Locator);
            Assert.IsNull(loc.LocatorDescription);
        }
        #endregion

        #region GenerateStringAsType
        [Test]
        public void GenerateStringAsType_PropertyType_CheckDeclaringType_False()
        {
            // Passing in string as first type should result in any new object made
            // with property string Subinventory should be returned as a long
            var c = new GenerateStringAsType<string, long>(fixture, "Subinventory");

            fixture.Customizations.Add(c);

            var pick = fixture.Create<DemoObject1>();
            var sub = fixture.Create<DemoObject2>();

            Assert.IsTrue(long.TryParse(pick.Subinventory, out long l));
            Assert.IsTrue(long.TryParse(sub.Subinventory, out long s));
        }

        [Test]
        public void GenerateStringAsType_PropertyType_CheckDeclaringType_True()
        {
            // Passing in string as first type should result in any new object made
            // with property string Subinventory should be returned as a long
            var c = new GenerateStringAsType<string, long>(true, fixture, "Subinventory");

            fixture.Customizations.Add(c);

            var pick = fixture.Create<DemoObject1>();
            var sub = fixture.Create<DemoObject2>();

            Assert.IsFalse(long.TryParse(pick.Subinventory, out long l));
            Assert.IsFalse(long.TryParse(sub.Subinventory, out long s));
        }

        [Test]
        public void GenerateStringAsType_DeclaringType_CheckDeclaringType_False()
        {
            // Passing in complex type as first type should result in only complex type
            // with property string Subinventory should be returned as a long
            var c = new GenerateStringAsType<DemoObject1, long>(fixture, "Subinventory");

            fixture.Customizations.Add(c);

            var pick = fixture.Create<DemoObject1>();
            var sub = fixture.Create<DemoObject2>();

            Assert.IsFalse(long.TryParse(pick.Subinventory, out long l));
            Assert.IsFalse(long.TryParse(sub.Subinventory, out long s));
        }

        [Test]
        public void GenerateStringAsType_DeclaringType_CheckDeclaringType_True()
        {
            // Passing in complex type as first type should result in only complex type
            // with property string Subinventory should be returned as a long
            var c = new GenerateStringAsType<DemoObject1, long>(true, fixture, "Subinventory");

            fixture.Customizations.Add(c);

            var pick = fixture.Create<DemoObject1>();
            var sub = fixture.Create<DemoObject2>();

            Assert.IsTrue(long.TryParse(pick.Subinventory, out long l));
            Assert.IsFalse(long.TryParse(sub.Subinventory, out long s));
        }
        #endregion
    }
}