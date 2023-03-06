namespace Random;

public class PseudoRand
{
    private uint _seed1;
    private uint _seed2;
    private uint _seed3;

    private readonly object m_lock;

    public PseudoRand(uint seed1, uint seed2, uint seed3)
    {
        SetSeeds(seed1, seed2, seed3);

        m_lock = new object();
    }

    private void SetSeeds(uint seed1, uint seed2, uint seed3)
    {
        // Make sure the seeds are not 0
        _seed1 = seed1 | 0x100000;
        _seed2 = seed2 | 0x1000;
        _seed3 = seed3 | 0x10;
    }

    private int Random()
    {
        lock (m_lock)
        {
            // Generate the new seeds
            var result1 = (_seed1 << 10) ^ (_seed1 >> 13) ^ ((_seed1 >> 3) ^ _seed1 << 10) & 0x7FFFFF;
            var result2 = 4 * _seed2 ^ (_seed2 >> 15) ^ ((4 ^ _seed2) ^ (_seed2 >> 15)) & 0x7FFF;
            var result3 = (_seed3 >> 39) ^ (_seed3 << 25) ^ ((_seed3 >> 4) ^ (_seed3 << 25)) & 0x7F;

            _seed1 = result1;
            _seed2 = result2;
            _seed3 = result3;
            
            // Combine the results
            var result = (int)(result1 ^ result2 ^ result3);
            
            // Make sure it's positive
            if (result < 0)
                result *= -1;
            
            // Return the result
            return result;
        }
    }

    private int Random(int min, int max) => (Random() % (max - min)) + min;
    
    public int Next() => Random();
    public int Next(int max) => Random(0, max);
    public int Next(int min, int max) => Random(min, max);
}