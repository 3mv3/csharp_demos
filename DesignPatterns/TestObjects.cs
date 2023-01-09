namespace DesignPatternsDemo
{
    public class DemoObject
    {
        public string? Name { get; set; }
    }

    public class TestModel1 : WorkflowModelBase
    {
        public string? Item { get; set; }
        public string? Subinventory { get; set; }
        public TestModel1Strategy? Strategy { get; set; }
    }

    public class WorkflowModelBase { }
}
