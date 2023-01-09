namespace DesignPatternsDemo
{
    public class Tests
    {
        // Create the object as an interface rather than a concrete object
        // This restricts the methods that are initially accessible
        public IAssertThat<DemoObject> FluentAssert { get; set; }

        [SetUp]
        public void Setup()
        {
            FluentAssert = new DemoAsserter<DemoObject>();
        }

        [Test]
        public void FluentInterfaceTest()
        {
            // Arrange
            var demo = new DemoObject
            {
                Name = "test",
            };

            // Act
            FluentAssert.That(() => demo)
                .Has(x => x.Name == "test")
                .Then()
                .Handle()
                .GenericRequest(() =>
                {
                    demo.Name = "changed";
                })
                .If(() => true)
                .And()
                .Finish();

            // Assert
            Assert.That(demo.Name, Is.EqualTo("changed"));
        }

        [Test]
        public void StrategyPatternTest_ViewDetailsByItemOnly()
        {
            // Arrange
            var model = new TestModel1
            {
                Item = "Blah"
            };

            var strategy = TestModel1StrategyFactory.Create(model);

            // Act
            strategy.Transition();

            // Assert
            Assert.That(strategy.SelectItemDetailDisplayed(), Is.False);
            Assert.That(model.Subinventory, Is.Null);
        }

        [Test]
        public void StrategyPatternTest_ViewDetailsByItemSub()
        {
            // Arrange
            var model = new TestModel1
            {
                Item = "Blah",
                Subinventory = "Sub"
            };

            var strategy = TestModel1StrategyFactory.Create(model);

            // Act
            strategy.Transition();

            // Assert
            Assert.That(strategy.SelectItemDetailDisplayed(), Is.True);
            Assert.That(model.Subinventory, Is.EqualTo("Transition test"));
        }
    }
}