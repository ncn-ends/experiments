namespace Subjects.Experiments.DataTypes;

public class Structs
{
    public void AsStruct()
    {
        var daisy = new PersonStruct("daisy");
        daisy.Activity();
    }
    
    
    public void AsClass()
    {
        var daisy = new PersonClass("daisy");
        daisy.Activity();
    }
}

public struct PersonStruct
{
    public PersonStruct(string name)
    {
        Name = name;
    }

    public readonly string Name { get; init; }

    public string Activity()
    {
        return $"{Name} is walking in the park!";
    }
}

public class PersonClass
{
    public PersonClass(string name)
    {
        Name = name;
    }

    public string Name { get; init; }

    public string Activity()
    {
        return $"{Name} is walking in the park!";
    }
}