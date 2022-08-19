namespace Framework
{
    public static class Math
    {
        public static double Lerp(double a, double b, double x)
        {
            return a + x * (b - a);
        }

        public static float Lerp(float a, float b, float x)
        {
            return a + x * (b - a);
        }
    }
}