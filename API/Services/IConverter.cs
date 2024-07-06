namespace API;

public interface IConverter
{
    string Convert(decimal value, bool allowRounding = false);
}
