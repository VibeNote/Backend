namespace WebApp.Abstractions.Attributes.Validation;

public static class ErrorMessages
{
    public const string Required = "Поле \"{0}\" необходимо заполнить.";
    public const string Url = "Адрес сайта некорректен.";
    public const string FloatingPoint = "Число заполнено неверно.";
    public const string Image = "Формат изображения \"{0}\" не поддерживается.";
    public const string Range = "\"{0}\" не входит в диапазон [{1}, {2}].";
    public const string Email = "Адрес электронной почты некорректен";
    public const string Phone = "Номер телефона некорректен";
    public const string StringLength = "Количество символов в поле '{0}' должно быть не больше {1} и не меньше {2}";
    public const string PasswordsNotMatch = "Пароли не совпадают.";
    public const string DateParseFailed = "Невозможно преобразовать поле \"{0}\" в дату";
}