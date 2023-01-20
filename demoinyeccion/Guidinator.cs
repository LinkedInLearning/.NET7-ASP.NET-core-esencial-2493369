namespace demoinyeccion;

public interface IGuidinator
{
    string Value { get; init; }
}

public class Guidinator : IGuidinator
{
    public string Value { get; init; }

    public Guidinator()
    {
        Value = Guid.NewGuid().ToString();
    }
}