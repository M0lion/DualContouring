abstract class BitHelper 
{
    public static bool Get(int map, int i) {
        return (map & (1 << i)) > 0;
    }

    public static int Set(int map, int i, bool val) {
        if(val) {
            return map | (1 << i);
        } else {
            return map & ~(1 << i);
        }
    }

    public static int ReduceDims(int val, int target, int dims) {
            int result = 0;
            int targetDim = 0;
            for(int dim = 0; dim < dims; dim++) {
                if(BitHelper.Get(target, dim)) {
                    result = BitHelper.Set(result, targetDim, BitHelper.Get(val, dim));
                    targetDim++;
                }
            }
            return result;
    }
}