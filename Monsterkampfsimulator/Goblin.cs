namespace Monsterkampfsimulator
{
    public class Goblin : Monster
    {
        public Goblin(float health, float attackStrength, float defense, float speed) : base(health, attackStrength, defense, speed, "Goblin")
        {
        }

        /// <summary>
        /// Get a color representing the monster's species.
        /// </summary>
        /// <returns>A color representing the monster's species.</returns>
        public override ConsoleColor GetMonsterColor() => ConsoleColor.DarkYellow;
    }
}
