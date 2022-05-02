using System.Security.Cryptography;
using SharedUtilities.Extensions;

namespace Monsterkampfsimulator
{
    /// <summary>
    /// A class representing a monster compatant
    /// </summary>
    public class Monster
    {
        private float _health;

        private Monster(float health, float attackStrength, float defense, float speed, Race race)
        {
            _health = health;
            AttackStrength = attackStrength;
            Defense = defense;
            Speed = speed;
            Race = race;
        }

        /// <summary>
        /// How much health the monster currently has.
        /// </summary>
        public float Health => _health;
        /// <summary>
        /// How much base ATK this monster has.
        /// </summary>
        public float AttackStrength { get; }
        /// <summary>
        /// How much base DEF this monster has.
        /// </summary>
        public float Defense { get; }
        /// <summary>
        /// How fast the monster is.
        /// </summary>
        public float Speed { get; }
        /// <summary>
        /// The race/species of the monster.
        /// </summary>
        public Race Race { get; }


        /// <summary>
        /// Attacks this monster and returns, whether the monster has died.
        /// </summary>
        /// <param name="attackStrength">The base ATK with which to attack the monster.</param>
        /// <param name="damage">The damage the monster effectively received.</param>
        /// <returns>Whether the monster has died.</returns>
        public bool Attack(float attackStrength, out float damage)
        {
            var rdm = RandomNumberGenerator.GetInt32(-10, 11) / 100f;
            attackStrength += attackStrength * rdm;

            attackStrength = (float)Math.Round(attackStrength, 2);

            damage = attackStrength - Defense;
            if (damage < 0)
            {
                damage = 0;
                return false;
            }

            _health -= damage;
            if (_health < 0)
            {
                _health = 0;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Get a color representing the monster's species.
        /// </summary>
        /// <returns>A color representing the monster's species.</returns>
        public ConsoleColor GetMonsterColor()
        {
            ConsoleColor color;
            switch (Race)
            {
                case Race.Orc:
                    color = ConsoleColor.DarkGreen;
                    break;
                case Race.Troll:
                    color = ConsoleColor.Blue;
                    break;
                case Race.Goblin:
                    color = ConsoleColor.DarkYellow;
                    break;
                default:
                    color = ConsoleColor.Gray;
                    break;
            }

            return color;
        }

        /// <summary>
        /// Create a new monster by prompting the user for input.
        /// </summary>
        /// <returns>A new monster.</returns>
        public static Monster CreateMonster()
        {
            var race = (Race)(ConsoleHelper.ReadInt32("Enter a value for a race (1 = Orc, 2 = Troll, 3 = Goblin): ", null, 1, 3, errorForegroundColor: ConsoleColor.DarkRed)! - 1);
            var health = ConsoleHelper.ReadFloat("Enter a value for the monster's health: ", null, 0, errorForegroundColor: ConsoleColor.DarkRed);
            var attack = ConsoleHelper.ReadFloat("Enter a value for the monster's attack strength: ", null, 0, errorForegroundColor: ConsoleColor.DarkRed);
            var defense = ConsoleHelper.ReadFloat("Enter a value for the monster's defense: ", null, 0, errorForegroundColor: ConsoleColor.DarkRed);
            var speed = ConsoleHelper.ReadFloat("Enter a value for the monster's speed: ", null, 0, errorForegroundColor: ConsoleColor.DarkRed);

            return new Monster(health!.Value, attack!.Value, defense!.Value, speed!.Value, race);
        }

        public override string ToString()
        {
            return $"Race: {Race}\r\nStrength: {AttackStrength}\r\nDefense: {Defense}\r\nSpeed: {Speed}";
        }
    }
}
