namespace Model.Core;

/// <summary>
/// Класс тренера - наследник Person
/// Демонстрирует наследование и полиморфизм
/// </summary>
public class Coach : Person, IReportable
{
    public List<TrainingGroup> TrainingGroups { get; set; } = new();
    public double Rating { get; private set; } = 0;
    private List<CoachRating> _ratings = new();

    public Coach(string firstName, string lastName, string patronymic, int age, char gender)
        : base(firstName, lastName, patronymic, age, gender)
    {
    }

    public override string GetInfo()
    {
        return $"Тренер: {GetFullName()}, Возраст: {Age}, Пол: {(Gender == 'M' ? "М" : "Ж")}, Количество групп: {TrainingGroups.Count}, Рейтинг: {Rating:F2}";
    }

    /// <summary>
    /// Добавить оценку от спортсмена
    /// </summary>
    public void AddRating(int lessonsCount, double rating)
    {
        if (rating < 1 || rating > 10)
            throw new ArgumentException("Рейтинг должен быть от 1 до 10", nameof(rating));
        
        _ratings.Add(new CoachRating { LessonsCount = lessonsCount, Rating = rating });
        CalculateRating();
    }

    /// <summary>
    /// Пересчитать средний рейтинг тренера
    /// </summary>
    private void CalculateRating()
    {
        if (_ratings.Count == 0)
        {
            Rating = 0;
            return;
        }

        double totalWeight = 0;
        double weightedSum = 0;

        foreach (var r in _ratings)
        {
            totalWeight += r.LessonsCount;
            weightedSum += r.Rating * r.LessonsCount;
        }

        Rating = totalWeight > 0 ? weightedSum / totalWeight : 0;
    }

    public List<CoachRating> GetRatings() => new List<CoachRating>(_ratings);

    /// <summary>
    /// Реализация IReportable - генерация отчета по тренеру
    /// </summary>
    public string GenerateReport()
    {
        return $"ОТЧЕТ ПО ТРЕНЕРУ\n" +
               $"================\n" +
               $"ФИО: {GetFullName()}\n" +
               $"Рейтинг: {Rating:F2}\n" +
               $"Количество групп: {TrainingGroups.Count}\n" +
               $"Количество оценок: {_ratings.Count}";
    }
}
