using System.Collections.Generic;

public class Directions
{
    public List<Student> students;
    public int NumberOfBudgetPlaces { get; set; }
    public string Name { get; set; }

    public Directions(List<Student> students,string name,int numberOfBudgetPlaces)
    {
        this.students = students;
        Name = name;
        NumberOfBudgetPlaces = numberOfBudgetPlaces;
        //students.Sort(); //пустые бюджетные нулем забивать будем
    }

    public Directions(string name, int numberOfBudgetPlaces)
    {
        students = new List<Student>();
        for (int i = 0; i < numberOfBudgetPlaces; i++)
        {
            students.Add(new Student(null, 0, new PrioritiesOfDirections("name",0)));
        }
        Name = name;
        NumberOfBudgetPlaces = numberOfBudgetPlaces;
        //students.Sort(); //пустые бюджетные нулем забивать будем
    }

    public bool AddStudent(ref List<Student> stud,int index)
    {
        Sort();
        for (int i=0; i<students.Count;i++)
        {
            if (stud[index].Mark > students[i].Mark)
            {
                if (students[i].Mark!=0)
                {
                    stud.Add(students[students.Count - 1]);
                }
                students.Insert(i, stud[index]);
                students.RemoveAt(students.Count-1);
                stud.Remove(stud[index]);
                return true;
            }
        }
        return false;
    }

    public void Sort()
    {
        for (int i = 0; i < students.Count; i++)
        {
            for (int j = i + 1; j < students.Count; j++)
            {
                if (students[i].Mark < students[j].Mark)
                {
                    Student temp = students[i];
                    students[i] = students[j];
                    students[j] = temp;
                }
            }
        }
    }

    public void Print()
    {
        for (int i = 0; i < students.Count; i++)
        {
            Console.WriteLine(i + 1 + "\t" + students[i].ToString());
        }
    }

    public void Print(string[] arr)
    {
        for (int i = 0; i < students.Count; i++)
        {
            Console.WriteLine(i + 1 + "\t"+ students[i].ToString(arr));
        }
    }
}
