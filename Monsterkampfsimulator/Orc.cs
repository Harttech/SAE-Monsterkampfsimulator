namespace Monsterkampfsimulator
{
    public class Orc : Monster
    {
        public Orc(float health, float attackStrength, float defense, float speed) : base(health, attackStrength, defense, speed, "Orc")
        {
        }

        /// <summary>
        /// Get a color representing the monster's species.
        /// </summary>
        /// <returns>A color representing the monster's species.</returns>
        public override ConsoleColor GetMonsterColor() => ConsoleColor.DarkGreen;
    }
}
