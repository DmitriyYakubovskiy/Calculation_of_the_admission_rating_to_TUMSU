using System.Text;

public class Student
{
    public string Name { get; set; }
    public int Mark  { get; set; }
    public PrioritiesOfDirections priorityOfDirections;

    public Student(string name, int mark, PrioritiesOfDirections priority)
    {
        Name = name;
        Mark = mark;
        priorityOfDirections = priority;
    }

    public string ToString(string[] directionNames)
    {
        if (Mark != 0 && this!=null)
        {
            StringBuilder sb =new StringBuilder($"{Name}\t{Mark}");
            for (int j = 0; j < directionNames.Length; j++)
            {
                if (GetPriority(directionNames[j]) > -1)
                {
                    sb.Append ('\t'+ directionNames[j] + ": "  + GetPriority(directionNames[j]).ToString()+'\t');
                }
            }
            return sb.ToString();
        }
        return null;
    }

    public string ToString()
    {
        string str = $"{Name}\t{Mark}\t";
        return str;
    }

    public int GetPriority(string name)
    {
        return priorityOfDirections.directions.ContainsKey(name)==true? priorityOfDirections.directions[name]: -1;
    }

    public static int FindStudent(List<Student> list,string name)
    {
        for(int i=0;i<list.Count;i++)
        {
            if (name == list[i].Name)
            {
                return i;
            }
        }
        return -1;
    }
}