using Common.Exceptions.BadRequestExceptions.Abstractions;

namespace Common.Exceptions.BadRequestExceptions.Enums;

public class EnumParsingException: BadRequestException
{
    protected EnumParsingException(string message): base(message)
    {
    }

    public static EnumParsingException CannotParseEnum<TEnum>(string value)
        => new EnumParsingException(
            $"Неподдерживаемое значение {value} для получения {typeof(TEnum)}"
        );
    
    public static EnumParsingException CannotParseEnum<TEnum>(int value)
        => new EnumParsingException(
            $"Неподдерживаемое значение {value} для получения {typeof(TEnum)}"
        );

    public static Exception CannotParseEnum<TEnum>(TEnum tagsEnum)
        => new EnumParsingException(
        $"Неподдерживаемое значение {tagsEnum?.ToString()} для получения {typeof(TEnum)}"
    );
}