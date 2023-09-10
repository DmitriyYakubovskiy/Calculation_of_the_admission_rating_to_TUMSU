using System.Text;

List<Student> students = new List<Student>();
List<Directions> directions = new List<Directions>();

string[] directionNames = new string[18] { "Мат.обеспеч", "Инф.безопас", "Приклад.инф", "Инф.безопас.Авт.Сис", "Робот", "Математика", "Механика", "Педагог", "Инф.СисиТех", "Комп.Безопас","Ландшафт.Арх","Тех.Физ", "Эконом.Безопас", "ШЕН_Физика", "ШЕН_Химия","Физика","Химия","Биоинжженерия" };
int[] numbersOfBudgetPlaces=new int[18] {80-2, 65-10, 73-3, 45-3, 25 , 25-2, 25,25-13,74-9,45-9,25-2,35,7,20-2,20,35-1,55,25-1};

for (int i = 0; i < directionNames.Length; i++)
{
    string textFromFile;
    string path = directionNames[i];
    using (FileStream fstream = File.OpenRead(path + ".txt"))
    {
        byte[] buffer = new byte[fstream.Length];
        await fstream.ReadAsync(buffer, 0, buffer.Length);
        textFromFile = Encoding.Default.GetString(buffer);
    }
    textFromFile =textFromFile.Replace("ещё\r\n", "\t");

    string[] strList = textFromFile.Split('\n');

    FillStudents(strList, ref students, path);
    directions.Add(new Directions(path, numbersOfBudgetPlaces[i]));
}


Sort(ref students);
PrintStudents(students, directionNames); // сколько всего с оригиналами
Console.WriteLine("\n"+(students.Count)+"\n");// количество

Logica(ref students, ref directions, directionNames, 10);

PrintDirections(directions);//списки направлений

Sort(ref students);
PrintStudents(students, directionNames);//кто остался
Console.WriteLine("\n" + Check(students, directions));

static int Check(List<Student> st,List<Directions> dir)
{
    int cnt = 0;
    foreach(var i in st)
    {
        if (i.Mark != 0) cnt++;
    }

    foreach(var i in dir)
    {
        foreach(var student in i.students)
        {
            if (student.Mark != 0) cnt++;
        }
    }

    return cnt;
}

static void Sort(ref List<Student> students)
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

static void Logica(ref List<Student> students, ref List<Directions> directions, string[] directionNames,int numberOfTimes)
{
    for (int time = 0; time < numberOfTimes; time++)
    {
        bool check;
        Sort(ref students);
        for (int priority = 1; priority < 8; priority++)
        {
            for (int i = 0; i < students.Count; i++)
            {
                foreach (var direct in students[i].priorityOfDirections.directions)
                {
                    if (direct.Value == priority)
                    {
                        for (int k = 0; k < directionNames.Length; k++)
                        {
                            if (direct.Key == directionNames[k])
                            {
                                check = directions[k].AddStudent(ref students, i);
                                if(check)
                                {
                                    i = -1;
                                    priority = 0;
                                }
                                continue;
                            }
                        }
                    }
                    continue;
                }
            }
        }
    }
}

static void FillStudents(string[] strList, ref List<Student> students,string path)
{
    for (int i = 0; i < strList.Length; i++)
    {
        if (strList[i].Split('\t')[9] == "да")
        {
            string name = strList[i].Split('\t')[1];
            int ball = Convert.ToInt32(strList[i].Split('\t')[8]);
            int priority = Convert.ToInt32(strList[i].Split('\t')[10]);

            if (ball != 0)
            {
                if (Student.FindStudent(students, name) > -1 && students[Student.FindStudent(students, name)].GetPriority(path) == -1)
                {
                    students[Student.FindStudent(students, name)].priorityOfDirections.Add(path, priority);
                }
                else if (Student.FindStudent(students, name) > -1 && students[Student.FindStudent(students, name)].GetPriority(path) != -1)
                {
                    return;
                }
                else
                {
                    students.Add(new Student(name, ball, new PrioritiesOfDirections(path, priority)));
                }
            }
        }
    }
}

static void PrintDirections(List<Directions> directions)
{
    for (int i = 0; i < directions.Count; i++)
    {
        Console.WriteLine(directions[i].Name);
        directions[i].Print();
        Console.WriteLine();
    }
}

static void PrintStudents(List<Student> students,string[] arr)
{
    int cnt = 1;
    foreach(var student in students)
    {
        if (student!=null && student.Mark!=0)Console.WriteLine(cnt+"\t"+student.ToString(arr));
        cnt++;
    }
}