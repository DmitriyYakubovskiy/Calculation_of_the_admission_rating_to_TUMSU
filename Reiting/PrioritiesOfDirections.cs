public class PrioritiesOfDirections 
{
    public Dictionary< string,int> directions;

    public PrioritiesOfDirections(string name, int priority)
    {
        directions = new Dictionary<string, int>();
        directions[name] = priority;
    }

    public void Add(string name,int priority)
    {
        directions[name] = priority;
    }
}