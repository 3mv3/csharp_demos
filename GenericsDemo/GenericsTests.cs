namespace GenericsDemo
{
    public class Generic<T>
    {
        public T? GenericProperty { get; set; }
    }

    public class DemoObject { }

    public class Tests
    {
        public Generic<DemoObject> GenericProperty { get; set; }

        [SetUp]
        public void Setup()
        {
            GenericProperty = new Generic<DemoObject>();
        }

        [Test]
        public void Test1()
        {
            Assert.That(GenericProperty.GenericProperty.GetType(), Is.EqualTo(typeof(DemoObject)));
        }
    }
}