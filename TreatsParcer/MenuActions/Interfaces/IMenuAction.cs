namespace ThreatsParser.MenuActions.Interfaces
{
    public interface IMenuAction
    {
        string Category { get; }
        string Name { get; }
        void Perform();
    }
}