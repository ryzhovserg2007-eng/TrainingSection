namespace Model.Core;

/// <summary>
/// Третья часть TrainingGroup: Статистика и рейтинг
/// </summary>
public partial class TrainingGroup
{
    /// <summary>
    /// Получить процент мужчин в группе
    /// </summary>
    public double ПолучитьПроцентМужчин()
    {
        if (Спортсмены.Count == 0)
            return 0;

        int мужчин = Спортсмены.Count(с => с.Gender == 'M');
        return (мужчин * 100.0) / Спортсмены.Count;
    }

    /// <summary>
    /// Получить процент женщин в группе
    /// </summary>
    public double ПолучитьПроцентЖенщин()
    {
        return 100 - ПолучитьПроцентМужчин();
    }

    /// <summary>
    /// Получить средний возраст спортсменов в группе
    /// </summary>
    public double ПолучитьСредниеВозраст()
    {
        if (Спортсмены.Count == 0)
            return 0;

        return Спортсмены.Average(с => с.Age);
    }

    /// <summary>
    /// Получить медианный возраст спортсменов в группе
    /// Использует обобщенный тип IEnumerable
    /// </summary>
    public int ПолучитьМедианныйВозраст()
    {
        if (Спортсмены.Count == 0)
            return 0;

        var отсортированныеВозраста = Спортсмены
            .Select(с => с.Age)
            .OrderBy(в => в)
            .ToList();

        int середина = отсортированныеВозраста.Count / 2;

        if (отсортированныеВозраста.Count % 2 == 0)
        {
            return (отсортированныеВозраста[середина - 1] + отсортированныеВозраста[середина]) / 2;
        }
        else
        {
            return отсортированныеВозраста[середина];
        }
    }

    /// <summary>
    /// Получить статистику по группе
    /// Возвращает объект с информацией о группе
    /// </summary>
    public GroupStatistics ПолучитьСтатистику()
    {
        return new GroupStatistics
        {
            НазваниеГруппы = НазваниеГруппы,
            ИмяТренера = Тренер.GetFullName(),
            РейтингТренера = Тренер.Rating,
            КоличествоСпортсменов = Спортсмены.Count,
            ПроцентМужчин = ПолучитьПроцентМужчин(),
            ПроцентЖенщин = ПолучитьПроцентЖенщин(),
            СредниеВозраст = ПолучитьСредниеВозраст(),
            МедианныйВозраст = ПолучитьМедианныйВозраст(),
            КоличествоЗанятий = КоличествоЗанятий
        };
    }

    /// <summary>
    /// Перегруженный оператор == для сравнения групп
    /// Требуемое условие: 1+ раз перегруженный оператор
    /// </summary>
    public static bool operator ==(TrainingGroup группа1, TrainingGroup группа2)
    {
        if (ReferenceEquals(группа1, группа2))
            return true;
        if (ReferenceEquals(группа1, null) || ReferenceEquals(группа2, null))
            return false;
        return группа1.ИД == группа2.ИД;
    }

    /// <summary>
    /// Перегруженный оператор != для сравнения групп
    /// </summary>
    public static bool operator !=(TrainingGroup группа1, TrainingGroup группа2)
    {
        return !(группа1 == группа2);
    }

    /// <summary>
    /// Перегруженный оператор > для сравнения по количеству спортсменов
    /// </summary>
    public static bool operator >(TrainingGroup группа1, TrainingGroup группа2)
    {
        if (ReferenceEquals(группа1, null) || ReferenceEquals(группа2, null))
            throw new ArgumentNullException("Группы не могут быть null");
        return группа1.Спортсмены.Count > группа2.Спортсмены.Count;
    }

    /// <summary>
    /// Перегруженный оператор < для сравнения по количеству спортсменов
    /// </summary>
    public static bool operator <(TrainingGroup группа1, TrainingGroup группа2)
    {
        if (ReferenceEquals(группа1, null) || ReferenceEquals(группа2, null))
            throw new ArgumentNullException("Группы не могут быть null");
        return группа1.Спортсмены.Count < группа2.Спортсмены.Count;
    }

    public override bool Equals(object obj)
    {
        return obj is TrainingGroup группа && this == группа;
    }

    public override int GetHashCode()
    {
        return ИД.GetHashCode();
    }
}

/// <summary>
/// Класс для хранения статистики по группе
/// </summary>
public class GroupStatistics
{
    public string НазваниеГруппы { get; set; }
    public string ИмяТренера { get; set; }
    public double РейтингТренера { get; set; }
    public int КоличествоСпортсменов { get; set; }
    public double ПроцентМужчин { get; set; }
    public double ПроцентЖенщин { get; set; }
    public double СредниеВозраст { get; set; }
    public int МедианныйВозраст { get; set; }
    public int КоличествоЗанятий { get; set; }
}
