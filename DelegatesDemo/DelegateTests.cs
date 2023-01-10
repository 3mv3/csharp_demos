using System.Linq.Expressions;

namespace DelegatesDemo
{
    public class Tests
    {
        public class DemoObject
        {
            public string Id { get; set; }
        }

        /// <summary>
        /// Actions are delegates that always return void, and can take 0 to 9 
        /// inputs.
        /// </summary>
        Action action;
        Action<int> action1;
        Action<int, int> action2;

        Task Task;
        Task<int> Task1;

        /// <summary>
        /// Funcs are delegates that always have a return type, and can take up to 9 optional
        /// parameters
        /// </summary>
        Func<bool> func;
        Func<int, bool> func1;
        Func<Task<int>> func2;

        /// <summary>
        /// Expressions are delegate wrappers that allow access to the lambda function
        /// </summary>
        Expression<string> expression;
        Expression<Func<int, bool>> expression1;
        Expression<Func<Task<bool>>> expression2;

        [SetUp]
        public async Task Setup()
        {

            // You can also declare expressions in-line, the syntax is almost identical to
            // that of a func
            expression1 = i => func1(i);

            expression2 = () => Task.FromResult(true);
        }

        [Test]
        public void Actions()
        {
            // You can assign funcs as actions
            // or assign existing methods
            action = () => { };
            action = EmptyAction;

            action1 = i => { };
            action1 = IntAction;

            action2 = (i1, i2) => { };
            action2 = IntAction2;

            // actions are invoked like methods
            action();
            action1(0);
            action2(0, 0);

            // you can access method info for the action
            var n = action.Method.Name;
        }

        [Test]
        public async Task Funcs()
        {
            // You can declare funcs in-line
            func1 = i =>
            {
                return i == 0;
            };

            // funcs can be declared as async
            func2 = async () =>
            {
                return await Task.FromResult(1);
            };

            // funcs are invoked in the same style as methods via the 'Invoke' method
            var result = func1.Invoke(1);

            // If a task is returned then it must be awaited to get the result
            var result2 = await func2.Invoke();
        }

        [Test]
        public void Expressions()
        {
            Expression<Func<int[]>> newArrayExample = () => new[]
            {
                1,2,3
            };

            Expression<Func<List<int>>> newListExample = () => new List<int>
            {
                1,2,3
            };

            var i = 0;
            Expression<Func<bool>> binaryExample = () => i > 0;
            Expression<Func<bool>> conditionalExmaple = () => i == 0 ? true : false;

            // expressions have the benefit of accessing the lambda to use properties
            var name = expression2.Name;
            var body = expression2.Body;

            // manual expression building
            var argument = Expression.Parameter(typeof(DemoObject));
            var left = Expression.Property(argument, "Id");
            var right = Expression.Constant("Blah");

            // binary expression
            var predicate = Expression.Lambda<Func<DemoObject, bool>>(
                Expression.ReferenceEqual(left, right),
                new[] { argument }
            );

            var rst = predicate.Compile().Invoke(new DemoObject
            {
                Id = "Blah"
            });

            Assert.That(predicate.Body, Is.AssignableTo<BinaryExpression>());

            // once created, expressions have different body types depending
            // on the return type
            Assert.That(newArrayExample.Body, Is.AssignableTo<NewArrayExpression>());
            Assert.That(newListExample.Body, Is.AssignableTo<ListInitExpression>());
            Assert.That(binaryExample.Body, Is.AssignableTo<BinaryExpression>());
            Assert.That(conditionalExmaple.Body, Is.AssignableTo<ConditionalExpression>());

            // expressions have multiple types depending on the body of the expression tree,
            // these are some that I have encountered
            if (expression is BinaryExpression)
            {

            }
            else if (expression is ConditionalExpression)
            {

            }
            else if (expression is ListInitExpression)
            {

            }
            else if (expression is NewArrayExpression)
            {

            }

            // expressions can be compiled and the resulting func can be invoked
            var arr = newArrayExample.Compile().Invoke();
            expression2.Compile().Invoke();
        }

        [Test]
        public async Task Tasks()
        {
            // tasks are a wrapper for calling methods async
            var task = new Task(() => { });

            // you can take an existing method and wrap it in a task to call it async
            task = new Task(EmptyAction);

            await task;
        }

        public void EmptyAction() { }
        public void IntAction(int parms) { }
        public void IntAction2(int parms, int parms2) { }
    }
}