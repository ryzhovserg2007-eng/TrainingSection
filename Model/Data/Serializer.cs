namespace Model.Data;

/// <summary>
/// Abstract base class for serialization
/// Used to implement JSON and XML serialization
/// </summary>
public abstract class Serializer
{
    /// <summary>
    /// Save data to file
    /// </summary>
    public abstract void SaveToFile(string filePath, object data);

    /// <summary>
    /// Load data from file
    /// </summary>
    public abstract T LoadFromFile<T>(string filePath) where T : class;

    /// <summary>
    /// Get file extension for this serializer
    /// </summary>
    public abstract string GetFileExtension();

    /// <summary>
    /// Convert object to string representation
    /// </summary>
    public abstract string SerializeToString(object data);

    /// <summary>
    /// Convert string to object
    /// </summary>
    public abstract T DeserializeFromString<T>(string data) where T : class;
}
