namespace Model.Core;

/// <summary>
/// Интерфейс, описывающий человека (базовый интерфейс для всех людей)
/// Требуемый минимум интерфейсов: 2+
/// </summary>
public interface IPerson
{
    string FirstName { get; set; }
    string LastName { get; set; }
    string Patronymic { get; set; }
    int Age { get; set; }
    char Gender { get; set; } // 'M' - мужской, 'F' - женский
    
    string GetFullName();
    string GetInfo();
}
