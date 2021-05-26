using Random = UnityEngine.Random;

namespace Enemies
{
    public class Randomizer
    {

        private int min;
        private int max;
    
        private int chance;
        public int value;

        public Randomizer(int min, int max,int chance)
        {
            this.min = min;
            this.max = max;
            this.chance = chance;
        }

        public int ExtractNumber()
        {
            return value = Random.Range(min, max);
        }

        public bool IsNumberInRange()
        {
            return value <= chance;
        }
    
    }
}