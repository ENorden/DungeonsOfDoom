namespace DungeonsOfDoom.Core.Item
{
    public interface IPickUpable
    {
        string Name { get; }
        int Count { get; set; }

        void Use(Character character);
    }
}
