namespace Model.Core;

/// <summary>
/// Класс спортсмена - наследник Person
/// Демонстрирует наследование и полиморфизм
/// </summary>
public class Athlete : Person
{
    public List<TrainingGroup> Groups { get; set; } = new();
    public int TrainingsCompleted { get; set; }

    public Athlete(string firstName, string lastName, string patronymic, int age, char gender)
        : base(firstName, lastName, patronymic, age, gender)
    {
        TrainingsCompleted = 0;
    }

    public override string GetInfo()
    {
        return $"Спортсмен: {GetFullName()}, Возраст: {Age}, Пол: {(Gender == 'M' ? "М" : "Ж")}, Завершено тренировок: {TrainingsCompleted}";
    }

    /// <summary>
    /// Проверка, может ли спортсмен оставить оценку для группы
    /// </summary>
    public bool CanLeaveFeedback(TrainingGroup group)
    {
        return Groups.Contains(group);
    }
}
