namespace VehicleRegistration.WebApi.Infrastructure;

public class RusRegNumberProvider
{
    private readonly char[] _chars = new[] { 'А', 'В', 'Е', 'К', 'М', 'Н', 'О', 'Р', 'С', 'Т', 'У', 'Х' };
    private int _i, _j, _k, _n;

    public RusRegNumberProvider()
    {
        _i = _j = _k = _n = 0;
    }
    public string GetNextNumber()
    {
        var numberStr = _n.ToString();
        numberStr = numberStr.PadLeft(3, '0');
        var result = $"{_chars[_i]}{numberStr}{_chars[_j]}{_chars[_k]}";
        
        if (_n >= 999)
        {
            _n = 0;
            NextSeries();
        }

        return result;
    }

    private void NextSeries()
    {
        _k++;
        
        if (_k >= _chars.Length)
        {
            _k = 0;
            _j++;
        }

        if (_j >= _chars.Length)
        {
            _j = 0;
            _i++;
        }

        if (_i >= _chars.Length)
        {
            throw new IndexOutOfRangeException();
        }
    }
}