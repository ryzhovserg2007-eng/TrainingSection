namespace Model.Core;

/// <summary>
/// Вторая часть TrainingGroup: Сортировка и фильтрация
/// </summary>
public partial class TrainingGroup
{
    /// <summary>
    /// Сортировка спортсменов по ФИО
    /// Использует встроенный делегат Comparison
    /// </summary>
    public List<Athlete> СортироватьПоФИО()
    {
        var отсортированные = new List<Athlete>(Спортсмены);
        
        // Перегруженный метод сортировки (перегрузка - требуемое условие)
        отсортированные.Sort((a1, a2) => 
        {
            int сравнениеФамилии = a1.LastName.CompareTo(a2.LastName);
            if (сравнениеФамилии != 0)
                return сравнениеФамилии;
            
            int сравнениеИмени = a1.FirstName.CompareTo(a2.FirstName);
            if (сравнениеИмени != 0)
                return сравнениеИмени;
            
            return a1.Patronymic.CompareTo(a2.Patronymic);
        });

        return отсортированные;
    }

    /// <summary>
    /// Отфильтровать спортсменов по полу
    /// Использует встроенный делегат Predicate
    /// </summary>
    public List<Athlete> ФильтроватьПоПолу(char пол)
    {
        if (пол != 'M' && пол != 'F')
            throw new ArgumentException("Пол должен быть 'M' или 'F'", nameof(пол));

        Predicate<Athlete> фильтр = а => а.Gender == пол;
        return Спортсмены.FindAll(фильтр);
    }

    /// <summary>
    /// Отфильтровать спортсменов по возрасту
    /// Использует встроенный делегат Predicate
    /// </summary>
    public List<Athlete> ФильтроватьПоВозрасту(int минимальныйВозраст, int максимальныйВозраст)
    {
        if (минимальныйВозраст > максимальныйВозраст)
            throw new ArgumentException("Минимальный возраст не может быть больше максимального");

        Predicate<Athlete> фильтр = а => а.Age >= минимальныйВозраст && а.Age <= максимальныйВозраст;
        return Спортсмены.FindAll(фильтр);
    }

    /// <summary>
    /// Комбинированная фильтрация: по полу и возрасту
    /// </summary>
    public List<Athlete> ФильтроватьПоПолуИВозрасту(char пол, int минВозраст, int максВозраст)
    {
        var поПолу = ФильтроватьПоПолу(пол);
        
        Predicate<Athlete> фильтрВозраста = а => а.Age >= минВозраст && а.Age <= максВозраст;
        return поПолу.FindAll(фильтрВозраста);
    }

    /// <summary>
    /// Получить спортсменов отсортированных и отфильтрованных
    /// Использует делегат Action для применения фильтров
    /// </summary>
    public List<Athlete> ПолучитьФильтрованныхИОтсортированных(
        Action<List<Athlete>> применитьФильтры = null)
    {
        var результат = СортироватьПоФИО();
        применитьФильтры?.Invoke(результат);
        return результат;
    }

    /// <summary>
    /// Использование обобщенного типа - Generic
    /// Поиск спортсмена по критерию (требуемое условие: 1+ использование Generic)
    /// </summary>
    public T НайтиСпортсмена<T>(Func<Athlete, bool> критерий) where T : Athlete
    {
        return Спортсмены.FirstOrDefault(критерий) as T;
    }
}
