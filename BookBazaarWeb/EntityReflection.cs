using System.Reflection;

namespace BookBazaar.Misc;

public static class EntityReflection
{
    public static void UpdateUnchangedProperties<T>(T originalEntity, T updatedEntity)
    {
        Type entityType = typeof(T);
        PropertyInfo[] properties = entityType.GetProperties();

        foreach (PropertyInfo property in properties)
        {
            object originalValue = property.GetValue(originalEntity);
            object updatedValue = property.GetValue(updatedEntity);

            if (IsDefaultValue(updatedValue))
            {
                // Dacă valoarea actualizată este valoarea implicită, păstrăm valoarea originală
                property.SetValue(updatedEntity, originalValue);
            }
        }
    }

    private static bool IsDefaultValue(object value)
    {
        if (value == null)
            return true;

        Type valueType = value.GetType();

        if (valueType.IsValueType)
            return value.Equals(Activator.CreateInstance(valueType));

        return false;
    }
}