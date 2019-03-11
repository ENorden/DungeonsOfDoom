using DungeonsOfDoom.Core.Characters;

namespace DungeonsOfDoom.Core.Items
{
    public interface IPickUpable
    {
        string Name { get; }
        int Count { get; set; }

        void Use(Character character);
    }
}
