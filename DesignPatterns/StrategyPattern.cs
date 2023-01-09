namespace DesignPatternsDemo
{
    #region Generic implementations to be re-used for different models 

    public class StrategyDefinitionBase<T>
        where T : WorkflowModelBase, new()
    {
        // https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/required
        // https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/init
        public required T Model { get; init; }
        public string Name => this.GetType().Name;
    }

    public interface IStrategyFactoryBase<TWorkflowModel, TDefinition>
        where TWorkflowModel : WorkflowModelBase
    {
        /// <summary>
        /// Returns a strategy definition based on the state of the current model.
        /// </summary>
        /// <param name="workflowModel"></param>
        /// <returns></returns>
        static TDefinition Create(TWorkflowModel workflowModel) => throw new NotImplementedException();
    }

    #endregion

    public class TestModel1StrategyFactory : IStrategyFactoryBase<TestModel1, TestModel1Strategy>
    {
        public static TestModel1Strategy Create(TestModel1 Model)
        {
            // Item only
            if (Model.Item != null && Model.Subinventory == null)
                return new ViewDetailsByItemOnly
                {
                    Model = Model
                };

            // Item + Sub
            else if (Model.Item != null && Model.Subinventory != null)
                return new ViewDetailsByItemSub
                {
                    Model = Model
                };

            else
                throw new Exception("Strategy not definable.");
        }
    }

    /// <summary>
    /// Methods for this strategy implementation that need defining on a strategy-by-strategy basis.
    /// </summary>
    public abstract class TestModel1Strategy : StrategyDefinitionBase<TestModel1>
    {
        public abstract bool SelectItemDetailDisplayed();

        public abstract void Transition();
    }

    public sealed class ViewDetailsByItemOnly : TestModel1Strategy
    {
        public override void Transition() { }
        public override bool SelectItemDetailDisplayed() => false;
    }

    public sealed class ViewDetailsByItemSub : TestModel1Strategy
    {
        public override void Transition()
        {
            Model.Subinventory = "Transition test";
        }

        public override bool SelectItemDetailDisplayed() => true;
    }
}
