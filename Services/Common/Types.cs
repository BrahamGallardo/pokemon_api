namespace Application.Common
{
    public class Types
    {
        public string Name {  get; set; }
    }
    public static class Lk_Types
    {
        public static IReadOnlyList<Types> Types { get; } = new List<Types>()
        {
            new Types { Name = "Fire"},
            new Types { Name = "Electric" }
        };
    }
}
