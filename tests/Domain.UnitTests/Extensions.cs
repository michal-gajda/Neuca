namespace Neuca.Domain.UnitTests;

public static class Extensions
{
    public static T[] Append<T>(this T[]? array, T item)
    {
        if (array is null)
        {
            return [item];
        }

        var result = new T[array.Length + 1];
        array.CopyTo(result, 0);
        result[array.Length] = item;

        return result;
    }
}
