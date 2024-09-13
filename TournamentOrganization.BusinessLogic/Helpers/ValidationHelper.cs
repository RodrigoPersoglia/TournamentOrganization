public static class ValidationHelper
{
    public static bool IsPowerOfTwo(int number)
    {
        if (number <= 1)
        {
            return false;
        }
        return (number & (number - 1)) == 0;
    }
}
