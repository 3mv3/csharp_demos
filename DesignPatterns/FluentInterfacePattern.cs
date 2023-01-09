namespace DesignPatternsDemo
{
    #region Interfaces

    public interface IAssertThat<T>
    {
        IAssertHas<T> That(Func<T> exp);
    }

    public interface IAssertHas<T>
    {
        IAssertProperty<T> Has(Func<T, bool> predicate);
    }

    public interface IAssertProperty<T>
    {
        IAssertThen Then();
    }

    public interface IAssertThen : IAssertAnd
    {
        IAssertHandle Handle();
    }

    public interface IAssertHandle
    {
        IAssertHandle GenericRequest(Action action);

        IAssertAnd And();

        IAssertHandle If(Func<bool> condition);
    }

    public interface IAssertAnd
    {
        void Finish();
    }
    #endregion

    #region Implementation

    public class DemoAsserter<T> : IAssertThat<T>,
        IAssertHas<T>,
        IAssertProperty<T>,
        IAssertThen,
        IAssertAnd,
        IAssertHandle
        where T : class
    {
        private T? TestObject { get; set; }

        private List<Action>? actions { get; set; }

        // The first state in the chain, passing in an object that will be evaluated against in subsequent methods
        public IAssertHas<T> That(Func<T> exp)
        {
            TestObject = exp.Invoke();
            return this;
        }

        public IAssertProperty<T> Has(Func<T, bool> predicate)
        {
            Assert.That(predicate(TestObject));
            return this;
        }

        public IAssertAnd And()
        {
            return this;
        }

        public IAssertHandle Handle()
        {
            return this;
        }

        public IAssertHandle GenericRequest(Action action)
        {
            if (actions == null)
            {
                actions = new List<Action>();
            }

            actions.Add(action);

            return this;
        }

        public IAssertHandle If(Func<bool> condition)
        {
            var last = actions.Last();

            if (!condition.Invoke())
            {
                actions.Remove(last);
            }
            else
            {
                last();
            }

            return this;
        }

        public IAssertThen Then()
        {
            return this;
        }

        // The final method returns void to end the chain
        public void Finish()
        {

        }
    }

    #endregion
}