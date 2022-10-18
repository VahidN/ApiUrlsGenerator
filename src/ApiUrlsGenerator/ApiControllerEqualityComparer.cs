namespace ApiUrlsGenerator;

/// <summary>
///     A Controller Dto IEqualityComparer
/// </summary>
public class ApiControllerEqualityComparer : IEqualityComparer<ControllerModel>
{
    /// <summary>Defines methods to support the comparison of objects for equality.</summary>
    public bool Equals(ControllerModel? x, ControllerModel? y)
    {
        if (ReferenceEquals(x, y))
        {
            return true;
        }

        if (ReferenceEquals(x, null))
        {
            return false;
        }

        if (ReferenceEquals(y, null))
        {
            return false;
        }

        if (x.GetType() != y.GetType())
        {
            return false;
        }

        return string.Equals(x.AreaName, y.AreaName, StringComparison.Ordinal) &&
               string.Equals(x.ControllerName, y.ControllerName, StringComparison.Ordinal);
    }

    /// <summary>Returns a hash code for the specified object.</summary>
    public int GetHashCode(ControllerModel obj)
    {
        if (obj == null)
        {
            throw new ArgumentNullException(nameof(obj));
        }

        return HashCode.Combine(obj.AreaName, obj.ControllerName);
    }
}