namespace Monsterkampfsimulator
{
    public class Troll : Monster
    {
        public Troll(float health, float attackStrength, float defense, float speed) : base(health, attackStrength, defense, speed, "Troll")
        {
        }

        /// <summary>
        /// Get a color representing the monster's species.
        /// </summary>
        /// <returns>A color representing the monster's species.</returns>
        public override ConsoleColor GetMonsterColor() => ConsoleColor.Blue;
    }
}
