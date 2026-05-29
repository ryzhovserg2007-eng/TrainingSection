namespace Model.Core;

/// <summary>
/// Базовый класс для человека (абстрактный класс)
/// Требуемый минимум абстрактных классов: 2+
/// </summary>
public abstract class Person : IPerson
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Patronymic { get; set; }
    public int Age { get; set; }
    public char Gender { get; set; }

    protected Person(string firstName, string lastName, string patronymic, int age, char gender)
    {
        FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
        LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
        Patronymic = patronymic ?? throw new ArgumentNullException(nameof(patronymic));
        Age = age >= 0 ? age : throw new ArgumentException("Возраст не может быть отрицательным", nameof(age));
        Gender = (gender == 'M' || gender == 'F') ? gender : throw new ArgumentException("Пол должен быть 'M' или 'F'", nameof(gender));
    }

    public virtual string GetFullName()
    {
        return $"{LastName} {FirstName} {Patronymic}";
    }

    public abstract string GetInfo();

    public override string ToString()
    {
        return GetFullName();
    }
}
